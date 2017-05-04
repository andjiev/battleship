using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BattleShip.Model;

namespace BattleShip
{
    public partial class form1 : Form
    {
        public List<Score> listScores;
        System.Media.SoundPlayer sound = new System.Media.SoundPlayer(Properties.Resources.war);
        public form1()
        {
            listScores = new List<Score>();
            InitializeComponent();
            sound.PlayLooping();
            
        }
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            sound.Stop();
            Game game = new Game();
            this.Hide();
            if (game.ShowDialog() == DialogResult.Cancel) {
                this.Show();
                sound.PlayLooping();
            }
            
        }

        private void btnControls_Click(object sender, EventArgs e)
        {
          sound.Stop();
            Controls controls = new Controls();
            this.Hide();
            if (controls.ShowDialog() == DialogResult.Cancel) {
                this.Show();
                sound.Play();
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            sound.Stop();
            Application.Exit();
        }

        private void btnNewGame_MouseEnter(object sender, EventArgs e)
        {
            AddAnimation(btnNewGame);
        }
        public void AddAnimation(Button button)
        {
            var expandTimer = new System.Windows.Forms.Timer();
            var contractTimer = new System.Windows.Forms.Timer();

            expandTimer.Interval = 10;//can adjust to determine the refresh rate
            contractTimer.Interval = 10;

            DateTime animationStarted = DateTime.Now;

            //TODO update as appropriate or make it a parameter
            TimeSpan animationDuration = TimeSpan.FromMilliseconds(250);
            int initialWidth = 75;
            int endWidth = 130;

            button.MouseHover += (_, args) =>
            {
                contractTimer.Stop();
                expandTimer.Start();
                animationStarted = DateTime.Now;
                
            };

            button.MouseLeave += (_, args) =>
            {
                expandTimer.Stop();
                contractTimer.Start();
                animationStarted = DateTime.Now;
                button.BackColor = Color.LightSkyBlue;
            };

            expandTimer.Tick += (_, args) =>
            {
                double percentComplete = (DateTime.Now - animationStarted).Ticks
                    / (double)animationDuration.Ticks;

                if (percentComplete >= 1)
                {
                    expandTimer.Stop();
                }
                else
                {
                    button.Width = (int)(initialWidth +
                        (endWidth - initialWidth) * percentComplete);
                }
            };

            contractTimer.Tick += (_, args) =>
            {
                double percentComplete = (DateTime.Now - animationStarted).Ticks
                    / (double)animationDuration.Ticks;

                if (percentComplete >= 1)
                {
                    contractTimer.Stop();
                }
                else
                {
                    button.Width = (int)(endWidth -
                        (endWidth - initialWidth) * percentComplete);
                }
            };
        }

        private void btnControls_MouseEnter(object sender, EventArgs e)
        {
            AddAnimation(btnControls);
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            AddAnimation(btnExit);
        }

        private void form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "\\highscores.csv"))
            {
                readFile();

            }
            else
            {
                File.Create(Application.StartupPath + "\\highscores.csv");
            

            }
        }

        private void btnHighscores_MouseEnter(object sender, EventArgs e)
        {
            AddAnimation(btnHighscores);
        }
        private void readFile() {
            var fs = File.OpenRead(Application.StartupPath + "\\highscores.csv");
            var reader = new StreamReader(fs);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                int p;
                int.TryParse(values[1], out p);
                Score s = new Score(values[0], p);
               
            }
        }
    }
}
