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
    public partial class SpaceShooterCCIs : Form
    {
        private string selectedShip;
        private Timer gameTimer;
        private Timer laserTimer;
        private Timer enemyLaserTimer;
        private PictureBox playerShip;
        private List<Enemy> enemies;
        private List<PictureBox> lasers;
        private List<PictureBox> enemyLasers;
        private Random random;
        private Timer enemySpawnTimer;
        private bool moveLeft, moveRight, moveUp, moveDown;
        private bool isPaused = false;
        private bool gameStarted = false; //need neto para hindi magstart yung game agad pag nag start
        private GameOverMenu gameOverMenu;
        private Label startGameLabel;
        private AxWindowsMediaPlayer MXP;

        //Create Sound
        private SoundPlayer playerHit;
        private SoundPlayer AtkSound;
        private SoundPlayer GameOver;

        private int points;
        private int elapsedTime;
        private Timer displayTimer;
        private Label pointsLabel;
        private Label timerLabel;

        private int hearts;
        private List<PictureBox> heartIcons;

        private Timer slowdownTimer;
        private bool isSlowedDown;

        private Panel pauseMenuPanel;
        private Button resumeButton;
        private Button restartButton;
        private Button exitButton;

        //moving background
        private List<Image> backgrounds;
        private List<Point> backgroundPositions;
        private int backgroundSpeed = 2;

        public SpaceShooterCCIs(string selectedShip)
        {
            InitializeComponent();
            //Initialize Sound
            playerHit = new SoundPlayer("playerHit.wav");
            GameOver = new SoundPlayer("Game_Over.wav");
            AtkSound = new SoundPlayer("8bit_atk_02.wav");
            InitializeMediaPlayer();

            int fixedwidth = 800;
            int fixedHeight = 1040;

            this.Size = new Size(fixedwidth, fixedHeight);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.selectedShip = selectedShip;
            InitializeGame();
            InitializeGameOverMenu();
            this.BackColor = Color.Transparent;
            
            this.BackgroundImage = LoadImageFromResource("Game_test_2.Resources.CCIS_SKIES.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            //this.FormBorderStyle = FormBorderStyle.None;                                          ///PWEDE gamitin if want borderless hehe
            backgrounds = new List<Image>
            {
                LoadImageFromResource("Game_test_2.Resources.CCIS_SKIES.png"),
                LoadImageFromResource("Game_test_2.Resources.SKY_LOOP1.png"),
                LoadImageFromResource("Game_test_2.Resources.SKY_LOOP2.png")
            };

            backgroundPositions = new List<Point>
            {
                new Point(0, 0),
                new Point(0, -this.ClientSize.Height), // Start the second background off-screen
                new Point(0, -this.ClientSize.Height * 2) // Start the third background off-screen
            };

            InitializePauseMenu();
        }
        private void InitializeMediaPlayer()
        {
            MXP = new AxWindowsMediaPlayer();
            MXP.CreateControl(); // Ensure the control is created

            MXP.URL = @"In_Game_Music.wav";
            MXP.settings.playCount = 999;  // Repeat the music indefinitely
            MXP.Visible = false;           // Make the player invisible
            MXP.Location = new Point((this.ClientSize.Width - 300) / 2, (this.ClientSize.Height - 200) / 2);
            MXP.Ctlcontrols.stop();       // Stop playback (if necessary)
            MXP.Ctlcontrols.play();       // Start playback
        }
        public class ShipAttributes
        {
            public string LaserImage { get; set; }
            public int LasersOfShip { get; set; }
            public float Speed { get; set; }
            public int MaxHearts { get; set; }
            public int LaserSpacing { get; set; }

        }
        private Dictionary<string, ShipAttributes> shipAttributes = new Dictionary<string, ShipAttributes>
{
    { "ChronosShip", new ShipAttributes { LaserImage = "Game_test_2.Resources.CHRONOS_BULLET.png", LasersOfShip  = 1, Speed = 10, MaxHearts = 3, LaserSpacing = 20 } },
    { "CyberNeticShip", new ShipAttributes { LaserImage = "Game_test_2.Resources.CYBERNETICS_BULLET.png", LasersOfShip  = 2, Speed = 5, MaxHearts = 3, LaserSpacing = 29 } },
    { "InnovatorShip", new ShipAttributes { LaserImage = "Game_test_2.Resources.INNOVATORS_BULLET.png", LasersOfShip  = 2, Speed = 7, MaxHearts = 3, LaserSpacing = 10 } },
    { "SentinelShip", new ShipAttributes { LaserImage = "Game_test_2.Resources.SENTINEL_BULLET.png", LasersOfShip  = 2, Speed = 4, MaxHearts = 6, LaserSpacing = 25 } }
};

        private void InitializeGameOverMenu()
        {
            gameOverMenu = new GameOverMenu
            {
                Size = new Size(300, 200),
                Location = new Point((this.ClientSize.Width - 300) / 2, (this.ClientSize.Height - 200) / 2),
                Visible = false
            };

            gameOverMenu.RestartClicked += GameOverMenu_RestartClicked;
            gameOverMenu.MainMenuClicked += GameOverMenu_MainMenuClicked;

            this.Controls.Add(gameOverMenu);
        }

        private void GameOverMenu_RestartClicked(object sender, EventArgs e)
        {


            gameOverMenu.Visible = false;
            ResetGame();
            this.Focus();

        }


        private void GameOverMenu_MainMenuClicked(object sender, EventArgs e)
        {
            MainMenuForm mainMenu = new MainMenuForm();
            GameOver.Stop();
            mainMenu.Show();
            this.Hide();
        }

        private void InitializeGame()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            random = new Random();
            enemies = new List<Enemy>();
            lasers = new List<PictureBox>();
            enemyLasers = new List<PictureBox>();

            hearts = 3; // Initialize hearts to 3
            heartIcons = new List<PictureBox>();
            // Initialize other game components

            // Initialize slowdown timer
            slowdownTimer = new Timer
            {
                Interval = 2000 // Slowdown duration in milliseconds
            };
            slowdownTimer.Tick += SlowdownTimer_Tick;

            // Initialize player ship based on selected ship
            string shipResource = $"Game_test_2.Resources.{selectedShip}.png";
            playerShip = new PictureBox
            {
                Image = LoadImageFromResource(shipResource),
                Size = new Size(80, 80),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(this.ClientSize.Width / 2, this.ClientSize.Height - 120),
            };
            this.Controls.Add(playerShip);

            // Initialize game timer
            gameTimer = new Timer { Interval = 20 };
            gameTimer.Tick += GameTimer_Tick;

            // Initialize laser timer for auto-firing
            laserTimer = new Timer { Interval = 900 }; // Adjust the interval as needed
            laserTimer.Tick += LaserTimer_Tick;

            // Initialize enemy laser timer for auto-firing
            enemyLaserTimer = new Timer { Interval = 2000 }; // Adjust the interval as needed
            enemyLaserTimer.Tick += EnemyLaserTimer_Tick;

            // Initialize enemy spawn timer
            enemySpawnTimer = new Timer { Interval = 5000 }; // Spawn enemies every 5 seconds
            enemySpawnTimer.Tick += EnemySpawnTimer_Tick;

            this.KeyDown += SpaceShooterCCIs_KeyDown;
            this.KeyUp += SpaceShooterCCIs_KeyUp;

            // Initialize start game label
            startGameLabel = new Label
            {
                Text = "Press arrow keys to start the game",
                AutoSize = true,
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Location = new Point((this.ClientSize.Width - 300) / 2, (this.ClientSize.Height - 50) / 2),
            };
            this.Controls.Add(startGameLabel);

            // Initialize points label
            pointsLabel = new Label
            {
                Text = "Points: 0",
                AutoSize = true,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Location = new Point(10, 10),
            };
            this.Controls.Add(pointsLabel);

            // Initialize timer label
            timerLabel = new Label
            {
                Text = "Time: 00:00",
                AutoSize = true,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Location = new Point(10, 40),
            };
            this.Controls.Add(timerLabel);
            ShipAttributes attributes = shipAttributes[selectedShip];


            // Set hearts based on the ship attributes
            hearts = attributes.MaxHearts;
            // Initialize display timer
            displayTimer = new Timer { Interval = 1000 }; // Update every second
            displayTimer.Tick += DisplayTimer_Tick;

            foreach (var heartIcon in heartIcons)
            {
                this.Controls.Remove(heartIcon);
            }
            heartIcons.Clear();

            if (selectedShip == "SentinelShip")
            {
                hearts = 6;
            }
            else if (selectedShip == "ChronosShip" || selectedShip == "CyberNeticShip" || selectedShip == "InnovatorShip")
            {
                hearts = 3;
            }
            else
            {
                hearts = 3; // Default value
            }
            // Initialize player ship based on selected ship
            if (playerShip != null)
            {
                this.Controls.Remove(playerShip);
            }

            playerShip = new PictureBox
            {
                Image = LoadImageFromResource($"Game_test_2.Resources.{selectedShip}.png"),
                Size = new Size(80, 80),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(this.ClientSize.Width / 2 - 40, this.ClientSize.Height - 120),
            };
            this.Controls.Add(playerShip);

            // Initialize heart icons
            heartIcons = new List<PictureBox>();
            for (int i = 0; i < hearts; i++)
            {
                PictureBox heartIcon = new PictureBox
                {
                    Image = LoadImageFromResource("Game_test_2.Resources.pngtree-pixelated-heart-shape-on-transparent-background-png-image_12432453.png"),
                    Size = new Size(30, 30),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(10 + i * 35, 70),
                    BackColor = Color.Transparent
                };
                heartIcons.Add(heartIcon);
                this.Controls.Add(heartIcon);
                playerShip.Tag = attributes.Speed; // Store speed as Tag for later use

            }
        }

        private void SlowdownTimer_Tick(object sender, EventArgs e)
        {
            isSlowedDown = false;
            slowdownTimer.Stop();

            // Reset the speed of all enemy lasers
            foreach (var enemyLaser in enemyLasers)
            {
                enemyLaser.Tag = (float)enemyLaser.Tag * 2; //  speed is halved
            }
        }
        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime++;
            TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
            timerLabel.Text = $"Time: {timeSpan:mm\\:ss}";
        }

        private void ResetGame()
        {
            // Reset game state variables
            points = 0;
            elapsedTime = 0;
            isPaused = false;
            isSlowedDown = false;
            gameStarted = false;

            // Reset hearts based on the selected ship
            hearts = shipAttributes[selectedShip].MaxHearts;


            UpdateHeartIcons();

            // Set player ship image based on the selected ship
            switch (selectedShip)
            {
                case "ChronosShip":
                    playerShip.Image = LoadImageFromResource("Game_test_2.Resources.ChronosShip.png");
                    break;
                case "CyberNeticShip":
                    playerShip.Image = LoadImageFromResource("Game_test_2.Resources.CyberNeticShip.png");
                    break;
                case "InnovatorShip":
                    playerShip.Image = LoadImageFromResource("Game_test_2.Resources.InnovatorShip.png");
                    break;
                case "SentinelShip":
                    playerShip.Image = LoadImageFromResource("Game_test_2.Resources.SentinelShip.png");
                    break;

            }



            // Clear existing enemies and lasers
            foreach (var enemy in enemies)
            {
                this.Controls.Remove(enemy.PictureBox);
            }
            enemies.Clear();

            foreach (var laser in lasers)
            {
                this.Controls.Remove(laser);
            }
            lasers.Clear();

            foreach (var enemyLaser in enemyLasers)
            {
                this.Controls.Remove(enemyLaser);
            }
            enemyLasers.Clear();

            // Reset player ship position
            playerShip.Location = new Point(this.ClientSize.Width / 2 - playerShip.Width / 2, this.ClientSize.Height - 100);

            // Do not start timers here
            gameStarted = false;

            // Show the start game label
            startGameLabel.Visible = true;
            // Update UI elements
            pointsLabel.Text = "Points: 0";
            timerLabel.Text = "Time: 00:00";
            this.Focus();
        }




        private void AddEnemy(string resourceName, Point location, int health, bool canShoot)
        {
            PictureBox enemyPictureBox = new PictureBox
            {
                Image = LoadImageFromResource(resourceName),
                Size = new Size(80, 80),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = location,
                Tag = canShoot ? "Enemy" : "Meteor", // Use Tag to identify enemy type
            };

            // Check for overlap with existing enemies
            bool positionIsValid = true;
            foreach (var enemy in enemies)
            {
                if (enemy.PictureBox.Bounds.IntersectsWith(enemyPictureBox.Bounds))
                {
                    positionIsValid = false;
                    break;
                }
            }

            if (positionIsValid)
            {
                Enemy enemy = new Enemy
                {
                    PictureBox = enemyPictureBox,
                    Health = health,
                    CanShoot = canShoot, 
                    Speed = 3.0f
                };
                enemies.Add(enemy);
                this.Controls.Add(enemyPictureBox);
            }
            else
            {
                // Retry adding the enemy with a new location
                Point newLocation = new Point(random.Next(0, this.ClientSize.Width - 70), -70);
                AddEnemy(resourceName, newLocation, health, canShoot);
            }
        }

        private void SpawnEnemy()
        {
            string[] enemyResources = {
                "Game_test_2.Resources.CORRUPTED_ACM.png",
                "Game_test_2.Resources.CORRUPTED_JPCS.png",
                "Game_test_2.Resources.CORRUPTED_JPCS.png",
                "Game_test_2.Resources.VIRUS_GREEN.png",
                "Game_test_2.Resources.VIRUS_PINK.png",
                "Game_test_2.Resources.VIRUS_RED.png",
                "Game_test_2.Resources.CORRUPTED_ACM.png" };    //DOuble byrus so more byrys 

            string resourceName = enemyResources[random.Next(enemyResources.Length)];
            Point location = new Point(random.Next(0, this.ClientSize.Width - 70), -70);
            int health = resourceName.Contains("Enemy") ? 3 : 5;
            bool canShoot = resourceName.Contains("CORRUPTED_ACM") || resourceName.Contains("CORRUPTED_JPCS") || resourceName.Contains("VIRUS");
            AddEnemy(resourceName, location, health, canShoot);
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            MovePlayer();
            MoveEnemies();
            MoveLasers();
            MoveEnemyLasers();
            CheckCollisions();

            // Update and redraw backgrounds
            UpdateBackgrounds();
            this.Invalidate();
        }

        private void LaserTimer_Tick(object sender, EventArgs e)
        {
            ShootLaser();
            AtkSound.Play();
        }

        private void EnemyLaserTimer_Tick(object sender, EventArgs e)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.CanShoot)
                {
                    ShootEnemyLaser(enemy.PictureBox);
                }
            }
        }

        private void EnemySpawnTimer_Tick(object sender, EventArgs e)
        {
            SpawnEnemy();
        }

        private void MovePlayer()
        {
            // Get the attributes for the currently selected ship
            ShipAttributes attributes = shipAttributes[selectedShip];
            float speed = attributes.Speed; // Get the speed of the selected ship

            // Use the speed value to move the ship
            if (moveLeft && playerShip.Left > 0)
            {
                playerShip.Left -= (int)speed; // Move left by the ship's speed
                playerShip.Invalidate(); // Clear previous position
            }
            if (moveRight && playerShip.Right < this.ClientSize.Width)
            {
                playerShip.Left += (int)speed; // Move right by the ship's speed
                playerShip.Invalidate(); // Clear previous position
            }
            if (moveUp && playerShip.Top > 0)
            {
                playerShip.Top -= (int)speed; // Move up by the ship's speed
                playerShip.Invalidate(); // Clear previous position
            }
            if (moveDown && playerShip.Bottom < this.ClientSize.Height)
            {
                playerShip.Top += (int)speed; // Move down by the ship's speed
                playerShip.Invalidate(); // Clear previous position
            }
        }

        private void MoveEnemies()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                var enemy = enemies[i];
                float speedMultiplier = isSlowedDown ? 0.5f : 1.0f; // Slow down by 50% if slowed down

                if (enemy.PictureBox.Tag.ToString() == "Enemy")
                {
                    // Move downwards
                    enemy.PictureBox.Top += (int)(enemy.Speed * speedMultiplier);

                    // Ensure byrus stays within the horizontal window bounds
                    if (enemy.PictureBox.Left < 0)
                        enemy.PictureBox.Left = 0;
                    if (enemy.PictureBox.Right > this.ClientSize.Width)
                        enemy.PictureBox.Left = this.ClientSize.Width - enemy.PictureBox.Width;

                    // Check if byrus is off-screen vertically
                    if (enemy.PictureBox.Top > this.ClientSize.Height)
                    {
                        this.Controls.Remove(enemy.PictureBox);
                        enemies.RemoveAt(i);
                        SpawnEnemy();
                    }
                }
                else
                {
                    enemy.PictureBox.Top += (int)(4 * speedMultiplier);

                    if (enemy.PictureBox.Top > this.ClientSize.Height)
                    {
                        enemy.PictureBox.Top = -enemy.PictureBox.Height;
                        enemy.PictureBox.Left = random.Next(0, this.ClientSize.Width - enemy.PictureBox.Width);
                    }
                }
            }
        }

        private void MoveLasers()
        {
            for (int i = lasers.Count - 1; i >= 0; i--)
            {
                double angle = (double)(lasers[i].Tag);
                double radians = angle * Math.PI / 180.0;
                int speed = 20;

                lasers[i].Left += (int)(speed * Math.Cos(radians));
                lasers[i].Top -= (int)(speed * Math.Sin(radians)); // Use -= to move upwards

                if (lasers[i].Top < 0 || lasers[i].Top > this.ClientSize.Height || lasers[i].Left < 0 || lasers[i].Left > this.ClientSize.Width)
                {
                    this.Controls.Remove(lasers[i]);
                    lasers.RemoveAt(i);
                }
            }
        }

        private void MoveEnemyLasers()
        {
            for (int i = enemyLasers.Count - 1; i >= 0; i--)
            {
                var enemyLaser = enemyLasers[i];
                float speed = (float)enemyLaser.Tag;
                enemyLaser.Top += (int)speed;

                if (enemyLaser.Top > this.ClientSize.Height)
                {
                    this.Controls.Remove(enemyLaser);
                    enemyLasers.RemoveAt(i);
                }
            }
        }

        private void CheckCollisions()
        {
            // Check for collisions between player ship and enemies
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (playerShip.Bounds.IntersectsWith(enemies[i].PictureBox.Bounds))
                {
                    this.Controls.Remove(enemies[i].PictureBox);
                    enemies.RemoveAt(i);
                    playerHit.Play();
                    LoseHeart();
                    FlashShipImage();
                    break;
                }
            }

            // Check for collisions between player ship and enemy lasers
            for (int i = enemyLasers.Count - 1; i >= 0; i--)
            {
                if (playerShip.Bounds.IntersectsWith(enemyLasers[i].Bounds))
                {
                    this.Controls.Remove(enemyLasers[i]);
                    enemyLasers.RemoveAt(i);
                    playerHit.Play();
                    LoseHeart();
                    FlashShipImage();
                    break;
                }
            }

            // Check for collisions between lasers and enemies
            for (int i = lasers.Count - 1; i >= 0; i--)
            {
                for (int j = enemies.Count - 1; j >= 0; j--)
                {
                    if (lasers[i].Bounds.IntersectsWith(enemies[j].PictureBox.Bounds))
                    {
                        this.Controls.Remove(lasers[i]);
                        lasers.RemoveAt(i);
                        enemies[j].Health--;
                        if (enemies[j].Health <= 0)
                        {
                            this.Controls.Remove(enemies[j].PictureBox);
                            enemies.RemoveAt(j);
                            points += 10; // Add points for destroying an enemy
                            pointsLabel.Text = $"Points: {points}";
                        }
                        break;
                    }
                }
            }
        }
        private void LoseHeart()
        {
            hearts--;
            UpdateHeartIcons();

            if (hearts <= 0)
            {
                EndGame();
            }
            else
            {
                // Trigger slowdown effect
                isSlowedDown = true;
                slowdownTimer.Start();

                // Slow down the speed of all existing enemy lasers
                foreach (var enemyLaser in enemyLasers)
                {
                    enemyLaser.Tag = (float)enemyLaser.Tag / 2;
                }
            }
        }

        private async void FlashShipImage()
        {
            playerShip.Image = LoadImageFromResource($"Game_test_2.Resources.{selectedShip}Hit.png");
            await Task.Delay(400);
            playerShip.Image = LoadImageFromResource($"Game_test_2.Resources.{selectedShip}.png");
            await Task.Delay(100);
        }
        private void UpdateHeartIcons()
        {
            // Remove existing heart icons
            foreach (var heartIcon in heartIcons)
            {
                this.Controls.Remove(heartIcon);
            }
            heartIcons.Clear();

            // Add new heart icons based on the current number of hearts
            for (int i = 0; i < hearts; i++)
            {
                PictureBox heartIcon = new PictureBox
                {
                    Image = LoadImageFromResource("Game_test_2.Resources.pngtree-pixelated-heart-shape-on-transparent-background-png-image_12432453.png"),
                    Size = new Size(30, 30),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(10 + (i * 35), 70) // Adjust the position as needed
                };
                heartIcons.Add(heartIcon);
                this.Controls.Add(heartIcon);
            }
        }

        private void EndGame()
        {
            gameStarted = false;
            MXP.Ctlcontrols.stop();
            GameOver.Play();
            gameTimer.Stop();
            laserTimer.Stop();
            enemyLaserTimer.Stop();
            enemySpawnTimer.Stop();
            displayTimer.Stop();
            gameOverMenu.Visible = true;
            gameOverMenu.BringToFront();
            UpdateTxtFile(selectedShip, points);
        }

        private void BtnMainMenu_Click(object sender, EventArgs e)
        {
            MainMenuForm mainMenu = new MainMenuForm();
            mainMenu.Show();
            this.Hide();
        }

        private void StartGame()
        {
            // Hide start game label
            startGameLabel.Visible = false;

            // Initialize enemies
            for (int i = 0; i < 2; i++) // Add 2 initial enemies
            {
                AddEnemy("Game_test_2.Resources.CORRUPTED_ACM.png", new Point(random.Next(0, this.ClientSize.Width - 100), random.Next(-200, -100)), 3, true);

                AddEnemy("Game_test_2.Resources.CORRUPTED_ACM.png", new Point(random.Next(0, this.ClientSize.Width - 100), random.Next(-200, -100)), 5, false);
            }

            // Start timers
            gameTimer.Start();
            laserTimer.Start();
            enemyLaserTimer.Start();
            enemySpawnTimer.Start();
            displayTimer.Start(); // Start the display timer
            MXP.Ctlcontrols.play();

            gameStarted = true; // Set this to true to indicate the game has started
            SpawnEnemy();
        }

        private void SpaceShooterCCIs_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gameStarted) // Check if the game has not started yet
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    StartGame();
                }
            }
            else // If the game has started, allow movement
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        moveLeft = true;
                        break;
                    case Keys.Right:
                        moveRight = true;
                        break;
                    case Keys.Up:
                        moveUp = true;
                        break;
                    case Keys.Down:
                        moveDown = true;
                        break;
                    case Keys.Escape:
                        TogglePause();
                        break;
                }
            }
        }

        private void SpaceShooterCCIs_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    moveLeft = false;
                    break;
                case Keys.Right:
                    moveRight = false;
                    break;
                case Keys.Up:
                    moveUp = false;
                    break;
                case Keys.Down:
                    moveDown = false;
                    break;
            }
        }
        private void TogglePause()
        {
            if (isPaused)
            {
                // Resume the game
                gameTimer.Start();
                laserTimer.Start();
                enemyLaserTimer.Start();
                enemySpawnTimer.Start();
                MXP.Ctlcontrols.play();
                displayTimer.Start();
                pauseMenuPanel.Visible = false;
                this.Focus();

            }
            else
            {
                // Pause the game
                gameTimer.Stop();
                laserTimer.Stop();
                enemyLaserTimer.Stop();
                enemySpawnTimer.Stop();
                MXP.Ctlcontrols.stop();
                displayTimer.Stop();
                pauseMenuPanel.Visible = true;

            }
            isPaused = !isPaused;
        }

        private void ShootLaser()
        {
            ShipAttributes attributes = shipAttributes[selectedShip];

            int numberOfLasers = attributes.LasersOfShip;
            int laserSpacing = attributes.LaserSpacing; // Get the spacing for lasers

            for (int i = 0; i < numberOfLasers; i++)
            {
                // Calculate the position based on the index and the laser spacing
                int laserXPosition = playerShip.Left + (playerShip.Width / 2) - (numberOfLasers > 1 ? (laserSpacing * (numberOfLasers - 1) / 2) - (i * laserSpacing) : 0);
                ShootLaserStraightUp(attributes.LaserImage, laserXPosition);
            }
        }

        private void ShootLaserStraightUp(string laserImage, int xPosition)
        {
            PictureBox laser = new PictureBox
            {
                Image = LoadImageFromResource(laserImage),
                Size = new Size(20, 60),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(xPosition, playerShip.Top - 60), // Use the calculated X position
                Tag = 90.0 // Set angle to 90 degrees for straight up as a double
            };
            lasers.Add(laser);
            this.Controls.Add(laser);
        }

        private void InitializePauseMenu()
        {
            pauseMenuPanel = new Panel
            {
                Size = new Size(300, 200),
                Location = new Point((this.ClientSize.Width - 300) / 2, (this.ClientSize.Height - 200) / 2),
                BackColor = Color.FromArgb(128, 0, 0, 0), // Semi-transparent black
                Visible = false // Initially hidden
            };

            resumeButton = new Button
            {
                Text = "Resume",
                Size = new Size(100, 40),
                Location = new Point(100, 30)
            };
            resumeButton.Click += ResumeButton_Click;

            restartButton = new Button
            {
                Text = "Restart",
                Size = new Size(100, 40),
                Location = new Point(100, 80)
            };
            restartButton.Click += RestartButton_Click;

            exitButton = new Button
            {
                Text = "Exit to Main Menu",
                Size = new Size(100, 40),
                Location = new Point(100, 130)
            };
            exitButton.Click += ExitButton_Click;


            pauseMenuPanel.Controls.Add(resumeButton);
            pauseMenuPanel.Controls.Add(restartButton);
            pauseMenuPanel.Controls.Add(exitButton);

            // Add panel to the form
            this.Controls.Add(pauseMenuPanel);
            pauseMenuPanel.BringToFront();
        }
        private void ResumeButton_Click(object sender, EventArgs e) //SA PAUSE MENU ITO
        {
            TogglePause(); // Resume the game
        }

        private void RestartButton_Click(object sender, EventArgs e) //SA PAUSE MENU ITO
        {
            pauseMenuPanel.Visible = false; // Hide the pause menu
            ResetGame(); // Reset the game

            startGameLabel.Visible = true;

            gameStarted = false;
        }

        private void ExitButton_Click(object sender, EventArgs e) //SA PAUSE MENU ITO
        {
            MainMenuForm mainMenu = new MainMenuForm();
            mainMenu.Show();
            this.Hide(); // Hide the current game form
        }

        private void SpaceShooterCCIs_Load(object sender, EventArgs e) //IDK ANO ITO BUT NSA FORM1.CS TOh so dont DELETE
        {

        }

        private void ShootEnemyLaser(PictureBox enemy)
        {
            PictureBox enemyLaser = new PictureBox
            {
                Image = LoadImageFromResource("Game_test_2.Resources.red_laser_blast.png"),
                Size = new Size(10, 30),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(enemy.Location.X + (enemy.Width / 2) - 5, enemy.Location.Y + enemy.Height)
            };

            float baseSpeed = 10f;
            enemyLaser.Tag = isSlowedDown ? baseSpeed / 2 : baseSpeed;
            enemyLasers.Add(enemyLaser);
            this.Controls.Add(enemyLaser);
        }

        private Image LoadImageFromResource(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    return Image.FromStream(stream);
                }
                else
                {
                    throw new Exception($"Resource '{resourceName}' not found.");
                }
            }
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            gameOverMenu.Visible = false;
            GameOver.Stop();
            ResetGame();
            this.Focus();
        }

        //BACKGROUNDs
        private void UpdateBackgrounds()
        {
            for (int i = 0; i < backgroundPositions.Count; i++)
            {
                // Get the current position
                Point currentPosition = backgroundPositions[i];

                // Move the background down
                currentPosition.Y += backgroundSpeed;

                // If the background has moved off the screen, reset its position
                if (currentPosition.Y >= this.ClientSize.Height)
                {
                    currentPosition.Y = (i - 1) * this.ClientSize.Height; // Reset to the position of the previous background
                }

                // Set the modified position back into the list
                backgroundPositions[i] = currentPosition;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw the backgrounds
            for (int i = 0; i < backgrounds.Count; i++)
            {
                e.Graphics.DrawImage(backgrounds[i], backgroundPositions[i]);
            }
        }


        private void UpdateTxtFile(string selectedShip, int score)
        {
            string shipName = string.Empty;  // Initialize the shipName variable
            // Hard Coding
            //string ScoresFile = @"C:\Users\Rui\Downloads\Main_Program_Ryan-20241123T084122Z-001\Main_Program_Ryan\Game test 2\CodedScores.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CodedScores.txt");
            string ScoresFile = filePath.Replace(@"\bin\Debug\", @"\");

            switch (selectedShip)
            {
                case "ChronosShip":
                    shipName = "Chronos Ship";
                    break;
                case "CyberNeticShip":
                    shipName = "Cybernetics Ship";  // Fixed typo here
                    break;
                case "InnovatorShip":
                    shipName = "Innovators Ship";
                    break;
                case "SentinelShip":
                    shipName = "Sentinels Ship";
                    break;
                default:
                    MessageBox.Show("Invalid ship name selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            try
            {
                if (!File.Exists(ScoresFile))
                {
                    MessageBox.Show($"Scores file not found at {ScoresFile}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var lines = File.ReadAllLines(ScoresFile).ToList();

                bool found = false;
                for (int i = 0; i < lines.Count; i++)
                {
                    var parts = lines[i].Split(',');
                    if (parts.Length == 2 && parts[0] == shipName)
                    {
                        lines[i] = $"{shipName},{score}";
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    lines.Add($"{shipName},{score}");
                }

                File.WriteAllLines(ScoresFile, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving score: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class Enemy
        {
            public PictureBox PictureBox { get; set; }
            public int Health { get; set; }
            public bool CanShoot { get; set; }
            public Point TargetPosition { get; set; }
            public float Speed { get; set; }

            public void SetNewTargetPosition(int clientWidth, int clientHeight, Random random, List<Enemy> existingEnemies)
            {
                bool positionIsValid;
                do
                {
                    positionIsValid = true;
                    TargetPosition = new Point(random.Next(0, clientWidth - PictureBox.Width), random.Next(0, clientHeight - PictureBox.Height));

                    // Check for overlap with existing enemies
                    foreach (var enemy in existingEnemies)
                    {
                        if (enemy != this && enemy.PictureBox.Bounds.IntersectsWith(new Rectangle(TargetPosition, PictureBox.Size)))
                        {
                            positionIsValid = false;
                            break;
                        }
                    }
                } while (!positionIsValid);
            }
        }
    }
}