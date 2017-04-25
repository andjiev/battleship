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

        public void Show(DataGridView grid)
        {
            int i;
            bool switched = false;
            if (Type == View.HORIZONTAL)
            {
                for (i = Position.Y; i < Position.Y +  Size; i++)
                {
                    if (i < 12)
                    {
                        grid.Rows[Position.X].Cells[i].Style.BackColor = Color;
                    }
                    else
                    {
                        grid.Rows[Position.X].Cells[i - Size].Style.BackColor = Color;
                        switched = true;
                       
                    }                                               
                }
                if(switched)
                    Position = new Point { X = Position.X, Y = 12 - Size};
            }
            else
            {
                for (i = Position.X; i < Position.X + Size; i++)
                {
                    if(i < 12)
                    {
                        grid.Rows[i].Cells[Position.Y].Style.BackColor = Color;
                    }
                    else
                    {
                        grid.Rows[i - Size].Cells[Position.Y].Style.BackColor = Color;
                        switched = true;
                        
                    }
                }
                if(switched)
                    Position = new Point { X = 12 - Size, Y = Position.Y };
            }
        }

        public bool Select(Point position)
        {
            if (Type == View.HORIZONTAL)
            {
                return Position.X == position.X && position.Y >= Position.Y && position.Y < Position.Y + Size;
            }
            else
            {
                return Position.Y == position.Y && position.X >= Position.X && position.X < Position.X + Size;
            }
        }

        public void ChangePosition(Point position)
        {
            if (Type == View.HORIZONTAL)
            {
                Type = View.VERTICAL;
            }
            else
            {
                Type = View.HORIZONTAL;
            }
            Position = position;
        }
       
    }
}
