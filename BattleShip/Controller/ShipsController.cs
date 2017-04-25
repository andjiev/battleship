using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Model;
using BattleShip;
using System.Drawing;
using System.Windows.Forms;

namespace BattleShip.Controller
{
    class ShipsController
    {
        private List<Ship> ships;
        public Ship selected;

        public ShipsController()
        {
            ships = new List<Ship>();
            ships.Add(new Ship(1, Color.Blue, new Point { X = 0, Y = 2 }, Ship.View.HORIZONTAL));
            ships.Add(new Ship(1, Color.Blue, new Point { X = 0, Y = 6 }, Ship.View.VERTICAL));
            ships.Add(new Ship(1, Color.Blue, new Point { X = 11, Y = 2 }, Ship.View.HORIZONTAL));
            ships.Add(new Ship(2, Color.Blue, new Point { X = 2, Y = 1 }, Ship.View.VERTICAL));
            ships.Add(new Ship(2, Color.Blue, new Point { X = 4, Y = 8 }, Ship.View.HORIZONTAL));
            ships.Add(new Ship(3, Color.Blue, new Point { X = 5, Y = 7 }, Ship.View.VERTICAL));
            ships.Add(new Ship(3, Color.Blue, new Point { X = 8, Y = 1 }, Ship.View.HORIZONTAL));
            ships.Add(new Ship(4, Color.Blue, new Point { X = 8, Y = 9 }, Ship.View.VERTICAL));
            ships.Add(new Ship(5, Color.Blue, new Point { X = 6, Y = 5 }, Ship.View.VERTICAL));
            selected = null;
        }          

        public void SetGridView(Control g)
        {
            DataGridView grid = (DataGridView)g;
            grid.Rows.Clear();
            grid.RowCount = 12;
            grid.ColumnCount = 12;;
            for (int i = 0; i < 12; i++)
            {
                grid.Rows[i].Height = 25;
                grid.Columns[i].Width = 25;
            }
            grid.RowsDefaultCellStyle.SelectionBackColor = Color.White;
        }

        public void ShowShips(Control g)
        {
            ships.ForEach(ship => ship.Show(g));
        }

        public void Select(Point Position)
        {
            selected = ships.FirstOrDefault(ship => ship.Select(Position));
        }

        public void UnSelect()
        {
            selected = null;
        }
    }
}
