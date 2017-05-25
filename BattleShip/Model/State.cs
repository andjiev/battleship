using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Controller;

namespace BattleShip.Model
{
    [Serializable]
    class State
    {
        public PlayerController Player { get; set; }
        public ComputerController Computer { get; set; }
        public int Score { get; set; }
        public bool Turn { get; set; }

    }
}
