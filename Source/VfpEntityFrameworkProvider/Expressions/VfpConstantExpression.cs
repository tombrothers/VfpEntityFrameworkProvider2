using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpConstantExpression : VfpExpression {
        public PrimitiveTypeKind ConstantKind { get; private set; }

        private object value;

        public object Value {
            get {
                if (ConstantKind == PrimitiveTypeKind.Binary && value != null) {
                    return ((byte[])value).Clone();
                }

                return value;
            }
            private set {
                this.value = value;
            }
        }

        internal VfpConstantExpression(TypeUsage resultType, object value)
            : base(VfpExpressionKind.Constant, resultType) {
            ConstantKind = ArgumentUtility.CheckNotNull("resultType", resultType).ToPrimitiveTypeKind();

            if (ConstantKind == PrimitiveTypeKind.Binary && value != null) {
                Value = ((byte[])value).Clone();
            }
            else {
                Value = value;
            }
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}