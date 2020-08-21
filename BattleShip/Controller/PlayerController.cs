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
    [Serializable]
    class PlayerController : Player
    {
        private Point shot;
        private Point first;
        public bool found;
        public int shots;
        public DataGridView dgvPlayer;
        private enum Direction
        {
            DOWN,
            UP,
            RIGHT,
            LEFT
        }
        private Direction direction;

        public PlayerController(List<GameMode> gameModes) : base(gameModes)
        {
            isPlayer = true;
            selected = null;
            shot = new Point();
            Random();
        }
        public void ShowShips(DataGridView grid)
        {
            ships.ForEach(ship => ship.ShowShip(grid));
            // if FOFB do instead of line above
            // ships.ForEach(ship => ship.ShowShipFOFB(grid, ships));
        }

        public void ShowSelectedShip(DataGridView grid)
        {
            selected.ShowShip(grid);
        }

        public bool SearchShip()
        {
            return ships.FindAll(ship => ship.ExistShip(selected)).Count > 1;
        }

        public bool SearchShip(Point position)
        {
            return ships.Exists(ship => ship.ExistPosition(position));
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
                    RemoveDeadShip();
                    selected = null;
                    if (!Game.MuteClicked)
                    {
                        sound2.Play();
                    }
                    else
                    {
                        sound2.Stop();
                    }
                    Game.score -= 50;
                    GenerateRandom(grid);
                    return;
                }

                if (direction == Direction.DOWN)
                {
                    if (positions.Contains(new Point { X = shot.X + 1, Y = shot.Y }))
                    {
                        position = new Point { X = shot.X + 1, Y = shot.Y };
                        positions.Remove(position);
                        if (selected.ExistPosition(position))
                        {
                            selected.ShootPosition(position);
                            RemoveDeadPoints(position);
                            if (!Game.MuteClicked)
                            {
                                sound2.Play();
                            }
                            else
                            {
                                sound2.Stop();
                            }
                            shot = position;
                            Game.score -= 50;

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

                if (direction == Direction.UP)
                {
                    if (positions.Contains(new Point { X = shot.X - 1, Y = shot.Y }))
                    {
                        position = new Point { X = shot.X - 1, Y = shot.Y };
                        positions.Remove(position);
                        if (selected.ExistPosition(position))
                        {
                            if (!Game.MuteClicked)
                            {
                                sound2.Play();
                            }
                            else
                            {
                                sound2.Stop();
                            }
                            selected.ShootPosition(position);
                            RemoveDeadPoints(position);
                            shot = position;
                            Game.score -= 50;
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
                        position = new Point { X = shot.X, Y = shot.Y - 1 };
                        positions.Remove(position);
                        if (selected.ExistPosition(position))
                        {
                            if (!Game.MuteClicked)
                            {
                                sound2.Play();
                            }
                            else
                            {
                                sound2.Stop();
                            }
                            selected.ShootPosition(position);
                            RemoveDeadPoints(position);
                            shot = position;
                            Game.score -= 50;

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
                            if (!Game.MuteClicked)
                            {
                                sound2.Play();
                            }
                            else
                            {
                                sound2.Stop();
                            }
                            selected.ShootPosition(position);
                            RemoveDeadPoints(position);
                            shot = position;
                            found = true;
                            Game.score -= 50;

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

            if(shots < 4)
            {
                shots++;
            }
            else
            {
                GenerateRandom(grid);

            }
            //computer turn
            //this is where we can move a ship
            if (activeGameModes.Contains(GameMode.MOVABLESHIPS))
            {
                turn++;
                if (turn % 5 == 0)
                {
                    EnableCells(dgvPlayer);
                }
                else
                {
                    DisableCells(dgvPlayer);
                }
            }
        }

        private void GenerateRandom(DataGridView grid)
        {
            if (!Game.isFinished)
            {
                int index = new Random().Next(positions.Count);
                Point position = positions[index];
                positions.RemoveAt(index);
                foreach (Ship ship in ships)
                {
                    if (ship.ExistPosition(position))
                    {
                        System.Media.SoundPlayer sound2 = new System.Media.SoundPlayer(Properties.Resources.explosion);
                        if (!Game.MuteClicked)
                        {
                            sound2.Play();
                        }
                        else
                        {
                            sound2.Stop();
                        }
                        ship.ShootPosition(position);
                        Game.score -= 50;
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

        }
        private void UpdateGrid(Point position, DataGridView grid)
        {
            DataGridViewImageCell imgCell = new DataGridViewImageCell();
            imgCell.Value = Properties.Resources.dotImage;

            // if FOFB do this instead of line above
            //string file = string.Format("_{0}", GetShipCount(position));
            //imgCell.Value = Properties.Resources.ResourceManager.GetObject(file);

            grid.Rows[position.X].Cells[position.Y] = imgCell;
            missedPositions.Add(position);
            System.Media.SoundPlayer sound = new System.Media.SoundPlayer(Properties.Resources.miss);
            if (!Game.MuteClicked)
            {
                sound.Play();
            }
            else
            {
                sound.Stop();
            }
            found = false;
        }

        public int GetShipCount(Point position)
        {
            int count = 0;
            foreach (Ship ship in ships)
            {
                if (ship.Cells[0].Positon.X == position.X || ship.Cells[0].Positon.Y == position.Y)
                    count++;
            }

            return count;
        }

        public void removeMissed(DataGridView dgv)
        {
            foreach (Point p in missedPositions)
            {
                dgv.Rows[p.X].Cells[p.Y] = null;
            }
        }
    }
}
