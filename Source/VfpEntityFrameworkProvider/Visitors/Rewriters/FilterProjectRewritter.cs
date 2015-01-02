using System.Collections.ObjectModel;
using System.Linq;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class FilterProjectRewritter : VfpExpressionVisitor {
        public static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new FilterProjectRewritter();

            return rewriter.Visit(expression);
        }

        protected override VfpExpressionBinding VisitVfpExpressionBinding(VfpExpressionBinding binding) {
            binding = base.VisitVfpExpressionBinding(binding);

            var filterExpression = binding.Expression as VfpFilterExpression;

            if (filterExpression == null) {
                return binding;
            }

            var project = filterExpression.Input.Expression as VfpProjectExpression;

            if (project == null) {
                return binding;
            }

            var scan = project.Input.Expression as VfpScanExpression;

            if (scan == null) {
                return binding;
            }

            var newInstance = project.Projection as VfpNewInstanceExpression;

            if (newInstance == null) {
                return binding;
            }

            var properties = newInstance.Arguments.OfType<VfpPropertyExpression>().ToList().AsReadOnly();

            if (!properties.Any()) {
                return binding;
            }

            if (!VfpPropertyExpressionChecker.CanRewrite(filterExpression.Predicate)) {
                return binding;
            }

            var newFilterPredicate = VfpPropertyExpressionRewriter.Rewrite(filterExpression.Predicate, properties);
            var newFilterExpression = project.Input.Filter(newFilterPredicate);
            var newFilterBinding = newFilterExpression.BindAs(binding.Variable.VariableName);
            var newProjection = VfpVariableReferenceExpressionRewriter.Rewrite(project.Projection, binding.Variable);
            var newProjectExpression = newFilterBinding.Project(newProjection);
            var newBinding = newProjectExpression.BindAs(binding.Variable.VariableName);

            return newBinding;
        }

        private class VfpVariableReferenceExpressionRewriter : VfpExpressionVisitor {
            private readonly VfpVariableReferenceExpression _variableReference;

            public static VfpExpression Rewrite(VfpExpression expression, VfpVariableReferenceExpression variableReference) {
                var rewriter = new VfpVariableReferenceExpressionRewriter(variableReference);

                return rewriter.Visit(expression);
            }

            public VfpVariableReferenceExpressionRewriter(VfpVariableReferenceExpression variableReference) {
                _variableReference = variableReference;
            }

            public override VfpExpression Visit(VfpVariableReferenceExpression expression) {
                return new VfpVariableReferenceExpression(_variableReference.ResultType, _variableReference.VariableName);
            }
        }

        private class VfpPropertyExpressionRewriter : VfpExpressionVisitor {
            private readonly ReadOnlyCollection<VfpPropertyExpression> _properties;

            public static VfpExpression Rewrite(VfpExpression expression, ReadOnlyCollection<VfpPropertyExpression> properties) {
                var rewriter = new VfpPropertyExpressionRewriter(properties);

                return rewriter.Visit(expression);
            }

            private VfpPropertyExpressionRewriter(ReadOnlyCollection<VfpPropertyExpression> properties) {
                _properties = properties;
            }

            public override VfpExpression Visit(VfpPropertyExpression expression) {
                if (!expression.Property.Name.StartsWith("C")) {
                    return expression;
                }

                var index = int.Parse(expression.Property.Name.Substring(1)) - 1;
                var property = _properties[index];

                return property.Instance.Property(property.ResultType, property.Property);
            }
        }

        private class VfpPropertyExpressionChecker : VfpExpressionVisitor {
            private bool _canRewrite = true;

            public static bool CanRewrite(VfpExpression expression) {
                var rewriter = new VfpPropertyExpressionChecker();

                rewriter.Visit(expression);

                return rewriter._canRewrite;
            }

            public override VfpExpression Visit(VfpPropertyExpression expression) {
                int index;

                if (!expression.Property.Name.StartsWith("C") || !int.TryParse(expression.Property.Name.Substring(1), out index)) {
                    _canRewrite = false;

                    return expression;
                }

                return expression;
            }
        }
    }
}