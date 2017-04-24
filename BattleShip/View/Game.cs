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
            SetGridView(dgvPlayer);
            SetGridView(dgvComputer);
            ShowShips(dgvPlayer);
        }

        private void SetGridView(Control grid)
        {
            controller.SetGridView(grid);
        }

        private void ShowShips(Control grid)
        {
            controller.ShowShips(grid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dgvPlayer_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
