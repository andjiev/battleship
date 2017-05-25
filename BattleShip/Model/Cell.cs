using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Model
{
    [Serializable]
    class Cell
    {
        public Point Positon { get; set; }
        public bool Alive { get; set; }
        public Image Img { get; set; }
        public bool ChangedOpacity { get; set; }

        public Cell(Point position)
        {
            Positon = position;
            Alive = true;
            ChangedOpacity = false;
        }

        public void SwapImage()
        {
            Img.RotateFlip(RotateFlipType.Rotate90FlipX);
        }

        public void Opacity(float opacityValue)
        {
            Bitmap bmp = new Bitmap(Img.Width, Img.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityValue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(Img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, Img.Width, Img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();
            Img = bmp;
        }
    }
}
