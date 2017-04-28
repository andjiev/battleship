using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Model
{
    class Cell
    {
        public Point Positon { get; set; }
        public bool Alive { get; set; }

        public Cell(Point position)
        {
            Positon = position;
            Alive = true;
        }
    }
}
