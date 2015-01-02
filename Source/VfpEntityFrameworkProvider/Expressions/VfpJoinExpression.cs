using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpJoinExpression : VfpExpression {
        public VfpExpressionBinding Left { get; private set; }
        public VfpExpressionBinding Right { get; private set; }
        public VfpExpression JoinCondition { get; private set; }

        internal VfpJoinExpression(VfpExpressionKind kind, TypeUsage collectionOfRowResultType, VfpExpressionBinding left, VfpExpressionBinding right, VfpExpression condition)
            : base(kind, collectionOfRowResultType) {
            Left = ArgumentUtility.CheckNotNull("left", left);
            Right = ArgumentUtility.CheckNotNull("right", right);
            JoinCondition = ArgumentUtility.CheckNotNull("condition", condition);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}