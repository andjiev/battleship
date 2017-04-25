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
                Ship ship = new Ship(size, Color.Blue, new Point { X = random.Next(0, 11), Y = random.Next(0,11) }, size % 4 == 0 ? Ship.View.VERTICAL : Ship.View.HORIZONTAL);

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

                //TODO : Make it precise
                /*if(s.Position == point)
                {
                    return point;
                    s.Color = Color.Red;
                }
                else if(s.Type==Ship.View.HORIZONTAL && point.Y <= s.Position.Y && point.Y>=s.Position.X)
                {
                    return point;

                }*/
                

            }
            grid.ClearSelection();
        }

        public void Shoot(DataGridView grid)
        {
            Random random = new Random();
            int row = random.Next(0, 11);
            int column = random.Next(0, 11);
            if(grid.Rows[row].Cells[column].Style.BackColor == Color.Gray)
            {
                grid.Rows[row].Cells[column].Style.BackColor = Color.Red;
            }                
            else
            {
                grid.Rows[row].Cells[column].Style.BackColor = Color.Purple;
            }
            grid.Rows[row].Cells[column].ReadOnly = true;
        }
        public void ShowShips(DataGridView grid)
        {
            ships.ForEach(ship => ship.Show(grid));
        }

    }
}