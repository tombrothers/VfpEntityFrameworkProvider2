using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SampleQueries.Harness;
using SampleQueries.Runner;
using SampleQueries.Utils;

namespace SampleQueries {
    public class SampleLoader {
        private List<string> _sourceDirectories = new List<string>();

        public void AddSourceDirectory(string dir) {
            _sourceDirectories.Add(dir);
        }

        public SampleGroup Load(SampleSuite suite) {
            Type suiteType = suite.GetType();

            // get attributes attached to suite
            var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(suiteType, typeof(DescriptionAttribute));
            var titleAttribute = (TitleAttribute)Attribute.GetCustomAttribute(suiteType, typeof(TitleAttribute));
            var prefixAttribute = (PrefixAttribute)Attribute.GetCustomAttribute(suiteType, typeof(PrefixAttribute));

            if (prefixAttribute == null)
                throw new InvalidOperationException("[Prefix] attribute not specified on " + suiteType.Name);
            string prefix = prefixAttribute.Prefix;

            SampleGroup suiteGroup = new SampleGroup(suite, (titleAttribute != null) ? titleAttribute.Title : "", (descriptionAttribute != null) ? descriptionAttribute.Description : "");
            string sourceFile = FindSourceFile(suiteType.Name + ".cs");
            string sourceCode = (sourceFile != null) ? File.ReadAllText(sourceFile) : "";

            var categories = suiteType.GetMethods()
                .Where(c => c.Name.ToLower().StartsWith(prefix.ToLower()))
                .Select(c => new { Attributes = (CategoryAttribute[])c.GetCustomAttributes(typeof(CategoryAttribute), false) })
                .Where(c => c.Attributes.Length > 0)
                .Select(c => c.Attributes.First().Category)
                .Distinct();

            foreach (var cat in categories) {
                suiteGroup.Children.Add(LoadCategory(suite, cat, prefix, sourceCode));
            }

            return suiteGroup;
        }

        private SampleGroup LoadCategory(SampleSuite suite, string category, string prefix, string fileSourceCode) {
            SampleGroup categoryGroup = new SampleGroup(suite, category, null);
            Type suiteType = suite.GetType();

            IEnumerable<MethodInfo> methodsInCategory =
                suiteType.GetMethods()
                .Where(c => c.Name.ToLower().StartsWith(prefix.ToLower()))
                .OrderBy(c => c.Name)
                .Select(c => new { Method = c, Attributes = (CategoryAttribute[])c.GetCustomAttributes(typeof(CategoryAttribute), false) })
                .Where(c => c.Attributes.Length > 0 && c.Attributes[0].Category == category)
                .Select(c => c.Method);

            foreach (MethodInfo mi in methodsInCategory) {
                categoryGroup.Children.Add(LoadSample(suite, mi, fileSourceCode));
            }
            return categoryGroup;
        }

        private Sample LoadSample(SampleSuite suite, MethodInfo method, string fileSourceCode) {
            var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(method, typeof(DescriptionAttribute));
            var titleAttribute = (TitleAttribute)Attribute.GetCustomAttribute(method, typeof(TitleAttribute));

            return new Sample(suite, method, (titleAttribute != null) ? titleAttribute.Title : "", (descriptionAttribute != null) ? descriptionAttribute.Description : "", CodeExtractor.GetCodeBlock(fileSourceCode, "void " + method.Name));
        }

        private string FindSourceFile(string fileName) {
            foreach (string dir in _sourceDirectories) {
                string fullName = Path.Combine(dir, fileName);
                if (File.Exists(fullName))
                    return fullName;
            }

            return null;
        }
    }
}