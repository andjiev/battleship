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
        ShipsController player;
        ShipsController computer;
        public Game()
        {
            InitializeComponent();
            player = new ShipsController();
            computer = new ShipsController();
            setGridView();
            setShips();
            showShips();
        }

        private void setShips()
        {
            for(int i=0; i<4; i++)
            {
                player.AddShip(i + 1, Color.Violet, new Point { X = 1 + i, Y = 2 + i });
                for (int j = 0; j < i + 1; j++)
                {
                    dgvPlayer.Rows[2 + i].Cells[1 + i + j].Style.BackColor = Color.Violet;
                }
            }
        }

        private void setGridView()
        {
            dgvPlayer.RowCount = 12;
            dgvPlayer.ColumnCount = 12;
            dgvComputer.RowCount = 12;           
            dgvComputer.ColumnCount = 12;
            for (int i = 0; i < 12; i++)
            {
                dgvPlayer.Rows[i].Height = 25;
                dgvPlayer.Columns[i].Width = 25;
                dgvComputer.Rows[i].Height = 25;
                dgvComputer.Columns[i].Width = 25;
            }
        }

        private void showShips()
        {
            for(int i=0; i<4; i++)
            {
                
            }
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
