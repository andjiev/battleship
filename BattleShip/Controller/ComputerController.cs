using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BattleShip.Model;
using System.Windows.Forms;

namespace BattleShip.Controller
{
    class ComputerController
    {
        List<Ship> ships;       

        public ComputerController()
        {
            Random random = new Random();
            ships = new List<Ship>();
            for (int i = 0; i < 10; i++)
            {
                int size = random.Next(1, 5);
                int width = random.Next(0, 11);
                Ship ship = new Ship(size, Color.Blue, new Point { X = random.Next(0, 12), Y = random.Next(0,12) }, size % 2 == 0 ? Ship.View.VERTICAL : Ship.View.HORIZONTAL);

                if (!ships.Contains(ship))
                {
                    ships.Add(ship);
                }
                else
                {
                    MessageBox.Show("Duplicate");
                    i--;
                }
            }
        }

        public void SetGridView(DataGridView grid)
        {
            grid.Rows.Clear();
            grid.RowCount = 12;
            grid.ColumnCount = 12; ;
            for (int i = 0; i < 12; i++)
            {
                grid.Rows[i].Height = 30;
                grid.Columns[i].Width = 30;              
            }
            grid.ClearSelection();
        }

        public void Shoot(DataGridView grid)
        {
            Random random = new Random();
            int row = random.Next(0, 12);
            int column = random.Next(0, 12);
            Color picked = grid.Rows[row].Cells[column].Style.BackColor;
            if (picked == Color.Gray)
            {
                grid.Rows[row].Cells[column].Style.BackColor = Color.Red;
                Shoot(grid);
            }                
            else if(picked != Color.Gray && picked != Color.Red)
            {
                grid.Rows[row].Cells[column].Style.BackColor = Color.Purple;
            }
            grid.Rows[row].Cells[column].ReadOnly = true;
        }
        public void ShowShips(DataGridView grid)
        {
            ships.ForEach(ship => ship.ShowShip(grid));
        }

    }
}