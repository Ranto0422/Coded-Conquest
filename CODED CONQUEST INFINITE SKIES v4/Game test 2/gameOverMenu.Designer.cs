using System;
using System.Windows.Forms;
using System.Drawing;

namespace Game_test_2
{
    partial class GameOverMenu
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnRestart;
        private Button btnMainMenu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnMainMenu = new System.Windows.Forms.Button();
            this.GameOverSign = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(50, 50);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(200, 50);
            this.btnRestart.TabIndex = 0;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.BtnRestart_Click);
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Location = new System.Drawing.Point(50, 120);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(200, 50);
            this.btnMainMenu.TabIndex = 1;
            this.btnMainMenu.Text = "Main Menu";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new System.EventHandler(this.BtnMainMenu_Click);
            // 
            // GameOverSign
            // 
            this.GameOverSign.BackColor = System.Drawing.SystemColors.Menu;
            this.GameOverSign.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameOverSign.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GameOverSign.Location = new System.Drawing.Point(50, 16);
            this.GameOverSign.Name = "GameOverSign";
            this.GameOverSign.Size = new System.Drawing.Size(200, 27);
            this.GameOverSign.TabIndex = 2;
            this.GameOverSign.Text = "GAME OVER";
            this.GameOverSign.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GameOverSign.TextChanged += new System.EventHandler(this.GameOverSign_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // GameOverMenu
            // 
            this.Controls.Add(this.GameOverSign);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnMainMenu);
            this.Name = "GameOverMenu";
            this.Size = new System.Drawing.Size(300, 200);
            this.Load += new System.EventHandler(this.GameOverMenu_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private TextBox GameOverSign;
        private ContextMenuStrip contextMenuStrip1;
    }
}