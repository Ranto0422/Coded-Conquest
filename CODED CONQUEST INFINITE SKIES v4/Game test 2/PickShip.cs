using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Game_test_2.SpaceShooterCCIs;
using System.Media;
using Game_test_2.Properties;
using AxWMPLib;
using System.IO;

namespace Game_test_2
{
    public partial class PickShip : Form
    {
        private string selectedShip;

        public PickShip()
        {
            InitializeComponent();

            // Resize the window to user's screen resolution
            int fixedwidth = 800;
            int fixedHeight = 1040;

            this.Size = new Size(fixedwidth, fixedHeight);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InnovatorShip_Click(object sender, EventArgs e)
        {
            selectedShip = "InnovatorShip";
            StartGame();
        }

        private void SentinelShip_Click(object sender, EventArgs e)
        {
            selectedShip = "SentinelShip";
            StartGame();
        }

        private void CyberNeticShip_Click(object sender, EventArgs e)
        {
            selectedShip = "CyberNeticShip";
            StartGame();
        }

        private void ChronosShip_Click(object sender, EventArgs e)
        {
            selectedShip = "ChronosShip";
            StartGame();
        }

        private void StartGame()
        {
            SpaceShooterCCIs gameForm = new SpaceShooterCCIs(selectedShip);
            gameForm.Show();
            this.Hide();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MainMenuForm mainMenuForm = new MainMenuForm();
            mainMenuForm.Show();
            this.Hide();
        }

        private void PickShip_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}