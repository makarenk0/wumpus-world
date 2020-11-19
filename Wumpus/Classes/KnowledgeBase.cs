using Nito.Collections;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus.Classes
{
    class KnowledgeBase
    {
        private KnowledgeUnit[,] _knowledge;
        private Deque<Point> _steps;

        Point _exit;
        bool _wumpusRegonized = false;

        public KnowledgeBase(int mapWidth, int mapHeight, Point exit)
        {
            _knowledge = new KnowledgeUnit[mapHeight, mapWidth];
            _steps = new Deque<Point>();
            for (int y = 0; y < _knowledge.GetLength(0); y++)
            {
                for (int x = 0; x < _knowledge.GetLength(1); x++)
                {
                    if (y == 0 || y == mapHeight - 1 || x == 0 || x == mapWidth - 1)
                    {
                        _knowledge[y, x] = new KnowledgeUnit(x, y, false, false, false, true);
                        _knowledge[y, x].Wumpus = false;
                        _knowledge[y, x].Pit = false;
                    }
                    else
                    {
                        _knowledge[y, x] = new KnowledgeUnit(x, y);
                    }
                    
                }
            }
            _exit = exit;
            _knowledge[exit.Y, exit.X].Wumpus = false;
            _knowledge[exit.Y, exit.X].Pit = false;
            _knowledge[exit.Y, exit.X].Visited = true;
        }

        public int GetStep(int x, int y)
        {
            Point current = new Point(x, y);
            Point target = new Point((Size)_steps.RemoveFromFront());
            if (current.X > target.X) return 4;
            else if (current.X < target.X) return 2;
            else if (current.Y > target.Y) return 1;
            else if (current.Y < target.Y) return 3;
            return 0;
        }

        public void PerceiveData(int x, int y, int currentDirection, bool stench = false, bool breeze = false, bool glitter = false, bool scream = false, bool bump = false)
        {
            _knowledge[y, x].Stench = stench;
            _knowledge[y, x].Breeze = breeze;
            _knowledge[y, x].Scream = scream;
            _knowledge[y, x].Glitter = glitter;
            _knowledge[y, x].Visited = true;

            //_knowledge[y, x].Wall = bump;  //TO DO: wall to neighbour cell
            



            if (!stench)
            {
                _knowledge[y - 1, x].Wumpus = false;
                _knowledge[y + 1, x].Wumpus = false;
                _knowledge[y, x - 1].Wumpus = false;
                _knowledge[y, x + 1].Wumpus = false;
            }
            else
            {
                RecognizeWumpus(x, y);
            }

            if (!breeze)
            {
                _knowledge[y - 1, x].Pit = false;
                _knowledge[y + 1, x].Pit = false;
                _knowledge[y, x - 1].Pit = false;
                _knowledge[y, x + 1].Pit = false;
            }


            if(_steps.Count == 0)
            {
                InformedAlgorithms informed = new InformedAlgorithms(x, y, _knowledge);
                List<Point> res = informed.GreedyAlgorithm();
                res.RemoveAt(0);
                foreach (var p in res)
                {
                    _steps.AddToBack(p);
                }
            }
        }


        private void RecognizeWumpus(int x, int y)
        {
            KeyValuePair<bool, Point> res = new KeyValuePair<bool, Point>();
            List<KnowledgeUnit> around = new List<KnowledgeUnit>();
            around.Add(_knowledge[y - 1, x]);
            around.Add(_knowledge[y + 1, x]);
            around.Add(_knowledge[y, x - 1]);
            around.Add(_knowledge[y, x + 1]);
            if(around.Count(m => !m.Wumpus) == 3)
            {
                int numberWumpus = around.IndexOf(around.Find(m => m.Wumpus));
                RemoveAllWumpusAssuming();
                around.ElementAt(numberWumpus).Wumpus = true;
                _wumpusRegonized = true;
            }
        }

        private void RemoveAllWumpusAssuming()
        {
            for (int y = 0; y < _knowledge.GetLength(0); y++)
            {
                for (int x = 0; x < _knowledge.GetLength(1); x++)
                {
                    _knowledge[y, x].Wumpus = false;

                }
            }
        }
    }
}
