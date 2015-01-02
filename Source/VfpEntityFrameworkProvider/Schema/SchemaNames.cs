namespace VfpEntityFrameworkProvider.Schema {
    public static class SchemaNames {
        public static readonly string Tables = VfpConnection.SchemaNames.Tables;
        public static readonly string TableColumns = "TableColumns";
        public static readonly string Views = VfpConnection.SchemaNames.Views;
        public static readonly string ViewColumns = "ViewColumns";
        public static readonly string Functions = "Functions";
        public static readonly string FunctionParameters = "FunctionParameters";
        public static readonly string FunctionReturnTableColumns = "FunctionReturnTableColumns";
        public static readonly string Procedures = VfpConnection.SchemaNames.Procedures;
        public static readonly string ProcedureParameters = VfpConnection.SchemaNames.ProcedureParameters;
        public static readonly string TableConstraints = "TableConstraints";
        public static readonly string CheckConstraints = "CheckConstraints";
        public static readonly string TableConstraintColumns = "TableConstraintColumns";
        public static readonly string TableForeignKeyConstraints = "TableForeignKeyConstraints";
        public static readonly string TableForeignKeys = "TableForeignKeys";
        public static readonly string ViewConstraints = "ViewConstraints";
        public static readonly string ViewConstraintColumns = "ViewConstraintColumns";
        public static readonly string ViewForeignKeys = "ViewForeignKeys";
    }
}