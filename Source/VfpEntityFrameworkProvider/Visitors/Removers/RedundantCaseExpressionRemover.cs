using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Removers {
    /*
     * Example
     * Before:
     * (ICASE((LEN(Extent1.ContactName)) > 0,.T., NOT ((LEN(Extent1.ContactName)) > 0),.F.)
     * 
     * After:
     * LEN(Extent1.ContactName)) > 0
     * */
    internal class RedundantCaseExpressionRemover : VfpExpressionVisitor {
        public static VfpExpression Remove(VfpExpression expression) {
            return new RedundantCaseExpressionRemover().Visit(expression);
        }

        public override VfpExpression Visit(VfpCaseExpression expression) {
            return IsExpectedCaseExpression(expression) ? base.Visit(expression.When[0]) : base.Visit(expression);
        }

        private static bool IsExpectedCaseExpression(VfpCaseExpression expression) =>
            IsComparisonCaseExpression(expression) || IsEmptyCaseExpression(expression) || IsNotAndIsEmptyCaseExpression(expression);

        private static bool IsNotAndIsEmptyCaseExpression(VfpCaseExpression expression) {
            if(expression == null || expression.When.Count != 1 || expression.Then.Count != 1) {
                return false;
            }

            if(!(expression.When.First() is VfpNotExpression notExpression) || !(notExpression.Argument is VfpIsEmptyExpression)) {
                return false;
            }

            if(!(expression.Then[0] is VfpConstantExpression constant1) || constant1.ConstantKind != PrimitiveTypeKind.Boolean || !(bool)constant1.Value) {
                return false;
            }


            if(!(expression.Else is VfpConstantExpression constant2) || constant2.ConstantKind != PrimitiveTypeKind.Boolean || (bool)constant2.Value) {
                return false;
            }

            return true;
        }

        private static bool IsEmptyCaseExpression(VfpCaseExpression expression) {
            if(expression == null || expression.When.Count != 1 || expression.Then.Count != 1) {
                return false;
            }

            if(!(expression.When.First() is VfpIsEmptyExpression)) {
                return false;
            }

            if(!(expression.Then[0] is VfpConstantExpression constant1) || constant1.ConstantKind != PrimitiveTypeKind.Boolean || !(bool)constant1.Value) {
                return false;
            }


            if(!(expression.Else is VfpConstantExpression constant2) || constant2.ConstantKind != PrimitiveTypeKind.Boolean || (bool)constant2.Value) {
                return false;
            }

            return true;
        }

        private static bool IsComparisonCaseExpression(VfpCaseExpression expression) {
            if(expression == null || expression.When.Count != 2 || expression.Then.Count != 2) {
                return false;
            }

            if(!(expression.Then[0] is VfpConstantExpression constant1) || constant1.ConstantKind != PrimitiveTypeKind.Boolean || !(bool)constant1.Value) {
                return false;
            }


            if(!(expression.Then[1] is VfpConstantExpression constant2) || constant2.ConstantKind != PrimitiveTypeKind.Boolean || (bool)constant2.Value) {
                return false;
            }

            return true;
        }
    }
}