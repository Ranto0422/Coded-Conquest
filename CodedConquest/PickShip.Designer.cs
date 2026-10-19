namespace Game_test_2
{
    partial class PickShip
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
            this.InnovatorShip = new System.Windows.Forms.PictureBox();
            this.SentinelShip = new System.Windows.Forms.PictureBox();
            this.CyberNeticShip = new System.Windows.Forms.PictureBox();
            this.ChronosShip = new System.Windows.Forms.PictureBox();
            this.BackButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.InnovatorShip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SentinelShip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CyberNeticShip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChronosShip)).BeginInit();
            this.SuspendLayout();
            // 
            // InnovatorShip
            // 
            this.InnovatorShip.BackColor = System.Drawing.Color.Transparent;
            this.InnovatorShip.Location = new System.Drawing.Point(37, 323);
            this.InnovatorShip.Name = "InnovatorShip";
            this.InnovatorShip.Size = new System.Drawing.Size(376, 237);
            this.InnovatorShip.TabIndex = 1;
            this.InnovatorShip.TabStop = false;
            this.InnovatorShip.Click += new System.EventHandler(this.InnovatorShip_Click);
            // 
            // SentinelShip
            // 
            this.SentinelShip.BackColor = System.Drawing.Color.Transparent;
            this.SentinelShip.Location = new System.Drawing.Point(422, 372);
            this.SentinelShip.Name = "SentinelShip";
            this.SentinelShip.Size = new System.Drawing.Size(320, 308);
            this.SentinelShip.TabIndex = 2;
            this.SentinelShip.TabStop = false;
            this.SentinelShip.Click += new System.EventHandler(this.SentinelShip_Click);
            // 
            // CyberNeticShip
            // 
            this.CyberNeticShip.BackColor = System.Drawing.Color.Transparent;
            this.CyberNeticShip.Location = new System.Drawing.Point(419, 68);
            this.CyberNeticShip.Name = "CyberNeticShip";
            this.CyberNeticShip.Size = new System.Drawing.Size(335, 280);
            this.CyberNeticShip.TabIndex = 3;
            this.CyberNeticShip.TabStop = false;
            this.CyberNeticShip.Click += new System.EventHandler(this.CyberNeticShip_Click);
            // 
            // ChronosShip
            // 
            this.ChronosShip.BackColor = System.Drawing.Color.Transparent;
            this.ChronosShip.Location = new System.Drawing.Point(37, 0);
            this.ChronosShip.Name = "ChronosShip";
            this.ChronosShip.Size = new System.Drawing.Size(333, 256);
            this.ChronosShip.TabIndex = 4;
            this.ChronosShip.TabStop = false;
            this.ChronosShip.Click += new System.EventHandler(this.ChronosShip_Click);
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(13, 13);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(75, 23);
            this.BackButton.TabIndex = 5;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Bauhaus 93", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(273, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 32);
            this.label2.TabIndex = 7;
            this.label2.Text = "<Pick Your Ship>";
            // 
            // PickShip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Game_test_2.Properties.Resources.CCIS_SHIP_MENU;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(754, 749);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.ChronosShip);
            this.Controls.Add(this.CyberNeticShip);
            this.Controls.Add(this.SentinelShip);
            this.Controls.Add(this.InnovatorShip);
            this.DoubleBuffered = true;
            this.Name = "PickShip";
            this.Text = "PickShip";
            this.Load += new System.EventHandler(this.PickShip_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InnovatorShip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SentinelShip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CyberNeticShip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChronosShip)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox InnovatorShip;
        private System.Windows.Forms.PictureBox SentinelShip;
        private System.Windows.Forms.PictureBox CyberNeticShip;
        private System.Windows.Forms.PictureBox ChronosShip;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Label label2;
    }
}