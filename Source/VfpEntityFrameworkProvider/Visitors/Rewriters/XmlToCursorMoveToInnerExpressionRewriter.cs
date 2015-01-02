using System.Collections.Generic;
using System.Linq;
using VfpEntityFrameworkProvider.Expressions;
using VfpEntityFrameworkProvider.Visitors.Gatherers;
using VfpEntityFrameworkProvider.Visitors.Removers;

namespace VfpEntityFrameworkProvider.Visitors.Rewriters {
    /*
     * Moves the XmlToCursor statement to the table instead of the outer most join
     

Before Example: 
     
    SELECT E1.OrderID, E4.ProductID, CAST( J1.Freight AS n(20,4)) AS Freight ;
        FROM   OrderDetails E1 ;
        INNER JOIN  (SELECT E2.OrderID OrderID1, E2.CustomerID CustomerID, E2.EmployeeID EmployeeID, E2.OrderDate OrderDate, E2.RequiredDate RequiredDate, E2.ShippedDate ShippedDate, E2.Freight Freight, E2.ShipName ShipName, E2.ShipAddress ShipAddress, E2.ShipCity ShipCity, E2.ShipRegion ShipRegion, E2.ShipPostalCode ShipPostalCode, E2.ShipCountry ShipCountry, E3.OrderID OrderID2, E3.CustomsDescription CustomsDescription, E3.ExciseTax ExciseTax ;
	        FROM  Orders E2 ;
	        LEFT JOIN InternationalOrders E3 ON E2.OrderID = E3.OrderID ) J1 ON E1.OrderID = J1.OrderID1 ;
        INNER JOIN Products E4 ON E1.ProductID = E4.ProductID ;
        WHERE J1.CustomerID IN (SELECT Id FROM (iif(XmlToCursor(__vfpClient___XmlToCursor1, 'curXml1') > 0, 'curXml1', '')))
      
After Example:
    SELECT E1.OrderID, E4.ProductID, CAST( J1.Freight AS n(20,4)) AS Freight ;
        FROM   OrderDetails E1 ;
        INNER JOIN  (SELECT E2.OrderID OrderID1, E2.CustomerID CustomerID, E2.EmployeeID EmployeeID, E2.OrderDate OrderDate, E2.RequiredDate RequiredDate, E2.ShippedDate ShippedDate, E2.Freight Freight, E2.ShipName ShipName, E2.ShipAddress ShipAddress, E2.ShipCity ShipCity, E2.ShipRegion ShipRegion, E2.ShipPostalCode ShipPostalCode, E2.ShipCountry ShipCountry, E3.OrderID OrderID2, E3.CustomsDescription CustomsDescription, E3.ExciseTax ExciseTax ;
	                    FROM   (SELECT * ;
		                            FROM Orders MX0 ;
		                            WHERE MX0.CustomerID IN (SELECT Id FROM (iif(XmlToCursor(__vfpClient___XmlToCursor1, 'curXml1_x') > 0, 'curXml1_x', ''))) ) E2 ;
	                    LEFT JOIN InternationalOrders E3 ON E2.OrderID = E3.OrderID ) J1 ON E1.OrderID = J1.OrderID1 ;
        INNER JOIN Products E4 ON E1.ProductID = E4.ProductID
     
     */
    internal class XmlToCursorMoveToInnerExpressionRewriter : VfpExpressionVisitor {
        public const string CursorNamePrefix = "MX";

        private int _count;
        private readonly IDictionary<string, List<XmlToCursorData>> _xmlToCursors;
        private readonly List<string> _xmlToCursorsToBeRemoved = new List<string>();

        private XmlToCursorMoveToInnerExpressionRewriter(VfpExpression expression) {
            _xmlToCursors = GetXmlToCursorsThatHaveNestedProperties(expression).GroupBy(x => x.TableProperty.Property.Name, x => x)
                                                                               .ToDictionary(x => x.Key, x => x.ToList());
        }

        private static IEnumerable<XmlToCursorData> GetXmlToCursorsThatHaveNestedProperties(VfpExpression expression) {
            return XmlToCursorExpressionGatherer.Gather(expression)
                                                .Select(x => new XmlToCursorData(x))
                                                .Where(x => x.TableProperty != null)
                                                .Where(x => x.TableProperty.Instance is VfpPropertyExpression)
                                                .Select(x => x);
        }

        internal static VfpExpression Rewrite(VfpExpression expression) {
            var rewriter = new XmlToCursorMoveToInnerExpressionRewriter(expression);

            if (!rewriter._xmlToCursors.Any()) {
                return expression;
            }

            expression = rewriter.Visit(expression);

            foreach (var cursorName in rewriter._xmlToCursorsToBeRemoved) {
                expression = XmlToCursorExpressionRemover.Remove(expression, cursorName);
            }

            return expression;
        }

        protected override VfpExpressionBinding VisitVfpExpressionBinding(VfpExpressionBinding binding) {
            binding = base.VisitVfpExpressionBinding(binding);

            if (!_xmlToCursors.ContainsKey(binding.VariableName)) {
                return binding;
            }

            var xmlToCursors = _xmlToCursors[binding.VariableName];

            if (!xmlToCursors.Any()) {
                return binding;
            }

            var scan = binding.Expression as VfpScanExpression;

            if (scan == null) {
                return binding;
            }

            var scanBinding = scan.BindAs(CursorNamePrefix + (_count++));
            VfpExpression predicate = null;

            foreach (var xmlToCursor in xmlToCursors) {
                var scanProperty = scanBinding.Variable.Property(xmlToCursor.ColumnProperty.Property);
                var xmlToCursorExpression = VfpExpressionBuilder.XmlToCursor(scanProperty, xmlToCursor.XmlToCursor.Parameter, CursorNamePrefix + xmlToCursor.XmlToCursor.CursorName, xmlToCursor.XmlToCursor.ItemType);

                _xmlToCursorsToBeRemoved.Add(xmlToCursor.XmlToCursor.CursorName);

                if (predicate == null) {
                    predicate = xmlToCursorExpression;
                }
                else {
                    predicate = predicate.And(xmlToCursorExpression);
                }
            }

            var filter = scanBinding.Filter(predicate);
            var filterBinding = filter.BindAs(binding.Variable.VariableName);

            return filterBinding;
        }

        private class XmlToCursorData {
            public VfpXmlToCursorExpression XmlToCursor { get; private set; }
            public VfpPropertyExpression ColumnProperty { get; private set; }
            public VfpPropertyExpression TableProperty { get; private set; }

            public XmlToCursorData(VfpXmlToCursorExpression expression) {
                ArgumentUtility.CheckNotNull("expression", expression);

                XmlToCursor = expression;
                ColumnProperty = expression.Property as VfpPropertyExpression;

                if (ColumnProperty == null) {
                    return;
                }

                TableProperty = ColumnProperty.Instance as VfpPropertyExpression;
            }
        }
    }
}