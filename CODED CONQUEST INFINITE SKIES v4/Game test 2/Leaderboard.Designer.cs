namespace Game_test_2
{
    partial class Leaderboard
    {
        private System.ComponentModel.IContainer components = null;

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
            this.lblLeaderboard = new System.Windows.Forms.Label();
            this.GoBack_Click = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLeaderboard
            // 
            this.lblLeaderboard.Font = new System.Drawing.Font("Perpetua Titling MT", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeaderboard.Location = new System.Drawing.Point(50, 30);
            this.lblLeaderboard.Name = "lblLeaderboard";
            this.lblLeaderboard.Size = new System.Drawing.Size(400, 300);
            this.lblLeaderboard.TabIndex = 0;
            this.lblLeaderboard.Text = "Game Leaderboard";
            this.lblLeaderboard.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblLeaderboard.Click += new System.EventHandler(this.lblLeaderboard_Click);
            //
            // Leaderboard Scores
            //
            this.lblScores = new System.Windows.Forms.Label();
            this.lblScores.AutoSize = false; // Make it a fixed-size label to apply background and border
            this.lblScores.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScores.Location = new System.Drawing.Point(95, 85);
            this.lblScores.Name = "lblScores";
            this.lblScores.Size = new System.Drawing.Size(300, 200); // Set a fixed size
            this.lblScores.BackColor = System.Drawing.Color.White; // Background color
            this.lblScores.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; // Simple border
            this.lblScores.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; // Center the text
            this.Controls.Add(this.lblScores);
            // 
            // GoBack_Click
            // 
            this.GoBack_Click.Location = new System.Drawing.Point(187, 310);
            this.GoBack_Click.Name = "GoBack_Click";
            this.GoBack_Click.Size = new System.Drawing.Size(115, 52);
            this.GoBack_Click.TabIndex = 1;
            this.GoBack_Click.Text = "Go back";
            this.GoBack_Click.UseVisualStyleBackColor = true;
            this.GoBack_Click.Click += new System.EventHandler(this.GoBack_Click_Click);
            // 
            // Leaderboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.GoBack_Click);
            this.Controls.Add(this.lblLeaderboard);
            this.Name = "Leaderboard";
            this.Text = "Leaderboard";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblLeaderboard;
        private System.Windows.Forms.Button GoBack_Click;
        private System.Windows.Forms.Label lblScores;
    }
}
// Leaderboard.Designer.cs