using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpXmlToCursorExpression : VfpExpression {
        public VfpExpression Property { get; private set; }
        public VfpExpression Parameter { get; private set; }
        public string CursorName { get; private set; }
        public Type ItemType { get; private set; }

        public VfpXmlToCursorExpression(VfpExpression property, VfpExpression parameter, string cursorName, Type itemType)
            : base(VfpExpressionKind.XmlToCursor, PrimitiveTypeKind.Boolean.ToTypeUsage()) {
            Property = ArgumentUtility.CheckNotNull("property", property);
            Parameter = ArgumentUtility.CheckNotNull("parameter", parameter);
            CursorName = ArgumentUtility.CheckNotNullOrEmpty("cursorName", cursorName);
            ItemType = ArgumentUtility.CheckNotNull("itemType", itemType);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}