using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpXmlToCursorScanExpression : VfpExpression {
        public VfpExpression Parameter { get; private set; }
        public string CursorName { get; private set; }

        internal VfpXmlToCursorScanExpression(VfpExpression parameter, string cursorName)
            : base(VfpExpressionKind.XmlToCursorScan, PrimitiveTypeKind.Boolean.ToTypeUsage()) {
            Parameter = ArgumentUtility.CheckNotNull("parameter", parameter);
            CursorName = ArgumentUtility.CheckNotNullOrEmpty("cursorName", cursorName);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}