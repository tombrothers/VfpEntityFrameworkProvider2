using System.Globalization;

namespace VfpEntityFrameworkProvider.SqlGeneration {
    internal class TopClause : SqlFragmentBase {
        internal ISqlFragment TopCount { get; private set; }

        internal TopClause(ISqlFragment topCount)
            : base(SqlFragmentType.TopClause) {
            TopCount = topCount;
        }

        internal TopClause(int topCount, bool withTies)
            : base(SqlFragmentType.TopClause) {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.Append(topCount.ToString(CultureInfo.InvariantCulture));

            TopCount = sqlBuilder;
        }

        public override void WriteSql(SqlWriter writer, SqlVisitor visitor) {
            writer.Write("TOP ");
            TopCount.WriteSql(writer, visitor);
            writer.Write(" ");
        }
    }
}