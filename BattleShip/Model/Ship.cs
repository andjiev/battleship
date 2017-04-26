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
        public int Health { get; set; }
        public Color Color { get; set; }
        public List<Point> Positions { get; set; }
        public View Type { get; set; }

        public Ship(int health, Color color, Point position, View type)
        {
            Health = health;
            Color = color;            
            Type = type;
            AddPositions(position);
        }

        public void AddPositions(Point position)
        {
            Positions = new List<Point>();
            if (Type == View.HORIZONTAL)
            {
                for(int i = position.Y; i < position.Y + Health; i++)
                {
                    if(i < 12)
                    {
                        Positions.Add(new Point { X = position.X, Y = i });
                    }
                    else
                    {
                        Positions.Add(new Point { X = position.X, Y = i - Health });
                    }                    
                }
            }
            else
            {
                for (int i = position.X; i < position.X + Health; i++)
                {
                    if(i < 12)
                    {
                        Positions.Add(new Point { X = i, Y = position.Y });
                    }
                    else
                    {
                        Positions.Add(new Point { X = i - Health, Y = position.Y });
                    }                    
                }
            }
        }

        public void ShowShip(DataGridView grid)
        {            
            Positions.ForEach(positon => grid.Rows[positon.X].Cells[positon.Y].Style.BackColor = Color);
        }

        public bool Exists(Point position)
        {
            return Positions.Exists(Position => Position.Equals(position));
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
            AddPositions(position);
        }
    }
}
