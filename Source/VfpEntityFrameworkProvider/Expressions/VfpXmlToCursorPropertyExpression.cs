using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpXmlToCursorPropertyExpression : VfpExpression {
        public VfpExpression Instance { get; private set; }

        internal VfpXmlToCursorPropertyExpression(TypeUsage resultType, VfpExpression instance)
            : base(VfpExpressionKind.XmlToCursorProperty, resultType) {
            Instance = ArgumentUtility.CheckNotNull("instance", instance);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}