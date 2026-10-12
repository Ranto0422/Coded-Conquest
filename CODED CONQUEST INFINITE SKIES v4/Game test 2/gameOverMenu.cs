using System;
using System.Windows.Forms;

namespace Game_test_2
{
    public partial class GameOverMenu : UserControl
    {
        public event EventHandler RestartClicked;
        public event EventHandler MainMenuClicked;

        public GameOverMenu()
        {
            InitializeComponent();
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            RestartClicked?.Invoke(this, e);
        }

        private void BtnMainMenu_Click(object sender, EventArgs e)
        {
            MainMenuClicked?.Invoke(this, e);
        }

        private void GameOverMenu_Load(object sender, EventArgs e)
        {

        }

        private void GameOverSign_TextChanged(object sender, EventArgs e)
        {
            GameOverSign.ReadOnly = true;
        }

        private void GameOverMenu_Load_1(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}