using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Model
{
    class Ship
    {
        public int Size { get; set; }
        public Color Color { get; set; }
        public Point Position { get; set; }

        public Ship(int size, Color color, Point position)
        {
            Size = size;
            Color = color;
            Position = position;
        }
    }
}
