namespace VfpEntityFrameworkProvider.Expressions {
    public class VfpSortClause {
        public bool Ascending { get; private set; }
        public string Collation { get; private set; }
        public VfpExpression Expression { get; private set; }

        public VfpSortClause(VfpExpression key, bool ascending, string collation) {
            Expression = ArgumentUtility.CheckNotNull("key", key);
            Ascending = ascending;
            Collation = collation;
        }
    }
}