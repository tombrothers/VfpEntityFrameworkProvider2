using System;
using System.Drawing;
using System.Windows.Forms;

namespace SampleQueries.Utils {
    internal static class SyntaxColorizer {
        public static void ColorizeCode(RichTextBox rtb) {
            string[] keywords = {"as", "do", "if", "in", "is", "for", "int", "new", "out", "ref", "try", "base", 
                                "bool", "byte", "case", "char", "else", "enum", "goto", "lock", "long", "null", 
                                "this", "true", "uint", "void", "break", "catch", "class", "const", "event", "false", 
                                "fixed", "float", "sbyte", "short", "throw", "ulong", "using", "where", "while", 
                                "yield", "double", "extern", "object", "params", "public", "return", "sealed", 
                                "sizeof", "static", "string", "struct", "switch", "typeof", "unsafe", "ushort", 
                                "checked", "decimal", "default", "finally", "foreach", "partial", "private", 
                                "virtual", "abstract", "continue", "delegate", "explicit", "implicit", "internal", 
                                "operator", "override", "readonly", "volatile",  
                                "interface", "namespace", "protected", "unchecked",
                                "stackalloc", 
                                "from", "in", "where", "select", "join", "equals", "let", "on", "group", "by", 
                                "into", "orderby", "ascending", "descending", "var"};
            string text = rtb.Text;

            rtb.SelectAll();
            rtb.SelectionColor = rtb.ForeColor;

            foreach (String keyword in keywords) {
                int keywordPos = rtb.Find(keyword, RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                while (keywordPos != -1) {
                    int commentPos = text.LastIndexOf("//", keywordPos, StringComparison.OrdinalIgnoreCase);
                    int newLinePos = text.LastIndexOf("\n", keywordPos, StringComparison.OrdinalIgnoreCase);
                    int quoteCount = 0;
                    int quotePos = text.IndexOf("\"", newLinePos + 1, keywordPos - newLinePos, StringComparison.OrdinalIgnoreCase);
                    while (quotePos != -1) {
                        quoteCount++;
                        quotePos = text.IndexOf("\"", quotePos + 1, keywordPos - (quotePos + 1), StringComparison.OrdinalIgnoreCase);
                    }

                    if (newLinePos >= commentPos && quoteCount % 2 == 0)
                        rtb.SelectionColor = Color.Blue;

                    keywordPos = rtb.Find(keyword, keywordPos + rtb.SelectionLength, RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                }
            }

            rtb.Select(0, 0);
        }
    }
}
