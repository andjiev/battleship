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
        public Image img { get; set; }
        public Cell(Point position, Image _img)
        {
            img = _img;
            Positon = position;
            Alive = true;
        }
        public bool checkEqual(Point p)
        {
            return Positon.Equals(p);
        }

        public void SwapImage()
        {
            img.RotateFlip(RotateFlipType.Rotate180FlipXY);
        }
    }
}
