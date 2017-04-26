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
      //  bool PlayerTurn;
        PlayerController player;
        ComputerController computer;
        public Game()
        {
            InitializeComponent();
            player = new PlayerController();
            computer = new ComputerController();
          //  PlayerTurn = true;
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
        }

        private void dgvPlayer_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(player.selected != null)
            {
                player.selected.Position = new Point { X = e.RowIndex, Y = e.ColumnIndex };
                ShowPlayerView();
            }
        }

        private void dgvPlayer_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            player.UnSelect();
        }

        private void dgvPlayer_MouseUp(object sender, MouseEventArgs e)
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
            }
            ShowPlayerView();
            player.UnSelect();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            player.DisableCells(dgvPlayer);
            player.ShowShips(dgvPlayer);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            player.EnableCells(dgvPlayer);
            player.ShowShips(dgvPlayer);
        }

        private void btnShoot_Click(object sender, EventArgs e)
        {
            computer.Shoot(dgvPlayer);
            player.CheckAlive(dgvPlayer);
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        private void Game_Leave(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void dgvComputer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            


        }

        private void ComputerTimer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            ComputerTimer.Interval = random.Next(1000, 6120);
            computer.Shoot(dgvPlayer);
            dgvComputer.Enabled = true;
            ComputerTimer.Dispose();
           
        }

        private void dgvPlayer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvComputer_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            player.Shoot(dgvComputer, new Point { X = e.RowIndex, Y = e.ColumnIndex });
            ComputerTimer.Start();
            dgvComputer.Enabled = false;
        }
    }
}
