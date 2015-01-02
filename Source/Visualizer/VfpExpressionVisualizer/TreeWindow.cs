using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace VfpExpressionVisualizer {
    public partial class TreeWindow : Form {
        public TreeWindow() {
            InitializeComponent();
        }

        public TreeWindow(VfpExpressionTreeNode tree) {
            InitializeComponent();
            treeView.Nodes.Add(tree);
            treeView.ExpandAll();
        }

        private void button1_Click(object sender, System.EventArgs e) {
            var text = textBox1.Text.ToUpper();

            foreach (var node in treeView.Nodes.Cast<TreeNode>()) {
                HighlightNodes(node, text);
            }
        }

        private void HighlightNodes(TreeNode parentNode, string text) {
            parentNode.BackColor = parentNode.Text.ToUpper().Contains(text) ? Color.Yellow : Color.Transparent;

            foreach (var childNode in parentNode.Nodes.Cast<TreeNode>()) {
                HighlightNodes(childNode, text);
            }
        }
    }
}