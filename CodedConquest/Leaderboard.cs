using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Game_test_2
{
    public partial class Leaderboard : Form
    {
        // Hard Coding
        //private static readonly string ScoresFile = @"C:\Users\Rui\Downloads\Main_Program_Ryan-20241123T084122Z-001\Main_Program_Ryan\Game test 2\bin\Debug\CodedScores.txt";

        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CodedScores.txt");
        private static readonly string ScoresFile = filePath.Replace(@"\bin\Debug\", @"\");

        public Leaderboard()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            LoadLeaderboard();
        }

        private void GoBack_Click_Click(object sender, EventArgs e)
        {
            MainMenuForm mainMenuForm = new MainMenuForm();
            mainMenuForm.ShowDialog();
            this.Close();
        }

        private void LoadLeaderboard()
        {
            try
            {
                List<(string ShipName, int Points)> scores = new List<(string, int)>();
                if (File.Exists(ScoresFile))
                {
                    var lines = File.ReadAllLines(ScoresFile);
                    foreach (var line in lines)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 2 && int.TryParse(parts[1], out int points))
                        {
                            scores.Add((parts[0], points));
                        }
                    }
                }

                scores = scores.OrderByDescending(s => s.Points).ToList();

                lblScores.Text = "";
                foreach (var score in scores)
                {
                    lblScores.Text += $"{score.ShipName}: {score.Points} points\n\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading leaderboard: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblLeaderboard_Click(object sender, EventArgs e)
        {

        }
        private void Leaderboard_Load(object sender, EventArgs e)
        {

        }
    }
}

   


// Leaderboard.cs