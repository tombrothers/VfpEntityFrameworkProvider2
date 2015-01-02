using System.Text;

namespace VfpEntityFrameworkProvider.SqlGeneration {
    internal abstract class SqlFragmentBase : ISqlFragment {
        public SqlFragmentType SqlFragmentType { get; private set; }

        public SqlFragmentBase(SqlFragmentType sqlFragmentType) {
            SqlFragmentType = sqlFragmentType;
        }

        public abstract void WriteSql(SqlWriter writer, SqlVisitor visitor);

        public ISqlFragment Accept(SqlFragmentVisitorBase visitor) {
            return visitor.Visit(this);
        }

        public string ToString(SqlVisitor visitor) {
            var builder = new StringBuilder(1024);

            using (var writer = new SqlWriter(builder)) {
                WriteSql(writer, visitor);
            }

            return builder.ToString();
        }
    }
}