using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using VfpEntityFrameworkProvider.Expressions;
using VfpEntityFrameworkProvider.VfpOleDb;
using VfpEntityFrameworkProvider.Visitors;
using VfpEntityFrameworkProvider.Visitors.Gatherers;
using VfpEntityFrameworkProvider.Visitors.Rewriters;

namespace VfpEntityFrameworkProvider.SqlGeneration {
    internal class SqlVisitor : IVfpExpressionVisitor<ISqlFragment> {
        public readonly Dictionary<string, int> AllColumnNames = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        public readonly Dictionary<string, int> AllExtentNames = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        public static readonly IEnumerable<string> AliasNames = new[] { "Extent", "Project", "Filter", "Join", "GroupBy", "Distinct", "UnionAll", };

        /// <summary>
        /// VariableReferenceExpressions are allowed only as children of VfpPropertyExpression
        /// or MethodExpression.  The cheapest way to ensure this is to set the following
        /// property in VfpVariableReferenceExpression and reset it in the allowed parent expressions.
        /// </summary>
        protected bool isVarRefSingle = false;

        #region Statics
        private static readonly Dictionary<string, FunctionHandler> builtInFunctionHandlers = InitializeBuiltInFunctionHandlers();
        private static readonly Dictionary<string, FunctionHandler> canonicalFunctionHandlers = InitializeCanonicalFunctionHandlers();
        private static readonly Dictionary<string, string> functionNameToOperatorDictionary = InitializeFunctionNameToOperatorDictionary();

        //public ISqlFragment Visit(Expressions.SubqueryExpression expression) {
        //    throw new NotImplementedException();
        //}
        private delegate ISqlFragment FunctionHandler(SqlVisitor visitor, VfpFunctionExpression functionExpr);

        /// <summary>
        /// All special built-in functions and their handlers
        /// </summary>
        private static Dictionary<string, FunctionHandler> InitializeBuiltInFunctionHandlers() {
            Dictionary<string, FunctionHandler> functionHandlers = new Dictionary<string, FunctionHandler>(5, StringComparer.Ordinal);
            functionHandlers.Add("concat", HandleConcatFunction);
            functionHandlers.Add("dateadd", HandleDatepartDateFunction);
            functionHandlers.Add("datediff", HandleDatepartDateFunction);
            functionHandlers.Add("datename", HandleDatepartDateFunction);
            functionHandlers.Add("datepart", HandleDatepartDateFunction);

            return functionHandlers;
        }

        private static Dictionary<string, FunctionHandler> InitializeCanonicalFunctionHandlers() {
            Dictionary<string, FunctionHandler> functionHandlers = new Dictionary<string, FunctionHandler>(51, StringComparer.Ordinal);
            functionHandlers.Add("IndexOf", HandleCanonicalFunctionIndexOf);
            functionHandlers.Add("Length", HandleCanonicalFunctionLength);

            functionHandlers.Add("Round", HandleCanonicalFunctionRound);
            functionHandlers.Add("Power", HandleCanonicalFunctionPower);
            functionHandlers.Add("Replace", HandleCanonicalFunctionReplace);
            functionHandlers.Add("Abs", HandleCanonicalFunctionAbs);
            functionHandlers.Add("ToLower", HandleCanonicalFunctionToLower);
            functionHandlers.Add("ToUpper", HandleCanonicalFunctionToUpper);
            functionHandlers.Add("Trim", HandleCanonicalFunctionTrim);
            functionHandlers.Add("Contains", HandleCanonicalFunctionContains);
            functionHandlers.Add("StartsWith", HandleCanonicalFunctionStartsWith);
            functionHandlers.Add("EndsWith", HandleCanonicalFunctionEndsWith);

            functionHandlers.Add("Substring", HandleCanonicalFunctionSubstring);

            //DateTime Functions
            functionHandlers.Add("Year", HandleCanonicalFunctionDatepart);
            functionHandlers.Add("Month", HandleCanonicalFunctionDatepart);
            functionHandlers.Add("Day", HandleCanonicalFunctionDatepart);
            functionHandlers.Add("Hour", HandleCanonicalFunctionDatepart);
            functionHandlers.Add("Minute", HandleCanonicalFunctionDatepart);
            functionHandlers.Add("Second", HandleCanonicalFunctionDatepart);

            functionHandlers.Add("DayOfYear", HandleCanonicalFunctionDatepart);
            functionHandlers.Add("CurrentDateTime", HandleCanonicalFunctionCurrentDateTime);
            functionHandlers.Add("CreateDateTime", HandleCanonicalFunctionCreateDateTime);
            functionHandlers.Add("TruncateTime", HandleCanonicalFunctionTruncateTime);

            functionHandlers.Add("AddYears", HandleCanonicalFunctionDateAdd);
            functionHandlers.Add("AddMonths", HandleCanonicalFunctionDateAdd);
            functionHandlers.Add("AddDays", HandleCanonicalFunctionDateAdd);
            functionHandlers.Add("AddHours", HandleCanonicalFunctionDateAdd);
            functionHandlers.Add("AddMinutes", HandleCanonicalFunctionDateAdd);
            functionHandlers.Add("AddSeconds", HandleCanonicalFunctionDateAdd);

            functionHandlers.Add("DiffYears", HandleCanonicalFunctionDateDiff);
            functionHandlers.Add("DiffMonths", HandleCanonicalFunctionDateDiff);
            functionHandlers.Add("DiffDays", HandleCanonicalFunctionDateDiff);
            functionHandlers.Add("DiffHours", HandleCanonicalFunctionDateDiff);
            functionHandlers.Add("DiffMinutes", HandleCanonicalFunctionDateDiff);
            functionHandlers.Add("DiffSeconds", HandleCanonicalFunctionDateDiff);

            //Functions that translate to operators
            functionHandlers.Add("Concat", HandleConcatFunction);
            functionHandlers.Add("BitwiseAnd", HandleCanonicalFunctionBitwiseAnd);
            functionHandlers.Add("BitwiseNot", HandleCanonicalFunctionBitwiseNot);
            functionHandlers.Add("BitwiseOr", HandleCanonicalFunctionBitwiseOr);
            functionHandlers.Add("BitwiseXor", HandleCanonicalFunctionBitwiseXor);

            #region NotSupported

            functionHandlers.Add("AsNonUnicode", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("AsUnicode", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("AddMilliseconds", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("AddMicroseconds", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("AddNanoseconds", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("DiffMilliseconds", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("DiffMicroseconds", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("DiffNanoseconds", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("Millisecond", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("CreateDateTimeOffset", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("CreateTime", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("NewGuid", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("GetTotalOffsetMinutes", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("Reverse", HandleCanonicalFunctionNotSupported);
            functionHandlers.Add("Truncate", HandleCanonicalFunctionNotSupported);

            #endregion

            return functionHandlers;
        }

        private static ISqlFragment HandleCanonicalFunctionNotSupported(SqlVisitor visitor, VfpFunctionExpression e) {
            throw new NotSupportedException(e.Function.Name);
        }

        /// <summary>
        /// Initializes the mapping from functions to T-SQL operators
        /// for all functions that translate to T-SQL operators
        /// </summary>
        private static Dictionary<string, string> InitializeFunctionNameToOperatorDictionary() {
            Dictionary<string, string> functionNameToOperatorDictionary = new Dictionary<string, string>(5, StringComparer.Ordinal);
            functionNameToOperatorDictionary.Add("Concat", "+");    //canonical
            functionNameToOperatorDictionary.Add("CONCAT", "+");    //store
            functionNameToOperatorDictionary.Add("BitwiseAnd", "&");
            functionNameToOperatorDictionary.Add("BitwiseNot", "~");
            functionNameToOperatorDictionary.Add("BitwiseOr", "|");
            functionNameToOperatorDictionary.Add("BitwiseXor", "^");
            return functionNameToOperatorDictionary;
        }

        #endregion

        #region Visit

        public ISqlFragment Visit(VfpCommandTree expression) {
            throw new NotSupportedException();
        }

        public ISqlFragment Visit(VfpDeleteCommandTree expression) {
            throw new NotSupportedException();
        }

        public ISqlFragment Visit(VfpFunctionCommandTree expression) {
            throw new NotSupportedException();
        }

        public ISqlFragment Visit(VfpInsertCommandTree expression) {
            throw new NotSupportedException();
        }

        public ISqlFragment Visit(VfpQueryCommandTree expression) {
            throw new NotSupportedException();
        }

        public ISqlFragment Visit(VfpUpdateCommandTree expression) {
            throw new NotSupportedException();
        }

        public ISqlFragment Visit(VfpAndExpression e) {
            return this.VisitBinaryExpression(VfpExpressionKind.And, e.Left, e.Right);
        }

        public ISqlFragment Visit(VfpApplyExpression expression) {
            throw new NotSupportedException("APPLY joins are not supported");
        }

        /// <summary>
        /// For binary expressions, we delegate to <see cref="VisitBinaryExpression"/>.
        /// We handle the other expressions directly.
        /// </summary>
        /// <returns>A <see cref="SqlBuilder"/></returns>
        public ISqlFragment Visit(VfpArithmeticExpression e) {
            SqlBuilder result;

            switch (e.ExpressionKind) {
                case VfpExpressionKind.Divide:
                case VfpExpressionKind.Minus:
                case VfpExpressionKind.Modulo:
                case VfpExpressionKind.Multiply:
                case VfpExpressionKind.Plus:
                    result = this.VisitBinaryExpression(e.ExpressionKind, e.Arguments[0], e.Arguments[1]);
                    break;

                case VfpExpressionKind.UnaryMinus:
                    result = new SqlBuilder();
                    result.Append(" -(");
                    result.Append(e.Arguments[0].Accept(this));
                    result.Append(")");
                    break;

                default:
                    Debug.Assert(false);
                    throw new InvalidOperationException();
            }

            return result;
        }

        public ISqlFragment Visit(VfpParameterExpression expression) {
            var result = new SqlBuilder();

            result.Append(expression.Name);

            return result;
        }

        public ISqlFragment Visit(VfpXmlToCursorPropertyExpression expression) {
            var result = new SqlBuilder();
            var previousIsVarRefSingle = isVarRefSingle;
            isVarRefSingle = false;
            
            result.Append(expression.Instance.Accept(this));
            result.Append(".Id");

            isVarRefSingle = previousIsVarRefSingle;

            return result;
        }

        public ISqlFragment Visit(VfpXmlToCursorScanExpression expression) {
            var builder = new SqlBuilder();

            builder.Append("(iif(XmlToCursor(");
            builder.Append(expression.Parameter.Accept(this));
            builder.Append(string.Format(", '{0}') > 0, '{0}', ''))", expression.CursorName));

            var result = new SqlSelectStatement();

            result.Select.Append("Id");
            result.From.Append(builder);

            return result;
        }

        public ISqlFragment Visit(VfpXmlToCursorExpression expression) {
            var result = new SqlBuilder();

            result.Append(expression.Property.Accept(this));

            var values = expression.Parameter;
            var notExpression = values as VfpNotExpression;

            if (notExpression != null) {
                result.Append(" NOT ");
            }

            result.Append(" IN (SELECT Id FROM (iif(XmlToCursor(");
            result.Append(expression.Parameter.Accept(this));
            result.Append(string.Format(", '{0}') > 0, '{0}', '')))", expression.CursorName));

            return result;
        }
        
        public ISqlFragment Visit(VfpInExpression expression) {
            var result = new SqlBuilder();

            result.Append(expression.Item.Accept(this));

            result.Append(" IN (");

            for (int index = 0; index < expression.List.Count; index++) {
                if (index > 0) {
                    result.Append(",");
                }

                result.Append(expression.List[index].Accept(this));
            }
            
            result.Append(") ");

            return result;
        }

        /// <summary>
        /// If the ELSE clause is null, we do not write it out.
        /// </summary>
        public ISqlFragment Visit(VfpCaseExpression e) {
            SqlBuilder result = new SqlBuilder();

            Debug.Assert(e.When.Count == e.Then.Count);

            result.Append("ICASE(");
            for (int i = 0; i < e.When.Count; ++i) {
                if (i > 0) {
                    result.Append(",");
                }

                result.Append(e.When[i].Accept(this));
                result.Append(",");
                result.Append(e.Then[i].Accept(this));
            }

            if (e.Else != null && !(e.Else is VfpNullExpression)) {
                result.Append(",");
                result.Append(e.Else.Accept(this));
            }

            result.Append(")");

            return result;
        }

        public ISqlFragment Visit(VfpCastExpression e) {
            SqlBuilder result = new SqlBuilder();
            result.Append(" CAST( ");
            result.Append(e.Argument.Accept(this));
            result.Append(" AS ");
            result.Append(GetSqlPrimitiveType(e.ResultType));
            result.Append(")");

            return result;
        }

        public ISqlFragment Visit(VfpComparisonExpression e) {
            SqlBuilder result = this.VisitBinaryExpression(e.ExpressionKind, e.Left, e.Right);

            return result;
        }

        public ISqlFragment Visit(VfpConstantExpression expression) {
            return this.VisitConstantExpression(expression.ResultType, expression.Value);
        }

        public ISqlFragment Visit(VfpCrossJoinExpression expression) {
            return VisitJoinExpression(expression.Inputs, expression.ExpressionKind, ", ", null);
        }

        public ISqlFragment Visit(VfpDerefExpression expression) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The DISTINCT has to be added to the beginning of SqlSelectStatement.Select,
        /// but it might be too late for that.  So, we use a flag on SqlSelectStatement
        /// instead, and add the "DISTINCT" in the second phase.
        /// </summary>
        /// <returns>A <see cref="SqlSelectStatement"/></returns>
        public ISqlFragment Visit(VfpDistinctExpression e) {
            SqlSelectStatement result = this.VisitExpressionEnsureSqlStatement(e.Argument);

            if (!IsCompatible(result, e.ExpressionKind)) {
                Symbol fromSymbol;
                TypeUsage inputType = e.Argument.ResultType.ToElementTypeUsage();
                result = this.CreateNewSelectStatement(result, "d", inputType, out fromSymbol);
                this.AddFromSymbol(result, "d", fromSymbol, false);
            }

            result.IsDistinct = true;
            return result;
        }

        public ISqlFragment Visit(VfpElementExpression e) {
            // ISSUE: What happens if the VfpElementExpression is used as an input expression?
            // i.e. adding the '('  might not be right in all cases.
            SqlBuilder result = new SqlBuilder();
            result.Append("(");
            result.Append(this.VisitExpressionEnsureSqlStatement(e.Argument));
            result.Append(")");

            return result;
        }

        public ISqlFragment Visit(VfpEntityRefExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(VfpExceptExpression expression) {
            //return VisitSetOpExpression(e.Left, e.Right, "MINUS");
            //return VisitSetOpExpression(e.Left, e.Right, "EXCEPT");
            throw new NotSupportedException();
        }

        public ISqlFragment Visit(VfpExpression expression) {
            return this.Visit((dynamic)expression);
        }

        public ISqlFragment Visit(VfpFilterExpression e) {
            return this.VisitFilterExpression(e.Input, e.Predicate, false);
        }

        /// <summary>
        /// Lambda functions are not supported.
        /// The functions supported are:
        /// <list type="number">
        /// <item>Canonical Functions - We recognize these by their dataspace, it is DataSpace.CSpace</item>
        /// <item>Store Functions - We recognize these by the BuiltInAttribute and not being Canonical</item>
        /// <item>User-defined Functions - All the rest except for Lambda functions</item>
        /// </list>
        /// We handle Canonical and Store functions the same way: If they are in the list of functions 
        /// that need special handling, we invoke the appropriate handler, otherwise we translate them to
        /// FunctionName(arg1, arg2, ..., argn).
        /// We translate user-defined functions to NamespaceName.FunctionName(arg1, arg2, ..., argn).
        /// </summary>
        /// <returns>A <see cref="SqlBuilder"/></returns>
        public ISqlFragment Visit(VfpFunctionExpression e) {
            // check if function requires special case processing, if so, delegates to it
            if (this.IsSpecialBuiltInFunction(e)) {
                return this.HandleSpecialBuiltInFunction(e);
            }

            if (this.IsSpecialCanonicalFunction(e)) {
                return this.HandleSpecialCanonicalFunction(e);
            }

            return this.HandleFunctionDefault(e);
        }

        /// <summary>
        /// <see cref="Visit(VfpFilterExpression)"/> for general details.
        /// We modify both the GroupBy and the Select fields of the SqlSelectStatement.
        /// GroupBy gets just the keys without aliases,
        /// and Select gets the keys and the aggregates with aliases.
        /// 
        /// Whenever there exists at least one aggregate with an argument that is not is not a simple
        /// <see cref="VfpPropertyExpression"/>  over <see cref="VfpVariableReferenceExpression"/>, 
        /// we create a nested query in which we alias the arguments to the aggregates. 
        /// That is due to the following two limitations of Sql Server:
        /// <list type="number">
        /// <item>If an expression being aggregated contains an outer reference, then that outer 
        /// reference must be the only column referenced in the expression </item>
        /// <item>Sql Server cannot perform an aggregate function on an expression containing 
        /// an aggregate or a subquery. </item>
        /// </list>
        /// 
        /// The default translation, without inner query is: 
        /// 
        ///     SELECT 
        ///         kexp1 AS key1, kexp2 AS key2,... kexpn AS keyn, 
        ///         aggf1(aexpr1) AS agg1, .. aggfn(aexprn) AS aggn
        ///     FROM input AS a
        ///     GROUP BY kexp1, kexp2, .. kexpn
        /// 
        /// When we inject an innner query, the equivalent translation is:
        /// 
        ///     SELECT 
        ///         key1 AS key1, key2 AS key2, .. keyn AS keys,  
        ///         aggf1(agg1) AS agg1, aggfn(aggn) AS aggn
        ///     FROM (
        ///             SELECT 
        ///                 kexp1 AS key1, kexp2 AS key2,... kexpn AS keyn, 
        ///                 aexpr1 AS agg1, .. aexprn AS aggn
        ///             FROM input AS a
        ///         ) as a
        ///     GROUP BY key1, key2, keyn
        /// 
        /// </summary>
        /// <returns>A <see cref="SqlSelectStatement"/></returns>
        public ISqlFragment Visit(VfpGroupByExpression e) {
            Symbol fromSymbol;
            SqlSelectStatement innerQuery = this.VisitInputExpression(e.Input.Expression, e.Input.VariableName, e.Input.VariableType, out fromSymbol);

            // GroupBy is compatible with Filter and OrderBy
            // but not with Project, GroupBy
            if (!IsCompatible(innerQuery, e.ExpressionKind)) {
                innerQuery = this.CreateNewSelectStatement(innerQuery, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
            }

            this.selectStatementStack.Push(innerQuery);
            this.symbolTable.EnterScope();

            this.AddFromSymbol(innerQuery, e.Input.VariableName, fromSymbol);

            // This line is not present for other relational nodes.
            this.symbolTable.Add(e.Input.GroupVariableName, fromSymbol);

            // The enumerator is shared by both the keys and the aggregates,
            // so, we do not close it in between.
            RowType groupByType = e.ResultType.GetEdmType<CollectionType>().TypeUsage.GetEdmType<RowType>();

            //Whenever there exists at least one aggregate with an argument that is not simply a PropertyExpression 
            // over a VarRefExpression, we need a nested query in which we alias the arguments to the aggregates.
            bool needsInnerQuery = NeedsInnerQuery(e.Aggregates);

            SqlSelectStatement result;
            if (needsInnerQuery) {
                //Create the inner query
                result = this.CreateNewSelectStatement(innerQuery, e.Input.VariableName, e.Input.VariableType, false, out fromSymbol);
                this.AddFromSymbol(result, e.Input.VariableName, fromSymbol, false);
            }
            else {
                result = innerQuery;
            }

            using (IEnumerator<EdmProperty> members = groupByType.Properties.GetEnumerator()) {
                members.MoveNext();
                Debug.Assert(result.Select.IsEmpty);

                string separator = string.Empty;

                foreach (VfpExpression key in e.Keys) {
                    EdmProperty member = members.Current;
                    string alias = member.Name;

                    result.GroupBy.Append(separator);

                    ISqlFragment keySql = key.Accept(this);

                    if (!needsInnerQuery) {
                        //Default translation: Key AS Alias
                        result.Select.Append(separator);
                        result.Select.AppendLine();
                        result.Select.Append(keySql);
                        result.Select.Append(" AS ");
                        result.Select.Append(alias);

                        result.GroupBy.Append(keySql);
                    }
                    else {
                        // The inner query contains the default translation Key AS Alias
                        innerQuery.Select.Append(separator);
                        innerQuery.Select.AppendLine();
                        innerQuery.Select.Append(keySql);
                        innerQuery.Select.Append(" AS ");
                        innerQuery.Select.Append(alias);

                        //The outer resulting query projects over the key aliased in the inner query: 
                        //  fromSymbol.Alias AS Alias
                        result.Select.Append(separator);
                        result.Select.AppendLine();
                        result.Select.Append(fromSymbol);
                        result.Select.Append(".");
                        result.Select.Append(alias);
                        result.Select.Append(" AS ");
                        result.Select.Append(alias);

                        result.GroupBy.Append(alias);
                    }

                    separator = ", ";
                    members.MoveNext();
                }

                foreach (VfpAggregate aggregate in e.Aggregates) {
                    EdmProperty member = members.Current;
                    string alias = member.Name;

                    Debug.Assert(aggregate.Arguments.Count == 1);
                    ISqlFragment translatedAggregateArgument = aggregate.Arguments[0].Accept(this);

                    object aggregateArgument;

                    if (needsInnerQuery) {
                        //In this case the argument to the aggregate is reference to the one projected out by the
                        // inner query
                        SqlBuilder wrappingAggregateArgument = new SqlBuilder();
                        wrappingAggregateArgument.Append(fromSymbol);
                        wrappingAggregateArgument.Append(".");
                        wrappingAggregateArgument.Append(alias);
                        aggregateArgument = wrappingAggregateArgument;

                        innerQuery.Select.Append(separator);
                        innerQuery.Select.AppendLine();
                        innerQuery.Select.Append(translatedAggregateArgument);
                        innerQuery.Select.Append(" AS ");
                        innerQuery.Select.Append(alias);
                    }
                    else {
                        aggregateArgument = translatedAggregateArgument;
                    }

                    ISqlFragment aggregateResult = this.VisitAggregate(aggregate, aggregateArgument);

                    result.Select.Append(separator);
                    result.Select.AppendLine();
                    result.Select.Append(aggregateResult);
                    result.Select.Append(" AS ");
                    result.Select.Append(alias);

                    separator = ", ";
                    members.MoveNext();
                }
            }

            this.symbolTable.ExitScope();
            this.selectStatementStack.Pop();

            return result;
        }

        public ISqlFragment Visit(VfpIntersectExpression expression) {
            throw new NotSupportedException("INTERSECT");
        }

        public ISqlFragment Visit(VfpIsEmptyExpression expression) {
            return this.VisitIsEmptyExpression(expression, false);
        }

        public ISqlFragment Visit(VfpIsNullExpression expression) {
            return this.VisitIsNullExpression(expression, false);
        }

        public ISqlFragment Visit(VfpIsOfExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(VfpJoinExpression e) {
            #region Map join type to a string
            string joinString;
            switch (e.ExpressionKind) {
                case VfpExpressionKind.FullOuterJoin:
                    joinString = "FULL OUTER JOIN";
                    break;

                case VfpExpressionKind.InnerJoin:
                    joinString = "INNER JOIN";
                    break;

                case VfpExpressionKind.LeftOuterJoin:
                    joinString = "LEFT JOIN";
                    break;

                default:
                    Debug.Assert(false);
                    joinString = null;
                    break;
            }
            #endregion

            List<VfpExpressionBinding> inputs = new List<VfpExpressionBinding>(2);
            inputs.Add(e.Left);
            inputs.Add(e.Right);

            return this.VisitJoinExpression(inputs, e.ExpressionKind, joinString, e.JoinCondition);
        }

        public ISqlFragment Visit(VfpLikeExpression e) {
            SqlBuilder result = new SqlBuilder();
            result.Append(e.Argument.Accept(this));
            result.Append(" LIKE ");
            result.Append(e.Pattern.Accept(this));

            if (e.Escape.ExpressionKind != VfpExpressionKind.Null) {
                result.Append(" ESCAPE ");
                result.Append(e.Escape.Accept(this));
            }

            return result;
        }

        public ISqlFragment Visit(VfpLikeCExpression e) {
            var result = new SqlBuilder();

            result.Append("LIKEC(");
            result.Append(e.Argument.Accept(this));
            result.Append(", STRTRAN(");
            result.Append(e.Pattern.Accept(this));
            result.Append(", '%', '*'))");

            return result;
        }

        public ISqlFragment Visit(VfpLimitExpression e) {
            Debug.Assert(e.Limit is VfpConstantExpression || e.Limit is VfpParameterReferenceExpression, "VfpLimitExpression.Limit is of invalid expression type");

            SqlSelectStatement result = this.VisitExpressionEnsureSqlStatement(e.Argument, false);
            Symbol fromSymbol;

            if (!IsCompatible(result, e.ExpressionKind)) {
                TypeUsage inputType = e.Argument.ResultType.ToElementTypeUsage();

                result = this.CreateNewSelectStatement(result, "top", inputType, out fromSymbol);
                this.AddFromSymbol(result, "top", fromSymbol, false);
            }

            ISqlFragment topCount = this.HandleCountExpression(e.Limit);

            result.Top = new TopClause(topCount);
            return result;
        }

        public ISqlFragment Visit(VfpNewInstanceExpression e) {
            if (e.ResultType.IsCollectionType()) {
                return this.VisitCollectionConstructor(e);
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// The Not expression may cause the translation of its child to change.
        /// These children are
        /// <list type="bullet">
        /// <item><see cref="VfpNotExpression"/>NOT(Not(x)) becomes x</item>
        /// <item><see cref="VfpIsEmptyExpression"/>NOT EXISTS becomes EXISTS</item>
        /// <item><see cref="VfpIsNullExpression"/>IS NULL becomes IS NOT NULL</item>
        /// <item><see cref="DbComparisonExpression"/>= becomes&lt;&gt; </item>
        /// </list>
        /// </summary>
        public ISqlFragment Visit(VfpNotExpression e) {
            // Flatten Not(Not(x)) to x.
            var notExpression = e.Argument as VfpNotExpression;
            if (notExpression != null) {
                return notExpression.Argument.Accept(this);
            }

            var isEmptyExpression = e.Argument as VfpIsEmptyExpression;
            if (isEmptyExpression != null) {
                return this.VisitIsEmptyExpression(isEmptyExpression, true);
            }

            var isNullExpression = e.Argument as VfpIsNullExpression;
            if (isNullExpression != null) {
                return this.VisitIsNullExpression(isNullExpression, true);
            }

            var comparisonExpression = e.Argument as VfpComparisonExpression;
            if (comparisonExpression != null) {
                if (comparisonExpression.ExpressionKind == VfpExpressionKind.Equals) {
                    return this.VisitBinaryExpression(VfpExpressionKind.NotEquals, comparisonExpression.Left, comparisonExpression.Right);
                }
            }

            SqlBuilder result = new SqlBuilder();
            result.Append(" NOT (");
            result.Append(e.Argument.Accept(this));
            result.Append(")");

            return result;
        }

        public ISqlFragment Visit(VfpNullExpression e) {
            SqlBuilder result = new SqlBuilder();

            result.Append("CAST(NULL AS ");
            TypeUsage type = e.ResultType;
            result.Append(GetSqlPrimitiveType(type));
            result.Append(")");
            return result;
        }

        public ISqlFragment Visit(VfpOfTypeExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(VfpOrExpression expression) {
            return this.VisitBinaryExpression(VfpExpressionKind.Or, expression.Left, expression.Right);
        }

        public ISqlFragment Visit(VfpParameterReferenceExpression e) {
            SqlBuilder result = new SqlBuilder();
            result.Append("@" + e.ParameterName);

            return result;
        }

        public ISqlFragment Visit(VfpProjectExpression expression) {
            Symbol fromSymbol;

            SqlSelectStatement result = this.VisitInputExpression(expression.Input.Expression, expression.Input.VariableName, expression.Input.VariableType, out fromSymbol);

            // Project is compatible with Filter
            // but not with Project, GroupBy
            if (!IsCompatible(result, expression.ExpressionKind)) {
                result = this.CreateNewSelectStatement(result, expression.Input.VariableName, expression.Input.VariableType, out fromSymbol);
            }

            this.selectStatementStack.Push(result);
            this.symbolTable.EnterScope();

            this.AddFromSymbol(result, expression.Input.VariableName, fromSymbol);

            // Project is the only node that can have VfpNewInstanceExpression as a child
            // so we have to check it here.
            // We call VisitNewInstanceExpression instead of Visit(VfpNewInstanceExpression), since
            // the latter throws.
            VfpNewInstanceExpression newInstanceExpression = expression.Projection as VfpNewInstanceExpression;
            if (newInstanceExpression != null) {
                result.Select.Append(this.VisitNewInstanceExpression(newInstanceExpression));
            }
            else {
                result.Select.Append(expression.Projection.Accept(this));
            }

            this.symbolTable.ExitScope();
            this.selectStatementStack.Pop();

            return result;
        }

        public ISqlFragment Visit(VfpPropertyVariableNameExpression e) {
            var result = new SqlBuilder();
            var propertyShortName = GetShortName(e.Property.Name);
            var variableName = GetShortName(e.VariableName);

            if (AllExtentNames.ContainsKey(variableName)) {
                result.Append(variableName);
                result.Append(".");
                result.Append(propertyShortName);

            }

            return result;
        }

        public ISqlFragment Visit(VfpPropertyExpression e) {
            SqlBuilder result;

            ISqlFragment instanceSql = e.Instance.Accept(this);
            var propertyShortName = GetShortName(e.Property.Name);

            // Since the VfpVariableReferenceExpression is a proper child of ours, we can reset
            // isVarSingle.
            VfpVariableReferenceExpression VfpVariableReferenceExpression = e.Instance as VfpVariableReferenceExpression;
            if (VfpVariableReferenceExpression != null) {
                this.isVarRefSingle = false;
            }

            // We need to flatten, and have not yet seen the first nested SELECT statement.
            JoinSymbol joinSymbol = instanceSql as JoinSymbol;
            if (joinSymbol != null && joinSymbol.NameToExtent.ContainsKey(propertyShortName)) {
                Debug.Assert(joinSymbol.NameToExtent.ContainsKey(propertyShortName));
                if (joinSymbol.IsNestedJoin) {
                    return new SymbolPair(joinSymbol, joinSymbol.NameToExtent[propertyShortName]);
                }
                else {
                    return joinSymbol.NameToExtent[propertyShortName];
                }
            }

            // ---------------------------------------
            // We have seen the first nested SELECT statement, but not the column.
            SymbolPair symbolPair = instanceSql as SymbolPair;
            if (symbolPair != null) {
                JoinSymbol columnJoinSymbol = symbolPair.Column as JoinSymbol;
                if (columnJoinSymbol != null) {
                    symbolPair.Column = columnJoinSymbol.NameToExtent[propertyShortName];
                    return symbolPair;
                }
                else {
                    // symbolPair.Column has the base extent.
                    // we need the symbol for the column, since it might have been renamed
                    // when handling a JOIN.
                    if (symbolPair.Column.Columns.ContainsKey(e.Property.Name)) {
                        result = new SqlBuilder();
                        result.Append(symbolPair.Source);
                        result.Append(".");

                        result.Append(symbolPair.Column.Columns[e.Property.Name]);
                        return result;
                    }
                }
            }

            result = new SqlBuilder();
            result.Append(instanceSql);
            result.Append(".");

            // At this point the column name cannot be renamed, so we do
            // not use a symbol.
            result.Append(propertyShortName);

            return result;
        }

        public static string GetShortName(string originalName) {
            foreach (var shortName in AliasNames) {
                if (originalName.StartsWith(shortName)) {
                    return originalName.Replace(shortName, shortName.Substring(0, 1));
                }
            }

            return originalName;
        }

        public ISqlFragment Visit(VfpQuantifierExpression e) {
            SqlBuilder result = new SqlBuilder();

            bool negatePredicate = e.ExpressionKind == VfpExpressionKind.All;
            if (e.ExpressionKind == VfpExpressionKind.Any) {
                result.Append("EXISTS (");
            }
            else {
                Debug.Assert(e.ExpressionKind == VfpExpressionKind.All);
                result.Append("NOT EXISTS (");
            }

            SqlSelectStatement filter = this.VisitFilterExpression(e.Input, e.Predicate, negatePredicate);
            if (filter.Select.IsEmpty) {
                this.AddDefaultColumns(filter);
            }

            result.Append(filter);
            result.Append(")");

            return result;
        }

        public ISqlFragment Visit(VfpRefExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(VfpRefKeyExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(VfpRelationshipNavigationExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(VfpScanExpression expression) {
            EntitySetBase target = expression.Target;

            if (this.IsParentAJoin) {
                SqlBuilder result = new SqlBuilder();
                result.Append(GetTargetSql(target));

                return result;
            }
            else {
                SqlSelectStatement result = new SqlSelectStatement();
                result.From.Append(GetTargetSql(target));

                return result;
            }
        }

        public ISqlFragment Visit(VfpSkipExpression e) {
            Debug.Assert(e.Count is VfpConstantExpression || e.Count is VfpParameterReferenceExpression, "VfpSkipExpression.Count is of invalid expression type");

            List<Symbol> inputColumns;
            Symbol row_numberSymbol;

            SqlSelectStatement input = this.VisitVfpSkipExpression_GetSqlSelectStatement(e, out inputColumns, out row_numberSymbol);

            //Create the resulting statement 
            //See CreateNewSelectStatement, it is very similar
            SqlSelectStatement result = new SqlSelectStatement();
            result.From.Append("(select *, recno() as row_number from ( ");
            result.From.Append(input);
            result.From.AppendLine();
            result.From.Append(") ");
            result.From.Append(input.FromExtents[0].Name);
            result.From.Append(") ");

            //Create a symbol for the input
            Symbol resultFromSymbol = null;

            if (input.FromExtents.Count == 1) {
                JoinSymbol oldJoinSymbol = input.FromExtents[0] as JoinSymbol;
                if (oldJoinSymbol != null) {
                    // Note: input.FromExtents will not do, since it might
                    // just be an alias of joinSymbol, and we want an actual JoinSymbol.
                    JoinSymbol newJoinSymbol = new JoinSymbol(e.Input.VariableName, e.Input.VariableType, oldJoinSymbol.ExtentList);

                    // This indicates that the oldStatement is a blocking scope
                    // i.e. it hides/renames extent columns
                    newJoinSymbol.IsNestedJoin = true;
                    newJoinSymbol.ColumnList = inputColumns;
                    newJoinSymbol.FlattenedExtentList = oldJoinSymbol.FlattenedExtentList;

                    resultFromSymbol = newJoinSymbol;
                }
            }

            if (resultFromSymbol == null) {
                // This is just a simple extent/SqlSelectStatement,
                // and we can get the column list from the type.
                resultFromSymbol = new Symbol(e.Input.VariableName, e.Input.VariableType);
            }

            //Add the ORDER BY part
            this.selectStatementStack.Push(result);
            this.symbolTable.EnterScope();

            this.AddFromSymbol(result, e.Input.VariableName, resultFromSymbol);
            resultFromSymbol.NeedsRenaming = true;

            //Add the predicate 
            result.Where.Append(resultFromSymbol);
            result.Where.Append(".row_number > ");
            result.Where.Append(this.HandleCountExpression(e.Count));

            this.AddSortKeys(result.OrderBy, e.SortOrder);

            this.symbolTable.ExitScope();
            this.selectStatementStack.Pop();

            return result;
        }

        public ISqlFragment Visit(VfpSortExpression e) {
            Symbol fromSymbol;
            SqlSelectStatement result = this.VisitInputExpression(e.Input.Expression, e.Input.VariableName, e.Input.VariableType, out fromSymbol);

            // OrderBy is compatible with Filter
            // and nothing else
            if (!IsCompatible(result, e.ExpressionKind)) {
                result = this.CreateNewSelectStatement(result, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
            }

            this.selectStatementStack.Push(result);
            this.symbolTable.EnterScope();

            this.AddFromSymbol(result, e.Input.VariableName, fromSymbol);
            this.AddSortKeys(result.OrderBy, e.SortOrder);

            this.symbolTable.ExitScope();
            this.selectStatementStack.Pop();

            return result;
        }

        public ISqlFragment Visit(VfpTreatExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(VfpUnionAllExpression e) {
            return this.VisitSetOpExpression(e.Left, e.Right, "UNION ALL");
        }

        /// <summary>
        /// This method determines whether an extent from an outer scope(free variable)
        /// is used in the CurrentSelectStatement.
        ///
        /// An extent in an outer scope, if its symbol is not in the FromExtents
        /// of the CurrentSelectStatement.
        /// </summary>
        public ISqlFragment Visit(VfpVariableReferenceExpression e) {
            if (this.isVarRefSingle) {
                throw new NotSupportedException();
                // A VfpVariableReferenceExpression has to be a child of VfpPropertyExpression or MethodExpression
                // This is also checked in GenerateSql(...) at the end of the visiting.
            }

            this.isVarRefSingle = true; // This will be reset by VfpPropertyExpression or MethodExpression

            Symbol result = this.symbolTable.Lookup(e.VariableName);
            if (!this.CurrentSelectStatement.FromExtents.Contains(result)) {
                this.CurrentSelectStatement.OuterExtents[result] = true;
            }

            return result;
        }

        #endregion

        private SqlSelectStatement VisitVfpSkipExpression_GetSqlSelectStatement(VfpSkipExpression e, out List<Symbol> inputColumns, out Symbol row_numberSymbol) {
            //Visit the input
            Symbol fromSymbol;
            row_numberSymbol = null;
            SqlSelectStatement input = this.VisitInputExpression(e.Input.Expression, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
            var includeOrderBy = true;

            // Skip is not compatible with anything that OrderBy is not compatible with, as well as with distinct
            if (!IsCompatible(input, e.ExpressionKind)) {
                input = this.CreateNewSelectStatement(input, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
            }

            this.selectStatementStack.Push(input);
            this.symbolTable.EnterScope();

            this.AddFromSymbol(input, e.Input.VariableName, fromSymbol);

            //Add the default columns
            Debug.Assert(input.Select.IsEmpty);
            inputColumns = this.AddDefaultColumns(input);

            if (includeOrderBy) {
                input.IsTopMost = true;
                this.AddSortKeys(input.OrderBy, e.SortOrder);
            }

            //The inner statement is complete, its scopes need not be valid any longer
            this.symbolTable.ExitScope();
            this.selectStatementStack.Pop();

            return input;
        }

        /// <summary>
        /// Translates a list of SortClauses.
        /// Used in the translation of OrderBy 
        /// </summary>
        /// <param name="orderByClause">The SqlBuilder to which the sort keys should be appended</param>
        private void AddSortKeys(SqlBuilder orderByClause, IList<VfpSortClause> sortKeys) {
            string separator = string.Empty;
            foreach (VfpSortClause sortClause in sortKeys) {
                var propertyVariableName = sortClause.Expression as VfpPropertyVariableNameExpression;

                if (propertyVariableName != null) {
                    if (!AllExtentNames.ContainsKey(GetShortName(propertyVariableName.VariableName))) {
                        continue;
                    }
                }

                orderByClause.Append(separator);
                orderByClause.Append(sortClause.Expression.Accept(this));
                Debug.Assert(sortClause.Collation != null);
                if (!String.IsNullOrEmpty(sortClause.Collation)) {
                    orderByClause.Append(" COLLATE ");
                    orderByClause.Append(sortClause.Collation);
                }

                orderByClause.Append(sortClause.Ascending ? string.Empty : " DESC");

                separator = ", ";
            }
        }

        /// <summary>
        /// Handles the expression representing VfpLimitExpression.Limit and VfpSkipExpression.Count.
        /// If it is a constant expression, it simply does to string thus avoiding casting it to the specific value
        /// (which would be done if <see cref="Visit(VfpConstantExpression)"/> is called)
        /// </summary>
        private ISqlFragment HandleCountExpression(VfpExpression e) {
            ISqlFragment result;

            if (e.ExpressionKind == VfpExpressionKind.Constant) {
                //For constant expression we should not cast the value, 
                // thus we don't go through the default DbConstantExpression handling
                SqlBuilder sqlBuilder = new SqlBuilder();
                sqlBuilder.Append(((VfpConstantExpression)e).Value.ToString());
                result = sqlBuilder;
            }
            else {
                result = e.Accept(this);
            }

            return result;
        }

        /// <summary>
        /// This handles the processing of join expressions.
        /// The extents on a left spine are flattened, while joins
        /// not on the left spine give rise to new nested sub queries.
        ///
        /// Joins work differently from the rest of the visiting, in that
        /// the parent (i.e. the join node) creates the SqlSelectStatement
        /// for the children to use.
        ///
        /// The "parameter" IsInJoinContext indicates whether a child extent should
        /// add its stuff to the existing SqlSelectStatement, or create a new SqlSelectStatement
        /// By passing true, we ask the children to add themselves to the parent join,
        /// by passing false, we ask the children to create new Select statements for
        /// themselves.
        ///
        /// This method is called from <see cref="Visit(VfpApplyExpression)"/> and
        /// <see cref="Visit(VfpJoinExpression)"/>.
        /// </summary>
        /// <returns> A <see cref="SqlSelectStatement"/></returns>
        private ISqlFragment VisitJoinExpression(IList<VfpExpressionBinding> inputs, VfpExpressionKind joinKind, string joinString, VfpExpression joinCondition) {
            SqlSelectStatement result;

            // If the parent is not a join( or says that it is not),
            // we should create a new SqlSelectStatement.
            // otherwise, we add our child extents to the parent's FROM clause.
            if (!this.IsParentAJoin) {
                result = new SqlSelectStatement();
                result.AllJoinExtents = new List<Symbol>();
                this.selectStatementStack.Push(result);
            }
            else {
                result = this.CurrentSelectStatement;
            }

            // Process each of the inputs, and then the joinCondition if it exists.
            // It would be nice if we could call VisitInputExpression - that would
            // avoid some code duplication
            // but the Join post processing is messy and prevents this reuse.
            this.symbolTable.EnterScope();

            string separator = string.Empty;
            bool isLeftMostInput = true;
            int inputCount = inputs.Count;
            for (int idx = 0; idx < inputCount; idx++) {
                VfpExpressionBinding input = inputs[idx];

                if (separator != string.Empty) {
                    result.From.AppendLine();
                }

                result.From.Append(separator + " ");

                // Change this if other conditions are required
                // to force the child to produce a nested SqlStatement.
                bool needsJoinContext = (input.Expression.ExpressionKind == VfpExpressionKind.Scan)
                                        || (isLeftMostInput &&
                                            (input.Expression.IsJoinExpression()
                                             || input.Expression.IsApplyExpression()));

                this.isParentAJoinStack.Push(needsJoinContext ? true : false);

                // if the child reuses our select statement, it will append the from
                // symbols to our FromExtents list.  So, we need to remember the
                // start of the child's entries.
                int fromSymbolStart = result.FromExtents.Count;

                ISqlFragment fromExtentFragment = input.Expression.Accept(this);

                this.isParentAJoinStack.Pop();

                this.ProcessJoinInputResult(fromExtentFragment, result, input, fromSymbolStart);
                separator = joinString;

                isLeftMostInput = false;
            }

            // Visit the on clause/join condition.
            switch (joinKind) {
                case VfpExpressionKind.FullOuterJoin:
                case VfpExpressionKind.InnerJoin:
                case VfpExpressionKind.LeftOuterJoin:
                    result.From.Append(" ON ");
                    this.isParentAJoinStack.Push(false);
                    result.From.Append(joinCondition.Accept(this));
                    this.isParentAJoinStack.Pop();
                    break;
            }

            this.symbolTable.ExitScope();

            if (!this.IsParentAJoin) {
                this.selectStatementStack.Pop();
            }

            return result;
        }

        /// <summary>
        /// This is called from <see cref="VisitJoinExpression"/>.
        ///
        /// This is responsible for maintaining the symbol table after visiting
        /// a child of a join expression.
        ///
        /// The child's sql statement may need to be completed.
        ///
        /// The child's result could be one of
        /// <list type="number">
        /// <item>The same as the parent's - this is treated specially.</item>
        /// <item>A sql select statement, which may need to be completed</item>
        /// <item>An extent - just copy it to the from clause</item>
        /// <item>Anything else (from a collection-valued expression) -
        /// unnest and copy it.</item>
        /// </list>
        ///
        /// If the input was a Join, we need to create a new join symbol,
        /// otherwise, we create a normal symbol.
        ///
        /// We then call AddFromSymbol to add the AS clause, and update the symbol table.
        ///
        ///
        ///
        /// If the child's result was the same as the parent's, we have to clean up
        /// the list of symbols in the FromExtents list, since this contains symbols from
        /// the children of both the parent and the child.
        /// The happens when the child visited is a Join, and is the leftmost child of
        /// the parent.
        /// </summary>
        private void ProcessJoinInputResult(ISqlFragment fromExtentFragment, SqlSelectStatement result, VfpExpressionBinding input, int fromSymbolStart) {
            Symbol fromSymbol = null;

            if (result != fromExtentFragment) {
                // The child has its own select statement, and is not reusing
                // our select statement.
                // This should look a lot like VisitInputExpression().
                SqlSelectStatement sqlSelectStatement = fromExtentFragment as SqlSelectStatement;
                if (sqlSelectStatement != null) {
                    if (sqlSelectStatement.Select.IsEmpty) {
                        List<Symbol> columns = this.AddDefaultColumns(sqlSelectStatement);

                        if (input.Expression.IsJoinExpression()
                            || input.Expression.IsApplyExpression()) {
                            List<Symbol> extents = sqlSelectStatement.FromExtents;
                            JoinSymbol newJoinSymbol = new JoinSymbol(input.VariableName, input.VariableType, extents);
                            newJoinSymbol.IsNestedJoin = true;
                            newJoinSymbol.ColumnList = columns;

                            fromSymbol = newJoinSymbol;
                        }
                        else {
                            // this is a copy of the code in CreateNewSelectStatement.

                            // if the oldStatement has a join as its input, ...
                            // clone the join symbol, so that we "reuse" the
                            // join symbol.  Normally, we create a new symbol - see the next block
                            // of code.
                            JoinSymbol oldJoinSymbol = sqlSelectStatement.FromExtents[0] as JoinSymbol;
                            if (oldJoinSymbol != null) {
                                // Note: sqlSelectStatement.FromExtents will not do, since it might
                                // just be an alias of joinSymbol, and we want an actual JoinSymbol.

                                JoinSymbol newJoinSymbol = new JoinSymbol(input.VariableName, input.VariableType, oldJoinSymbol.ExtentList);
                                // This indicates that the sqlSelectStatement is a blocking scope
                                // i.e. it hides/renames extent columns
                                newJoinSymbol.IsNestedJoin = true;
                                newJoinSymbol.ColumnList = columns;
                                newJoinSymbol.FlattenedExtentList = oldJoinSymbol.FlattenedExtentList;

                                fromSymbol = newJoinSymbol;
                            }
                        }
                    }

                    result.From.Append(" (");
                    result.From.Append(sqlSelectStatement);
                    result.From.Append(" )");
                }
                else if (input.Expression is VfpScanExpression) {
                    result.From.Append(fromExtentFragment);
                }
                else {
                    WrapNonQueryExtent(result, fromExtentFragment, input.Expression.ExpressionKind);
                }

                // i.e. not a join symbol
                if (fromSymbol == null) {
                    fromSymbol = new Symbol(input.VariableName, input.VariableType);
                }

                this.AddFromSymbol(result, input.VariableName, fromSymbol);
                result.AllJoinExtents.Add(fromSymbol);
            }
            else {
                // result == fromExtentFragment.  The child extents have been merged into the parent's.

                // we are adding extents to the current sql statement via flattening.
                // We are replacing the child's extents with a single Join symbol.
                // The child's extents are all those following the index fromSymbolStart.
                List<Symbol> extents = new List<Symbol>();

                // We cannot call extents.AddRange, since the is no simple way to
                // get the range of symbols fromSymbolStart..result.FromExtents.Count
                // from result.FromExtents.
                // We copy these symbols to create the JoinSymbol later.
                for (int i = fromSymbolStart; i < result.FromExtents.Count; ++i) {
                    extents.Add(result.FromExtents[i]);
                }

                result.FromExtents.RemoveRange(fromSymbolStart, result.FromExtents.Count - fromSymbolStart);
                fromSymbol = new JoinSymbol(input.VariableName, input.VariableType, extents);
                result.FromExtents.Add(fromSymbol);

                // this Join Symbol does not have its own select statement, so we
                // do not set IsNestedJoin

                // We do not call AddFromSymbol(), since we do not want to add
                // "AS alias" to the FROM clause- it has been done when the extent was added earlier.
                this.symbolTable.Add(input.VariableName, fromSymbol);
            }
        }

        private SqlBuilder VisitIsEmptyExpression(VfpIsEmptyExpression e, bool negate) {
            SqlBuilder result = new SqlBuilder();
            if (!negate) {
                result.Append(" NOT");
            }

            result.Append(" EXISTS (");
            result.Append(this.VisitExpressionEnsureSqlStatement(e.Argument));
            result.AppendLine();
            result.Append(")");

            return result;
        }

        #region Function Handling Helpers
        /// <summary>
        /// Determines whether the given function is a built-in function that requires special handling
        /// </summary>
        private bool IsSpecialBuiltInFunction(VfpFunctionExpression e) {
            return e.Function.IsBuiltinFunction() && builtInFunctionHandlers.ContainsKey(e.Function.Name);
        }

        /// <summary>
        /// Determines whether the given function is a canonical function that requires special handling
        /// </summary>
        private bool IsSpecialCanonicalFunction(VfpFunctionExpression e) {
            return e.Function.IsCanonicalFunction() && canonicalFunctionHandlers.ContainsKey(e.Function.Name);
        }

        /// <summary>
        /// Default handling for functions
        /// Translates them to FunctionName(arg1, arg2, ..., argn)
        /// </summary>
        private ISqlFragment HandleFunctionDefault(VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();
            this.WriteFunctionName(result, e.Function);
            this.HandleFunctionArgumentsDefault(e, result);
            return result;
        }

        private void WriteFunctionName(SqlBuilder result, EdmFunction function) {
            string storeFunctionName = function.TryGetValueForMetadataProperty<string>("StoreFunctionNameAttribute");

            if (string.IsNullOrEmpty(storeFunctionName)) {
                storeFunctionName = function.Name;
            }

            // If the function is a builtin (ie) the BuiltIn attribute has been
            // specified, then, the function name should not be quoted; additionally,
            // no namespace should be used.
            if (function.IsBuiltinFunction()) {
                if (function.NamespaceName == "Edm") {
                    switch (storeFunctionName.ToUpperInvariant()) {
                        default:
                            result.Append(storeFunctionName);
                            break;
                    }
                }
                else {
                    result.Append(storeFunctionName);
                }
            }
            else {
                result.Append(storeFunctionName);
            }
        }

        /// <summary>
        /// Default handling for functions with a given name.
        /// Translates them to functionName(arg1, arg2, ..., argn)
        /// </summary>
        private ISqlFragment HandleFunctionDefaultGivenName(VfpFunctionExpression e, string functionName) {
            SqlBuilder result = new SqlBuilder();
            result.Append(functionName);
            this.HandleFunctionArgumentsDefault(e, result);
            return result;
        }

        /// <summary>
        /// Default handling on function arguments
        /// Appends the list of arguments to the given result
        /// If the function is niladic it does not append anything,
        /// otherwise it appends (arg1, arg2, ..., argn)
        /// </summary>
        private void HandleFunctionArgumentsDefault(VfpFunctionExpression e, SqlBuilder result) {
            bool isNiladicFunction = e.Function.IsNiladic();

            if (isNiladicFunction && e.Arguments.Count > 0) {
                throw new InvalidOperationException("Niladic functions cannot have parameters");
            }

            if (!isNiladicFunction) {
                result.Append("(");
                string separator = string.Empty;
                foreach (VfpExpression arg in e.Arguments) {
                    result.Append(separator);
                    result.Append(arg.Accept(this));
                    separator = ", ";
                }

                result.Append(")");
            }
        }

        private ISqlFragment HandleSpecialBuiltInFunction(VfpFunctionExpression e) {
            return this.HandleSpecialFunction(builtInFunctionHandlers, e);
        }

        private ISqlFragment HandleSpecialCanonicalFunction(VfpFunctionExpression e) {
            return this.HandleSpecialFunction(canonicalFunctionHandlers, e);
        }

        private ISqlFragment HandleSpecialFunction(Dictionary<string, FunctionHandler> handlers, VfpFunctionExpression e) {
            if (!handlers.ContainsKey(e.Function.Name)) {
                throw new InvalidOperationException("Special handling should be called only for functions in the list of special functions");
            }

            return handlers[e.Function.Name](this, e);
        }

        /// <summary>
        /// Handles functions that are translated into TSQL operators.
        /// The given function should have one or two arguments. 
        /// Functions with one arguemnt are translated into 
        ///     op arg
        /// Functions with two arguments are translated into
        ///     arg0 op arg1
        /// Also, the arguments can be optionaly enclosed in parethesis
        /// </summary>
        /// <param name="parenthesiseArguments">Whether the arguments should be enclosed in parethesis</param>
        private ISqlFragment HandleSpecialFunctionToOperator(VfpFunctionExpression e, bool parenthesiseArguments) {
            SqlBuilder result = new SqlBuilder();
            Debug.Assert(e.Arguments.Count > 0 && e.Arguments.Count <= 2, "There should be 1 or 2 arguments for operator");

            if (e.Arguments.Count > 1) {
                if (parenthesiseArguments) {
                    result.Append("(");
                }

                result.Append(e.Arguments[0].Accept(this));
                if (parenthesiseArguments) {
                    result.Append(")");
                }
            }

            result.Append(" ");
            Debug.Assert(functionNameToOperatorDictionary.ContainsKey(e.Function.Name), "The function can not be mapped to an operator");
            result.Append(functionNameToOperatorDictionary[e.Function.Name]);
            result.Append(" ");

            if (parenthesiseArguments) {
                result.Append("(");
            }

            result.Append(e.Arguments[e.Arguments.Count - 1].Accept(this));
            if (parenthesiseArguments) {
                result.Append(")");
            }

            return result;
        }

        private static ISqlFragment HandleConcatFunction(SqlVisitor visitor, VfpFunctionExpression e) {
            return visitor.HandleSpecialFunctionToOperator(e, false);
        }

        private static ISqlFragment HandleCanonicalFunctionBitwise(SqlVisitor visitor, VfpFunctionExpression e) {
            return visitor.HandleSpecialFunctionToOperator(e, true);
        }

        private static ISqlFragment HandleCanonicalFunctionBitwiseAnd(SqlVisitor visitor, VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();
            // TODO: code contract for e.Arguments.Count > 1

            result.Append("BITAND(");
            result.Append(e.Arguments[0].Accept(visitor));

            for (int index = 1, total = e.Arguments.Count; index < total; index++) {
                result.Append(",");
                result.Append(e.Arguments[index].Accept(visitor));
            }

            result.Append(")");

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionBitwiseXor(SqlVisitor visitor, VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();
            // TODO: code contract for e.Arguments.Count > 1

            result.Append("BITXOR(");
            result.Append(e.Arguments[0].Accept(visitor));

            for (int index = 1, total = e.Arguments.Count; index < total; index++) {
                result.Append(",");
                result.Append(e.Arguments[index].Accept(visitor));
            }

            result.Append(")");

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionBitwiseOr(SqlVisitor visitor, VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();
            // TODO: code contract for e.Arguments.Count > 1

            result.Append("BITOR(");
            result.Append(e.Arguments[0].Accept(visitor));

            for (int index = 1, total = e.Arguments.Count; index < total; index++) {
                result.Append(",");
                result.Append(e.Arguments[index].Accept(visitor));
            }

            result.Append(")");

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionBitwiseNot(SqlVisitor visitor, VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();
            // TODO: code contract for e.Arguments.Count  = 1

            result.Append("BITNOT(");
            result.Append(e.Arguments[0].Accept(visitor));
            result.Append(")");

            return result;
        }

        /// <summary>
        /// Handles special case in which datapart 'type' parameter is present. all the functions
        /// handles here have *only* the 1st parameter as datepart. datepart value is passed along
        /// the QP as string and has to be expanded as TSQL keyword.
        /// </summary>
        private static ISqlFragment HandleDatepartDateFunction(SqlVisitor visitor, VfpFunctionExpression e) {
            Debug.Assert(e.Arguments.Count > 0, "e.Arguments.Count > 0");

            var constExpr = e.Arguments[0] as VfpConstantExpression;
            if (null == constExpr) {
                throw new InvalidOperationException(string.Format("DATEPART argument to function '{0}.{1}' must be a literal string", e.Function.NamespaceName, e.Function.Name));
            }

            string datepart = constExpr.Value as string;
            if (null == datepart) {
                throw new InvalidOperationException(string.Format("DATEPART argument to function '{0}.{1}' must be a literal string", e.Function.NamespaceName, e.Function.Name));
            }

            SqlBuilder result = new SqlBuilder();

            ////
            //// check if datepart value is valid
            ////
            //if (!_datepartKeywords.Contains(datepart)) {
            //    throw new InvalidOperationException(string.Format("{0}' is not a valid value for DATEPART argument in '{1}.{2}' function", datepart, e.Function.NamespaceName, e.Function.Name));
            //}

            // finaly, expand the function name
            visitor.WriteFunctionName(result, e.Function);
            result.Append("(");

            // expand the datepart literal as tsql kword
            result.Append(datepart);
            string separator = ", ";

            // expand remaining arguments
            for (int i = 1; i < e.Arguments.Count; i++) {
                result.Append(separator);
                result.Append(e.Arguments[i].Accept(visitor));
            }

            result.Append(")");

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionDatepart(SqlVisitor visitor, VfpFunctionExpression e) {
            return visitor.HandleCanonicalFunctionDatepart(e.Function.Name.ToLowerInvariant(), e);
        }

        private ISqlFragment HandleCanonicalFunctionDatepart(string datepart, VfpFunctionExpression e) {
            Debug.Assert(e.Arguments.Count == 1, "Canonical datepart functions should have exactly one argument");

            SqlBuilder result = new SqlBuilder();

            if (datepart == "dayofyear") {
                result.Append("INT(VAL(SYS(11, ");
                result.Append(e.Arguments[0].Accept(this));
                result.Append(")) - VAL(SYS(11, DATE(YEAR(");
                result.Append(e.Arguments[0].Accept(this));
                result.Append("), 1, 1))) + 1)");
            }
            else {
                switch (datepart) {
                    case "day":
                    case "month":
                    case "year":
                    case "hour":
                    case "minute":
                        result.Append(datepart);
                        break;
                    case "second":
                        result.Append("SEC");
                        break;
                    case "dayofweek":
                        result.Append("DOW");
                        break;
                    default:
                        throw new NotImplementedException(datepart);
                }

                result.Append("(");
                result.Append(e.Arguments[0].Accept(this));
                result.Append(")");
            }

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionCurrentDateTime(SqlVisitor visitor, VfpFunctionExpression e) {
            return visitor.HandleFunctionDefaultGivenName(e, "DATETIME");
        }

        private static ISqlFragment HandleCanonicalFunctionCreateDateTime(SqlVisitor visitor, VfpFunctionExpression e) {
            return visitor.HandleCanonicalFunctionDateTimeTypeCreation(e.Arguments);
        }

        private SqlBuilder ParanthesizeExpressionIfNeeded(VfpExpression e) {
            SqlBuilder result = new SqlBuilder();
            this.ParanthesizeExpressionIfNeeded(e, result);
            return result;
        }

        /// <summary>
        /// Dump out an expression - optionally wrap it with parantheses if possible
        /// </summary>
        private void ParanthesizeExpressionIfNeeded(VfpExpression e, SqlBuilder result) {
            if (e.IsComplexExpression()) {
                result.Append("(");
                result.Append(e.Accept(this));
                result.Append(")");
            }
            else {
                result.Append(e.Accept(this));
            }
        }

        private ISqlFragment HandleCanonicalFunctionDateTimeTypeCreation(IList<VfpExpression> args) {
            SqlBuilder result = new SqlBuilder();

            if (args.Count == 3) {
                result.Append("DATE(");
                result.Append(args[0].Accept(this));
                result.Append(",");
                result.Append(args[1].Accept(this));
                result.Append(",");
                result.Append(args[2].Accept(this));
                result.Append(")");
                return result;
            }
            else if (args.Count == 6) {
                result.Append("DATETIME(");
                result.Append(args[0].Accept(this));
                result.Append(",");
                result.Append(args[1].Accept(this));
                result.Append(",");
                result.Append(args[2].Accept(this));
                result.Append(",");
                result.Append(args[3].Accept(this));
                result.Append(",");
                result.Append(args[4].Accept(this));
                result.Append(",");
                result.Append(args[5].Accept(this));
                result.Append(")");
                return result;
            }

            throw new NotSupportedException();
        }

        private static ISqlFragment HandleCanonicalFunctionDateAdd(SqlVisitor visitor, VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();

            result.Append("(");

            switch (e.Function.Name.ToLower()) {
                case "addyears":
                    result.Append("CTOT(DTOC(GOMONTH(");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append(",12*");
                    result.Append(e.Arguments[1].Accept(visitor));
                    result.Append(")) + ' ' + TTOC(");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append(", 2))");
                    break;
                case "addmonths":
                    result.Append("CTOT(DTOC(GOMONTH(");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append(",");
                    result.Append(e.Arguments[1].Accept(visitor));
                    result.Append(")) + ' ' + TTOC(");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append(", 2))");
                    break;
                case "adddays":
                    // The field could be a Date or a DateTime field.  The "CTOT(TTOC(<<field>>))" will ensure that the adding is against a DateTime.
                    result.Append("CTOT(TTOC(");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append("))");
                    result.Append("+");
                    result.Append(e.Arguments[1].Accept(visitor));
                    result.Append("*60*60*24");
                    break;
                case "addhours":
                    // The field could be a Date or a DateTime field.  The "CTOT(TTOC(<<field>>))" will ensure that the adding is against a DateTime.
                    result.Append("CTOT(TTOC(");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append("))");
                    result.Append("+");
                    result.Append(e.Arguments[1].Accept(visitor));
                    result.Append("*60*60");
                    break;
                case "addminutes":
                    // The field could be a Date or a DateTime field.  The "CTOT(TTOC(<<field>>))" will ensure that the adding is against a DateTime.
                    result.Append("CTOT(TTOC(");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append("))");
                    result.Append("+");
                    result.Append(e.Arguments[1].Accept(visitor));
                    result.Append("*60");
                    break;
                case "addseconds":
                    // The field could be a Date or a DateTime field.  The "CTOT(TTOC(<<field>>))" will ensure that the adding is against a DateTime.
                    result.Append("CTOT(TTOC(");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append("))");
                    result.Append("+");
                    result.Append(e.Arguments[1].Accept(visitor));
                    break;
                default:
                    throw new NotImplementedException(e.Function.Name.ToLower());
            }

            result.Append(")");

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionDateDiff(SqlVisitor visitor, VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();

            result.Append("INT(");

            switch (e.Function.Name) {
                case "DiffYears":
                    result.Append("(YEAR(");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append(")-");
                    result.Append("YEAR(");
                    result.Append(e.Arguments[1].Accept(visitor));
                    result.Append("))");
                    break;
                case "DiffMonths":
                    result.Append("(YEAR(");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append(")*12)+MONTH(");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append("))-((YEAR(");
                    result.Append(e.Arguments[1].Accept(visitor));
                    result.Append(")*12)+MONTH(");
                    result.Append(e.Arguments[1].Accept(visitor));
                    result.Append(")");
                    break;
                case "DiffDays":
                    result.Append("((");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append("-");
                    result.Append(e.Arguments[1].Accept(visitor));
                    result.Append(")/60/60/24)");
                    break;
                case "DiffHours":
                    result.Append("((");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append("-");
                    result.Append(e.Arguments[1].Accept(visitor));
                    result.Append(")/60/60)");
                    break;
                case "DiffMinutes":
                    result.Append("((");
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append("-");
                    result.Append(e.Arguments[1].Accept(visitor));
                    result.Append(")/60)");
                    break;
                case "DiffSeconds":
                    result.Append(e.Arguments[0].Accept(visitor));
                    result.Append("-");
                    result.Append(e.Arguments[1].Accept(visitor));
                    break;
                case "":
                    break;
            }

            result.Append(")");

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionPower(SqlVisitor visitor, VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();

            result.Append(e.Arguments[0].Accept(visitor));
            result.Append("^");
            result.Append(e.Arguments[1].Accept(visitor));

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionSubstring(SqlVisitor visitor, VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();

            result.Append("SUBSTR(");
            result.Append(e.Arguments[0].Accept(visitor));
            result.Append(", ");
            result.Append(e.Arguments[1].Accept(visitor));

            if (e.Arguments.Count > 2) {
                result.Append(", ");
                result.Append(e.Arguments[2].Accept(visitor));
            }

            result.Append(")");

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionIndexOf(SqlVisitor visitor, VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();
            VfpFunctionExpression argument1 = e.Arguments[0] as VfpFunctionExpression;

            bool isReverse = argument1 != null && argument1.Function.Name == "Reverse";

            if (isReverse) {
                result.Append("len(");
                result.Append(e.Arguments[1].Accept(visitor));
                result.Append(")-1-");

                result.Append("RAT(");
            }
            else {
                result.Append("AT(");
            }

            result.Append(e.Arguments[0].Accept(visitor));
            result.Append(", ");
            result.Append(e.Arguments[1].Accept(visitor));
            result.Append(")");

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionLength(SqlVisitor visitor, VfpFunctionExpression e) {
            return visitor.HandleFunctionDefaultGivenName(e, "LEN");
        }

        private static ISqlFragment HandleCanonicalFunctionRound(SqlVisitor visitor, VfpFunctionExpression e) {
            return visitor.HandleCanonicalFunctionRound(e);
        }

        private ISqlFragment HandleCanonicalFunctionRound(VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();

            result.Append("ROUND(");

            Debug.Assert(e.Arguments.Count <= 2, "Round or truncate should have at most 2 arguments");
            result.Append(e.Arguments[0].Accept(this));
            result.Append(", ");

            if (e.Arguments.Count > 1) {
                result.Append(e.Arguments[1].Accept(this));
            }
            else {
                result.Append("0");
            }

            result.Append(")");

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionAbs(SqlVisitor visitor, VfpFunctionExpression e) {
            // Convert the call to Abs(Byte) to a no-op, since Byte is an unsigned type. 
            if (e.Arguments[0].ResultType.IsPrimitiveType(PrimitiveTypeKind.Byte)) {
                SqlBuilder result = new SqlBuilder();
                result.Append(e.Arguments[0].Accept(visitor));
                return result;
            }
            else {
                return visitor.HandleFunctionDefault(e);
            }
        }

        private static ISqlFragment HandleCanonicalFunctionTrim(SqlVisitor visitor, VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();

            result.Append("ALLTRIM(");

            Debug.Assert(e.Arguments.Count == 1, "Trim should have one argument");
            result.Append(e.Arguments[0].Accept(visitor));

            result.Append(")");

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionTruncateTime(SqlVisitor visitor, VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();

            result.Append("TTOD(");

            Debug.Assert(e.Arguments.Count == 1, "Trim should have one argument");
            result.Append(e.Arguments[0].Accept(visitor));

            result.Append(")");

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionReplace(SqlVisitor visitor, VfpFunctionExpression e) {
            return visitor.HandleFunctionDefaultGivenName(e, "STRTRAN");
        }

        private static ISqlFragment HandleCanonicalFunctionToLower(SqlVisitor visitor, VfpFunctionExpression e) {
            return visitor.HandleFunctionDefaultGivenName(e, "LOWER");
        }

        private static ISqlFragment HandleCanonicalFunctionToUpper(SqlVisitor visitor, VfpFunctionExpression e) {
            return visitor.HandleFunctionDefaultGivenName(e, "UPPER");
        }

        private void TranslateConstantParameterForLike(VfpExpression targetExpression, VfpConstantExpression constSearchParamExpression, SqlBuilder result, bool insertPercentStart, bool insertPercentEnd) {
            result.Append(targetExpression.Accept(this));
            result.Append(" LIKE ");

            // If it's a DbConstantExpression then escape the search parameter if necessary.
            //bool escapingOccurred;

            StringBuilder searchParamBuilder = new StringBuilder();
            if (insertPercentStart == true) {
                searchParamBuilder.Append("%");
            }

            //searchParamBuilder.Append(VfpProviderManifest.EscapeLikeText(constSearchParamExpression.Value as string, false, out escapingOccurred));
            searchParamBuilder.Append(constSearchParamExpression.Value as string);
            if (insertPercentEnd == true) {
                searchParamBuilder.Append("%");
            }

            result.Append(this.VisitConstantExpression(constSearchParamExpression.ResultType, searchParamBuilder.ToString()));

            //// If escaping did occur (special characters were found), then append the escape character used.
            //if (escapingOccurred)
            //    result.Append(" ESCAPE '" + VfpProviderManifest.LikeEscapeChar + "'");
        }

        protected SqlBuilder VisitBinaryExpression(VfpExpressionKind kind, VfpExpression left, VfpExpression right) {
            //BinaryFragment.EnsurePropertyExpressionIsOnLeft(ref kind, ref left, ref right);        

            SqlBuilder result = new SqlBuilder();

            if (this.isProcessingFilter && this.HandleIndexOfWithStartsWithOrEndsWith(result, left, right)) {
                if (kind == VfpExpressionKind.NotEquals) {
                    var notResult = new SqlBuilder();

                    notResult.Append("!(");
                    notResult.Append(result);
                    notResult.Append(")");

                    return notResult;
                }

                return result;
            }

            BinaryFragment binaryFragment = new BinaryFragment(kind,
                                                                this.ParanthesizeExpressionIfNeeded(left),
                                                                this.ParanthesizeExpressionIfNeeded(right));

            result.Append(binaryFragment);
            return result;
        }

        /// <summary>
        /// EntityFramework seems to interpret StartsWith and EndWith commands as an IndexOf VfpFunctionExpression.  This is not ideal as it would
        /// result in a sql statement that includes AT() or RAT() instead of a Like statement.
        /// </summary>
        // TODO:  might need to add code to make sure that this is only executed as part of a FilterExpression
        protected bool HandleIndexOfWithStartsWithOrEndsWith(SqlBuilder result, VfpExpression left, VfpExpression right) {
            var function = left as VfpFunctionExpression;
            var constant = right as VfpConstantExpression;

            if (function != null && constant != null && function.Function.Name == "IndexOf" && constant.Value.ToString() == "1") {
                if (function.Arguments.Count > 0) {
                    var leftFunction = function.Arguments[0] as VfpFunctionExpression;
                    var rightFunction = function.Arguments[1] as VfpFunctionExpression;

                    if (leftFunction != null && leftFunction.Function.Name == "Reverse" && rightFunction != null && rightFunction.Function.Name == "Reverse") {
                        var arguments = new List<VfpExpression>();
                        arguments.AddRange(leftFunction.Arguments);
                        arguments.AddRange(rightFunction.Arguments);

                        HandleCanonicalFunctionEndsWith(this, arguments, result);
                        return true;
                    }

                    HandleCanonicalFunctionStartsWith(this, function.Arguments, result);
                    return true;
                }
            }

            return false;
        }

        protected ISqlFragment VisitConstantExpression(TypeUsage expressionType, object expressionValue) {
            SqlBuilder result = new SqlBuilder();

            PrimitiveTypeKind typeKind;
            // Model Types can be (at the time of this implementation):
            //      Binary, Boolean, Byte, DateTime, Decimal, Double, Int16, Int32, Int64,Single, String
            if (expressionType.TryGetPrimitiveTypeKind(out typeKind)) {
                switch (typeKind) {
                    case PrimitiveTypeKind.Int32:
                        // default sql server type for integral values.
                        result.Append(expressionValue.ToString());
                        break;

                    //case PrimitiveTypeKind.Binary:
                    //    result.Append(" 0x");
                    //    result.Append(ByteArrayToBinaryString((Byte[])expressionValue));
                    //    result.Append(" ");
                    //    break;

                    case PrimitiveTypeKind.Boolean:
                        result.Append((bool)expressionValue ? ".T." : ".F.");
                        break;

                    case PrimitiveTypeKind.Byte:
                        result.Append("cast(");
                        result.Append(expressionValue.ToString());
                        result.Append(" as i)");
                        break;

                    case PrimitiveTypeKind.DateTime:
                        var dateTime = (System.DateTime)expressionValue;

                        result.Append("DATETIME(");
                        result.Append(dateTime.Year.ToString());
                        result.Append(",");
                        result.Append(dateTime.Month.ToString());
                        result.Append(",");
                        result.Append(dateTime.Day.ToString());
                        result.Append(",");
                        result.Append(dateTime.Hour.ToString());
                        result.Append(",");
                        result.Append(dateTime.Minute.ToString());
                        result.Append(",");
                        result.Append(dateTime.Second.ToString());
                        result.Append(")");
                        return result;

                    //case PrimitiveTypeKind.Time:
                    //    result.Append("convert(");
                    //    result.Append(expressionType.EdmType.Name);
                    //    result.Append(", ");
                    //    result.Append(EscapeSingleQuote(expressionValue.ToString()));
                    //    result.Append(", 121)");
                    //    break;

                    case PrimitiveTypeKind.Decimal:
                        string strDecimal = ((decimal)expressionValue).ToString(CultureInfo.InvariantCulture);
                        // if the decimal value has no decimal part, cast as decimal to preserve type
                        // if the number has precision > int64 max precision, it will be handled as decimal by sql server
                        // and does not need cast. if precision is lest then 20, then cast using Max(literal precision, sql default precision)
                        if (-1 == strDecimal.IndexOf('.') && (strDecimal.TrimStart(new char[] { '-' }).Length < 20)) {
                            byte precision = (byte)strDecimal.Length;
                            FacetDescription precisionFacetDescription;
                            Debug.Assert(expressionType.EdmType.TryGetTypeFacetDescriptionByName("precision", out precisionFacetDescription), "Decimal primitive type must have Precision facet");
                            if (expressionType.EdmType.TryGetTypeFacetDescriptionByName("precision", out precisionFacetDescription)) {
                                if (precisionFacetDescription.DefaultValue != null) {
                                    precision = Math.Max(precision, (byte)precisionFacetDescription.DefaultValue);
                                }
                            }

                            Debug.Assert(precision > 0, "Precision must be greater than zero");
                            result.Append("cast(");
                            result.Append(strDecimal);
                            result.Append(" as n(");
                            result.Append(precision.ToString(CultureInfo.InvariantCulture));
                            result.Append("))");
                        }
                        else {
                            result.Append(strDecimal);
                        }

                        break;

                    case PrimitiveTypeKind.Double:
                        string doubleStr = ((double)expressionValue).ToString(CultureInfo.InvariantCulture);

                        if (!doubleStr.Contains(".")) {
                            doubleStr += ".0"; // to prevent divide by integer issues
                        }

                        result.Append(doubleStr);

                        break;

                    //case PrimitiveTypeKind.Guid:
                    //    result.Append("cast(");
                    //    result.Append(EscapeSingleQuote(expressionValue.ToString(), false /* IsUnicode */));
                    //    result.Append(" as uniqueidentifier)");
                    //    break;

                    case PrimitiveTypeKind.Int16:
                        //result.Append("cast(");
                        result.Append(expressionValue.ToString());
                        //result.Append(" as smallint)");
                        break;

                    case PrimitiveTypeKind.Int64:
                        //result.Append("cast(");
                        result.Append(expressionValue.ToString());
                        //result.Append(" as bigint)");
                        break;

                    case PrimitiveTypeKind.Single:
                        //result.Append("cast(");
                        result.Append(((float)expressionValue).ToString(CultureInfo.InvariantCulture));
                        //result.Append(" as real)");
                        break;

                    case PrimitiveTypeKind.String:
                        result.Append(EscapeSingleQuote(expressionValue as string));
                        break;

                    default:
                        throw new NotSupportedException("Primitive type kind " + typeKind + " is not supported.");
                }
            }
            else {
                throw new NotSupportedException();
            }

            return result;
        }

        //static string ByteArrayToBinaryString(Byte[] binaryArray) {
        //    StringBuilder sb = new StringBuilder(binaryArray.Length * 2);
        //    for (int i = 0; i < binaryArray.Length; i++) {
        //        sb.Append(hexDigits[(binaryArray[i] & 0xF0) >> 4]).Append(hexDigits[binaryArray[i] & 0x0F]);
        //    }
        //    return sb.ToString();
        //}

        /// <summary>
        /// Before we embed a string literal in a SQL string, we should
        /// convert all ' to '', and enclose the whole string in single quotes.
        /// </summary>
        /// <returns>The escaped sql string.</returns>
        protected static string EscapeSingleQuote(string s) {
            return "'" + s.Replace("'", "' + chr(39) + '") + "'";
        }

        /// <summary>
        /// Handler for Contains. Wraps the normal translation with a case statement
        /// </summary>
        private static ISqlFragment HandleCanonicalFunctionContains(SqlVisitor visitor, VfpFunctionExpression e) {
            return visitor.WrapPredicate(HandleCanonicalFunctionContains, e);
        }

        /// <summary>
        /// CONTAINS(arg0, arg1) => arg0 LIKE '%arg1%'
        /// </summary>
        private static SqlBuilder HandleCanonicalFunctionContains(SqlVisitor visitor, IList<VfpExpression> args, SqlBuilder result) {
            Debug.Assert(args.Count == 2, "Contains should have two arguments");
            // Check if args[1] is a DbConstantExpression
            var constSearchParamExpression = args[1] as VfpConstantExpression;
            if ((constSearchParamExpression != null) && (string.IsNullOrEmpty(constSearchParamExpression.Value as string) == false)) {
                visitor.TranslateConstantParameterForLike(args[0], constSearchParamExpression, result, true, true);
            }
            else {
                // We use CHARINDEX when the search param is a VfpNullExpression because all of SQL Server 2008, 2005 and 2000
                // consistently return NULL as the result.
                //  However, if instead we use the optimized LIKE translation when the search param is a VfpNullExpression,
                //  only SQL Server 2005 yields a True instead of a DbNull as compared to SQL Server 2008 and 2000.
                result.Append("CHARINDEX( ");
                result.Append(args[1].Accept(visitor));
                result.Append(", ");
                result.Append(args[0].Accept(visitor));
                result.Append(") > 0");
            }

            return result;
        }

        private static ISqlFragment HandleCanonicalFunctionStartsWith(SqlVisitor visitor, VfpFunctionExpression e) {
            return visitor.WrapPredicate(HandleCanonicalFunctionStartsWith, e);
        }

        /// <summary>
        /// STARTSWITH(arg0, arg1) => arg0 LIKE 'arg1%'
        /// </summary>
        protected static SqlBuilder HandleCanonicalFunctionStartsWith(SqlVisitor visitor, IList<VfpExpression> args, SqlBuilder result) {
            Debug.Assert(args.Count == 2, "StartsWith should have two arguments");

            var property = args[0];
            var pattern = args[1];

            if (PropertyGatherer.Gather(pattern).Any()) {
                property = args[1];
                pattern = args[0];
            }

            var constSearchParamExpression = pattern as VfpConstantExpression;

            if ((constSearchParamExpression != null) && (string.IsNullOrEmpty(constSearchParamExpression.Value as string) == false)) {
                visitor.TranslateConstantParameterForLike(property, constSearchParamExpression, result, false, true);
            }
            else {
                result.Append(property.Accept(visitor));
                result.Append(" LIKE ");
                result.Append(pattern.Accept(visitor));
                result.Append(" + '%'  ");
            }

            return result;
        }

        /// <summary>
        /// Handler for EndsWith. Wraps the normal translation with a case statement
        /// </summary>
        private static ISqlFragment HandleCanonicalFunctionEndsWith(SqlVisitor visitor, VfpFunctionExpression e) {
            return visitor.WrapPredicate(HandleCanonicalFunctionEndsWith, e);
        }

        /// <summary>
        /// ENDSWITH(arg0, arg1) => arg0 LIKE '%arg1'
        /// </summary>
        private static SqlBuilder HandleCanonicalFunctionEndsWith(SqlVisitor visitor, IList<VfpExpression> args, SqlBuilder result) {
            Debug.Assert(args.Count == 2, "EndsWith should have two arguments");

            var property = args[0];
            var pattern = args[1];

            if (PropertyGatherer.Gather(pattern).Any()) {
                property = args[1];
                pattern = args[0];
            }

            var constSearchParamExpression = pattern as VfpConstantExpression;

            // Check if args[1] is a DbConstantExpression and if args [0] is a VfpPropertyExpression
            if ((constSearchParamExpression != null) && (string.IsNullOrEmpty(constSearchParamExpression.Value as string) == false)) {
                visitor.TranslateConstantParameterForLike(property, constSearchParamExpression, result, true, false);
            }
            else {
                result.Append(property.Accept(visitor));
                result.Append(" LIKE '%' + ");
                result.Append(pattern.Accept(visitor));
            }

            return result;
        }

        /// <summary>
        /// Turns a predicate into a statement returning a bit
        /// PREDICATE => CASE WHEN (PREDICATE) THEN CAST(1 AS BIT) WHEN (NOT (PREDICATE)) CAST (O AS BIT) END
        /// The predicate is produced by the given predicateTranslator.
        /// </summary>
        private ISqlFragment WrapPredicate(Func<SqlVisitor, IList<VfpExpression>, SqlBuilder, SqlBuilder> predicateTranslator, VfpFunctionExpression e) {
            SqlBuilder result = new SqlBuilder();
            result.Append("CASE WHEN (");
            predicateTranslator(this, e.Arguments, result);
            result.Append(") THEN cast(1 as bit) WHEN ( NOT (");
            predicateTranslator(this, e.Arguments, result);
            result.Append(")) THEN cast(0 as bit) END");
            return result;
        }
        #endregion

        /// <summary>
        /// Simply calls <see cref="VisitExpressionEnsureSqlStatement(VfpExpression, bool)"/>
        /// with addDefaultColumns set to true
        /// </summary>
        private SqlSelectStatement VisitExpressionEnsureSqlStatement(VfpExpression e) {
            return this.VisitExpressionEnsureSqlStatement(e, true);
        }

        private bool isProcessingFilter = false;

        private SqlSelectStatement VisitExpressionEnsureSqlStatement(VfpExpression e, bool addDefaultColumns) {
            Debug.Assert(e.ResultType.IsCollectionType());

            SqlSelectStatement result;
            switch (e.ExpressionKind) {
                case VfpExpressionKind.Filter:
                    result = e.Accept(this) as SqlSelectStatement;
                    break;

                case VfpExpressionKind.Project:
                case VfpExpressionKind.GroupBy:
                case VfpExpressionKind.Sort:
                    result = e.Accept(this) as SqlSelectStatement;
                    break;

                default:
                    Symbol fromSymbol;
                    string inputVarName = "c";  // any name will do - this is my random choice.
                    this.symbolTable.EnterScope();

                    TypeUsage type = null;
                    switch (e.ExpressionKind) {
                        case VfpExpressionKind.Scan:
                        case VfpExpressionKind.CrossJoin:
                        case VfpExpressionKind.FullOuterJoin:
                        case VfpExpressionKind.InnerJoin:
                        case VfpExpressionKind.LeftOuterJoin:
                        case VfpExpressionKind.CrossApply:
                        case VfpExpressionKind.OuterApply:
                            type = e.ResultType.ToElementTypeUsage();
                            break;

                        default:
                            Debug.Assert(e.ResultType.IsCollectionType());
                            type = e.ResultType.GetEdmType<CollectionType>().TypeUsage;
                            break;
                    }

                    result = this.VisitInputExpression(e, inputVarName, type, out fromSymbol);
                    this.AddFromSymbol(result, inputVarName, fromSymbol);
                    this.symbolTable.ExitScope();
                    break;
            }

            if (addDefaultColumns && result.Select.IsEmpty) {
                this.AddDefaultColumns(result);
            }

            return result;
        }

        private readonly SymbolTable symbolTable = new SymbolTable();

        #region Visitor parameter stacks
        /// <summary>
        /// Every relational node has to pass its SELECT statement to its children
        /// This allows them (VfpVariableReferenceExpression eventually) to update the list of
        /// outer extents (free variables) used by this select statement.
        /// </summary>
        private Stack<SqlSelectStatement> selectStatementStack = new Stack<SqlSelectStatement>();

        /// <summary>
        /// The top of the stack
        /// </summary>
        private SqlSelectStatement CurrentSelectStatement {
            // There is always something on the stack, so we can always Peek.
            get { return this.selectStatementStack.Peek(); }
        }

        /// <summary>
        /// Nested joins and extents need to know whether they should create
        /// a new Select statement, or reuse the parent's.  This flag
        /// indicates whether the parent is a join or not.
        /// </summary>
        private Stack<bool> isParentAJoinStack = new Stack<bool>();

        /// <summary>
        /// The top of the stack
        /// </summary>
        private bool IsParentAJoin {
            // There might be no entry on the stack if a Join node has never
            // been seen, so we return false in that case.
            get { return this.isParentAJoinStack.Count == 0 ? false : this.isParentAJoinStack.Peek(); }
        }

        #endregion
        
        public ISqlFragment Visit(VfpProviderManifest vfpManifest, System.Data.Entity.Core.Common.CommandTrees.DbQueryCommandTree commandTree, out List<DbParameter> parameters) {
            var queryCommandTree = GetCommandTreeExpression<VfpQueryCommandTree>(vfpManifest, commandTree);

            parameters = GetParameters(queryCommandTree.Query);

            ISqlFragment result;
            if (queryCommandTree.Query.ResultType.IsCollectionType()) {
                var sqlStatement = this.VisitExpressionEnsureSqlStatement(queryCommandTree.Query);
                Debug.Assert(sqlStatement != null, "The outer most sql statment is null");
                sqlStatement.IsTopMost = true;
                result = sqlStatement;
            }
            else {
                SqlBuilder sqlBuilder = new SqlBuilder();
                sqlBuilder.Append("SELECT ");
                sqlBuilder.Append(queryCommandTree.Query.Accept(this));

                result = sqlBuilder;
            }

            if (this.isVarRefSingle) {
                throw new NotSupportedException();
                // A VfpVariableReferenceExpression has to be a child of VfpPropertyExpression or MethodExpression
            }

            // Check that the parameter stacks are not leaking.

            Debug.Assert(this.selectStatementStack.Count == 0);
            Debug.Assert(this.isParentAJoinStack.Count == 0);

            return result;
        }

        private ReadOnlyCollection<VfpPropertyExpression> _properties;

        protected T GetCommandTreeExpression<T>(VfpProviderManifest vfpManifest, System.Data.Entity.Core.Common.CommandTrees.DbCommandTree commandTree) where T : VfpCommandTree {
            var visitor = new ExpressionConverterVisitor();
            var queryCommandTree = visitor.Visit(commandTree);
            var vfpCommandTree = (T)ExpressionRewritter.Rewrite(vfpManifest, queryCommandTree);

            _properties = PropertyGatherer.Gather(vfpCommandTree);

            return vfpCommandTree;
        }

        

        protected List<DbParameter> GetParameters(VfpExpression expression) {
            var parameterExpressions = ParameterGatherer.Gather(expression);

            if (parameterExpressions.Any()) {
                var parameterHelper = new VfpParameterHelper();

                return parameterExpressions.Select(x => parameterHelper.CreateVfpParameter(x.Name, x.ResultType, ParameterMode.In, x.Value.Value)).Cast<DbParameter>().ToList();
            }

            return new List<DbParameter>();
        }

        private SqlSelectStatement VisitFilterExpression(VfpExpressionBinding input, VfpExpression predicate, bool negatePredicate) {
            Symbol fromSymbol;
            SqlSelectStatement result = this.VisitInputExpression(input.Expression, input.VariableName, input.VariableType, out fromSymbol);

            // Filter is compatible with OrderBy
            // but not with Project, another Filter or GroupBy
            if (!IsCompatible(result, VfpExpressionKind.Filter)) {
                result = this.CreateNewSelectStatement(result, input.VariableName, input.VariableType, out fromSymbol);
            }

            this.selectStatementStack.Push(result);
            this.symbolTable.EnterScope();

            this.AddFromSymbol(result, input.VariableName, fromSymbol);

            this.isProcessingFilter = true;

            if (negatePredicate) {
                result.Where.Append("NOT (");
            }

            result.Where.Append(predicate.Accept(this));
            if (negatePredicate) {
                result.Where.Append(")");
            }

            this.isProcessingFilter = false;

            this.symbolTable.ExitScope();
            this.selectStatementStack.Pop();

            return result;
        }

        internal static string GetTargetSql(EntitySetBase entitySetBase) {
            // construct escaped T-SQL referencing entity set
            StringBuilder builder = new StringBuilder(50);
            string definingQuery = entitySetBase.TryGetValueForMetadataProperty<string>("DefiningQuery");
            if (!string.IsNullOrEmpty(definingQuery)) {
                builder.Append("(");
                builder.Append(definingQuery);
                builder.Append(")");
            }
            else {
                string tableName = entitySetBase.TryGetValueForMetadataProperty<string>("Table");
                if (!string.IsNullOrEmpty(tableName)) {
                    builder.Append(tableName);
                }
                else {
                    builder.Append(entitySetBase.Name);
                }
            }

            return builder.ToString();
        }

        private ISqlFragment VisitNewInstanceExpression(VfpNewInstanceExpression e) {
            SqlBuilder result = new SqlBuilder();
            RowType rowType = e.ResultType.EdmType as RowType;

            if (null != rowType) {
                ReadOnlyMetadataCollection<EdmProperty> members = rowType.Properties;
                string separator = string.Empty;
                for (int i = 0; i < e.Arguments.Count; ++i) {
                    VfpExpression argument = e.Arguments[i];
                    if (argument.ResultType.IsRowType()) {
                        // We do not support nested records or other complex objects.
                        throw new NotSupportedException();
                    }

                    EdmProperty member = members[i];
                    result.Append(separator);
                    result.AppendLine();

                    var columnFragment = argument.Accept(this);
                    var columnName = columnFragment.ToString(this);

                    result.Append(columnName);

                    // prevent writing a column alias if the property name and member name are the same (for less command text)
                    VfpPropertyExpression VfpPropertyExpression = argument as VfpPropertyExpression;

                    if (!columnName.EndsWith("." + member.Name) || VfpPropertyExpression == null || VfpPropertyExpression.Property.Name != member.Name) {
                        result.Append(" AS ");
                        result.Append(member.Name);
                    }

                    separator = ", ";
                }
            }
            else {
                // Types other then RowType (such as UDTs for instance) are not supported.
                throw new NotSupportedException();
            }

            return result;
        }

        private SqlSelectStatement CreateNewSelectStatement(SqlSelectStatement oldStatement,
                                                            string inputVarName,
                                                            TypeUsage inputVarType,
                                                            out Symbol fromSymbol) {
            return this.CreateNewSelectStatement(oldStatement, inputVarName, inputVarType, true, out fromSymbol);
        }

        private SqlSelectStatement CreateNewSelectStatement(SqlSelectStatement oldStatement,
                                                            string inputVarName,
                                                            TypeUsage inputVarType,
                                                            bool finalizeOldStatement,
                                                            out Symbol fromSymbol) {
            fromSymbol = null;

            // Finalize the old statement
            if (finalizeOldStatement && oldStatement.Select.IsEmpty) {
                List<Symbol> columns = this.AddDefaultColumns(oldStatement);

                // Thid could not have been called from a join node.
                Debug.Assert(oldStatement.FromExtents.Count == 1);

                // if the oldStatement has a join as its input, ...
                // clone the join symbol, so that we "reuse" the
                // join symbol.  Normally, we create a new symbol - see the next block
                // of code.
                JoinSymbol oldJoinSymbol = oldStatement.FromExtents[0] as JoinSymbol;
                if (oldJoinSymbol != null) {
                    // Note: oldStatement.FromExtents will not do, since it might
                    // just be an alias of joinSymbol, and we want an actual JoinSymbol.
                    JoinSymbol newJoinSymbol = new JoinSymbol(inputVarName, inputVarType, oldJoinSymbol.ExtentList);
                    // This indicates that the oldStatement is a blocking scope
                    // i.e. it hides/renames extent columns
                    newJoinSymbol.IsNestedJoin = true;
                    newJoinSymbol.ColumnList = columns;
                    newJoinSymbol.FlattenedExtentList = oldJoinSymbol.FlattenedExtentList;

                    fromSymbol = newJoinSymbol;
                }
            }

            if (fromSymbol == null) {
                // This is just a simple extent/SqlSelectStatement,
                // and we can get the column list from the type.
                fromSymbol = new Symbol(inputVarName, inputVarType);
            }

            // Observe that the following looks like the body of Visit(ExtentExpression).
            SqlSelectStatement selectStatement = new SqlSelectStatement();
            selectStatement.From.Append("( ");
            selectStatement.From.Append(oldStatement);
            selectStatement.From.AppendLine();
            selectStatement.From.Append(") ");

            return selectStatement;
        }

        private static bool IsCompatible(SqlSelectStatement result, VfpExpressionKind expressionKind) {
            switch (expressionKind) {
                case VfpExpressionKind.Distinct:
                    return result.Top == null
                        // The projection after distinct may not project all 
                        // columns used in the Order By
                        && result.OrderBy.IsEmpty;

                case VfpExpressionKind.Filter:
                    return result.Select.IsEmpty
                            && result.Where.IsEmpty
                            && result.GroupBy.IsEmpty
                            && result.Top == null;

                case VfpExpressionKind.GroupBy:
                    return result.Select.IsEmpty
                            && result.GroupBy.IsEmpty
                            && result.OrderBy.IsEmpty
                            && result.Top == null;

                case VfpExpressionKind.Limit:
                case VfpExpressionKind.Element:
                    return result.Top == null;

                case VfpExpressionKind.Project:
                    return result.Select.IsEmpty
                            && result.GroupBy.IsEmpty
                            && !result.IsDistinct;

                case VfpExpressionKind.Skip:
                    return result.Select.IsEmpty
                            && result.GroupBy.IsEmpty
                            && result.OrderBy.IsEmpty
                            && !result.IsDistinct;

                case VfpExpressionKind.Sort:
                    return result.Select.IsEmpty
                            && result.GroupBy.IsEmpty
                            && result.OrderBy.IsEmpty
                            && !result.IsDistinct;

                default:
                    Debug.Assert(false);
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// This is called by the relational nodes.  It does the following
        /// <list>
        /// <item>If the input is not a SqlSelectStatement, it assumes that the input
        /// is a collection expression, and creates a new SqlSelectStatement </item>
        /// </list>
        /// </summary>
        /// <returns>A <see cref="SqlSelectStatement"/> and the main fromSymbol for this select statement.</returns>
        private SqlSelectStatement VisitInputExpression(VfpExpression inputExpression, string inputVarName, TypeUsage inputVarType, out Symbol fromSymbol) {
            SqlSelectStatement result;
            ISqlFragment sqlFragment = inputExpression.Accept(this);
            result = sqlFragment as SqlSelectStatement;

            if (result == null) {
                result = new SqlSelectStatement();
                WrapNonQueryExtent(result, sqlFragment, inputExpression.ExpressionKind);
            }

            if (result.FromExtents.Count == 0) {
                // input was an extent
                fromSymbol = new Symbol(inputVarName, inputVarType);
            }
            else if (result.FromExtents.Count == 1) {
                // input was Filter/GroupBy/Project/OrderBy
                // we are likely to reuse this statement.
                fromSymbol = result.FromExtents[0];
            }
            else {
                // input was a join.
                // we are reusing the select statement produced by a Join node
                // we need to remove the original extents, and replace them with a
                // new extent with just the Join symbol.
                JoinSymbol joinSymbol = new JoinSymbol(inputVarName, inputVarType, result.FromExtents);
                joinSymbol.FlattenedExtentList = result.AllJoinExtents;

                fromSymbol = joinSymbol;
                result.FromExtents.Clear();
                result.FromExtents.Add(fromSymbol);
            }

            return result;
        }

        /// <summary>
        /// If the sql fragment for an input expression is not a SqlSelect statement
        /// or other acceptable form (e.g. an extent as a SqlBuilder), we need
        /// to wrap it in a form acceptable in a FROM clause.  These are
        /// primarily the
        /// <list type="bullet">
        /// <item>The set operation expressions - union all, intersect, except</item>
        /// <item>TVFs, which are conceptually similar to tables</item>
        /// </list>
        /// </summary>
        private static void WrapNonQueryExtent(SqlSelectStatement result, ISqlFragment sqlFragment, VfpExpressionKind expressionKind) {
            switch (expressionKind) {
                case VfpExpressionKind.Function:
                    // TVF
                    result.From.Append(sqlFragment);
                    break;

                default:
                    result.From.Append(" (");
                    result.From.Append(sqlFragment);
                    result.From.Append(")");
                    break;
            }
        }

        private void AddFromSymbol(SqlSelectStatement selectStatement, string inputVarName, Symbol fromSymbol) {
            this.AddFromSymbol(selectStatement, inputVarName, fromSymbol, true);
        }

        /// <summary>
        /// <para>This method is called after the input to a relational node is visited.
        /// <see cref="Visit(VfpProjectExpression)"/> and <see cref="ProcessJoinInputResult"/>
        /// There are 2 scenarios
        /// <list type="number">
        /// <item>The fromSymbol is new i.e. the select statement has just been
        /// created, or a join extent has been added.</item>
        /// <item>The fromSymbol is old i.e. we are reusing a select statement.</item>
        /// </list>
        /// </para>
        /// If we are not reusing the select statement, we have to complete the FROM clause with the alias
        /// <code>
        /// -- if the input was an extent
        /// FROM = [SchemaName].[TableName]
        /// -- if the input was a Project
        /// FROM = (SELECT ... FROM ... WHERE ...)
        /// </code>
        /// These become
        /// <code>
        /// -- if the input was an extent
        /// FROM = [SchemaName].[TableName] AS alias
        /// -- if the input was a Project
        /// FROM = (SELECT ... FROM ... WHERE ...) AS alias
        /// </code>
        /// and look like valid FROM clauses.
        /// <para>Finally, we have to add the alias to the global list of aliases used,
        /// and also to the current symbol table.</para>
        /// </summary>
        /// <param name="inputVarName">The alias to be used.</param>
        private void AddFromSymbol(SqlSelectStatement selectStatement, string inputVarName, Symbol fromSymbol, bool addToSymbolTable) {
            // the first check is true if this is a new statement
            // the second check is true if we are in a join - we do not
            // check if we are in a join context.
            // We do not want to add "AS alias" if it has been done already
            // e.g. when we are reusing the Sql statement.
            if (selectStatement.FromExtents.Count == 0 || fromSymbol != selectStatement.FromExtents[0]) {
                selectStatement.FromExtents.Add(fromSymbol);
                selectStatement.From.Append(" ");
                selectStatement.From.Append(fromSymbol);

                // We have this inside the if statement, since
                // we only want to add extents that are actually used.
                this.AllExtentNames[fromSymbol.Name] = 0;
            }

            if (addToSymbolTable) {
                this.symbolTable.Add(inputVarName, fromSymbol);
            }
        }

        /// <summary>
        /// <para>Expands Select * to "select the_list_of_columns"
        /// If the columns are taken from an extent, they are written as
        /// {original_column_name AS Symbol(original_column)} to allow renaming.</para>
        /// <para>If the columns are taken from a Join, they are written as just
        /// {original_column_name}, since there cannot be a name collision.</para>
        /// <para>We concatenate the columns from each of the inputs to the select statement.
        /// Since the inputs may be joins that are flattened, we need to recurse.
        /// The inputs are inferred from the symbols in FromExtents.</para>
        /// </summary>
        private List<Symbol> AddDefaultColumns(SqlSelectStatement selectStatement) {
            // This is the list of columns added in this select statement
            // This forms the "type" of the Select statement, if it has to
            // be expanded in another SELECT *
            List<Symbol> columnList = new List<Symbol>();

            // A lookup for the previous set of columns to aid column name
            // collision detection.
            Dictionary<string, Symbol> columnDictionary = new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase);

            string separator = string.Empty;
            // The Select should usually be empty before we are called,
            // but we do not mind if it is not.
            if (!selectStatement.Select.IsEmpty) {
                separator = ", ";
            }

            foreach (Symbol symbol in selectStatement.FromExtents) {
                this.AddColumns(selectStatement, symbol, columnList, columnDictionary, ref separator);
            }

            return columnList;
        }

        /// <summary>
        /// <see cref="AddDefaultColumns"/>
        /// <para>Add the column names from the referenced extent/join to the select statement.</para>
        /// <para>If the symbol is a JoinSymbol, we recursively visit all the extents,
        /// halting at real extents and JoinSymbols that have an associated SqlSelectStatement.</para>
        /// <para>The column names for a real extent can be derived from its type.
        /// The column names for a Join Select statement can be got from the
        /// list of columns that was created when the Join's select statement
        /// was created.</para>
        /// <para>
        /// We do the following for each column.
        /// <list type="number">
        /// <item>Add the SQL string for each column to the SELECT clause</item>
        /// <item>Add the column to the list of columns - so that it can
        /// become part of the "type" of a JoinSymbol</item>
        /// <item>Check if the column name collides with a previous column added
        /// to the same select statement.  Flag both the columns for renaming if true.</item>
        /// <item>Add the column to a name lookup dictionary for collision detection.</item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="selectStatement">The select statement that started off as SELECT *</param>
        /// <param name="symbol">The symbol containing the type information for
        /// the columns to be added.</param>
        /// <param name="columnList">Columns that have been added to the Select statement.
        /// This is created in <see cref="AddDefaultColumns"/>.</param>
        /// <param name="columnDictionary">A dictionary of the columns above.</param>
        /// <param name="separator">Comma or nothing, depending on whether the SELECT
        /// clause is empty.</param>
        private void AddColumns(SqlSelectStatement selectStatement, Symbol symbol, List<Symbol> columnList, Dictionary<string, Symbol> columnDictionary, ref string separator) {
            JoinSymbol joinSymbol = symbol as JoinSymbol;
            if (joinSymbol != null) {
                if (!joinSymbol.IsNestedJoin) {
                    // Recurse if the join symbol is a collection of flattened extents
                    foreach (Symbol sym in joinSymbol.ExtentList) {
                        // if sym is ScalarType means we are at base case in the
                        // recursion and there are not columns to add, just skip
                        if (sym.Type.IsPrimitiveType()) {
                            continue;
                        }

                        this.AddColumns(selectStatement, sym, columnList, columnDictionary, ref separator);
                    }
                }
                else {
                    foreach (Symbol joinColumn in joinSymbol.ColumnList) {
                        // we write tableName.columnName
                        // rather than tableName.columnName as alias
                        // since the column name is unique (by the way we generate new column names)
                        //
                        // We use the symbols for both the table and the column,
                        // since they are subject to renaming.
                        selectStatement.Select.Append(separator);
                        selectStatement.Select.Append(symbol);
                        selectStatement.Select.Append(".");
                        selectStatement.Select.Append(joinColumn);

                        // check for name collisions.  If there is,
                        // flag both the colliding symbols.
                        if (columnDictionary.ContainsKey(joinColumn.Name)) {
                            columnDictionary[joinColumn.Name].NeedsRenaming = true; // the original symbol
                            joinColumn.NeedsRenaming = true; // the current symbol.
                        }
                        else {
                            columnDictionary[joinColumn.Name] = joinColumn;
                        }

                        columnList.Add(joinColumn);

                        separator = ", ";
                    }
                }
            }
            else {
                // This is a non-join extent/select statement, and the CQT type has
                // the relevant column information.

                // The type could be a record type(e.g. Project(...),
                // or an entity type ( e.g. EntityExpression(...)
                // so, we check whether it is a structuralType.

                // Consider an expression of the form J(a, b=P(E))
                // The inner P(E) would have been translated to a SQL statement
                // We should not use the raw names from the type, but the equivalent
                // symbols (they are present in symbol.Columns) if they exist.

                // We add the new columns to the symbol's columns if they do
                // not already exist.

                foreach (EdmProperty property in symbol.Type.GetProperties()) {
                    if(!_properties.Any(x => x.Property.Name == property.Name && x.Property.DeclaringType == property.DeclaringType)) {
                        continue;
                    }

                    string recordMemberName = property.Name;
                    // Since all renaming happens in the second phase
                    // we lose nothing by setting the next column name index to 0
                    // many times.
                    this.AllColumnNames[recordMemberName] = 0;

                    // Create a new symbol/reuse existing symbol for the column
                    Symbol columnSymbol;
                    if (!symbol.Columns.TryGetValue(recordMemberName, out columnSymbol)) {
                        // we do not care about the types of columns, so we pass null
                        // when construction the symbol.
                        columnSymbol = new Symbol(recordMemberName, null);
                        symbol.Columns.Add(recordMemberName, columnSymbol);
                    }

                    selectStatement.Select.Append(separator);
                    selectStatement.Select.Append(symbol);
                    selectStatement.Select.Append(".");

                    // We use the actual name before the " ", the new name goes
                    // after the AS.
                    selectStatement.Select.Append(recordMemberName);

                    if (!recordMemberName.Equals("desc", StringComparison.InvariantCultureIgnoreCase)) {
                        selectStatement.Select.Append(" ");
                        selectStatement.Select.Append(columnSymbol);
                    }

                    // Check for column name collisions.
                    if (columnDictionary.ContainsKey(recordMemberName)) {
                        columnDictionary[recordMemberName].NeedsRenaming = true;
                        columnSymbol.NeedsRenaming = true;
                    }
                    else {
                        columnDictionary[recordMemberName] = symbol.Columns[recordMemberName];
                    }

                    columnList.Add(columnSymbol);

                    separator = ", ";
                }
            }
        }

        private ISqlFragment VisitSetOpExpression(VfpExpression left, VfpExpression right, string separator) {
            SqlSelectStatement leftSelectStatement = this.VisitExpressionEnsureSqlStatement(left);
            SqlSelectStatement rightSelectStatement = this.VisitExpressionEnsureSqlStatement(right);

            SqlBuilder setStatement = new SqlBuilder();
            setStatement.Append(leftSelectStatement);
            setStatement.AppendLine();
            setStatement.Append(separator); // e.g. UNION ALL
            setStatement.AppendLine();
            setStatement.Append(rightSelectStatement);

            return setStatement;
        }

        /// <summary>
        /// Translate a NewInstance(Element(X)) expression into
        ///   "select top(1) * from X"
        /// </summary>
        private ISqlFragment VisitCollectionConstructor(VfpNewInstanceExpression e) {
            Debug.Assert(e.Arguments.Count <= 1);

            if (e.Arguments.Count == 1 && e.Arguments[0].ExpressionKind == VfpExpressionKind.Element) {
                VfpElementExpression elementExpr = e.Arguments[0] as VfpElementExpression;
                SqlSelectStatement result = this.VisitExpressionEnsureSqlStatement(elementExpr.Argument);

                if (!IsCompatible(result, VfpExpressionKind.Element)) {
                    Symbol fromSymbol;
                    TypeUsage inputType = elementExpr.Argument.ResultType.ToElementTypeUsage();

                    result = this.CreateNewSelectStatement(result, "element", inputType, out fromSymbol);
                    this.AddFromSymbol(result, "element", fromSymbol, false);
                }

                result.Top = new TopClause(1, false);
                return result;
            }

            // Otherwise simply build this out as a union-all ladder
            CollectionType collectionType = e.ResultType.GetEdmType<CollectionType>();
            Debug.Assert(collectionType != null);
            bool isScalarElement = collectionType.TypeUsage.IsPrimitiveType();

            SqlBuilder resultSql = new SqlBuilder();
            string separator = string.Empty;

            // handle empty table
            if (e.Arguments.Count == 0) {
                Debug.Assert(isScalarElement);
                resultSql.Append(" SELECT CAST(null as ");
                resultSql.Append(GetSqlPrimitiveType(collectionType.TypeUsage));
                resultSql.Append(") AS X FROM (SELECT 1) AS Y WHERE 1=0");
            }

            foreach (VfpExpression arg in e.Arguments) {
                resultSql.Append(separator);
                resultSql.Append(" SELECT ");
                resultSql.Append(arg.Accept(this));
                // For scalar elements, no alias is appended yet. Add this.
                if (isScalarElement) {
                    resultSql.Append(" AS X ");
                }

                resultSql.Append(" FROM " + VfpCommand.SingleRowTempTableRequiredToken);
                separator = " UNION ALL ";
            }

            return resultSql;
        }

        /// <summary>
        /// Returns the sql primitive/native type name. 
        /// It will include size, precision or scale depending on type information present in the 
        /// type facets
        /// </summary>
        internal static string GetSqlPrimitiveType(TypeUsage type) {
            PrimitiveType primitiveType = type.GetEdmType<PrimitiveType>();

            string typeName = primitiveType.Name.ToLower();

            if (typeName == "currency" && primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Decimal) {
                return "y";
            }

            if (typeName == "date" && primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.DateTime) {
                return "d";
            }

            bool isFixedLength = false;
            int maxLength = 0;
            string length = "max";
            bool preserveSeconds = true;
            byte decimalPrecision = 0;
            byte decimalScale = 0;


            switch (primitiveType.PrimitiveTypeKind) {
                case PrimitiveTypeKind.Binary:
                    typeName = "w";
                    break;

                case PrimitiveTypeKind.Guid:
                    typeName = "c(38)";
                    break;
                case PrimitiveTypeKind.String:
                    isFixedLength = type.GetFacetValueOrDefault<bool>(FacetInfo.FixedLengthFacetName, false);
                    maxLength = type.GetFacetValueOrDefault<int>(FacetInfo.MaxLengthFacetName, Int32.MinValue);

                    if (maxLength == Int32.MinValue) {
                        length = "254";
                    }
                    else {
                        length = maxLength.ToString(CultureInfo.InvariantCulture);
                    }

                    if (maxLength == int.MaxValue) {
                        typeName = "m";
                    }
                    else if (isFixedLength) {
                        typeName = "c(" + length + ")";
                    }
                    else {
                        typeName = "v(" + length + ")";
                    }

                    //// Using the Unicode Facet to determine if the field is binary...
                    //var binary = type.GetFacetValueOrDefault<bool>(FacetInfo.UnicodeFacetName, false);

                    //if (binary) {
                    //    typeName += " NOCPTRANS";
                    //}

                    break;

                case PrimitiveTypeKind.DateTime:
                    preserveSeconds = type.GetFacetValueOrDefault<bool>(FacetInfo.PreserveSecondsFacetName, false);
                    typeName = "t";
                    break;

                case PrimitiveTypeKind.Single:
                    decimalPrecision = type.GetFacetValueOrDefault<byte>(FacetInfo.PrecisionFacetName, 18);
                    Debug.Assert(decimalPrecision > 0, "decimal precision must be greater than zero");
                    decimalScale = type.GetFacetValueOrDefault<byte>(FacetInfo.ScaleFacetName, 0);
                    Debug.Assert(decimalPrecision >= decimalScale, "decimalPrecision must be greater or equal to decimalScale");
                    Debug.Assert(decimalPrecision <= 38, "decimalPrecision must be less than or equal to 38");
                    typeName = "f(" + decimalPrecision + "," + decimalScale + ")";
                    break;
                case PrimitiveTypeKind.Double:
                    typeName = "b";
                    break;
                case PrimitiveTypeKind.Decimal:
                    decimalPrecision = type.GetFacetValueOrDefault<byte>(FacetInfo.PrecisionFacetName, 18);
                    Debug.Assert(decimalPrecision > 0, "decimal precision must be greater than zero");
                    decimalScale = type.GetFacetValueOrDefault<byte>(FacetInfo.ScaleFacetName, 0);
                    Debug.Assert(decimalPrecision >= decimalScale, "decimalPrecision must be greater or equal to decimalScale");
                    Debug.Assert(decimalPrecision <= 38, "decimalPrecision must be less than or equal to 38");
                    typeName = "n(" + decimalPrecision + "," + decimalScale + ")";
                    break;
                case PrimitiveTypeKind.Int64:
                    typeName = "n(20)";
                    break;
                case PrimitiveTypeKind.Byte:
                case PrimitiveTypeKind.Int16:
                case PrimitiveTypeKind.Int32:
                    typeName = "i";
                    break;

                case PrimitiveTypeKind.Boolean:
                    typeName = "l";
                    break;

                default:
                    throw new NotSupportedException("Unsupported EdmType: " + primitiveType.PrimitiveTypeKind);
            }

            return typeName;
        }

        private SqlBuilder VisitIsNullExpression(VfpIsNullExpression e, bool negate) {
            SqlBuilder result = new SqlBuilder();

            if (this.isProcessingFilter) {
                result.Append("(");
                result.Append(e.Argument.Accept(this));
                result.Append(")");
                if (!negate) {
                    result.Append(" IS NULL");
                }
                else {
                    result.Append(" IS NOT NULL");
                }
            }
            else {
                if (negate) {
                    result.Append("!");
                }

                result.Append("ISNULL(");
                result.Append(e.Argument.Accept(this));
                result.Append(")");
            }

            return result;
        }

        /// <summary>
        /// Helper method for the Group By visitor
        /// Returns true if at least one of the aggregates in the given list
        /// has an argument that is not a <see cref="VfpPropertyExpression"/> 
        /// over <see cref="VfpVariableReferenceExpression"/>
        /// </summary>
        private static bool NeedsInnerQuery(IList<VfpAggregate> aggregates) {
            foreach (VfpAggregate aggregate in aggregates) {
                Debug.Assert(aggregate.Arguments.Count == 1);
                if (!aggregate.Arguments[0].IsPropertyOverVarRef()) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Aggregates are not visited by the normal visitor walk.
        /// </summary>
        /// <param name="aggregate">The aggregate go be translated</param>
        /// <param name="aggregateArgument">The translated aggregate argument</param>
        private SqlBuilder VisitAggregate(VfpAggregate aggregate, object aggregateArgument) {
            SqlBuilder aggregateResult = new SqlBuilder();
            VfpFunctionAggregate functionAggregate = aggregate as VfpFunctionAggregate;

            if (functionAggregate == null) {
                throw new NotSupportedException();
            }

            switch (functionAggregate.Function.Name.ToLower()) {
                case "count":
                case "avg":
                case "min":
                case "max":
                case "sum":
                case "bigcount":
                    break;
                default:
                    throw new NotSupportedException(functionAggregate.Function.Name);
            }

            /*
             * Added a CAST(X as n(20, 14)) to fix the following exception:
             *      System.InvalidOperationException: The provider could not determine the Decimal value. For example, the row was just created, the default for the Decimal column was not available, and the consumer had not yet set a new Decimal value.
             */
            if (functionAggregate.Function.IsCanonicalFunction()
                && String.Equals(functionAggregate.Function.Name, "BigCount", StringComparison.Ordinal)) {
                aggregateResult.Append("COUNT");
            }
            else {
                if (functionAggregate.Function.Name == "Avg" || functionAggregate.Function.Name == "Count") {
                    aggregateResult.Append("CAST(");
                }

                this.WriteFunctionName(aggregateResult, functionAggregate.Function);
            }

            aggregateResult.Append("(");

            VfpFunctionAggregate fnAggr = functionAggregate;
            if (null != fnAggr && fnAggr.Distinct) {
                aggregateResult.Append("DISTINCT ");
            }

            aggregateResult.Append(aggregateArgument);

            aggregateResult.Append(")");

            if (functionAggregate.Function.Name == "Avg") {
                aggregateResult.Append(" as n(20,14))");
            }
            else if (functionAggregate.Function.Name == "Count") {
                aggregateResult.Append(" as i)");
            }

            return aggregateResult;
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbAndExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbApplyExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbArithmeticExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbCaseExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbCastExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbComparisonExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbConstantExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbCrossJoinExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbDerefExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbDistinctExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbElementExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbEntityRefExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbExceptExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbFilterExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbFunctionExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbGroupByExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbInExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbIntersectExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbIsEmptyExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbIsNullExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbIsOfExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbJoinExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbLambdaExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbLikeExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbLimitExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbNewInstanceExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbNotExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbNullExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbOfTypeExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbOrExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbParameterReferenceExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbProjectExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbPropertyExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbQuantifierExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbRefExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbRefKeyExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbRelationshipNavigationExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbScanExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbSkipExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbSortExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbTreatExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbUnionAllExpression expression) {
            throw new NotImplementedException();
        }

        public ISqlFragment Visit(System.Data.Entity.Core.Common.CommandTrees.DbVariableReferenceExpression expression) {
            throw new NotImplementedException();
        }


        public ISqlFragment Visit(VfpLambdaExpression expression) {
            throw new NotImplementedException();
        }
    }
}