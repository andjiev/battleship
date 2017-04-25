using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Model;
using System.Windows.Forms;

namespace BattleShip.Controller
{
    class ComputerController
    {
        private List<Ship> ships;

        public ComputerController()
        {
            ships = new List<Ship>();
        }
    }
}
