using BattleShip.Model;
using BattleShip.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BattleShip.Game;

namespace BattleShip.Controller
{
    [Serializable]
    class Player
    {
        protected List<Ship> ships;
        protected List<int> amounts;
        public List<Point> positions;
        public List<Point> missedPositions;
        public Ship selected;
        public int gridSize;

        public int turn;
        public List<GameMode> activeGameModes;

        protected bool isPlayer;
        
        public Player(List<GameMode> gameModes)
        {
            this.turn = 0;
            this.activeGameModes = gameModes;

            this.gridSize = activeGameModes.Contains(GameMode.BIGBOARD) ? 20 : 10;

            this.amounts = new List<int>();
            this.positions = new List<Point>();
            this.missedPositions = new List<Point>();
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    positions.Add(new Point { X = i, Y = j });
                }
            }
            this.amounts.Add(3);
            this.amounts.Add(2);
            this.amounts.Add(2);
            this.amounts.Add(1);
            this.amounts.Add(1);
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
        }

        public void RemoveDeadPoints(Point position)
        {
            positions.Remove(new Point { X = position.X - 1, Y = position.Y - 1 });
            positions.Remove(new Point { X = position.X - 1, Y = position.Y + 1 });
            positions.Remove(new Point { X = position.X + 1, Y = position.Y - 1 });
            positions.Remove(new Point { X = position.X + 1, Y = position.Y + 1 });
        }

        public void RemoveDeadShip()
        {
            foreach (Point point in selected.viewPoints)
            {
                positions.Remove(point);
            }
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

                        Ship primary = new Ship(i + 1, Color.Blue, position, type);
                        if (ships.Exists(ship => ship.ExistShip(primary)))
                        {
                            primary.ChangePosition(position);
                        }
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

        public void UpdateMissed(DataGridView grid)
        {
            foreach (Point position in missedPositions)
            {
                DataGridViewImageCell imgCell = new DataGridViewImageCell();
                imgCell.Value = Properties.Resources.dotImage;
                grid.Rows[position.X].Cells[position.Y] = imgCell;
            }
        }

        public bool Won()
        {
            return ships.All(ship => ship.Destroyed());
        }
    }
}
