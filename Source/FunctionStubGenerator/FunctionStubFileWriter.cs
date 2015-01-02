using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Text;

namespace FunctionStubGenerator {
    internal class FunctionStubFileWriter {
        private readonly IEnumerable<EdmFunction> _functions;
        private readonly Dictionary<string, string> _funcDictionaryToUse;
        private readonly Dictionary<string, string> _paramDictionaryToUse;

        public FunctionStubFileWriter(IEnumerable<EdmFunction> functions, Dictionary<string, string> functionNames, Dictionary<string, string> parameterNames) {
            _functions = functions;
            _funcDictionaryToUse = functionNames;
            _paramDictionaryToUse = parameterNames;
        }

        public void GenerateToFile(string destinationFile, string namespacestring, string className, string attributeNamespace, bool pascalCaseFunctionNames) {
            //Use passed in class information to generate the class definition.
            var newCode = GenerateCode(namespacestring, className, attributeNamespace, pascalCaseFunctionNames);

            //Write to file.
            try {
                using (var writer = new StreamWriter(destinationFile, false)) {
                    writer.Write(newCode.ToString());
                    writer.Close();
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public StringWriter GenerateCode(string namespacestring, string className, string attributeNamespace, bool pascalCaseFunctionNames) {
            var newCode = new StringWriter();
            bool isAggregateFunction;
            bool hasSByteParameterOrReturnType;
            bool hasstringInParameterName;
            string separator;

            GenerateFileHeader(newCode, className);
            GenerateUsingStatements(newCode);

            newCode.WriteLine("namespace " + namespacestring);
            newCode.WriteLine("{");

            GenerateClassHeader(newCode, className, attributeNamespace);

            foreach (EdmFunction function in _functions) {
                isAggregateFunction = false;
                hasSByteParameterOrReturnType = false;
                hasstringInParameterName = false;
                separator = string.Empty;

                var functionNameToUse = FindCorrectFunctionName(function.Name, pascalCaseFunctionNames);
                GenerateFunctionHeader(newCode, attributeNamespace, function.Name);
                var returnType = ((PrimitiveType)function.ReturnParameter.TypeUsage.EdmType).ClrEquivalentType;

                //Suppress warning that 'SByte' is not CLS-compliant.
                if (returnType == typeof(sbyte)) {
                    hasSByteParameterOrReturnType = true;
                }

                var functionSignaturestring = new StringBuilder();
                AppendSpaces(functionSignaturestring, 8);
                functionSignaturestring.Append("public static ");
                WriteType(functionSignaturestring, returnType);
                functionSignaturestring.Append(functionNameToUse + "(");

                var functionParameters = function.Parameters;
                Type parameterType;
                foreach (var parameter in functionParameters) {
                    var parameterNameToUse = parameter.Name;
                    parameterNameToUse = FindCorrectParameterName(parameterNameToUse);

                    //Detect aggregate functions. They have just one parameter and so stub can be generated here.
                    if (parameter.TypeUsage.EdmType.GetType() == typeof(System.Data.Entity.Core.Metadata.Edm.CollectionType)) {
                        isAggregateFunction = true;

                        if (parameterNameToUse.ToLowerInvariant().Contains("string")) {
                            hasstringInParameterName = true;
                        }

                        var collectionType = (System.Data.Entity.Core.Metadata.Edm.CollectionType)parameter.TypeUsage.EdmType;
                        parameterType = ((PrimitiveType)collectionType.TypeUsage.EdmType).ClrEquivalentType;

                        //Detect if there is an 'SByte' parameter to suppress non-CLS-compliance warning.
                        //Generate the attribute only once for each function.
                        if (parameterType == typeof(sbyte)) {
                            hasSByteParameterOrReturnType = true;
                        }

                        //Generate stub for non-nullable input parameters
                        functionSignaturestring.Append("IEnumerable<" + parameterType.ToString());

                        //Supress fxcop message and CLS non-compliant attributes
                        GenerateFunctionAttributes(newCode, hasstringInParameterName, hasSByteParameterOrReturnType);

                        //Use the constructed function signature
                        newCode.Write(functionSignaturestring.ToString());
                        GenerateAggregateFunctionStub(newCode, parameterType, returnType, parameterNameToUse, false);

                        //Generate stub for nullable input parameters
                        //Special Case: Do not generate nullable stub for input parameter of types Byte[]
                        //and string, since they are nullable.
                        if (!IsNullableType(parameterType)) {
                            GenerateFunctionHeader(newCode, attributeNamespace, function.Name);

                            //Supress fxcop message and CLS non-compliant attributes
                            GenerateFunctionAttributes(newCode, hasstringInParameterName, hasSByteParameterOrReturnType);

                            //Use the constructed function signature
                            newCode.Write(functionSignaturestring.ToString());
                            GenerateAggregateFunctionStub(newCode, parameterType, returnType, parameterNameToUse, true);
                        }
                    }
                    else {
                        //Process each parameter in case of non-aggregate functions.
                        parameterType = ((PrimitiveType)parameter.TypeUsage.EdmType).ClrEquivalentType;
                        functionSignaturestring.Append(separator);
                        WriteType(functionSignaturestring, parameterType);
                        functionSignaturestring.Append(parameterNameToUse);
                        separator = ", ";

                        //Detect if there is an 'SByte' parameter to suppress non-CLS-compliance warning.
                        if (parameterType == typeof(sbyte)) {
                            hasSByteParameterOrReturnType = true;
                        }

                        if (parameterNameToUse.ToLowerInvariant().Contains("string")) {
                            hasstringInParameterName = true;
                        }
                    }
                } //End for each parameter

                //Generate stub for Non-aggregate functions after all input parameters are found.
                if (!isAggregateFunction) {
                    //Supress fxcop supression and CLS non-compliant attributes
                    GenerateFunctionAttributes(newCode, hasstringInParameterName, hasSByteParameterOrReturnType);
                    newCode.WriteLine(functionSignaturestring.ToString() + ")");
                    AppendSpaces(newCode, 8);
                    newCode.WriteLine("{");
                    WriteExceptionStatement(newCode);
                }
            } //End for each function

            AppendSpaces(newCode, 4);
            newCode.WriteLine("}");
            newCode.WriteLine("}");
            newCode.Close();

            return newCode;
        }

        private void GenerateAggregateFunctionStub(StringWriter newCode, Type parameterType, Type returnType, string parameterNameToUse, bool isNullable) {
            GenerateQuestionMark(newCode, isNullable);
            newCode.Write("> ");
            newCode.WriteLine(parameterNameToUse + ")");
            AppendSpaces(newCode, 8);
            newCode.WriteLine("{");
            AppendSpaces(newCode, 12);
            newCode.Write("ObjectQuery<" + parameterType.ToString());
            GenerateQuestionMark(newCode, isNullable);
            newCode.Write("> objectQuerySource = " + parameterNameToUse);
            newCode.Write(" as ObjectQuery<" + parameterType.ToString());
            GenerateQuestionMark(newCode, isNullable);
            newCode.WriteLine(">;");

            AppendSpaces(newCode, 12);
            newCode.WriteLine("if (objectQuerySource != null)");
            AppendSpaces(newCode, 12);
            newCode.WriteLine("{");
            AppendSpaces(newCode, 16);
            newCode.Write("return ((IQueryable)objectQuerySource).Provider.Execute<" + returnType.ToString());

            //Special case: Byte[], string are nullable
            if (!IsNullableType(returnType)) {
                newCode.Write("?");
            }

            newCode.Write(">(Expression.Call((MethodInfo)MethodInfo.GetCurrentMethod(),Expression.Constant(" + parameterNameToUse);
            newCode.WriteLine(")));");
            AppendSpaces(newCode, 12);
            newCode.WriteLine("}");
            WriteExceptionStatement(newCode);
        }

        public void GenerateQuestionMark(StringWriter newCode, bool isNullable) {
            if (isNullable) {
                newCode.Write("?");
            }
        }

        private void GenerateFunctionAttributes(StringWriter newCode, bool hasstringInParameterName, bool hasSByteParameterOrReturnType) {
            //Supress fxcop message about 'string' in argument names.
            if (hasstringInParameterName) {
                GenerateFxcopSuppressionAttribute(newCode);
            }

            //Suppress warning that 'SByte' is not CLS-compliant, generate the attribute only once.
            if (hasSByteParameterOrReturnType) {
                GenerateSByteCLSNonComplaintAttribute(newCode);
            }
        }

        private void GenerateFileHeader(StringWriter newCode, string className) {
            var theTime = DateTime.Now;

            newCode.WriteLine("//------------------------------------------------------------------------------");
            newCode.WriteLine("// <auto-generated>");
            newCode.WriteLine("//     This code was generated by a tool.");
            newCode.Write("//     Generation date and time : ");
            newCode.WriteLine(theTime.Date.ToShortDateString() + " " + theTime.TimeOfDay.ToString());
            newCode.WriteLine("//");
            newCode.WriteLine("//     Changes to this file will be lost if the code is regenerated.");
            newCode.WriteLine("// </auto-generated>");
            newCode.WriteLine("//------------------------------------------------------------------------------");
            newCode.WriteLine();
        }

        private void GenerateUsingStatements(StringWriter newCode) {
            newCode.WriteLine(@"using System;");
            newCode.WriteLine(@"using System.Collections.Generic;");
            newCode.WriteLine(@"using System.Data.Objects;");
            newCode.WriteLine(@"using System.Data.Objects.DataClasses;");
            newCode.WriteLine(@"using System.Linq;");
            newCode.WriteLine(@"using System.Linq.Expressions;");
            newCode.WriteLine(@"using System.Reflection;");
            newCode.WriteLine();
        }

        private void GenerateFunctionHeader(StringWriter newCode, string attributeNamespace, string functionName) {
            AppendSpaces(newCode, 8);
            newCode.WriteLine("/// <summary>");
            AppendSpaces(newCode, 8);
            newCode.WriteLine("/// Proxy for the function " + attributeNamespace + "." + functionName);
            AppendSpaces(newCode, 8);
            newCode.WriteLine("/// </summary>");
            AppendSpaces(newCode, 8);
            newCode.WriteLine("[DbFunction(\"" + attributeNamespace + "\", \"" + functionName + "\")]");
        }

        private void GenerateSByteCLSNonComplaintAttribute(StringWriter newCode) {
            AppendSpaces(newCode, 8);
            newCode.WriteLine("[CLSCompliant(false)]");
        }

        private void GenerateFxcopSuppressionAttribute(StringWriter newCode) {
            AppendSpaces(newCode, 8);
            newCode.WriteLine("[System.Diagnostics.CodeAnalysis.SuppressMessage(\"Microsoft.Naming\", \"CA1720:IdentifiersShouldNotContainTypeNames\", MessageId = \"string\")]");
        }

        private bool IsNullableType(Type parameterType) {
            return parameterType == typeof(byte[]) || parameterType == typeof(string);
        }

        private void WriteType(StringBuilder code, Type parameterType) {
            code.Append(parameterType.ToString());
            if (!IsNullableType(parameterType)) {
                code.Append("?");
            }

            code.Append(" ");
        }

        private void WriteExceptionStatement(StringWriter code) {
            AppendSpaces(code, 12);
            code.WriteLine("throw new NotSupportedException(\"This function can only be invoked from LINQ to Entities.\");");
            AppendSpaces(code, 8);
            code.WriteLine("}");
            code.WriteLine();
        }

        private void AppendSpaces(StringWriter str, int num) {
            for (int i = 0; i < num; i++) {
                str.Write(" ");
            }
        }

        private void AppendSpaces(StringBuilder str, int num) {
            for (int i = 0; i < num; i++) {
                str.Append(" ");
            }
        }

        private string FindCorrectFunctionName(string inputName, bool pascalCaseFunctionNames) {
            if (_funcDictionaryToUse == null) {
                return inputName;
            }

            string value;

            if (_funcDictionaryToUse.TryGetValue(inputName, out value)) {
                return value;
            }
            else if (pascalCaseFunctionNames) {
                string interFunctionName = inputName.ToLower();
                char[] charFuncName = interFunctionName.ToCharArray();
                charFuncName[0] = char.ToUpper(charFuncName[0]);
                return new string(charFuncName);
            }

            return inputName;
        }

        private string FindCorrectParameterName(string inputParameterName) {
            string value;

            if (_paramDictionaryToUse == null) {
                return inputParameterName;
            }

            if (_paramDictionaryToUse.TryGetValue(inputParameterName, out value)) {
                return value;
            }

            return inputParameterName;
        }

        public void GenerateClassHeader(StringWriter newCode, string className, string namespacestring) {
            AppendSpaces(newCode, 4);
            newCode.WriteLine("/// <summary>");
            AppendSpaces(newCode, 4);
            newCode.Write("/// Contains function stubs that expose " + namespacestring);
            newCode.WriteLine(" methods in Linq to Entities.");
            AppendSpaces(newCode, 4);
            newCode.WriteLine("/// </summary>");
            AppendSpaces(newCode, 4);
            newCode.Write("public static ");
            if (className.Equals("EntityFunctions", StringComparison.Ordinal)) {
                newCode.Write("partial ");
            }

            newCode.WriteLine("class " + className);
            AppendSpaces(newCode, 4);
            newCode.WriteLine("{");
        }
    }
}