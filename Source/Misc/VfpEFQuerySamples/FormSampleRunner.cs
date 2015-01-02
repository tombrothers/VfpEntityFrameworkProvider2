using System;
using System.Configuration;
using System.Windows.Forms;
using SampleQueries.Dumper;
using SampleQueries.Harness;
using SampleQueries.Runner;
using SampleQueries.Utils;

namespace SampleQueries {
    internal partial class FormSampleRunner : Form {
        private Sample _currentSample;
        private SampleRunner _runner;

        public FormSampleRunner(SampleGroup rootSampleGroup) {
            InitializeComponent();

            Text = rootSampleGroup.Title;

            ObjectDumper dumper = new ObjectDumper();
            dumper.TreeView = treeViewOutput;
            dumper.OutputTextBox = outputTextBox;
            dumper.GeneratedSqlTextBox = traceTextBox;
            dumper.GeneratedVfpTextBox = vfpTraceTextBox;

            _runner = new SampleRunnerImpl(this, dumper);

            TreeNode rootNode = CreateTreeNode(rootSampleGroup, 0);

            rootNode.ImageKey = rootNode.SelectedImageKey = "Help";
            samplesTreeView.Nodes.Add(rootNode);
            rootNode.Expand();
        }

        private TreeNode CreateTreeNode(SampleBase sample, int level) {
            TreeNode node = new TreeNode(sample.Title);
            node.Tag = sample;
            SampleGroup sg = sample as SampleGroup;
            if (sg != null) {
                foreach (SampleBase sb in sg.Children) {
                    node.Nodes.Add(CreateTreeNode(sb, level + 1));
                }
                node.ImageKey = node.SelectedImageKey = "BookClosed";
            }
            else {
                node.ImageKey = node.SelectedImageKey = "Item";
            }
            return node;
        }
        private void samplesTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
            TreeNode currentNode = samplesTreeView.SelectedNode;
            _currentSample = currentNode.Tag as Sample;
            outputTextBox.Clear();
            traceTextBox.Clear();
            treeViewOutput.Nodes.Clear();
            if (_currentSample != null) {
                runButton.Enabled = true;
                descriptionTextBox.Text = _currentSample.Description;
                codeRichTextBox.Clear();
                codeRichTextBox.Text = _currentSample.SourceCode;
                SyntaxColorizer.ColorizeCode(codeRichTextBox);
            }
            else {
                runButton.Enabled = false;
                descriptionTextBox.Text = "Select a query from the tree to the left.";
                codeRichTextBox.Clear();
            }
        }

        private void samplesTreeView_AfterExpand(object sender, TreeViewEventArgs e) {
            switch (e.Node.Level) {
                case 1:
                case 2:
                    e.Node.ImageKey = "BookOpen";
                    e.Node.SelectedImageKey = "BookOpen";
                    break;
            }
        }

        private void samplesTreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e) {
            switch (e.Node.Level) {
                case 0:
                    e.Cancel = true;
                    break;
            }
        }

        private void samplesTreeView_AfterCollapse(object sender, TreeViewEventArgs e) {
            switch (e.Node.Level) {
                case 1:
                    e.Node.ImageKey = "BookStack";
                    e.Node.SelectedImageKey = "BookStack";
                    break;

                case 2:
                    e.Node.ImageKey = "BookClosed";
                    e.Node.SelectedImageKey = "BookClosed";
                    break;
            }
        }

        private void samplesTreeView_DoubleClick(object sender, EventArgs e) {
            if (this._currentSample != null) {
                RunCurrentSample();
            }
        }

        private void runButton_Click(object sender, EventArgs e) {
            RunCurrentSample();
        }

        private void RunCurrentSample() {
            _runner.ConnectionString = ((ConnectionStringSettings)comboBoxConnectionString.SelectedItem).ConnectionString;
            _runner.Run(_currentSample);
        }

        private void SampleForm_Load(object sender, EventArgs e) {
            comboBoxConnectionString.DisplayMember = "Name";
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings) {
                if (css.ProviderName != "System.Data.EntityClient")
                    continue;

                comboBoxConnectionString.Items.Add(css);
            }
            comboBoxConnectionString.SelectedIndex = 0;
        }

        class SampleRunnerImpl : SampleRunner {
            private FormSampleRunner _form;

            internal SampleRunnerImpl(FormSampleRunner form, IObjectDumper dumper)
                : base(dumper) {
                _form = form;
            }

            public override void OnStarting(Sample sample) {
                _form.treeViewOutput.Nodes.Clear();
            }

            public override void OnSuccess(Sample sample) {
            }

            public override void OnFailure(Sample sample, Exception ex) {
                this.ObjectDumper.Write(ex);

            }

            public override void OnStartingGroup(SampleGroup group) {
            }

            public override void OnFinishedGroup(SampleGroup group) {
            }
        }
    }
}