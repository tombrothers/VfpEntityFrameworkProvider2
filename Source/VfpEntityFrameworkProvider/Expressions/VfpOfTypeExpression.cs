using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpOfTypeExpression : VfpUnaryExpression {
        public TypeUsage OfType { get; private set; }

        internal VfpOfTypeExpression(VfpExpressionKind expressionKind, TypeUsage collectionResultType, VfpExpression argument, TypeUsage type)
            : base(expressionKind, collectionResultType, argument) {
            OfType = ArgumentUtility.CheckNotNull("type", type);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}