using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip.Model
{
    class Grid : DataGridView
    {
       
        protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
        {
         
            //base.PaintBackground(graphics, clipBounds, gridBounds);
            Rectangle rectSource = new Rectangle(Location.X, Location.Y, Width, Height);
            Rectangle rectDest = new Rectangle(0, 0, rectSource.Width, rectSource.Height);

            Bitmap b = new Bitmap(Parent.ClientRectangle.Width, Parent.ClientRectangle.Height);
            Graphics.FromImage(b).DrawImage(Parent.BackgroundImage, Parent.ClientRectangle);


            graphics.DrawImage(b, rectDest, rectSource, GraphicsUnit.Pixel);
            SetCellsTransparent();

        }
        public void SetCellsTransparent()
        {
            EnableHeadersVisualStyles = false;
            ColumnHeadersDefaultCellStyle.BackColor = Color.Transparent;
            RowHeadersDefaultCellStyle.BackColor = Color.Transparent;
            ClearSelection();

            foreach (DataGridViewColumn col in Columns)
            {
                col.DefaultCellStyle.BackColor = Color.Transparent; 
            }     
        }
    }
}
