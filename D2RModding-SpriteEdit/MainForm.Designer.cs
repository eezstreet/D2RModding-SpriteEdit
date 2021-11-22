
namespace D2RModding_SpriteEdit
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.combineFramesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.massTranslateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportFramesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.massExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate90ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate_180 = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate_90_clockwise = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate_90_counterclockwise = new System.Windows.Forms.ToolStripMenuItem();
            this.flipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flip_horizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.flip_vertical = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filter_hsv = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.d2RModdingDiscordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolbarText = new System.Windows.Forms.ToolStripStatusLabel();
            this.imagePreview = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.numFramesTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.frameSelectionComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.zoomTrackBar = new System.Windows.Forms.TrackBar();
            this.zoomAmountLabel = new System.Windows.Forms.Label();
            this.resetPan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.transformToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1029, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.importToolStripMenuItem,
            this.importFrameToolStripMenuItem,
            this.combineFramesToolStripMenuItem,
            this.massTranslateToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.massExportToolStripMenuItem,
            this.exportFrameToolStripMenuItem,
            this.exportFramesToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.saveToolStripMenuItem.Text = "Save As...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.importToolStripMenuItem.Text = "Import...";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.ImportToolStripMenuItem_Click);
            // 
            // importFrameToolStripMenuItem
            // 
            this.importFrameToolStripMenuItem.Name = "importFrameToolStripMenuItem";
            this.importFrameToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.importFrameToolStripMenuItem.Text = "Import Frame...";
            this.importFrameToolStripMenuItem.Click += new System.EventHandler(this.ImportFrameToolStripMenuItem_Click);
            // 
            // combineFramesToolStripMenuItem
            // 
            this.combineFramesToolStripMenuItem.Name = "combineFramesToolStripMenuItem";
            this.combineFramesToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.combineFramesToolStripMenuItem.Text = "Import Frames...";
            this.combineFramesToolStripMenuItem.Click += new System.EventHandler(this.CombineFramesToolStripMenuItem_Click);
            // 
            // massTranslateToolStripMenuItem
            // 
            this.massTranslateToolStripMenuItem.Name = "massTranslateToolStripMenuItem";
            this.massTranslateToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.massTranslateToolStripMenuItem.Text = "Mass Translate...";
            this.massTranslateToolStripMenuItem.Click += new System.EventHandler(this.MassTranslateToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Enabled = false;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exportToolStripMenuItem.Text = "Export...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItem_Click);
            // 
            // massExportToolStripMenuItem
            // 
            this.massExportToolStripMenuItem.Enabled = true;
            this.massExportToolStripMenuItem.Name = "massExportToolStripMenuItem";
            this.massExportToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.massExportToolStripMenuItem.Text = "Mass Export...";
            this.massExportToolStripMenuItem.Click += new System.EventHandler(this.MassExportToolStripMenuItem_Click);
            // 
            // exportFrameToolStripMenuItem
            // 
            this.exportFrameToolStripMenuItem.Enabled = false;
            this.exportFrameToolStripMenuItem.Name = "exportFrameToolStripMenuItem";
            this.exportFrameToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exportFrameToolStripMenuItem.Text = "Export Frame...";
            this.exportFrameToolStripMenuItem.Click += new System.EventHandler(this.ExportFrameToolStripMenuItem_Click);
            // 
            // exportFramesToolStripMenuItem
            // 
            this.exportFramesToolStripMenuItem.Enabled = false;
            this.exportFramesToolStripMenuItem.Name = "exportFramesToolStripMenuItem";
            this.exportFramesToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exportFramesToolStripMenuItem.Text = "Export Frames...";
            this.exportFramesToolStripMenuItem.Click += new System.EventHandler(this.ExportFramesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // transformToolStripMenuItem
            // 
            this.transformToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotate90ToolStripMenuItem,
            this.flipToolStripMenuItem,
            this.filtersToolStripMenuItem});
            this.transformToolStripMenuItem.Name = "transformToolStripMenuItem";
            this.transformToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.transformToolStripMenuItem.Text = "Transform...";
            // 
            // rotate90ToolStripMenuItem
            // 
            this.rotate90ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotate_180,
            this.rotate_90_clockwise,
            this.rotate_90_counterclockwise});
            this.rotate90ToolStripMenuItem.Name = "rotate90ToolStripMenuItem";
            this.rotate90ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.rotate90ToolStripMenuItem.Text = "Rotate...";
            // 
            // rotate_180
            // 
            this.rotate_180.Enabled = false;
            this.rotate_180.Name = "rotate_180";
            this.rotate_180.Size = new System.Drawing.Size(195, 22);
            this.rotate_180.Text = "180°";
            this.rotate_180.Click += new System.EventHandler(this.Rotate_180_Click);
            // 
            // rotate_90_clockwise
            // 
            this.rotate_90_clockwise.Enabled = false;
            this.rotate_90_clockwise.Name = "rotate_90_clockwise";
            this.rotate_90_clockwise.Size = new System.Drawing.Size(195, 22);
            this.rotate_90_clockwise.Text = "90° Clockwise";
            this.rotate_90_clockwise.Click += new System.EventHandler(this.Rotate_90_clockwise_Click);
            // 
            // rotate_90_counterclockwise
            // 
            this.rotate_90_counterclockwise.Enabled = false;
            this.rotate_90_counterclockwise.Name = "rotate_90_counterclockwise";
            this.rotate_90_counterclockwise.Size = new System.Drawing.Size(195, 22);
            this.rotate_90_counterclockwise.Text = "90° Counter-Clockwise";
            this.rotate_90_counterclockwise.Click += new System.EventHandler(this.Rotate_90_counterclockwise_Click);
            // 
            // flipToolStripMenuItem
            // 
            this.flipToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.flip_horizontal,
            this.flip_vertical});
            this.flipToolStripMenuItem.Name = "flipToolStripMenuItem";
            this.flipToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.flipToolStripMenuItem.Text = "Flip...";
            // 
            // flip_horizontal
            // 
            this.flip_horizontal.Enabled = false;
            this.flip_horizontal.Name = "flip_horizontal";
            this.flip_horizontal.Size = new System.Drawing.Size(129, 22);
            this.flip_horizontal.Text = "Horizontal";
            this.flip_horizontal.Click += new System.EventHandler(this.Flip_horizontal_Click);
            // 
            // flip_vertical
            // 
            this.flip_vertical.Enabled = false;
            this.flip_vertical.Name = "flip_vertical";
            this.flip_vertical.Size = new System.Drawing.Size(129, 22);
            this.flip_vertical.Text = "Vertical";
            this.flip_vertical.Click += new System.EventHandler(this.Flip_vertical_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filter_hsv});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.filtersToolStripMenuItem.Text = "Filters...";
            // 
            // filter_hsv
            // 
            this.filter_hsv.Enabled = false;
            this.filter_hsv.Name = "filter_hsv";
            this.filter_hsv.Size = new System.Drawing.Size(155, 22);
            this.filter_hsv.Text = "Hue/Saturation";
            this.filter_hsv.Click += new System.EventHandler(this.filter_hsv_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.d2RModdingDiscordToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // d2RModdingDiscordToolStripMenuItem
            // 
            this.d2RModdingDiscordToolStripMenuItem.Name = "d2RModdingDiscordToolStripMenuItem";
            this.d2RModdingDiscordToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.d2RModdingDiscordToolStripMenuItem.Text = "D2RModding Discord";
            this.d2RModdingDiscordToolStripMenuItem.Click += new System.EventHandler(this.D2RModdingDiscordToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbarText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 631);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1029, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolbarText
            // 
            this.toolbarText.Name = "toolbarText";
            this.toolbarText.Size = new System.Drawing.Size(101, 17);
            this.toolbarText.Text = "No Image Loaded";
            // 
            // imagePreview
            // 
            this.imagePreview.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.imagePreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePreview.Location = new System.Drawing.Point(0, 24);
            this.imagePreview.Name = "imagePreview";
            this.imagePreview.Size = new System.Drawing.Size(1029, 607);
            this.imagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imagePreview.TabIndex = 2;
            this.imagePreview.TabStop = false;
            this.imagePreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.imagePreview.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            this.imagePreview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.imagePreview.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.numFramesTextBox,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.frameSelectionComboBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1029, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(106, 22);
            this.toolStripLabel1.Text = "Number of Frames";
            // 
            // numFramesTextBox
            // 
            this.numFramesTextBox.Enabled = false;
            this.numFramesTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numFramesTextBox.Name = "numFramesTextBox";
            this.numFramesTextBox.Size = new System.Drawing.Size(100, 25);
            this.numFramesTextBox.TextChanged += new System.EventHandler(this.OnFrameCountChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(83, 22);
            this.toolStripLabel2.Text = "Current Frame";
            // 
            // frameSelectionComboBox
            // 
            this.frameSelectionComboBox.Enabled = false;
            this.frameSelectionComboBox.Name = "frameSelectionComboBox";
            this.frameSelectionComboBox.Size = new System.Drawing.Size(121, 25);
            this.frameSelectionComboBox.TextChanged += new System.EventHandler(this.OnFrameComboBoxChanged);
            // 
            // zoomTrackBar
            // 
            this.zoomTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomTrackBar.LargeChange = 50;
            this.zoomTrackBar.Location = new System.Drawing.Point(913, 583);
            this.zoomTrackBar.Maximum = 200;
            this.zoomTrackBar.Minimum = 50;
            this.zoomTrackBar.Name = "zoomTrackBar";
            this.zoomTrackBar.Size = new System.Drawing.Size(104, 45);
            this.zoomTrackBar.SmallChange = 10;
            this.zoomTrackBar.TabIndex = 4;
            this.zoomTrackBar.TickFrequency = 50;
            this.zoomTrackBar.Value = 100;
            this.zoomTrackBar.Scroll += new System.EventHandler(this.ZoomTrackBar_Scroll);
            // 
            // zoomAmountLabel
            // 
            this.zoomAmountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomAmountLabel.AutoSize = true;
            this.zoomAmountLabel.Location = new System.Drawing.Point(952, 567);
            this.zoomAmountLabel.Name = "zoomAmountLabel";
            this.zoomAmountLabel.Size = new System.Drawing.Size(33, 13);
            this.zoomAmountLabel.TabIndex = 5;
            this.zoomAmountLabel.Text = "100%";
            this.zoomAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // resetPan
            // 
            this.resetPan.Location = new System.Drawing.Point(814, 583);
            this.resetPan.Name = "resetPan";
            this.resetPan.Size = new System.Drawing.Size(75, 23);
            this.resetPan.TabIndex = 6;
            this.resetPan.Text = "Reset Pan";
            this.resetPan.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(867, 543);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "PREVIEW";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 653);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resetPan);
            this.Controls.Add(this.zoomAmountLabel);
            this.Controls.Add(this.zoomTrackBar);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.imagePreview);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "SpriteEdit";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolbarText;
        private System.Windows.Forms.PictureBox imagePreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem d2RModdingDiscordToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TrackBar zoomTrackBar;
        private System.Windows.Forms.Label zoomAmountLabel;
        private System.Windows.Forms.Button resetPan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem transformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotate90ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotate_180;
        private System.Windows.Forms.ToolStripMenuItem rotate_90_clockwise;
        private System.Windows.Forms.ToolStripMenuItem rotate_90_counterclockwise;
        private System.Windows.Forms.ToolStripMenuItem flipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flip_horizontal;
        private System.Windows.Forms.ToolStripMenuItem flip_vertical;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filter_hsv;
        private System.Windows.Forms.ToolStripMenuItem importFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem combineFramesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem massTranslateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportFramesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem massExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox numFramesTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox frameSelectionComboBox;
    }
}

