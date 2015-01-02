using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Text;
using VfpEntityFrameworkProvider.Visitors;

namespace VfpEntityFrameworkProvider.SqlGeneration {
    internal sealed class SqlGenerator {
        internal static string GenerateSql(VfpProviderManifest vfpManifest, DbCommandTree commandTree, out List<DbParameter> parameters, out CommandType commandType) {
            commandType = CommandType.Text;
            parameters = null;

            //Handle Query
            var queryCommandTree = commandTree as DbQueryCommandTree;

            if (queryCommandTree != null) {
                var sqlSelectVisitor = new SqlVisitor();
                var sqlFragment = sqlSelectVisitor.Visit(vfpManifest, queryCommandTree, out parameters);

                return WriteSql(sqlFragment, sqlSelectVisitor);
            }

            //Handle Function
            var functionCommandTree = commandTree as DbFunctionCommandTree;

            if (functionCommandTree != null) {
                return DmlSqlFormatter.GenerateFunctionSql(functionCommandTree, out commandType);
            }

            //Handle Insert
            var insertCommandTree = commandTree as DbInsertCommandTree;

            if (insertCommandTree != null) {
                return DmlSqlFormatter.GenerateInsertSql(vfpManifest, insertCommandTree, out parameters);
            }

            //Handle Delete
            var deleteCommandTree = commandTree as DbDeleteCommandTree;

            if (deleteCommandTree != null) {
                return DmlSqlFormatter.GenerateDeleteSql(vfpManifest, deleteCommandTree, out parameters);
            }

            //Handle Update
            var updateCommandTree = commandTree as DbUpdateCommandTree;

            if (updateCommandTree != null) {
                return DmlSqlFormatter.GenerateUpdateSql(vfpManifest, updateCommandTree, out parameters);
            }

            return null;

        }

        public static string WriteSql(ISqlFragment sqlStatement, SqlVisitor visitor) {
            var builder = new StringBuilder(1024);

            using (var writer = new SqlWriter(builder)) {
                sqlStatement.WriteSql(writer, visitor);
            }

            return builder.ToString();
        }
    }
}