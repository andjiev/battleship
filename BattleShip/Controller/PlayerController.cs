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
        private List<int> amounts;
        public Ship selected;
        private Point shot;
        private Point first;
        public bool found;
        private enum Direction 
        {
            DOWN,
            UP,
            RIGHT,
            LEFT
        }
        private Direction direction;
       
        public PlayerController()
        {
            ships = new List<Ship>();
            positions = new List<Point>();
            for(int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    positions.Add(new Point { X = i, Y = j });
                }
            }
            selected = null;
            shot = new Point();
            amounts = new List<int>();
            amounts.Add(3);
            amounts.Add(2);
            amounts.Add(2);
            amounts.Add(1);
            amounts.Add(1);
            Random();
        }          

        public void SetGridView(DataGridView grid)
        {
            grid.Rows.Clear();
            grid.RowCount = 10;
            grid.ColumnCount = 10;;
            for (int i = 0; i < 10; i++)
            {
                grid.Rows[i].Height = 36;
                grid.Columns[i].Width = 36;
            }
            grid.TabStop = false;
            grid.ClearSelection();
        }

        public void ShowShips(DataGridView grid)
        {
            ships.ForEach(ship => ship.ShowShip(grid,selected));
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
            System.Media.SoundPlayer sound2 = new System.Media.SoundPlayer(Properties.Resources.explosion);
            if (selected != null)
            {
                Point position = new Point();
                if (selected.Destroyed())
                {
                    selected = null;
                    sound2.Play();
                    GenerateRandom(grid);
                    return;
                }

                if(direction == Direction.DOWN)
                {
                    if(positions.Contains(new Point { X = shot.X + 1, Y = shot.Y }))
                    {
                        position = new Point { X = shot.X + 1, Y = shot.Y };
                        positions.Remove(position);
                        if (selected.ExistPosition(position))
                        {
                            selected.ShootPosition(position);                           
                            sound2.Play();
                            shot = position;
                            found = true;
                            return;
                        }
                        else
                        {
                            direction = Direction.UP;
                            shot = first;
                            UpdateGrid(position, grid);
                            return;
                        }
                    }
                    else
                    {
                        direction = Direction.UP;
                        shot = first;
                        Shoot(grid);
                        return;
                    }                    
                }

                if(direction == Direction.UP)
                {
                    if (positions.Contains(new Point { X = shot.X - 1, Y = shot.Y }))
                    {
                        position = new Point { X = shot.X - 1, Y = shot.Y };
                        positions.Remove(position);
                        if (selected.ExistPosition(position))
                        {
                            sound2.Play();
                            selected.ShootPosition(position);
                            shot = position;
                            found = true;
                            return;
                        }
                        else
                        {
                            direction = Direction.DOWN;
                            shot = first;
                            UpdateGrid(position, grid);
                            return;
                        }
                    }
                    else if (selected.Cells.FindAll(cell => !cell.Alive).Count > 1)
                    {
                        direction = Direction.DOWN;
                    }
                   
                    else
                    {
                        direction = Direction.LEFT;
                    }
                    shot = first;
                    Shoot(grid);
                    return;
                }

                if (direction == Direction.LEFT)
                {
                    if (positions.Contains(new Point { X = shot.X, Y = shot.Y - 1 }))
                    {
                        position = new Point { X = shot.X, Y = shot.Y - 1};
                        positions.Remove(position);
                        if (selected.ExistPosition(position))
                        {
                            sound2.Play();
                            selected.ShootPosition(position);
                            shot = position;
                            found = true;
                            return;
                        }
                        else
                        {
                            direction = Direction.RIGHT;
                            shot = first;
                            UpdateGrid(position, grid);
                            return;
                        }
                    }
                   
                    else
                    {
                        direction = Direction.RIGHT;
                        shot = first;
                        Shoot(grid);
                        return;
                    }
                }

                if (direction == Direction.RIGHT)
                {
                    if (positions.Contains(new Point { X = shot.X, Y = shot.Y + 1 }))
                    {
                        position = new Point { X = shot.X, Y = shot.Y + 1 };
                        positions.Remove(position);
                        if (selected.ExistPosition(position))
                        {
                            sound2.Play();
                            selected.ShootPosition(position);
                            shot = position;
                            found = true;
                            return;
                        }
                        else
                        {
                            direction = Direction.LEFT;
                            shot = first;
                            UpdateGrid(position, grid);
                            return;
                        }
                    }
                    else if (selected.Cells.FindAll(cell => !cell.Alive).Count > 1)
                    {
                        direction = Direction.LEFT;
                    }

                    else
                    {
                        direction = Direction.DOWN;
                    }
                    shot = first;
                    Shoot(grid);
                    return;
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
                    System.Media.SoundPlayer sound2 = new System.Media.SoundPlayer(Properties.Resources.explosion);
                    sound2.Play();
                    ship.ShootPosition(position);
                    direction = (Direction)new Random().Next(4);
                    shot = position;
                    first = shot;
                    found = true;
                    Select(position);
                    return;
                }
            }
            UpdateGrid(position, grid);
        }

        private void UpdateGrid(Point position,DataGridView grid)
        {
            grid.Rows[position.X].Cells[position.Y].Value = "X";
            grid.Rows[position.X].Cells[position.Y].Style.BackColor = Color.Green;
            System.Media.SoundPlayer sound = new System.Media.SoundPlayer(Properties.Resources.miss);
            sound.Play();
            found = false;
        }

        public bool Won()
        {
            return ships.All(ship => ship.Destroyed());
        }

        public void Random()
        {
            ships = new List<Ship>();            
            bool picked = false;            
            for (int i = 4; i >= 0; i--)
            {
                for(int j = 0; j < amounts[i]; j++)
                {
                    while(!picked)
                    {
                        int index = new Random().Next(positions.Count);
                        Ship.View type = (Ship.View)new Random().Next(2);
                        Point position = positions[index];
                        
                      
                            Ship primary = new Ship(i + 1, Color.Blue, position, type);
                            //if(ships.Exists(ship => ship.ExistShip(primary)))
                           // {
                            //    primary.ChangePosition(position);
                          //  }
                            if (!ships.Exists(ship => ship.ExistShip(primary)))
                            {
                                ships.Add(primary);
                                picked = true;
                                RemovePositions(primary);
                            }
                    }                        
                    
                    picked = false;
                }                
            }
            positions = new List<Point>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    positions.Add(new Point { X = i, Y = j });
                }
            }
        }

        private void RemovePositions(Ship primary)
        {
            foreach (Point point in primary.viewPoints)
            {
                positions.Remove(point);
            }            
        }
    }
}
