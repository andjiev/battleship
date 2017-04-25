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

        public Point Shoot()
        {
            Random random = new Random();
            Point point = new Point { X = random.Next(0, 11), Y = random.Next(0, 11) };
           // Point point = new Point { X = 0, Y = 2 };
            MessageBox.Show("Shot at coordinates X= " + point.X + " Y= " + point.Y);
           foreach(Ship s in PlayerController.ships)
            {
                if(s.Position == point)
                {
                    return point;
                    s.Color = Color.Red;
                }
                else if(s.Type==Ship.View.HORIZONTAL && point.Y <= s.Position.Y && point.Y>=s.Position.X)
                {
                    return point;

                }
            }
            return new Point { X = -1, Y = -1 };
        }
        public void Show(DataGridView grid)
        {
            ships.ForEach(ship => ship.Show(grid));
        }

    }
}