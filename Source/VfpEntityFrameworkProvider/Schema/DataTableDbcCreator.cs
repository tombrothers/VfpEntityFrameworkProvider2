using System;
using System.IO;

namespace VfpEntityFrameworkProvider.Schema {
    public class DataTableDbcCreator : VfpClient.Utils.DbcCreator.DataTableDbcCreator {
        public DataTableDbcCreator()
            : base(GetDbcPath(), new DataTableToTableConverter(), new DbcFilesProvider()) {
        }

        private static string GetDbcPath() {
            var path = Path.Combine(Path.Combine(Path.GetTempPath(), "VfpEfProvider"), DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Guid.NewGuid().ToString());

            return Path.Combine(path, "VfpEFProviderSchema.dbc");
        }
    }
}