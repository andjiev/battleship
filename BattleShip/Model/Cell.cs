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
        public Image Img { get; set; }
        public Cell(Point position)
        {
            Positon = position;
            Alive = true;
        }
        public bool checkEqual(Point p)
        {
            return Positon.Equals(p);
        }

        public void SwapImage()
        {
            Img.RotateFlip(RotateFlipType.Rotate90FlipX);
        }
    }
}
