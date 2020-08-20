using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip.Model
{
    [Serializable]
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
        public int gridSize;

        public Ship(int size, Color color, Point position, View type, int boardSize)
        {
            gridSize = boardSize;
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
                    if (i < gridSize)
                    {
                        Cells.Add(new Cell(new Point { X = position.X, Y = i }, gridSize));

                    }
                    else
                    {
                        Cells.Add(new Cell(new Point { X = position.X, Y = i - Size }, gridSize));
                    }
                }
            }
            else
            {
                for (int i = position.X; i < position.X + Size; i++)
                {
                    if (i < gridSize)
                    {
                        Cells.Add(new Cell(new Point { X = i, Y = position.Y }, gridSize));
                    }
                    else
                    {
                        Cells.Add(new Cell(new Point { X = i - Size, Y = position.Y }, gridSize));
                    }
                }
            }
            Cells = Cells.OrderBy(cell => cell.Positon.X).ThenBy(cell => cell.Positon.Y).ToList();
            AddImages();

            AddViewPoints();
        }

        private void AddImages()
        {
            for (int i = 0; i < Size; i++)
            {
                string file = string.Format("_{0}{1}", Size, i + 1);
                Cell cell = Cells.ElementAt(i);
                cell.Img = (Image)Properties.Resources.ResourceManager.GetObject(file);
                cell.Opacity(1);
                if (Type == View.HORIZONTAL)
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
                imgCell.Value = Properties.Resources.dotImage;
                grid.Rows[position.X - 1].Cells[position.Y - 1] = imgCell;
            }
            if (position.X - 1 >= 0 && position.Y + 1 < gridSize)
            {
                DataGridViewImageCell imgCell = new DataGridViewImageCell();
                imgCell.Value = Properties.Resources.dotImage;
                grid.Rows[position.X - 1].Cells[position.Y + 1] = imgCell;
            }
            if (position.X + 1 < gridSize && position.Y - 1 >= 0)
            {
                DataGridViewImageCell imgCell = new DataGridViewImageCell();
                imgCell.Value = Properties.Resources.dotImage;
                grid.Rows[position.X + 1].Cells[position.Y - 1] = imgCell;
            }
            if (position.X + 1 < gridSize && position.Y + 1 < gridSize)
            {
                DataGridViewImageCell imgCell = new DataGridViewImageCell();
                imgCell.Value = Properties.Resources.dotImage;
                grid.Rows[position.X + 1].Cells[position.Y + 1] = imgCell;
            }
        }

        public void ShowShip(DataGridView grid)
        {
            if (Destroyed())
            {
                foreach (Point point in viewPoints)
                {
                    if (point.X >= 0 && point.X < gridSize && point.Y >= 0 && point.Y < gridSize)
                    {
                        if (!Cells.Exists(cell => cell.Positon.Equals(point)))
                        {
                            DataGridViewImageCell imgCell = new DataGridViewImageCell();
                            imgCell.Value = Properties.Resources.dotImage;
                            grid.Rows[point.X].Cells[point.Y] = imgCell;
                        }
                    }
                }
                foreach(Cell cell in Cells)
                {
                    if (!cell.ChangedOpacity)
                    {
                        double opacity = 0.6;
                        cell.Opacity((float)opacity);
                        cell.ChangedOpacity = true;
                        
                    }
                    DataGridViewImageCell imgCell = new DataGridViewImageCell();
                    imgCell.Value = cell.Img;
                    grid.Rows[cell.Positon.X].Cells[cell.Positon.Y] = imgCell;
                    grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Black;
                }
            }
            else
            {
                foreach (Cell cell in Cells)
                {
                    DataGridViewImageCell imgCell = new DataGridViewImageCell();
                    imgCell.Value = cell.Img;
                    grid.Rows[cell.Positon.X].Cells[cell.Positon.Y] = imgCell;
                    if (!cell.Alive)
                    {
                        grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Red;
                        ShowDeadCells(grid, cell.Positon);
                    }
                }
            }
        }

        public void ShowShipFOFB(DataGridView grid, List<Ship> ships)
        {
            if (Destroyed())
            {
                foreach (Point point in viewPoints)
                {
                    if (point.X >= 0 && point.X < gridSize && point.Y >= 0 && point.Y < gridSize)
                    {
                        if (!Cells.Exists(cell => cell.Positon.Equals(point)))
                        {
                            DataGridViewImageCell imgCell = new DataGridViewImageCell();
                            string file = string.Format("_{0}", GetShipCount(point.X, point.Y, ships));
                            imgCell.Value = Properties.Resources.ResourceManager.GetObject(file);
                            grid.Rows[point.X].Cells[point.Y] = imgCell;
                        }
                    }
                }
                foreach (Cell cell in Cells)
                {
                    if (!cell.ChangedOpacity)
                    {
                        double opacity = 0.6;
                        cell.Opacity((float)opacity);
                        cell.ChangedOpacity = true;

                    }
                    DataGridViewImageCell imgCell = new DataGridViewImageCell();
                    imgCell.Value = cell.Img;
                    grid.Rows[cell.Positon.X].Cells[cell.Positon.Y] = imgCell;
                    grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Black;
                }
            }
            else
            {
                foreach (Cell cell in Cells)
                {
                    DataGridViewImageCell imgCell = new DataGridViewImageCell();
                    imgCell.Value = cell.Img;
                    grid.Rows[cell.Positon.X].Cells[cell.Positon.Y] = imgCell;
                    if (!cell.Alive)
                    {
                        grid.Rows[cell.Positon.X].Cells[cell.Positon.Y].Style.BackColor = Color.Red;
                        ShowDeadCells(grid, cell.Positon);
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
                    if (point.X >= 0 && point.X < gridSize && point.Y >= 0 && point.Y < gridSize)
                    {
                        if (!Cells.Exists(cell => cell.Positon.Equals(point)))
                        {
                            DataGridViewImageCell imgCell = new DataGridViewImageCell();
                            imgCell.Value = Properties.Resources.dotImage;
                            grid.Rows[point.X].Cells[point.Y] = imgCell;
                        }
                    }
                }
                foreach (Cell cell in Cells)
                {

                    DataGridViewImageCell imgCell = new DataGridViewImageCell();
                    if(!cell.ChangedOpacity)
                    {                       
                        double opacity = 0.6;
                        cell.Opacity((float)opacity);
                        cell.ChangedOpacity = true;
                    }                    
                    imgCell.Value = cell.Img;

                    grid.Rows[cell.Positon.X].Cells[cell.Positon.Y] = imgCell;
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

        public void enemyShipsDrawFOFB(DataGridView grid, List<Ship> ships)
        {
            if (Destroyed())
            {
                foreach (Point point in viewPoints)
                {
                    if (point.X >= 0 && point.X < gridSize && point.Y >= 0 && point.Y < gridSize)
                    {
                        if (!Cells.Exists(cell => cell.Positon.Equals(point)))
                        {
                            DataGridViewImageCell imgCell = new DataGridViewImageCell();
                            string file = string.Format("_{0}", GetShipCount(point.X, point.Y, ships));
                            imgCell.Value = Properties.Resources.ResourceManager.GetObject(file);
                            grid.Rows[point.X].Cells[point.Y] = imgCell;
                        }
                    }
                }
                foreach (Cell cell in Cells)
                {

                    DataGridViewImageCell imgCell = new DataGridViewImageCell();
                    if (!cell.ChangedOpacity)
                    {
                        double opacity = 0.6;
                        cell.Opacity((float)opacity);
                        cell.ChangedOpacity = true;
                    }
                    imgCell.Value = cell.Img;

                    grid.Rows[cell.Positon.X].Cells[cell.Positon.Y] = imgCell;
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
        public int GetShipCount(int x, int y, List<Ship> ships)
        {
            int count = 0;
            foreach (Ship ship in ships)
            {
                if (ship.Cells[0].Positon.X == x || ship.Cells[0].Positon.Y == y)
                    count++;
            }

            return count;
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
