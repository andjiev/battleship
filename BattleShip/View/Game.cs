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

namespace BattleShip
{
    public partial class Game : Form
    {
        bool GameStarted;
        PlayerController player;
        ComputerController computer;
        Point startedPosition;

        public Game()
        {
            InitializeComponent();
            player = new PlayerController();
            computer = new ComputerController();
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
            startedPosition = new Point { X = e.RowIndex, Y = e.ColumnIndex };
            player.Select(startedPosition);
        }

        private void dgvPlayer_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(player.selected != null)
            {
                player.selected.AddPositions(new Point { X = e.RowIndex, Y = e.ColumnIndex });
                ShowPlayerView();
            }
        }

        private void dgvPlayer_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(player.selected != null)
            {
                if (player.SearchShip())
                {
                    player.selected.AddPositions(startedPosition);
                    ShowPlayerView();
                }
            }
            player.UnSelect();
        }

        private void dgvPlayer_MouseLeave(object sender, EventArgs e)
        {
            player.UnSelect();
        }

        private void dgvPlayer_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Point position = new Point { X = e.RowIndex, Y = e.ColumnIndex };
            player.Select(position);
            if (player.selected != null)
            {
                player.selected.ChangePosition(position);
                if (player.SearchShip())
                {
                    player.selected.ChangePosition(position);
                }
            }
            ShowPlayerView();
            player.UnSelect();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            player.DisableCells(dgvPlayer);
            player.ShowShips(dgvPlayer);
            GameStarted = true;
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            player.EnableCells(dgvPlayer);
            player.ShowShips(dgvPlayer);
            GameStarted=false;
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
           /* Random random = new Random();
            ComputerTimer.Interval = random.Next(1000, 6120);
            computer.Shoot(dgvPlayer);
            dgvComputer.Enabled = true;
            ComputerTimer.Dispose();  */
        }

        private void dgvComputer_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            computer.Shoot(new Point { X = e.RowIndex, Y = e.ColumnIndex }, dgvComputer);

            if (GameStarted)
            {
                // player.Shoot(dgvComputer, new Point { X = e.RowIndex, Y = e.ColumnIndex });
                //ComputerTimer.Start();
                //dgvComputer.Enabled = false;
            }

            //In shoots only on right Click (need to be fixed)
        }

        private void dgvComputer_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            computer.ShowShips(dgvComputer);
        }
    }
}
