using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip.Model
{
    class Ship
    {
        public enum View
        {
            HORIZONTAL,
            VERTICAL
        }
        public int Size { get; set; }
        public Color Color { get; set; }
        public Point Position { get; set; }
        public View Type { get; set; }

        public Ship(int size, Color color, Point position, View type)
        {
            Size = size;
            Color = color;
            Position = position;
            Type = type;
        }

        public void Show(Control g)
        {
            DataGridView grid = (DataGridView)g;
            if (Type == View.HORIZONTAL)
            {
                for (int i = Position.Y; i < Size + Position.Y; i++)
                {
                    grid.Rows[Position.X].Cells[i].Style.BackColor = Color;
                }
            }
            else
            {
                for (int i = Position.X; i < Size + Position.X; i++)
                {
                    grid.Rows[i].Cells[Position.Y].Style.BackColor = Color;
                }
            }
            
        }
    }
}
