using System.Data.Entity.Core.Metadata.Edm;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public abstract class VfpExpression {
        public VfpExpressionKind ExpressionKind { get; private set; }
        public TypeUsage ResultType { get; private set; }

        protected VfpExpression(VfpExpressionKind expressionKind, TypeUsage resultType) {
            ExpressionKind = ArgumentUtility.CheckIsDefined("expressionKind", expressionKind);
            ResultType = ArgumentUtility.CheckNotNull("resultType", resultType);
        }

        public abstract TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor);
    }
}