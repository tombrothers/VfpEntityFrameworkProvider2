using System;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.SqlGeneration {
    internal class BinaryFragment : SqlFragmentBase {
        public VfpExpressionKind Kind { get; private set; }
        public ISqlFragment Left { get; private set; }
        public ISqlFragment Right { get; private set; }

        public BinaryFragment(VfpExpressionKind kind, ISqlFragment left, ISqlFragment right)
            : base(SqlFragmentType.Binary) {
            Kind = kind;
            Left = left;
            Right = right;
        }

        public override void WriteSql(SqlWriter writer, SqlVisitor visitor) {
            Left.WriteSql(writer, visitor);
            writer.Write(GetOperator(Kind));
            Right.WriteSql(writer, visitor);
        }

        public static string GetOperator(VfpExpressionKind kind) {
            switch (kind) {
                case VfpExpressionKind.And:
                    return " AND ";
                case VfpExpressionKind.Divide:
                    return " / ";
                case VfpExpressionKind.Equals:
                    return " = ";
                case VfpExpressionKind.GreaterThan:
                    return " > ";
                case VfpExpressionKind.GreaterThanOrEquals:
                    return " >= ";
                case VfpExpressionKind.LessThan:
                    return " < ";
                case VfpExpressionKind.LessThanOrEquals:
                    return " <= ";
                case VfpExpressionKind.Minus:
                    return " - ";
                case VfpExpressionKind.Modulo:
                    return " % ";
                case VfpExpressionKind.Multiply:
                    return " * ";
                case VfpExpressionKind.Or:
                    return " OR ";
                case VfpExpressionKind.Plus:
                    return " + ";
                case VfpExpressionKind.NotEquals:
                    return " <> ";
                default:
                    throw new InvalidOperationException("Invalid ExpressionKind:  " + kind.ToString());
            }
        }
    }
}