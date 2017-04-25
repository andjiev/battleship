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
        ShipsController controller;
        public Game()
        {
            InitializeComponent();
            controller = new ShipsController();
            ShowShips(dgvPlayer);
            SetGridView(dgvComputer);
        }

        private void SetGridView(DataGridView grid)
        {
            controller.SetGridView(grid);
        }

        private void ShowShips(DataGridView grid)
        {
            SetGridView(grid);
            controller.ShowShips(grid);
        }

        private void dgvPlayer_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            controller.Select(new Point { X = e.RowIndex, Y = e.ColumnIndex });
        }

        private void dgvPlayer_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(controller.selected != null)
            {
                controller.selected.Position = new Point { X = e.RowIndex, Y = e.ColumnIndex };
                ShowShips(dgvPlayer);
            }
        }

        private void dgvPlayer_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            controller.UnSelect();
        }

        private void dgvPlayer_MouseUp(object sender, MouseEventArgs e)
        {
            controller.UnSelect();
        }

        private void dgvPlayer_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Point position = new Point { X = e.RowIndex, Y = e.ColumnIndex };
            controller.Select(position);
            if (controller.selected != null)
            {
                controller.selected.ChangePosition(position);
            }
            ShowShips(dgvPlayer);
            controller.UnSelect();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            controller.DisableCells(dgvPlayer);
            ShowShips(dgvPlayer);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            controller.EnableCells(dgvPlayer);
            ShowShips(dgvPlayer);
        }
    }
}
