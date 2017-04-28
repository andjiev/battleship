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
        public Image img { get; set; }
        
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
                        Cells.Add(new Cell(new Point { X = position.X, Y = i },img));
                        
                    }
                    else
                    {
                        Cells.Add(new Cell(new Point { X = position.X, Y = i - Health }, img));
                    }                    
                }
            }
            else
            {
                for (int i = position.X; i < position.X + Health; i++)
                {
                    if(i < 12)
                    {
                        Cells.Add(new Cell(new Point { X = i, Y = position.Y },img));
                    }
                    else
                    {
                        Cells.Add(new Cell(new Point { X = i - Health, Y = position.Y }, img));
                    }                    
                }
            }
        }

        public void ShowShip(DataGridView grid)
        {   
            //TODO : Cell images, needs implementing
            DataGridViewImageCell imgCell = new DataGridViewImageCell();
            string path = Application.StartupPath;
            img = System.Drawing.Image.FromFile(path + "\\Images\\Remove-icon.png");
            imgCell.Value = Image.FromFile(path +  "\\Images\\Remove-icon.png");
         //    grid.Rows[1].Cells[1].Value = imgCell;
            if (this.Destroyed())
            {                
                Cells.ForEach(cell => grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Black);                
            }
            else
            {
                foreach (Cell cell in Cells)
                {
                    if (!cell.Alive)
                    {
                        grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Red;

                    }
                    else
                    {
                        grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color;
                    }
                }
            }                
        }
        public void enemyShipsDraw(DataGridView grid)
        {
            if (this.Destroyed())
            {
                Cells.ForEach(cell => grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Black);
            }
            else {


                foreach (Cell cell in Cells)
                {
                    if (!cell.Alive)
                    {
                        grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Red;

                    }
                    else
                    {
                        grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.PeachPuff;
                    }
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

        public bool Destroyed()
        {
            return Cells.All(cell => !cell.Alive);
        }
        public bool checkOverlapping(Ship s)
        {
            foreach (Cell c in s.Cells.ToList()) {
                if (Cells.Contains(c) || c.checkEqual(new Point { X=c.Positon.Y, Y = c.Positon.X }))
                {
                    return false;
                }

            }
            return true;
        }
    }
}
