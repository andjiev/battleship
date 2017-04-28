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
            ships.Add(new Ship(1, Color.Blue, new Point { X = 0, Y = 2 }, Ship.View.HORIZONTAL));
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
            direction = (Direction)new Random().Next(4);
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
                    if(shot.X + 1 > 11)
                    {
                        direction = Direction.UP;
                        shot = first;
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
                    if (shot.X - 1 < 0)
                    {
                        direction = Direction.DOWN;
                        shot = first;
                    }
                    else
                    {
                        direction = Direction.LEFT;
                        shot = first;
                        Shoot(grid);
                        return;
                    }
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
                    if (shot.Y - 1 < 0)
                    {
                        direction = Direction.RIGHT;
                        shot = first;
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
                    if (shot.Y + 1 > 11)
                    {
                        direction = Direction.LEFT;
                        shot = first;
                    }
                    else
                    {
                        direction = Direction.DOWN;
                        shot = first;
                        Shoot(grid);
                        return;
                    }
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
    }
}
