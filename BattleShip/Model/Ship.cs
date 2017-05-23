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
        public List<Cell> Cells { get; set; }
        public View Type { get; set; }
        public List<Point> viewPoints { get; set; }
        private Point shotPosition;

        public Ship(int size, Color color, Point position, View type)
        {
            Size = size;
            Color = color;
            Type = type;
            AddPositions(position);
        }

        public void AddPositions(Point position)
        {
            Cells = new List<Cell>();

            if (Type == View.HORIZONTAL)
            {
                for (int i = position.Y; i < position.Y + Size; i++)
                {
                    if (i < 10)
                    {
                        Cells.Add(new Cell(new Point { X = position.X, Y = i }));

                    }
                    else
                    {
                        Cells.Add(new Cell(new Point { X = position.X, Y = i - Size }));
                    }
                }               
            }
            else
            {
                for (int i = position.X; i < position.X + Size; i++)
                {
                    if (i < 10)
                    {
                        Cells.Add(new Cell(new Point { X = i, Y = position.Y }));
                    }
                    else
                    {
                        Cells.Add(new Cell(new Point { X = i - Size, Y = position.Y }));
                    }
                }
            }
            Cells = Cells.OrderBy(cell => cell.Positon.X).ThenBy(cell => cell.Positon.Y).ToList();
            if (Size > 1)
           {
                AddImages();
           }
            
            AddViewPoints();
        }

        private void AddImages()
        {            
            for (int i = 0; i < Size; i++)
            {
                string file = string.Format("_{0}{1}", Size, i + 1);
                Cell cell = Cells.ElementAt(i);
                cell.Img = (Image)Properties.Resources.ResourceManager.GetObject(file);
                if(Type == View.HORIZONTAL)
                {
                    cell.SwapImage();
                }
            }
        }

        private void ShowDeadCells(DataGridView grid, Point position)
        {
            if (position.X - 1 >= 0 && position.Y - 1 >= 0)
            {             
                DataGridViewImageCell imgCell = new DataGridViewImageCell();
                imgCell.Value = Properties.Resources.missImage;
                grid.Rows[position.X - 1].Cells[position.Y - 1] = imgCell;
            }
            if (position.X - 1 >= 0 && position.Y + 1 < 10)
            {
                DataGridViewImageCell imgCell = new DataGridViewImageCell();
                imgCell.Value = Properties.Resources.missImage;
                grid.Rows[position.X - 1].Cells[position.Y + 1] = imgCell;
            }
            if (position.X + 1 < 10 && position.Y - 1 >= 0)
            {
                DataGridViewImageCell imgCell = new DataGridViewImageCell();
                imgCell.Value = Properties.Resources.missImage;
                grid.Rows[position.X + 1].Cells[position.Y - 1] = imgCell;
            }
            if (position.X + 1 < 10 && position.Y + 1 < 10)
            {
                DataGridViewImageCell imgCell = new DataGridViewImageCell();
                imgCell.Value = Properties.Resources.missImage;
                grid.Rows[position.X + 1].Cells[position.Y + 1] = imgCell;
            }
        }

        public void ShowShip(DataGridView grid)
        {        
            if (Destroyed())
            {
                foreach(Point point in viewPoints)
                {
                    if(point.X >= 0 && point.X < 10 && point.Y >= 0 && point.Y < 10)
                    {
                        if(!Cells.Exists(cell => cell.Positon.Equals(point)))
                        {
                            DataGridViewImageCell imgCell = new DataGridViewImageCell();
                            imgCell.Value = Properties.Resources.missImage;
                            grid.Rows[point.X].Cells[point.Y] = imgCell;
                        }
                    }
                }
                Cells.ForEach(cell => grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Black);
            }
            else
            {
                foreach (Cell cell in Cells)
                {
                    if (!cell.Alive)
                    {
                        grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Red;
                        ShowDeadCells(grid, cell.Positon);
                    }
                    else
                    {
                        if (Size > 1)
                        {
                            DataGridViewImageCell imgCell = new DataGridViewImageCell();
                            imgCell.Value = cell.Img;
                           
                            grid.Rows[cell.Positon.X].Cells[cell.Positon.Y] = imgCell;
                        } 
                      
                        else 
                        {
                            string name = "_11";
                            DataGridViewImageCell imgCell = new DataGridViewImageCell();
                            imgCell.Value = Properties.Resources.ResourceManager.GetObject(name);
                            grid.Rows[cell.Positon.X].Cells[cell.Positon.Y] = imgCell;
                        }
                    }
                }
            }
        }

        public void enemyShipsDraw(DataGridView grid)
        {
            if (Destroyed())
            {
                foreach (Point point in viewPoints)
                {
                    if (point.X >= 0 && point.X < 10 && point.Y >= 0 && point.Y < 10)
                    {
                        if (!Cells.Exists(cell => cell.Positon.Equals(point)))
                        {
                            DataGridViewImageCell imgCell = new DataGridViewImageCell();
                            imgCell.Value = Properties.Resources.missImage;
                            grid.Rows[point.X].Cells[point.Y] = imgCell;
                        }
                    }
                }
                foreach (Cell cell in Cells)
                {                    
                    grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Black;
                }                
            }
            else
            {
                foreach (Cell cell in Cells)
                {
                    if (!cell.Alive)
                    {
                        grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Red;
                        ShowDeadCells(grid, cell.Positon);
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
                    return true;
            }
            return false;
        }

        public void ChangePosition(Point point)
        {
            if (Type == View.HORIZONTAL)
            {
                Type = View.VERTICAL;
            }
            else
            {
                Type = View.HORIZONTAL;
            }
            AddPositions(point);
        }

        public void ShootPosition(Point position)
        {
            Cells.Find(cell => cell.Positon.Equals(position)).Alive = false;
            shotPosition = position;
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
            viewPoints = new List<Point>();
            Point position = Cells[0].Positon;

            if (Type == View.HORIZONTAL)
            {
                for (int i = position.X - 1; i <= position.X + 1; i++)
                {
                    for (int j = position.Y - 1; j <= position.Y + Size; j++)
                    {
                        viewPoints.Add(new Point { X = i, Y = j });
                    }
                }
                return;
            }

            if (Type == View.VERTICAL)
            {
                for (int i = position.X - 1; i <= position.X + Size; i++)
                {
                    for (int j = position.Y - 1; j <= position.Y + 1; j++)
                    {
                        viewPoints.Add(new Point { X = i, Y = j });
                    }
                }
            }
        }
    }
}
