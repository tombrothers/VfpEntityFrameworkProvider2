using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Xml;

namespace FunctionStubGenerator {
    public class LinqFunctionStubsCodeGen {
        public static void Generate(string outputFileName) {
            //The following functions are omitted because they have counterparts in the BCL
            string[] omittedFunctions = new[]
            {
                "Sum", "Min", "Max", "Average", "Avg",
                "Count", "BigCount", 
                "Trim", "RTrim", "LTrim",
                "Concat", "Length", "Substring",
                "Replace", "IndexOf", "ToUpper", "ToLower",
                "Contains", "StartsWith", "EndsWith", "Year", "Month", "Day",
                "DayOfYear", "Hour", "Minute", "Second", "Millisecond", "CurrentDateTime", "CurrentDateTimeOffset",
                "CurrentUtcDateTime",
                "BitwiseAnd", "BitwiseOr", "BitwiseXor", "BitwiseNot",
                "Round", "Abs", "Power", "NewGuid",
                "Floor", "Ceiling",
            };

            //The following functions are omitted from VfpFunctions because they already exist in EntityFunctions
            string[] omittedSqlFunctions = new[]
            {
                "STDEV", "STDEVP", 
                "Left", "Right", "Reverse", "GetTotalOffsetMinutes", 
                "TruncateTime", "CreateDateTime",
                "CreateDateTimeOffset", "CreateTime", "Add", "Diff",
                "Truncate", 
                "LEN", "LOWER", "UPPER", 
            };

            //Generate function stubs
            var ssdl = @"<Schema Namespace='LinqFunctionStubsGenerator' Alias='Self' Provider='VfpEntityFrameworkProvider' ProviderManifestToken='Vfp' xmlns='http://schemas.microsoft.com/ado/2006/04/edm/ssdl'></Schema>";

            XmlReader[] xmlReaders = new XmlReader[1];
            xmlReaders[0] = XmlReader.Create(new StringReader(ssdl));

            StoreItemCollection storeItemCollection = new StoreItemCollection(xmlReaders);
            IEnumerable<EdmFunction> sqlFunctions = storeItemCollection.GetItems<EdmFunction>()
                .Where(f => f.NamespaceName == "Vfp")
                .Where(f => !omittedFunctions.Concat(omittedSqlFunctions).Contains(f.Name, StringComparer.OrdinalIgnoreCase));

            FunctionStubFileWriter sqlStubsFileWriter = new FunctionStubFileWriter(sqlFunctions, GetFunctionNamingDictionary(), GetParameterNamingDictionary());
            sqlStubsFileWriter.GenerateToFile(outputFileName, "VfpEntityFrameworkProvider", "VfpFunctions", "Vfp", true);
        }

        private static Dictionary<string, string> GetFunctionNamingDictionary() {
            return new Dictionary<string, string> {
                { "ISDIGIT", "IsDigit" },
                { "LTRIM", "LeftTrim" },
                { "ASC", "Ascii" },
                { "RTRIM", "RightTrim" },
                { "ALLTRIM", "AllTrim" },
                { "DATETIME", "DateTime" },
                { "CDAY", "DayName" },
                { "CDOW", "DayOfWeek" },
                { "CMONTH", "MonthName" },
                { "SQRT", "SquareRoot" },
                { "STR", "StringConvert" }
            };
        }

        private static Dictionary<string, string> GetParameterNamingDictionary() {
            return new Dictionary<string, string>();
            ////sqlFunctionParameterNames.Add("strSearch", "toSearch");
            ////sqlFunctionParameterNames.Add("strTarget", "target");
            ////sqlFunctionParameterNames.Add("datepart", "datePartArg");
            ////sqlFunctionParameterNames.Add("enddate", "endDate");
            ////sqlFunctionParameterNames.Add("startdate", "startDate");
            ////sqlFunctionParameterNames.Add("decimal", "decimalArg");
            ////sqlFunctionParameterNames.Add("str1", "string1");
            ////sqlFunctionParameterNames.Add("str2", "string2");
            ////sqlFunctionParameterNames.Add("str", "stringArg");
            ////sqlFunctionParameterNames.Add("string_expression", "stringExpression");
            ////sqlFunctionParameterNames.Add("x", "baseArg");
            ////sqlFunctionParameterNames.Add("y", "exponentArg");
            ////sqlFunctionParameterNames.Add("character_string", "stringArg");
            ////sqlFunctionParameterNames.Add("quote_character", "quoteCharacter");
            ////sqlFunctionParameterNames.Add("numeric_expression", "numericExpression");
            ////sqlFunctionParameterNames.Add("strInput", "stringInput");
            ////sqlFunctionParameterNames.Add("strReplacement", "stringReplacement");
            ////sqlFunctionParameterNames.Add("strPattern", "stringPattern");
            ////sqlFunctionParameterNames.Add("datetimeoffset", "dateTimeOffsetArg");
        }
    }
}