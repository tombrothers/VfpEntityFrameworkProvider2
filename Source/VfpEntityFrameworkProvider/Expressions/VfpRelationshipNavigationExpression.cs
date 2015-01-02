using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpRelationshipNavigationExpression : VfpExpression {
        public VfpExpression NavigationSource { get; private set; }
        public RelationshipType Relationship { get; private set; }
        public RelationshipEndMember NavigateTo { get; private set; }
        public RelationshipEndMember NavigateFrom { get; private set; }

        internal VfpRelationshipNavigationExpression(TypeUsage resultType, RelationshipType relType, RelationshipEndMember fromEnd, RelationshipEndMember toEnd, VfpExpression navigateFrom)
            : base(VfpExpressionKind.RelationshipNavigation, resultType) {
            Relationship = ArgumentUtility.CheckNotNull("relType", relType);
            NavigateFrom = ArgumentUtility.CheckNotNull("fromEnd", fromEnd);
            NavigateTo = ArgumentUtility.CheckNotNull("toEnd", toEnd);
            NavigationSource = ArgumentUtility.CheckNotNull("navigateFrom", navigateFrom);
        }

        [DebuggerStepThrough]
        public override TResultType Accept<TResultType>(IVfpExpressionVisitor<TResultType> visitor) {
            return ArgumentUtility.CheckNotNull("visitor", visitor).Visit(this);
        }
    }
}