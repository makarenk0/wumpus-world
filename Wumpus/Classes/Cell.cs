using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus.Classes
{
    public class Cell
    {
        private bool _wall;
        private bool _wumpus;
        private bool _pit;
        private bool _stench;
        private bool _glitter;
        private bool _breeze;
        private bool _scream;

        public bool Wall { get => _wall; set => _wall = value; }
        public bool Wumpus { get => _wumpus; set => _wumpus = value; }
        public bool Pit { get => _pit; set => _pit = value; }
        public bool Stench { get => _stench; set => _stench = value; }
        public bool Glitter { get => _glitter; set => _glitter = value; }
        public bool Breeze { get => _breeze; set => _breeze = value; }
        public bool Scream { get => _scream; set => _scream = value; }

        public Cell(bool wall = false, bool wumpus = false, bool pit = false, bool stench = false, 
                    bool glitter = false, bool breeze = false, bool scream = false)
        {
            Wall = wall;
            Wumpus = wumpus;
            Pit = pit;
            Stench = stench;
            Glitter = glitter;
            Breeze = breeze;
            Scream = scream;
        }
    }
}
