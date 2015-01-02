using System.IO;
using System.Text;

namespace VfpEntityFrameworkProvider.SqlGeneration {
    class SqlWriter : StringWriter {
        internal int Indent { get; set; }

        private bool _isAtBeginningOfLine = true;

        public SqlWriter(StringBuilder b)
            : base(b, System.Globalization.CultureInfo.InvariantCulture) {
            Indent = -1;
        }

        public override void Write(string value) {
            if (value == "\r\n") {
                base.WriteLine();

                _isAtBeginningOfLine = true;
            }
            else {
                if (_isAtBeginningOfLine) {
                    if (Indent > 0) {
                        base.Write(new string('\t', Indent));
                    }

                    _isAtBeginningOfLine = false;
                }

                base.Write(value);
            }
        }

        public override void WriteLine() {
            base.WriteLine();

            _isAtBeginningOfLine = true;
        }
    }
}