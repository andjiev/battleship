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
using BattleShip.View;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BattleShip.Controller;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BattleShip
{
    public partial class form1 : Form
    {

        System.Media.SoundPlayer sound = new System.Media.SoundPlayer(Properties.Resources.war);
   
        public bool MuteClicked { get; set; }

        public form1()
        {          
            InitializeComponent();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            btnContinue.Enabled = File.Exists(path + "/game.bs");
            MuteClicked = false;
            sound.PlayLooping();
            string pathh = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            this.Cursor = LoadCustomCursor(pathh + @"\Resources\AOM-Titans Cursor.cur");
        }

        public static Cursor LoadCustomCursor(string path)
        {
            IntPtr hCurs = LoadCursorFromFile(path);
            if (hCurs == IntPtr.Zero) throw new Win32Exception();
            var curs = new Cursor(hCurs);
            // Note: force the cursor to own the handle so it gets released properly
            var fi = typeof(Cursor).GetField("ownHandle", BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(curs, true);
            return curs;
        }
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            sound.Stop();
            Game game = new Game();
            this.Hide();
            if (game.ShowDialog() == DialogResult.Cancel) {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string fileName = path + "/game.bs";
                using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, game.state);
                    btnContinue.Enabled = true;
                }
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

            expandTimer.Interval = 5;//can adjust to determine the refresh rate
            contractTimer.Interval = 10;

            DateTime animationStarted = DateTime.Now;

            
            TimeSpan animationDuration = TimeSpan.FromMilliseconds(250);
            int initialWidth = 87;
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
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\highscores.csv"))
            {
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\highscores.csv");

            }
        }

        private void btnHighscores_MouseEnter(object sender, EventArgs e)
        {
            AddAnimation(btnHighscores);
        }

        private void btnHighscores_Click(object sender, EventArgs e)
        {
            sound.Stop();
            Highscores Highscores = new Highscores();
            this.Hide();
            if (Highscores.ShowDialog() == DialogResult.Cancel)
            {
                this.Show();
                sound.Play();
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {

            sound.Stop();
            
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = path + "/game.bs";
            Game game = new Game();
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                IFormatter formatter = new BinaryFormatter();
                game.state = formatter.Deserialize(fileStream) as State;
            }
            game.UpdateState();
            this.Hide();
            if (game.ShowDialog() == DialogResult.Cancel)
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, game.state);
                }
                this.Show();
                sound.PlayLooping();
            }
        }

        private void btnContinue_MouseEnter(object sender, EventArgs e)
        {
            AddAnimation(btnContinue);
        }

        private void Label4_Click(object sender, EventArgs e)
        {
            MuteClicked = true;
            sound.Stop();
            label4.Hide();
            label6.Show();
        }

        private void Label6_Click(object sender, EventArgs e)
        {
            MuteClicked = false;
            sound.PlayLooping();
            label6.Hide();
            label4.Show();
        }

        private void btnContinue_MouseHover(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void label6_MouseHover(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }
    }
}
