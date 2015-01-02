namespace VfpEntityFrameworkProvider.SqlGeneration {
    internal interface ISqlFragment {
        void WriteSql(SqlWriter writer, SqlVisitor visitor);
        ISqlFragment Accept(SqlFragmentVisitorBase visitor);
        SqlFragmentType SqlFragmentType { get; }
        string ToString(SqlVisitor visitor);
    }
}