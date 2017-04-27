using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Model;
using BattleShip;
using System.Drawing;
using System.Windows.Forms;

namespace BattleShip.Controller
{
    class PlayerController
    {
        private List<Ship> ships;
        private List<Point> positions;
        public Ship selected;
        private Point shot;
        private Point first;
        private bool useThis;
       
        public PlayerController()
        {
            ships = new List<Ship>();
            positions = new List<Point>();
            ships.Add(new Ship(1, Color.Blue, new Point { X = 0, Y = 2 }, Ship.View.HORIZONTAL));
            ships.Add(new Ship(1, Color.Blue, new Point { X = 0, Y = 6 }, Ship.View.VERTICAL));
            ships.Add(new Ship(1, Color.Blue, new Point { X = 11, Y = 2 }, Ship.View.HORIZONTAL));
            ships.Add(new Ship(2, Color.Blue, new Point { X = 2, Y = 1 }, Ship.View.VERTICAL));
            ships.Add(new Ship(2, Color.Blue, new Point { X = 4, Y = 8 }, Ship.View.HORIZONTAL));
            ships.Add(new Ship(3, Color.Blue, new Point { X = 5, Y = 7 }, Ship.View.VERTICAL));
            ships.Add(new Ship(3, Color.Blue, new Point { X = 8, Y = 1 }, Ship.View.HORIZONTAL));
            ships.Add(new Ship(4, Color.Blue, new Point { X = 8, Y = 9 }, Ship.View.VERTICAL));
            ships.Add(new Ship(5, Color.Blue, new Point { X = 6, Y = 5 }, Ship.View.VERTICAL));
            for(int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    positions.Add(new Point { X = i, Y = j });
                }
            }
            selected = null;
            shot = new Point();
            useThis = true;
        }          

        public void SetGridView(DataGridView grid)
        {
            grid.Rows.Clear();
            grid.RowCount = 12;
            grid.ColumnCount = 12;;
            for (int i = 0; i < 12; i++)
            {
                grid.Rows[i].Height = 30;
                grid.Columns[i].Width = 30;
            }
            grid.ClearSelection();
        }

        public void ShowShips(DataGridView grid)
        {
            ships.ForEach(ship => ship.ShowShip(grid));
        }

        public bool SearchShip()
        {
            return ships.FindAll(ship => ship.ExistShip(selected)).Count > 1;
        }

        public void Select(Point Position)
        {
            selected = ships.FirstOrDefault(ship => ship.ExistPosition(Position));
        }

        public void UnSelect()
        {
            selected = null;
        }

        public void DisableCells(DataGridView grid)
        {
            grid.Enabled = false;
            ships.ForEach(ship => ship.Color = Color.Gray);
            /*grid.DefaultCellStyle.BackColor = Color.LightGray;
            grid.DefaultCellStyle.ForeColor = Color.DarkGray;*/
        }

        public void EnableCells(DataGridView grid)
        {
            grid.Enabled = true;            
            ships.ForEach(ship => ship.Color = Color.Blue);
        }

        public void Shoot(DataGridView grid)
        {
            if(selected != null)
            {
                if (selected.Destroyed())
                {
                    selected = null;
                    GenerateRandom(grid);
                    return;
                }
                if (shot.X + 1 < 12 && useThis && positions.Contains(new Point { X = shot.X + 1, Y = shot.X }))
                {
                    Point pos = new Point { X = shot.X + 1, Y = shot.Y };
                    positions.Remove(pos);
                    if(selected.ExistPosition(pos))
                    {
                        selected.ShootPosition(pos);
                        shot = pos;
                        return;
                    }
                    else
                    {
                        useThis = false;                        
                        shot = first;
                        UpdateGrid(pos, grid);
                        return;
                    }
                }
                if(shot.X - 1 >= 0 && !useThis && positions.Contains(new Point { X = shot.X - 1, Y = shot.X }))
                {
                    Point pos = new Point { X = shot.X - 1, Y = shot.Y };
                    positions.Remove(pos);
                    if (selected.ExistPosition(pos))
                    {
                        selected.ShootPosition(pos);
                        shot = pos;
                        return;
                    }
                    else
                    {
                        useThis = true;                        
                        shot = first;
                        UpdateGrid(pos, grid);
                        return;
                    }
                }
                else
                {
                    useThis = true;
                    Shoot(grid);
                }
            }
            GenerateRandom(grid);
        }

        private void GenerateRandom(DataGridView grid)
        {
            int index = new Random().Next(positions.Count);
            Point position = positions[index];
            positions.RemoveAt(index);
            foreach (Ship ship in ships)
            {
                if (ship.ExistPosition(position))
                {
                    ship.ShootPosition(position);
                    shot = position;
                    first = shot;
                    Select(position);
                    return;
                }
            }
            UpdateGrid(position, grid);
        }

        private void UpdateGrid(Point position,DataGridView grid)
        {
            //grid.Rows[position.X].Cells[position.Y].Style.BackColor = Color.LightBlue;
            grid.Rows[position.X].Cells[position.Y].Value = "X";
            grid.Rows[position.X].Cells[position.Y].Style.BackColor = Color.Green;
        }
    }
}
