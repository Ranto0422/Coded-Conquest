using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game_test_2
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();


            int fixedwidth = 800;
            int fixedHeight = 1040;

            this.Size = new Size(fixedwidth, fixedHeight);
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.FormBorderStyle = FormBorderStyle.None;

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            PickShip pickShipForm = new PickShip();
            pickShipForm.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {

        }
        private Label lblAboutDescription;
        private void About_Click(object sender, EventArgs e)
        {
            string aboutDescription = "Coded Conquest: Infinite Skies is a portrait side-scrolling shooter game built in C#." +
                              " Players pilot CCIS-themed ships utilizing automatic guns and orb-based power-ups. " +
                              "Featuring an Infinite mode and custom-designed sprites, the game offers endless action " +
                              "and a unique, immersive experience.";

            MessageBox.Show(aboutDescription, "About Coded Conquest: Infinite Skies", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Leaderboard_Click(object sender, EventArgs e)
        {
            Leaderboard leaderboardForm = new Leaderboard();
            leaderboardForm.Show();
            this.Hide();
        }

        private void InstructionButton_Click(object sender, EventArgs e)
        {
            string aboutDescription = Text = "Press Any Arrow Key To Start and Move your Ship\n" +
                                    "Press ESC to pause the game" +
                                       "" +
                                       "";

            MessageBox.Show(aboutDescription, "About Coded Conquest: Infinite Skies", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

// Main Menu
