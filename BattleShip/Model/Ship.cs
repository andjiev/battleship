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

            if(Size == 3)
            {
                AddImages();
            }
            if(Size == 5)
            {
                AddImages();
            }
            AddViewPoints();
        }

        private void AddImages()
        {
            Cells = Cells.OrderBy(cell => cell.Positon.X).ThenBy(cell => cell.Positon.Y).ToList();
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
                grid.Rows[position.X - 1].Cells[position.Y - 1].Style.BackColor = Color.LightBlue;
            }
            if (position.X - 1 >= 0 && position.Y + 1 < 10)
            {
                grid.Rows[position.X - 1].Cells[position.Y + 1].Style.BackColor = Color.LightBlue;
            }
            if (position.X + 1 < 10 && position.Y - 1 >= 0)
            {
                grid.Rows[position.X + 1].Cells[position.Y - 1].Style.BackColor = Color.LightBlue;
            }
            if (position.X + 1 < 10 && position.Y + 1 < 10)
            {
                grid.Rows[position.X + 1].Cells[position.Y + 1].Style.BackColor = Color.LightBlue;
            }
        }

        public void ShowShip(DataGridView grid)
        {
            //TODO : Cell images, needs implementing
           
            //string path = Application.StartupPath;
            
            
            //imgCell.Value = Image.FromFile(path +  "\\Images\\Remove-icon.png");
            /*  foreach (Point point in selected.viewPoints)
                   {
                       if (point.X >= 0 && point.X < 10 && point.Y >= 0 && point.Y < 10)
                           grid.Rows[point.X].Cells[point.Y].Style.BackColor = Color.Purple;
                   }*/

            if (this.Destroyed())
            {
                foreach(Point point in viewPoints)
                {
                    if(point.X >= 0 && point.X < 10 && point.Y >= 0 && point.Y < 10)
                    {
                        if(!Cells.Exists(cell => cell.Positon.Equals(point)))
                        {
                            
                            grid.Rows[point.X].Cells[point.Y].Style.BackColor = Color.LightBlue;
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
                        if (Size == 3)
                        {
                            DataGridViewImageCell imgCell = new DataGridViewImageCell();
                            imgCell.Value = cell.Img;
                            grid.Rows[cell.Positon.X].Cells[cell.Positon.Y] = imgCell;
                        } else if(Size == 5)
                        {
                            DataGridViewImageCell imgCell = new DataGridViewImageCell();
                            imgCell.Value = cell.Img;
                            grid.Rows[cell.Positon.X].Cells[cell.Positon.Y] = imgCell;
                        }
                        else
                        {
                            string name = "Remove_icon";
                            DataGridViewImageCell imgCell = new DataGridViewImageCell();
                            imgCell.Value = Properties.Resources.ResourceManager.GetObject(name);
                            //grid.NotifyCurrentCellDirty(true);
                            //grid.BeginEdit(false);
                            grid.Rows[cell.Positon.X].Cells[cell.Positon.Y] = imgCell;
                            // grid.EndEdit();
                        }


                    }
                }
            }
        }

        public void RemoveShip(DataGridView grid)
        {
            foreach (Cell cell in Cells)
            {
               grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Value = new Bitmap(36, 36);
            }
        }

        public void enemyShipsDraw(DataGridView grid)
        {
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
            if (Type == View.HORIZONTAL)
            {
                for (int i = X - 1; i <= X + 1; i++)
                {
                    for (int j = Y - 1; j <= Y + Size; j++)
                    {
                        viewPoints.Add(new Point { X = i, Y = j });
                    }
                }
                return;
            }
            if (Type == View.VERTICAL)
            {
                for (int i = X - 1; i <= X + Size; i++)
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
