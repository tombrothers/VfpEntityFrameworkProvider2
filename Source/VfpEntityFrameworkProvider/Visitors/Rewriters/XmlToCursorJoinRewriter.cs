using System.Collections.Generic;
using System.Linq;
using VfpEntityFrameworkProvider.Expressions;
using VfpEntityFrameworkProvider.Visitors.Gatherers;
using VfpEntityFrameworkProvider.Visitors.Removers;
using VfpEntityFrameworkProvider.Visitors.Replacers;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class XmlToCursorJoinRewriter : VfpExpressionVisitor {
        private int _count;
        private readonly Stack<VfpExpressionBinding> _filterBindings = new Stack<VfpExpressionBinding>();
        private readonly List<string> _xmlToCursorsToBeRemoved = new List<string>();
        private readonly IDictionary<string, List<XmlToCursorData>> _xmlToCursors;

        private XmlToCursorJoinRewriter(VfpExpression expression) {
            _xmlToCursors = GetXmlToCursors(expression).GroupBy(x => x.TableProperty.Property.Name, x => x)
                                                       .ToDictionary(x => x.Key, x => x.ToList());
        }

        private IEnumerable<XmlToCursorData> GetXmlToCursors(VfpExpression expression) {
            return XmlToCursorExpressionGatherer.Gather(expression)
                                                .Where(x => !x.CursorName.StartsWith(XmlToCursorMoveToInnerExpressionRewriter.CursorNamePrefix)) // TODO:  figure out how to rewrite these as inner joins
                                                .Select(x => new XmlToCursorData(x))
                                                .Where(x => x.TableProperty != null)
                                                .Select(x => x).ToList().AsReadOnly();
        }

        internal static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new XmlToCursorJoinRewriter(expression);

            expression = rewriter.Visit(expression);

            return expression;
        }

        protected override VfpExpressionBinding VisitVfpExpressionBinding(VfpExpressionBinding binding) {
            var isFilterBinding = binding.Expression is VfpFilterExpression;

            if (isFilterBinding) {
                _filterBindings.Push(binding);
            }

            binding = base.VisitVfpExpressionBinding(binding);

            if (isFilterBinding) {
                var expression = binding.Expression;

                foreach (var cursorName in _xmlToCursorsToBeRemoved) {
                    expression = XmlToCursorExpressionRemover.Remove(expression, cursorName);
                }

                _xmlToCursorsToBeRemoved.Clear();
                _filterBindings.Pop();

                binding = expression.BindAs(binding.Variable.VariableName);
            }

            if (!_filterBindings.Any()) {
                return binding;
            }

            var filterBinding = _filterBindings.Peek();

            if (filterBinding == null) {
                return binding;
            }

            var scan = binding.Expression as VfpScanExpression;

            if (scan == null) {
                return binding;
            }

            if (!_xmlToCursors.ContainsKey(binding.VariableName)) {
                return binding;
            }

            var xmlToCursors = _xmlToCursors[binding.VariableName];

            if (!xmlToCursors.Any()) {
                return binding;
            }

            foreach (var xmlToCursor in xmlToCursors) {
                var xmlToCursorExpression = xmlToCursor.XmlToCursor;

                _count++;

                var variableReference = xmlToCursorExpression.ItemType.ToTypeUsage().Variable("Xml" + _count);
                var xmlToCursorScan = VfpExpressionBuilder.XmlToCursorScan(xmlToCursorExpression.Parameter, xmlToCursorExpression.CursorName + "_j");
                var xmlToCursorBinding = xmlToCursorScan.BindAs(variableReference.VariableName);
                var xmlToCursorProperty = VfpExpressionBuilder.XmlToCursorProperty(variableReference.ResultType, variableReference);
                var scanProperty = GetScanProperty(binding.Variable, xmlToCursorExpression.Property);
                var comparison = scanProperty.ExpressionEquals(xmlToCursorProperty);

                var joinVariableReference = binding.VariableType.Variable(binding.VariableName);
                var joinExpression = binding.InnerJoin(xmlToCursorBinding, comparison, binding.Expression.ResultType);

                _xmlToCursorsToBeRemoved.Add(xmlToCursorExpression.CursorName);

                binding = joinExpression.BindAs(joinVariableReference.VariableName);
            }

            return binding;
        }

        private static VfpExpression GetScanProperty(VfpVariableReferenceExpression variable, VfpExpression property) {
            var expression = VariableReferenceReplacer.Replace(variable, property);
            var scanProperty = expression as VfpPropertyExpression;

            if (scanProperty == null) {
                return expression;
            }

            var variables = VariableReferenceGatherer.Gather(expression);

            if (!variables.Any()) {
                return expression;
            }

            var scanVariable = variables.First();

            return scanVariable.Property(scanProperty.ResultType, scanProperty.Property);
        }

        private class XmlToCursorData {
            public VfpXmlToCursorExpression XmlToCursor { get; private set; }
            public VfpPropertyExpression ColumnProperty { get; private set; }
            public VfpPropertyExpression TableProperty { get; private set; }

            public XmlToCursorData(VfpXmlToCursorExpression expression) {
                ArgumentUtility.CheckNotNull("expression", expression);

                XmlToCursor = expression;
                ColumnProperty = expression.Property as VfpPropertyExpression;

                if (ColumnProperty == null) {
                    return;
                }

                TableProperty = ColumnProperty.Instance as VfpPropertyExpression;
            }
        }
    }
}