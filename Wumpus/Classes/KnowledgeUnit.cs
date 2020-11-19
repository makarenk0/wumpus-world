using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus.Classes
{
    class KnowledgeUnit
    {
        private int _xCoordinate;
        private int _yCoordinate;

        private bool _wall;
        private bool _stench;
        private bool _glitter;
        private bool _breeze;
        private bool _scream;
        private bool _save;

        public bool Save { get => _save; set => _save = value; }
        public bool Wall { get => _wall; set => _wall = value; }
        public bool Stench { get => _stench; set => _stench = value; }
        public bool Glitter { get => _glitter; set => _glitter = value; }
        public bool Breeze { get => _breeze; set => _breeze = value; }
        public bool Scream { get => _scream; set => _scream = value; }


        public int XCoordinate { get => _xCoordinate; set => _xCoordinate = value; }
        public int YCoordinate { get => _yCoordinate; set => _yCoordinate = value; }
        
        public KnowledgeUnit(int x, int y, bool save = false, bool stench = false, bool breeze = false, bool glitter = false, bool bump = false, bool scream = false)
        {
            XCoordinate = x;
            YCoordinate = y;

            Save = save;
            Stench = stench;
            Breeze = breeze;
            Glitter = glitter;
            Wall = bump;
            Scream = scream;
        }
    }
}
