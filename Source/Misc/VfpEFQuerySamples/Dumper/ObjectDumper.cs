using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using SampleQueries.Harness;
using VfpClient;

namespace SampleQueries.Dumper {
    public class ObjectDumper : IObjectDumper {
        public TreeView TreeView { get; set; }
        public TextBox GeneratedSqlTextBox { get; set; }
        public TextBox GeneratedVfpTextBox { get; set; }
        public TextBox OutputTextBox { get; set; }

        public static bool CatchExceptions = true;
        private readonly StringBuilderTraceListener _traceListener;

        public ObjectDumper() {
            _traceListener = new StringBuilderTraceListener();
            VfpClientTracing.Tracer = new TraceSource("VfpClient", SourceLevels.Information);
            VfpClientTracing.Tracer.Listeners.Add(_traceListener);
        }

        public void Write(object o) {
            Write(o, 0);
        }

        public void Write(object o, int expandDepth) {
            Write(o, expandDepth, 10);
        }

        public void Write(object o, int expandDepth, int maxLoadDepth) {
            if (o != null && o is IEnumerable && CatchExceptions) {
                // ObjectResult<T> collections cannot be iterated twice, so we cache them
                // in an ObjectCollectionCache object

                o = new ObjectCollectionCache(o);
            }

            try {
                if (TreeView != null) {
                    TreeGenerator gen = new TreeGenerator(Math.Max(expandDepth, 1), maxLoadDepth);
                    TreeNode node = gen.GetTreeNode(o);
                    TreeView.Nodes.Add(node);
                }
                if (o != null && GeneratedSqlTextBox != null) {
                    MethodInfo mi = o.GetType().GetMethod("ToTraceString");
                    if (mi != null)
                        GeneratedSqlTextBox.Text = (string)mi.Invoke(o, null);
                }
                if (o != null && GeneratedVfpTextBox != null) {
                    GeneratedVfpTextBox.Text = _traceListener.ToString();
                    _traceListener.Clear();
                }
                if (OutputTextBox != null) {
                    StringWriter sw = new StringWriter();
                    PrettyPrinter printer = new PrettyPrinter(sw, maxLoadDepth);
                    printer.Write(o);
                    OutputTextBox.Text = sw.ToString();
                }
            }
            catch (Exception ex) {
                if (!CatchExceptions)
                    throw;
                MessageBox.Show("EXCEPTION: " + ex.ToString());
            }
        }

        private class StringBuilderTraceListener : TraceListener {
            private readonly StringBuilder _stringBuilder = new StringBuilder();

            public override void Write(string message) {
                _stringBuilder.Append(message);
            }

            public override void WriteLine(string message) {
                _stringBuilder.AppendLine(message);
            }

            public void Clear() {
                _stringBuilder.Clear();
            }

            public override string ToString() {
                return _stringBuilder.ToString();
            }
        }
    }
}