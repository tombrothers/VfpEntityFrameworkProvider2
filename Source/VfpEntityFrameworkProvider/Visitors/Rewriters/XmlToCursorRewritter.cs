using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using VfpClient.Utils;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    internal class XmlToCursorRewritter : VfpExpressionVisitor {
        private int _count;

        public static VfpExpression Rewrite(VfpExpression expression) {
            return new XmlToCursorRewritter().Visit(expression);
        }

        public override VfpExpression Visit(VfpInExpression expression) {
            const int convertToXmltocursorMintextLength = 200;

            if (expression.List.Any(x => x.ExpressionKind != VfpExpressionKind.Constant)) {
                return base.Visit(expression);
            }

            var values = new StringBuilder(convertToXmltocursorMintextLength);
            var array = expression.List.Cast<VfpConstantExpression>().Select(x => x.Value).Distinct().ToArray();

            foreach (var item in array) {
                values.Append(item);

                if (values.Length > convertToXmltocursorMintextLength) {
                    break;
                }
            }

            if (values.Length > convertToXmltocursorMintextLength) {
                _count++;

                var arrayXmlToCursor = new ArrayXmlToCursor(array);
                var xml = VfpExpressionBuilder.Constant(arrayXmlToCursor.Xml);
                var parameter = VfpExpressionBuilder.Parameter(PrimitiveTypeKind.String.ToTypeUsage(), "@__XmlToCursor" + _count, xml);
                var cursorName = "curXml" + _count;
                var xmlToCursor = VfpExpressionBuilder.XmlToCursor(expression.Item, parameter, cursorName, arrayXmlToCursor.ItemType);

                return base.Visit(xmlToCursor);
            }

            return base.Visit(expression);
        }
    }
}