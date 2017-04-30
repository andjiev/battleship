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
        public List<Point> viewPoints { get; set; }
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
            AddViewPoints();
        }

        public void ShowShip(DataGridView grid)
        {   
            //TODO : Cell images, needs implementing
           // DataGridViewImageCell imgCell = new DataGridViewImageCell();
            //string path = Application.StartupPath;
            //img = System.Drawing.Image.FromFile(path + "\\Images\\Remove-icon.png");
            //imgCell.Value = Image.FromFile(path +  "\\Images\\Remove-icon.png");
         //    grid.Rows[1].Cells[1].Value = imgCell;
         foreach(Point point in viewPoints)
            {
                if(point.X >= 0 && point.X < 12 && point.Y >=0 && point.Y < 12)
                grid.Rows[point.X].Cells[point.Y].Style.BackColor = Color.Purple;
            }
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
                        grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Empty;
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
            foreach (Point point in selected.viewPoints)
            {
                if (Cells.Exists(cell => cell.Positon.Equals(point)))
                //if(viewPoints.Exists(position => position.Equals(point)))
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

        public bool ExistsBig(Ship primary)
        {
            foreach (Point point in primary.viewPoints)
            {
                if (viewPoints.Exists(position => position.Equals(point)))
                    return true;
            }
            return false;
        }

        public void AddViewPoints()
        {
            /*List<Point> positions = new List<Point>();
             HashSet<Point> searchPoints = new HashSet<Point>();
             Cells.ForEach(cell => positions.Add(cell.Positon));

             foreach (Point point in positions)
             {
                 for (int i = point.X - 1; i <= point.X + 1; ++i)
                 {
                     for (int j = point.Y - 1; j <= point.Y + 1; ++j)
                     {
                         searchPoints.Add(new Point { X = i, Y = j });
                     }
                 }
             }
             viewPoints = searchPoints.ToList();
             */
            viewPoints = new List<Point>();
            int X = Cells.Min(cell => cell.Positon.X);
            int Y = Cells.Min(cell => cell.Positon.Y);
            if(Type == View.HORIZONTAL)
            {
                for (int i = X - 1; i <= X + 1; i++)
                {
                    for(int j = Y - 1; j <= Y + Health; j++)
                    {
                        viewPoints.Add(new Point { X = i, Y = j });
                    }
                }
                return;
            }
            if (Type == View.VERTICAL)
            {
                for(int i = X - 1; i <= X + Health; i++)
                {
                    for (int j = Y - 1; j <= Y + 1; j++)
                    {
                        viewPoints.Add(new Point { X = i, Y = j });
                    }
                }
            }
        }
    }
}
