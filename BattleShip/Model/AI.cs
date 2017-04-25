using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace BattleShip.Model
{
    class AI
    {
        List<Ship> ships;

        public AI() {
            Random random = new Random();
            for (int i = 0; i < 9; i++) {
                Ship ship = new Ship(random.Next(1, 5), Color.Blue, new Point { X = random.Next(0, 11), Y = random.Next(0, 11) }, random.Next(0, 12) % 2 == 0 ? Ship.View.HORIZONTAL: Ship.View.VERTICAL);

                if (!ships.Contains(ship)) {
                    ships.Add(ship);
                } else
                {
                    i--;
                }
            }
       

        }

        public void Shoot() {
            Random random = new Random();
            Point point = new Point { X = random.Next(0, 11), Y = random.Next(0, 11) };
           foreach(Point p in ships.)
        }

    }
}
