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
        private List<Ship> ships;

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
        }

        public void Shoot(Point position, DataGridView grid)
        {
            foreach (Ship ship in ships)
            {
                if (ship.ExistPosition(position))
                {
                    ship.ShootPosition(position);
                }
                else
                {
                    grid.Rows[position.X].Cells[position.Y].Style.BackColor = Color.LightBlue;
                }
            }
        }
        public void ShowShips(DataGridView grid)
        {
            ships.ForEach(ship => ship.ShowShip(grid));
        }

    }
}