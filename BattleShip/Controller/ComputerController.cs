using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BattleShip.Model;
using System.Windows.Forms;

namespace BattleShip.Controller
{
    [Serializable]
    class ComputerController
    {
        private List<Ship> ships;
        private List<int> amounts;
        public List<Point> positions;
        public List<Point> missedPositions;
        public int gridSize;
        public ComputerController()
        {
            gridSize = 10;
            amounts = new List<int>();
            positions = new List<Point>();
            missedPositions = new List<Point>();
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    positions.Add(new Point { X = i, Y = j });
                }
            }
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
            grid.RowCount = gridSize;
            grid.ColumnCount = gridSize; ;
            for (int i = 0; i < gridSize; i++)
            {
                grid.Rows[i].Height = 36;
                grid.Columns[i].Width = 36;
            }
            
            grid.ClearSelection();
        }

        public void RemoveDeadPoints(Point position)
        {
            positions.Remove(new Point { X = position.X - 1, Y = position.Y - 1 });
            positions.Remove(new Point { X = position.X - 1, Y = position.Y + 1 });
            positions.Remove(new Point { X = position.X + 1, Y = position.Y - 1 });
            positions.Remove(new Point { X = position.X + 1, Y = position.Y + 1 });
        }

        public void RemoveDeadShip(Ship selected)
        {
            foreach (Point point in selected.viewPoints)
            {
                positions.Remove(point);
            }
        }

        public bool Shoot(Point position, DataGridView grid)
        {
            grid.Enabled = true;
            if(positions.Contains(position))
            {
                positions.Remove(position);
                foreach (Ship ship in ships)
                {
                    if (ship.ExistPosition(position))
                    {
                        ship.ShootPosition(position);
                        if (ship.Destroyed())
                        {
                            RemoveDeadShip(ship);
                            Game.score += 500;
                        }
                        else
                        {
                            RemoveDeadPoints(position);
                        }                       
                        System.Media.SoundPlayer sound2 = new System.Media.SoundPlayer(Properties.Resources.explosion);
                        if (!Game.MuteClicked)
                        {
                            sound2.Play();
                        }
                        else
                        {
                            sound2.Stop();
                        }
                        
                        Game.score += 100;
                        
                        grid.Rows[position.X].Cells[position.Y].Style.BackColor = Color.Red;
                        ShowShips(grid);
                        grid.Enabled = false;
                        return false;
                    }
                }
                DataGridViewImageCell imgCell = new DataGridViewImageCell();
                imgCell.Value = Properties.Resources.dotImage;
                missedPositions.Add(position);
                grid.Rows[position.X].Cells[position.Y] = imgCell;

                grid.Enabled = false;
                System.Media.SoundPlayer sound = new System.Media.SoundPlayer(Properties.Resources.miss);
                if (!Game.MuteClicked)
                {
                    sound.Play();
                }else
                {
                    sound.Stop();
                }
                return true;
            }
            return false;
        }

        public void UpdateMissed(DataGridView grid)
        {
            foreach (Point position in missedPositions)
            {
                DataGridViewImageCell imgCell = new DataGridViewImageCell();
                imgCell.Value = Properties.Resources.dotImage;
                grid.Rows[position.X].Cells[position.Y] = imgCell;
            }
        }

        public void ShowShips(DataGridView grid)
        {
            ships.ForEach(ship => ship.enemyShipsDraw(grid));
        }

        public void ShowEndShips(DataGridView grid)
        {
            ships.ForEach(ship => ship.ShowShip(grid));
        }

        public bool Won() {

            return ships.All(ship => ship.Destroyed());
        }
       

        public void Random()
        {
            ships = new List<Ship>();           
            
            bool picked = false;
            for (int i = 4; i >= 0; i--)
            {
                for (int j = 0; j < amounts[i]; j++)
                {
                    while (!picked)
                    {
                        int index = new Random().Next(positions.Count);
                        Ship.View type = (Ship.View)new Random().Next(2);
                        Point position = positions[index];
                        if (position == new Point(0, 0))
                            continue;
                        Ship primary = new Ship(i + 1, Color.Blue, position, type);
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
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
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