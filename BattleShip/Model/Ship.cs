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
        public List<Cell> Cells { get; set; }
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
            Cells = new List<Cell>();
            if (Type == View.HORIZONTAL)
            {
                for(int i = position.Y; i < position.Y + Health; i++)
                {
                    if(i < 12)
                    {
                        Cells.Add(new Cell(new Point { X = position.X, Y = i }));
                    }
                    else
                    {
                        Cells.Add(new Cell(new Point { X = position.X, Y = i - Health }));
                    }                    
                }
            }
            else
            {
                for (int i = position.X; i < position.X + Health; i++)
                {
                    if(i < 12)
                    {
                        Cells.Add(new Cell(new Point { X = i, Y = position.Y }));
                    }
                    else
                    {
                        Cells.Add(new Cell(new Point { X = i - Health, Y = position.Y }));
                    }                    
                }
            }
        }

        public void ShowShip(DataGridView grid)
        {
            foreach (Cell cell in Cells)
            {
                if(cell.Alive)
                {
                    grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color;
                }
                else
                {
                    grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Red;
                }
            }            
        }

        public bool ExistPosition(Point position)
        {
            return Cells.Exists(cell => cell.Positon.Equals(position));
        }

        public bool ExistShip(Ship selected)
        {
            foreach(Cell cell in Cells)
            {
                if (selected.ExistPosition(cell.Positon))
                    return true;
            }
            return false;
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

        public void ShootPosition(Point position)
        {
            Cells.Find(cell => cell.Positon.Equals(position)).Alive = false;
        }
    }
}
