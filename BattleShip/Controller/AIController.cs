using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BattleShip.Model
{
    class AIController
    {
        List<Ship> ships;

        public AIController() {
            Random random = new Random();
            ships = new List<Ship>();
            for (int i = 0; i < 10; i++) {
                int size = random.Next(1, 5);
                Ship ship = new Ship(size, Color.Blue, new Point { X = random.Next(0, size), Y = random.Next(size+1, 12) }, size % 4 == 0 ? Ship.View.VERTICAL: Ship.View.HORIZONTAL);

                if (!ships.Contains(ship)  ) {
                     
                    ships.Add(ship);
           
                } else
                {
                    MessageBox.Show("Duplicate");
                    i--;
                }
            }
       

        }

        public void Shoot() {
            Random random = new Random();
            Point point = new Point { X = random.Next(0, 11), Y = random.Next(0, 11) };
           
        }
        public void Show(DataGridView grid) {
            ships.ForEach(ship => ship.Show(grid));
        }

    }
}
