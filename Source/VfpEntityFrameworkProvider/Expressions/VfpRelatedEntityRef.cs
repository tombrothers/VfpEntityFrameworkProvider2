using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpRelatedEntityRef {
        public RelationshipEndMember SourceEnd { get; private set; }
        public RelationshipEndMember TargetEnd { get; private set; }
        public VfpExpression TargetEntityRef { get; private set; }

        internal VfpRelatedEntityRef(RelationshipEndMember sourceEnd, RelationshipEndMember targetEnd, VfpExpression targetEntityRef) {
            TargetEntityRef = ArgumentUtility.CheckNotNull("targetEntityRef", targetEntityRef);
            TargetEnd = ArgumentUtility.CheckNotNull("targetEnd", targetEnd);
            SourceEnd = ArgumentUtility.CheckNotNull("sourceEnd", sourceEnd);
        }
    }
}