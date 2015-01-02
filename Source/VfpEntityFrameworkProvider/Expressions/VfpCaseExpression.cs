using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpCaseExpression : VfpExpression {
        public VfpExpression Else { get; private set; }
        public VfpExpressionList Then { get; private set; }
        public VfpExpressionList When { get; private set; }

        internal VfpCaseExpression(TypeUsage commonResultType, 
                                   VfpExpressionList whenExpressions, 
                                   VfpExpressionList thenExpressions, 
                                   VfpExpression elseExpression)
            : base(VfpExpressionKind.Case, commonResultType) {
            When = ArgumentUtility.CheckNotNull("whenExpressions", whenExpressions);
            Then = ArgumentUtility.CheckNotNull("thenExpressions", thenExpressions);
            Else = ArgumentUtility.CheckNotNull("elseExpression", elseExpression);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}