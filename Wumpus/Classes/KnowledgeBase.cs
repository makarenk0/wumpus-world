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
                    }
                    else
                    {
                        _knowledge[y, x] = new KnowledgeUnit(x, y);
                    }
                    
                }
            }
            _exit = exit;

        }

        public void PerceiveData(int x, int y, bool stench = false, bool breeze = false, bool glitter = false, bool scream = false, bool bump = false)
        {
            _knowledge[y, x].Save = true;
            _knowledge[y, x].Stench = stench;
            _knowledge[y, x].Breeze = breeze;
            _knowledge[y, x].Scream = scream;
            _knowledge[y, x].Glitter = glitter;


            //_knowledge[y, x].Wall = bump;  //TO DO: wall to neighbour cell
        }
    }
}
