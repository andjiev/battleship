using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleShip.Model;
using BattleShip.Controller;

namespace BattleShip
{
    public partial class Game : Form
    {
        PlayerController player;
        ComputerController bot;
        public Game()
        {
            InitializeComponent();
            player = new PlayerController();
            bot = new ComputerController();
            SetGridView(dgvComputer);
            bot.Show(dgvComputer);
            ShowShips(dgvPlayer);
           
          
        }

        private void SetGridView(DataGridView grid)
        {
            player.SetGridView(grid);
        }

        private void ShowShips(DataGridView grid)
        {
            SetGridView(grid);
            player.ShowShips(grid);
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
                ShowShips(dgvPlayer);
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
            ShowShips(dgvPlayer);
            player.UnSelect();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            player.DisableCells(dgvPlayer);
            ShowShips(dgvPlayer);
            bot.Shoot();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            player.EnableCells(dgvPlayer);
            ShowShips(dgvPlayer);
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        private void btnShoot_Click(object sender, EventArgs e)
        {
            Point p = bot.Shoot();
            if (p.X !=-1) {
               MessageBox.Show("HIT!");
                dgvPlayer.Rows[p.X].Cells[p.Y].Style.BackColor = Color.Red;
            }

        }
    }
}
