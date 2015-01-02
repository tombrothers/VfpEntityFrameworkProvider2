using System;
using System.Data.Common;
using VfpClient;

namespace VfpEntityFrameworkProvider.VfpOleDb {
    /// <summary>
    /// VFP returns column names in lower case.  This was causing a problem when returning AutoInc values because EF used the GetName method
    /// to determine the Entity's property name.  This class simply overrides the GetName method to return the name in the expected case.
    /// </summary>
    internal class VfpAutoIncDataReader : VfpDataReader {
        private readonly string _commandText;

        public VfpAutoIncDataReader(DbDataReader reader, string commandText)
            : base(reader) {
            _commandText = commandText;
        }

        public override string GetName(int i) {
            var name = base.GetName(i);
            var padding = 1;

            var startIndex = _commandText.IndexOf(" " + name + Environment.NewLine, StringComparison.InvariantCultureIgnoreCase);

            if (startIndex == -1) {
                startIndex = _commandText.IndexOf(" " + name + ",", StringComparison.InvariantCultureIgnoreCase);

                if (startIndex == -1) {
                    startIndex = _commandText.IndexOf(" " + name, StringComparison.InvariantCultureIgnoreCase);

                    if (startIndex == -1) {
                        padding = 0;
                        startIndex = _commandText.IndexOf(name, StringComparison.InvariantCultureIgnoreCase);
                    }
                }
            }

            return _commandText.Substring(startIndex + padding, name.Length);
        }
    }
}