//Copyright (C) Microsoft Corporation.  All rights reserved.

namespace SampleQueries
{
    internal partial class FormSampleRunner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSampleRunner));
            this.outerSplitContainer = new System.Windows.Forms.SplitContainer();
            this.comboBoxConnectionString = new System.Windows.Forms.ComboBox();
            this.samplesTreeView = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.samplesLabel = new System.Windows.Forms.Label();
            this.rightContainer = new System.Windows.Forms.SplitContainer();
            this.rightUpperSplitContainer = new System.Windows.Forms.SplitContainer();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.codeRichTextBox = new System.Windows.Forms.RichTextBox();
            this.codeLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.treeViewOutput = new System.Windows.Forms.TreeView();
            this.imageListResults = new System.Windows.Forms.ImageList(this.components);
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.traceTextBox = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.vfpTraceTextBox = new System.Windows.Forms.TextBox();
            this.runButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.outerSplitContainer)).BeginInit();
            this.outerSplitContainer.Panel1.SuspendLayout();
            this.outerSplitContainer.Panel2.SuspendLayout();
            this.outerSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightContainer)).BeginInit();
            this.rightContainer.Panel1.SuspendLayout();
            this.rightContainer.Panel2.SuspendLayout();
            this.rightContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightUpperSplitContainer)).BeginInit();
            this.rightUpperSplitContainer.Panel1.SuspendLayout();
            this.rightUpperSplitContainer.Panel2.SuspendLayout();
            this.rightUpperSplitContainer.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // outerSplitContainer
            // 
            this.outerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outerSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.outerSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.outerSplitContainer.Name = "outerSplitContainer";
            // 
            // outerSplitContainer.Panel1
            // 
            this.outerSplitContainer.Panel1.Controls.Add(this.comboBoxConnectionString);
            this.outerSplitContainer.Panel1.Controls.Add(this.samplesTreeView);
            this.outerSplitContainer.Panel1.Controls.Add(this.label1);
            this.outerSplitContainer.Panel1.Controls.Add(this.samplesLabel);
            // 
            // outerSplitContainer.Panel2
            // 
            this.outerSplitContainer.Panel2.Controls.Add(this.rightContainer);
            this.outerSplitContainer.Size = new System.Drawing.Size(952, 682);
            this.outerSplitContainer.SplitterDistance = 268;
            this.outerSplitContainer.TabIndex = 0;
            // 
            // comboBoxConnectionString
            // 
            this.comboBoxConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxConnectionString.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConnectionString.FormattingEnabled = true;
            this.comboBoxConnectionString.Location = new System.Drawing.Point(0, 28);
            this.comboBoxConnectionString.Name = "comboBoxConnectionString";
            this.comboBoxConnectionString.Size = new System.Drawing.Size(266, 21);
            this.comboBoxConnectionString.TabIndex = 2;
            // 
            // samplesTreeView
            // 
            this.samplesTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.samplesTreeView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.samplesTreeView.HideSelection = false;
            this.samplesTreeView.ImageKey = "Item";
            this.samplesTreeView.ImageList = this.imageList;
            this.samplesTreeView.Location = new System.Drawing.Point(0, 71);
            this.samplesTreeView.Name = "samplesTreeView";
            this.samplesTreeView.SelectedImageKey = "Item";
            this.samplesTreeView.ShowNodeToolTips = true;
            this.samplesTreeView.ShowRootLines = false;
            this.samplesTreeView.Size = new System.Drawing.Size(266, 611);
            this.samplesTreeView.TabIndex = 1;
            this.samplesTreeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.samplesTreeView_BeforeCollapse);
            this.samplesTreeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.samplesTreeView_AfterCollapse);
            this.samplesTreeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.samplesTreeView_AfterExpand);
            this.samplesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.samplesTreeView_AfterSelect);
            this.samplesTreeView.DoubleClick += new System.EventHandler(this.samplesTreeView_DoubleClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Help");
            this.imageList.Images.SetKeyName(1, "BookStack");
            this.imageList.Images.SetKeyName(2, "BookClosed");
            this.imageList.Images.SetKeyName(3, "BookOpen");
            this.imageList.Images.SetKeyName(4, "Item");
            this.imageList.Images.SetKeyName(5, "Run");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection String:";
            // 
            // samplesLabel
            // 
            this.samplesLabel.AutoSize = true;
            this.samplesLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.samplesLabel.Location = new System.Drawing.Point(3, 52);
            this.samplesLabel.Name = "samplesLabel";
            this.samplesLabel.Size = new System.Drawing.Size(62, 16);
            this.samplesLabel.TabIndex = 0;
            this.samplesLabel.Text = "Samples:";
            // 
            // rightContainer
            // 
            this.rightContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightContainer.Location = new System.Drawing.Point(0, 0);
            this.rightContainer.Name = "rightContainer";
            this.rightContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rightContainer.Panel1
            // 
            this.rightContainer.Panel1.Controls.Add(this.rightUpperSplitContainer);
            // 
            // rightContainer.Panel2
            // 
            this.rightContainer.Panel2.Controls.Add(this.tabControl1);
            this.rightContainer.Panel2.Controls.Add(this.runButton);
            this.rightContainer.Size = new System.Drawing.Size(680, 682);
            this.rightContainer.SplitterDistance = 357;
            this.rightContainer.TabIndex = 0;
            // 
            // rightUpperSplitContainer
            // 
            this.rightUpperSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightUpperSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.rightUpperSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.rightUpperSplitContainer.Name = "rightUpperSplitContainer";
            this.rightUpperSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rightUpperSplitContainer.Panel1
            // 
            this.rightUpperSplitContainer.Panel1.Controls.Add(this.descriptionTextBox);
            this.rightUpperSplitContainer.Panel1.Controls.Add(this.descriptionLabel);
            // 
            // rightUpperSplitContainer.Panel2
            // 
            this.rightUpperSplitContainer.Panel2.Controls.Add(this.codeRichTextBox);
            this.rightUpperSplitContainer.Panel2.Controls.Add(this.codeLabel);
            this.rightUpperSplitContainer.Size = new System.Drawing.Size(680, 357);
            this.rightUpperSplitContainer.SplitterDistance = 95;
            this.rightUpperSplitContainer.TabIndex = 0;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.descriptionTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionTextBox.Location = new System.Drawing.Point(0, 28);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(680, 67);
            this.descriptionTextBox.TabIndex = 1;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLabel.Location = new System.Drawing.Point(3, 9);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(76, 16);
            this.descriptionLabel.TabIndex = 0;
            this.descriptionLabel.Text = "Description:";
            // 
            // codeRichTextBox
            // 
            this.codeRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeRichTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.codeRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codeRichTextBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeRichTextBox.Location = new System.Drawing.Point(0, 18);
            this.codeRichTextBox.Name = "codeRichTextBox";
            this.codeRichTextBox.ReadOnly = true;
            this.codeRichTextBox.Size = new System.Drawing.Size(680, 240);
            this.codeRichTextBox.TabIndex = 1;
            this.codeRichTextBox.Text = "";
            this.codeRichTextBox.WordWrap = false;
            // 
            // codeLabel
            // 
            this.codeLabel.AutoSize = true;
            this.codeLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeLabel.Location = new System.Drawing.Point(3, -1);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Size = new System.Drawing.Size(42, 16);
            this.codeLabel.TabIndex = 0;
            this.codeLabel.Text = "Code:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(680, 286);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.treeViewOutput);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(672, 260);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Output";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // treeViewOutput
            // 
            this.treeViewOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewOutput.ImageIndex = 0;
            this.treeViewOutput.ImageList = this.imageListResults;
            this.treeViewOutput.Location = new System.Drawing.Point(3, 3);
            this.treeViewOutput.Name = "treeViewOutput";
            this.treeViewOutput.SelectedImageIndex = 0;
            this.treeViewOutput.Size = new System.Drawing.Size(666, 254);
            this.treeViewOutput.TabIndex = 0;
            // 
            // imageListResults
            // 
            this.imageListResults.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListResults.ImageStream")));
            this.imageListResults.TransparentColor = System.Drawing.Color.Magenta;
            this.imageListResults.Images.SetKeyName(0, "VSObject_Field.bmp");
            this.imageListResults.Images.SetKeyName(1, "VSObject_Class.bmp");
            this.imageListResults.Images.SetKeyName(2, "VSObject_Structure.bmp");
            this.imageListResults.Images.SetKeyName(3, "VSObject_ValueType.bmp");
            this.imageListResults.Images.SetKeyName(4, "VSObject_Namespace.bmp");
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.outputTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(672, 260);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Text Output";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // outputTextBox
            // 
            this.outputTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputTextBox.Location = new System.Drawing.Point(3, 3);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTextBox.Size = new System.Drawing.Size(666, 254);
            this.outputTextBox.TabIndex = 4;
            this.outputTextBox.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.traceTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(672, 260);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Generated SQL";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // traceTextBox
            // 
            this.traceTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.traceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.traceTextBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.traceTextBox.Location = new System.Drawing.Point(3, 3);
            this.traceTextBox.Multiline = true;
            this.traceTextBox.Name = "traceTextBox";
            this.traceTextBox.ReadOnly = true;
            this.traceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.traceTextBox.Size = new System.Drawing.Size(666, 254);
            this.traceTextBox.TabIndex = 3;
            this.traceTextBox.WordWrap = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.vfpTraceTextBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(672, 260);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Vfp Trace";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // vfpTraceTextBox
            // 
            this.vfpTraceTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.vfpTraceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vfpTraceTextBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vfpTraceTextBox.Location = new System.Drawing.Point(0, 0);
            this.vfpTraceTextBox.Multiline = true;
            this.vfpTraceTextBox.Name = "vfpTraceTextBox";
            this.vfpTraceTextBox.ReadOnly = true;
            this.vfpTraceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.vfpTraceTextBox.Size = new System.Drawing.Size(672, 260);
            this.vfpTraceTextBox.TabIndex = 4;
            this.vfpTraceTextBox.WordWrap = false;
            // 
            // runButton
            // 
            this.runButton.Enabled = false;
            this.runButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.runButton.ImageKey = "Run";
            this.runButton.ImageList = this.imageList;
            this.runButton.Location = new System.Drawing.Point(0, -1);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(119, 27);
            this.runButton.TabIndex = 0;
            this.runButton.Text = " &Run Sample!";
            this.runButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // FormSampleRunner
            // 
            this.AcceptButton = this.runButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 682);
            this.Controls.Add(this.outerSplitContainer);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSampleRunner";
            this.Text = "Samples";
            this.Load += new System.EventHandler(this.SampleForm_Load);
            this.outerSplitContainer.Panel1.ResumeLayout(false);
            this.outerSplitContainer.Panel1.PerformLayout();
            this.outerSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outerSplitContainer)).EndInit();
            this.outerSplitContainer.ResumeLayout(false);
            this.rightContainer.Panel1.ResumeLayout(false);
            this.rightContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightContainer)).EndInit();
            this.rightContainer.ResumeLayout(false);
            this.rightUpperSplitContainer.Panel1.ResumeLayout(false);
            this.rightUpperSplitContainer.Panel1.PerformLayout();
            this.rightUpperSplitContainer.Panel2.ResumeLayout(false);
            this.rightUpperSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightUpperSplitContainer)).EndInit();
            this.rightUpperSplitContainer.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer outerSplitContainer;
        private System.Windows.Forms.Label samplesLabel;
        private System.Windows.Forms.SplitContainer rightContainer;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.SplitContainer rightUpperSplitContainer;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label codeLabel;
        private System.Windows.Forms.TreeView samplesTreeView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.RichTextBox codeRichTextBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox traceTextBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView treeViewOutput;
        private System.Windows.Forms.ImageList imageListResults;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.ComboBox comboBoxConnectionString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox vfpTraceTextBox;
    }
}