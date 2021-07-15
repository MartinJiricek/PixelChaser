
namespace PixelChaser
{
    partial class DebugWindow
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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.WorldTab = new System.Windows.Forms.TabPage();
            this.WordInfoBox = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.MaxSizeBox = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.MaxLimitBox = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.MaxUnitsPerRowBox = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.MaxUnitsBox = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.WorldTickBox = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.BottomMenu = new System.Windows.Forms.StatusStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WorldPropertiesBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.GeneratorPropertiesBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.ChaserPropertiesBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.WorldTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxSizeBox)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxLimitBox)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxUnitsPerRowBox)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxUnitsBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorldTickBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(800, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.WorldTab);
            this.Tabs.Controls.Add(this.tabPage2);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 24);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(800, 404);
            this.Tabs.TabIndex = 2;
            // 
            // WorldTab
            // 
            this.WorldTab.Controls.Add(this.WordInfoBox);
            this.WorldTab.Controls.Add(this.groupBox2);
            this.WorldTab.Location = new System.Drawing.Point(4, 24);
            this.WorldTab.Name = "WorldTab";
            this.WorldTab.Padding = new System.Windows.Forms.Padding(3);
            this.WorldTab.Size = new System.Drawing.Size(792, 376);
            this.WorldTab.TabIndex = 0;
            this.WorldTab.Text = "World Settings";
            this.WorldTab.UseVisualStyleBackColor = true;
            // 
            // WordInfoBox
            // 
            this.WordInfoBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.WordInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.WordInfoBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.WordInfoBox.Location = new System.Drawing.Point(539, 3);
            this.WordInfoBox.Name = "WordInfoBox";
            this.WordInfoBox.ReadOnly = true;
            this.WordInfoBox.Size = new System.Drawing.Size(250, 370);
            this.WordInfoBox.TabIndex = 1;
            this.WordInfoBox.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(144, 370);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.MaxSizeBox);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Location = new System.Drawing.Point(3, 215);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(138, 49);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Maximum pixel size";
            // 
            // MaxSizeBox
            // 
            this.MaxSizeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MaxSizeBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MaxSizeBox.Location = new System.Drawing.Point(3, 19);
            this.MaxSizeBox.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.MaxSizeBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxSizeBox.Name = "MaxSizeBox";
            this.MaxSizeBox.Size = new System.Drawing.Size(132, 23);
            this.MaxSizeBox.TabIndex = 0;
            this.MaxSizeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaxSizeBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxSizeBox.ValueChanged += new System.EventHandler(this.MaxSizeBox_ValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.MaxLimitBox);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(3, 166);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(138, 49);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Maximum pixel speed";
            // 
            // MaxLimitBox
            // 
            this.MaxLimitBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MaxLimitBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MaxLimitBox.Location = new System.Drawing.Point(3, 19);
            this.MaxLimitBox.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.MaxLimitBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxLimitBox.Name = "MaxLimitBox";
            this.MaxLimitBox.Size = new System.Drawing.Size(132, 23);
            this.MaxLimitBox.TabIndex = 0;
            this.MaxLimitBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaxLimitBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxLimitBox.ValueChanged += new System.EventHandler(this.MaxLimitBox_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.MaxUnitsPerRowBox);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 117);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(138, 49);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Maximum units per row";
            // 
            // MaxUnitsPerRowBox
            // 
            this.MaxUnitsPerRowBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MaxUnitsPerRowBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MaxUnitsPerRowBox.Location = new System.Drawing.Point(3, 19);
            this.MaxUnitsPerRowBox.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.MaxUnitsPerRowBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxUnitsPerRowBox.Name = "MaxUnitsPerRowBox";
            this.MaxUnitsPerRowBox.Size = new System.Drawing.Size(132, 23);
            this.MaxUnitsPerRowBox.TabIndex = 0;
            this.MaxUnitsPerRowBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaxUnitsPerRowBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxUnitsPerRowBox.ValueChanged += new System.EventHandler(this.MaxUnitsPerRowBox_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.MaxUnitsBox);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 68);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(138, 49);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Maximum units count";
            // 
            // MaxUnitsBox
            // 
            this.MaxUnitsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MaxUnitsBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MaxUnitsBox.Location = new System.Drawing.Point(3, 19);
            this.MaxUnitsBox.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.MaxUnitsBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxUnitsBox.Name = "MaxUnitsBox";
            this.MaxUnitsBox.Size = new System.Drawing.Size(132, 23);
            this.MaxUnitsBox.TabIndex = 0;
            this.MaxUnitsBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaxUnitsBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxUnitsBox.ValueChanged += new System.EventHandler(this.MaxUnitsBox_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.WorldTickBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tick interval";
            // 
            // WorldTickBox
            // 
            this.WorldTickBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorldTickBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.WorldTickBox.Location = new System.Drawing.Point(3, 19);
            this.WorldTickBox.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.WorldTickBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.WorldTickBox.Name = "WorldTickBox";
            this.WorldTickBox.Size = new System.Drawing.Size(132, 23);
            this.WorldTickBox.TabIndex = 0;
            this.WorldTickBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.WorldTickBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.WorldTickBox.ValueChanged += new System.EventHandler(this.WorldTickBox_ValueChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 376);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // BottomMenu
            // 
            this.BottomMenu.Location = new System.Drawing.Point(0, 428);
            this.BottomMenu.Name = "BottomMenu";
            this.BottomMenu.Size = new System.Drawing.Size(800, 22);
            this.BottomMenu.TabIndex = 3;
            this.BottomMenu.Text = "statusStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WorldPropertiesBtn,
            this.GeneratorPropertiesBtn,
            this.ChaserPropertiesBtn});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // WorldPropertiesBtn
            // 
            this.WorldPropertiesBtn.Name = "WorldPropertiesBtn";
            this.WorldPropertiesBtn.Size = new System.Drawing.Size(182, 22);
            this.WorldPropertiesBtn.Text = "World Properties";
            this.WorldPropertiesBtn.Click += new System.EventHandler(this.WorldPropertiesBtn_Click);
            // 
            // GeneratorPropertiesBtn
            // 
            this.GeneratorPropertiesBtn.Name = "GeneratorPropertiesBtn";
            this.GeneratorPropertiesBtn.Size = new System.Drawing.Size(182, 22);
            this.GeneratorPropertiesBtn.Text = "Generator Properties";
            this.GeneratorPropertiesBtn.Click += new System.EventHandler(this.GeneratorPropertiesBtn_Click);
            // 
            // ChaserPropertiesBtn
            // 
            this.ChaserPropertiesBtn.Name = "ChaserPropertiesBtn";
            this.ChaserPropertiesBtn.Size = new System.Drawing.Size(182, 22);
            this.ChaserPropertiesBtn.Text = "Chaser Properties";
            this.ChaserPropertiesBtn.Click += new System.EventHandler(this.ChaserPropertiesBtn_Click);
            // 
            // DebugWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.BottomMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "DebugWindow";
            this.Text = "DebugWindow";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.Tabs.ResumeLayout(false);
            this.WorldTab.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MaxSizeBox)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MaxLimitBox)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MaxUnitsPerRowBox)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MaxUnitsBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WorldTickBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage WorldTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.StatusStrip BottomMenu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown WorldTickBox;
        private System.Windows.Forms.RichTextBox WordInfoBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown MaxUnitsBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown MaxUnitsPerRowBox;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown MaxSizeBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown MaxLimitBox;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WorldPropertiesBtn;
        private System.Windows.Forms.ToolStripMenuItem GeneratorPropertiesBtn;
        private System.Windows.Forms.ToolStripMenuItem ChaserPropertiesBtn;
    }
}