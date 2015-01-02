using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpEntityFrameworkProvider.Visitors.Gatherers {
    internal class EntityTypeGatherer : VfpExpressionVisitor {
        private readonly List<EntityType> _entityTypes = new List<EntityType>();

        public static ReadOnlyCollection<EntityType> Gather(VfpExpression expression) {
            var visitor = new EntityTypeGatherer();

            visitor.Visit(expression);

            return visitor._entityTypes.AsReadOnly();
        }

        protected override VfpExpressionBinding VisitVfpExpressionBinding(VfpExpressionBinding binding) {
            var entityType = binding.VariableType.EdmType as EntityType;

            if (entityType != null) {
                _entityTypes.Add(entityType);
            }

            return base.VisitVfpExpressionBinding(binding);
        }
    }
}