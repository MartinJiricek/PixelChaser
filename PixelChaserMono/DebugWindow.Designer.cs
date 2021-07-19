
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
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WorldPropertiesBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.GeneratorPropertiesBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.ChaserPropertiesBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomMenu = new System.Windows.Forms.StatusStrip();
            this.GamePropertiesBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(462, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WorldPropertiesBtn,
            this.GeneratorPropertiesBtn,
            this.ChaserPropertiesBtn,
            this.GamePropertiesBtn});
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
            // BottomMenu
            // 
            this.BottomMenu.Location = new System.Drawing.Point(0, 126);
            this.BottomMenu.Name = "BottomMenu";
            this.BottomMenu.Size = new System.Drawing.Size(462, 22);
            this.BottomMenu.TabIndex = 3;
            this.BottomMenu.Text = "statusStrip1";
            // 
            // GamePropertiesBtn
            // 
            this.GamePropertiesBtn.Name = "GamePropertiesBtn";
            this.GamePropertiesBtn.Size = new System.Drawing.Size(182, 22);
            this.GamePropertiesBtn.Text = "Game Properties";
            this.GamePropertiesBtn.Click += new System.EventHandler(this.GamePropertiesBtn_Click);
            // 
            // DebugWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 148);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.BottomMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "DebugWindow";
            this.Text = "DebugWindow";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.StatusStrip BottomMenu;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WorldPropertiesBtn;
        private System.Windows.Forms.ToolStripMenuItem GeneratorPropertiesBtn;
        private System.Windows.Forms.ToolStripMenuItem ChaserPropertiesBtn;
        private System.Windows.Forms.ToolStripMenuItem GamePropertiesBtn;
    }
}