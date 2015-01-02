using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace VfpEntityFrameworkProvider.SqlGeneration {
    internal class SqlBuilder : SqlFragmentBase {
        private List<object> sqlFragments;
        internal List<object> SqlFragments {
            get { return sqlFragments ?? (sqlFragments = new List<object>()); }
        }

        public SqlBuilder() : base(SqlFragmentType.SqlBuilder) {
        }

        public void Clear() {
            sqlFragments = null;
        }

        public void Append(object s) {
            Debug.Assert(s != null);
            SqlFragments.Add(s);
        }

        public void AppendLine() {
            SqlFragments.Add("\r\n");
        }

        public bool IsEmpty {
            get { return ((null == sqlFragments) || (0 == sqlFragments.Count)); }
        }

        public override void WriteSql(SqlWriter writer, SqlVisitor visitor) {
            if (null == sqlFragments) {
                return;
            }

            foreach (var o in sqlFragments) {
                var str = (o as String);
                if (null != str) {
                    writer.Write(str);
                }
                else {
                    var sqlFragment = (o as ISqlFragment);

                    if (null != sqlFragment) {
                        sqlFragment.WriteSql(writer, visitor);
                    }
                    else {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}