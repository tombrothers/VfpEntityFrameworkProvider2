using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider.Expressions {
    public abstract class VfpUnaryExpression : VfpExpression {
        public VfpExpression Argument { get; private set; }

        protected VfpUnaryExpression(VfpExpressionKind kind, TypeUsage resultType, VfpExpression argument)
            : base(kind, resultType) {
            Argument = ArgumentUtility.CheckNotNull("argument", argument);
        }
    }
}