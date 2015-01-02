using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace VfpEntityFrameworkProvider {
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    internal class TableIndex {
        public string TableName { get; set; }
        public string IndexName { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string IndexExpression { get; set; }

        [ExcludeFromCodeCoverage]
        private string DebuggerDisplay() {
            return string.Format("TableName={0} | IndexName={1} |IsPrimaryKey={2} | IndexExpression={3}", TableName, IndexName, IsPrimaryKey, IndexExpression);
        }
    }
}