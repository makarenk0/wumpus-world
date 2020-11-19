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
        private bool _assumption;
        
        private bool _wumpus;
        private bool _pit;
        private bool _visited;
      



        public bool Wall { get => _wall; set => _wall = value; }
        public bool Stench { get => _stench; set => _stench = value; }
        public bool Glitter { get => _glitter; set => _glitter = value; }
        public bool Breeze { get => _breeze; set => _breeze = value; }
        public bool Scream { get => _scream; set => _scream = value; }


        public bool Wumpus { get => _wumpus; set => _wumpus = value; }
        public bool Pit { get => _pit; set => _pit = value; }
        
        public bool Visited { get => _visited; set => _visited = value; }

        //public bool AssumptionWumpus { get => _aWumpus; set => _aWumpus = value; }  //indicates that we didnt visit it, only assuming 
        //public bool AssumptionPit { get => _aPit; set => _aPit = value; }  //indicates that we didnt visit it, only assuming 


        public int XCoordinate { get => _xCoordinate; set => _xCoordinate = value; }
        public int YCoordinate { get => _yCoordinate; set => _yCoordinate = value; }
        
        public KnowledgeUnit(int x, int y, bool stench = false, bool breeze = false, bool glitter = false, bool bump = false, bool scream = false)
        {
            XCoordinate = x;
            YCoordinate = y;

         
            Stench = stench;
            Breeze = breeze;
            Glitter = glitter;
            Wall = bump;
            Scream = scream;

            Wumpus = true;
            //AssumptionWumpus = true;
            Pit = true;
            Visited = false;
            //AssumptionPit = true;
        }

        public KnowledgeUnit(KnowledgeUnit unit) : this(unit.XCoordinate, unit.YCoordinate, unit.Stench, unit.Breeze, unit.Glitter, unit.Wall, unit.Scream)
        {
            Wumpus = unit.Wumpus;
            Pit = unit.Pit;
            Visited = unit.Visited;
        }


        public bool IsSave()
        {
            return !Wall && !Wumpus && !Pit;
        }
    }
}
