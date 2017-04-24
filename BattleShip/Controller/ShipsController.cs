using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Model;
using BattleShip;
using System.Drawing;

namespace BattleShip.Controller
{
    class ShipsController
    {
        private List<Ship> ships;

        public ShipsController()
        {
            ships = new List<Ship>();
        }

        public void AddShip(int size,Color color,Point position)
        {
            ships.Add(new Ship(size, color, position));
        }

        public void ShowShips()
        {
            
        }
    }
}
