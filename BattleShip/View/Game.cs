using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleShip.Controller;
using BattleShip.Model;

namespace BattleShip
{
    public partial class Game : Form
    {
        bool GameStarted;
        public bool Turn;
      public static  int score;
        PlayerController player;
        ComputerController computer;
        Point startedPosition;
        
        public Game()
        {
            this.DoubleBuffered = true;
            Turn = true;
            InitializeComponent();
            player = new PlayerController();

            score = 0;
            computer = new ComputerController();
            dgvPlayer.DoubleBuffered(true);
            dgvComputer.DoubleBuffered(true);
            GameStarted = false;
            ShowPlayerView();
            ShowComputerView();
        }
        
        public void ShowPlayerView()
        {
            player.SetGridView(dgvPlayer);
            player.ShowShips(dgvPlayer);
        }

        public void ShowComputerView()
        {
            computer.SetGridView(dgvComputer);
            computer.ShowShips(dgvComputer);
        }

        private void dgvPlayer_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {            
            player.Select(new Point { X = e.RowIndex, Y = e.ColumnIndex });
            if(player.selected != null)
            {
                startedPosition = player.selected.Cells[0].Positon;
            }            
        }

        private void dgvPlayer_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            Point position = new Point { X = e.RowIndex, Y = e.ColumnIndex };
            
            if (player.selected != null)
            {
                player.selected.AddPositions(position);                
                if (player.SearchShip())
                {
                    Cursor.Current = Cursors.No;
                    double opacity = 0.6;
                    player.selected.Cells.ForEach(cell => cell.Opacity((float)opacity));
                    ShowPlayerView();
                    return;                    
                }
                ShowPlayerView();
            }
            if (player.SearchShip(position))
            {
                Cursor.Current = Cursors.SizeAll;
                if(player.selected != null)
                player.selected.Cells.ForEach(cell => cell.Opacity(1));
                ShowPlayerView();
            }
        }

        private void dgvPlayer_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (player.selected != null)
            {
                if (player.SearchShip())
                {
                    player.selected.AddPositions(startedPosition);
                    ShowPlayerView();
                }
            }
            player.UnSelect();
        }

        private void dgvPlayer_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Point newPosition = new Point { X = e.RowIndex, Y = e.ColumnIndex };
            player.Select(newPosition);
            Point oldPosition = new Point();
            if (player.selected != null)
            {
               oldPosition = player.selected.Cells[0].Positon;
            }
             
            if (player.selected != null)
            {
                player.selected.ChangePosition(newPosition);
                if (player.SearchShip())
                {
                    player.selected.ChangePosition(oldPosition);
                }
            }
            ShowPlayerView();
            player.UnSelect();            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            player.DisableCells(dgvPlayer);
            player.ShowShips(dgvPlayer);
            ComputerTimer.Start();
            System.Media.SoundPlayer song = new System.Media.SoundPlayer(Properties.Resources.start);
            song.Play();
            GameStarted = true;
            Turn = true;
            dgvComputer.Enabled = true;
            button2.Enabled = false;
            label1.Hide();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            player.EnableCells(dgvPlayer);
            player.ShowShips(dgvPlayer);
            GameStarted=false;
            dgvComputer.Enabled = false;
        }

        private void btnShoot_Click(object sender, EventArgs e)
        {
            player.Shoot(dgvPlayer);
            player.ShowShips(dgvPlayer);
        }

        private void Game_Leave(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ComputerTimer_Tick(object sender, EventArgs e)
        {
            label2.Text=Turn? "Your turn":"Bot's turn";
            lblScore.Text = score.ToString();
            if (score < 0)
            {
                lblScore.ForeColor = Color.Red;

            }
            if (!Turn)
            {
                dgvComputer.Enabled = false;
                ShootTimer.Start();
            }
            if (Turn)
            {
                dgvComputer.Enabled = true;
                ShootTimer.Stop();
            }

            if (computer.Won())
            {
                ComputerTimer.Interval = 999999999;
                MessageBox.Show("YOU WON!","VICTORY");
                ShootTimer.Enabled = false;
                //ShowPlayerView();
                //ShowComputerView();
                ComputerTimer.Enabled = false;
                dgvComputer.Enabled = false;
               
                    if (score>2000) {
                    saveFile(Microsoft.VisualBasic.Interaction.InputBox("Highscore!", "Save your Highscore", "Name", 150, 150), score);
                    }


                

                ComputerTimer.Dispose();

            }
            if (player.Won())
            {
                ComputerTimer.Enabled = false;
                dgvComputer.Enabled = false;
                MessageBox.Show("YOU LOST!", "LOSE");
                ComputerTimer.Interval = 999999999;

                // ShowPlayerView();
                //ShowComputerView();
                dgvComputer.Enabled = false;
                ComputerTimer.Dispose();
            }
        }

        private void dgvComputer_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (GameStarted)
            {
                if (computer.Shoot(new Point { X = e.RowIndex, Y = e.ColumnIndex }, dgvComputer))
                {
                    Turn = false;
                    dgvComputer.Enabled = false;

                }
                computer.ShowShips(dgvComputer);                
            }
        }    

        private void button1_Click(object sender, EventArgs e)
        {    
            Random random = new Random();
            computer.Shoot(new Point { X = random.Next(0, 12), Y = random.Next(0, 12) }, dgvComputer);
           
        }

        private void ShootTimer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            ShootTimer.Interval = random.Next(1000, 2000);
            player.Shoot(dgvPlayer);
            player.ShowShips(dgvPlayer);
            Turn = !player.found;
            lblScore.Text = score.ToString();
            if (score < 0)
            {
                lblScore.ForeColor = Color.Red;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            player.Random();
            ShowPlayerView();
        }

        private void dgvComputer_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!startedPosition.IsEmpty && dgvComputer.Rows[startedPosition.X].Cells[startedPosition.Y].Style.BackColor == Color.DimGray)
            {
                dgvComputer[0, 0].Style.BackColor = Color.Transparent;
                dgvComputer.Rows[startedPosition.X].Cells[startedPosition.Y].Style.BackColor = Color.Transparent;
            }
            Point position = new Point { X = e.RowIndex, Y = e.ColumnIndex }; ;
            if (computer.positions.Exists(point => point.Equals(position))) 
            {
                dgvComputer.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.DimGray;
                startedPosition = position;
                Cursor.Current = Cursors.Hand;
            }          
        }

        private void dgvPlayer_MouseLeave(object sender, EventArgs e)
        {
            if (player.selected != null)
            {
                if (player.SearchShip())
                {
                    player.selected.AddPositions(startedPosition);
                    ShowPlayerView();
                }
            }
            player.UnSelect();
        }

        private void dgvComputer_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (!startedPosition.IsEmpty && dgvComputer.Rows[startedPosition.X].Cells[startedPosition.Y].Style.BackColor == Color.DimGray)
            {
                dgvComputer[0, 0].Style.BackColor = Color.Transparent;
                dgvComputer.Rows[startedPosition.X].Cells[startedPosition.Y].Style.BackColor = Color.Transparent;
            }
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void saveFile(String name, int Score)
        {
            System.IO.File.WriteAllText(Application.StartupPath + "\\highscores.txt", name + ";" + Score.ToString());
        }
    }
}
