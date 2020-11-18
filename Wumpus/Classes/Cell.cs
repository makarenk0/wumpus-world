using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wumpus.Classes
{
    public class Cell
    {
        private int _xCoordinate;
        private int _yCoordinate;

        const int _xOffset = 80;

        private bool _wall;
        private bool _wumpus;
        private bool _pit;
        private bool _stench;
        private bool _glitter;
        private bool _breeze;
        private bool _scream;

        private PictureBox _pic;

        public bool Wall { get => _wall; set => _wall = value; }
        public bool Wumpus { get => _wumpus; set => _wumpus = value; }
        public bool Pit { get => _pit; set => _pit = value; }
        public bool Stench { get => _stench; set => _stench = value; }
        public bool Glitter { get => _glitter; set => _glitter = value; }
        public bool Breeze { get => _breeze; set => _breeze = value; }
        public bool Scream { get => _scream; set => _scream = value; }

        public PictureBox Pic { get => _pic; set => _pic = value; }
       
        public int XCoordinate { get => _xCoordinate; set => _xCoordinate = value; }
        public int YCoordinate { get => _yCoordinate; set => _yCoordinate = value; }

        public Cell(int xCoordinate = 0, int yCoordinate = 0, bool wall = false, bool wumpus = false, bool pit = false, 
            bool stench = false, bool glitter = false, bool breeze = false, bool scream = false)
        {
            _xCoordinate = xCoordinate;
            _yCoordinate = yCoordinate;

            Wall = wall;
            Wumpus = wumpus;
            Pit = pit;
            Stench = stench;
            Glitter = glitter;
            Breeze = breeze;
            Scream = scream;

            Pic = new PictureBox();
            Pic.SizeMode = PictureBoxSizeMode.AutoSize;
            Pic.Location = new Point(xCoordinate * 32 , yCoordinate * 32 + _xOffset);
        }

        public void LoadResource()
        {
            if (Wall) Pic.Image = Utilities.ChooseRandomly(0, 2) == 0 ? Properties.Resources.wall : Properties.Resources.wall_green;
            else if (Wumpus && Glitter && Breeze) Pic.Image = Properties.Resources.wumpus_gold_breeze;
            else if (Pit && Wumpus && Glitter) Pic.Image = Properties.Resources.pit_wumpus_gold;
            else if (Glitter && Stench && Breeze) Pic.Image = Properties.Resources.glitter_stench_breeze;
            else if (Pit && Wumpus) Pic.Image = Properties.Resources.pit_wumpus;
            else if (Wumpus && Breeze) Pic.Image = Properties.Resources.wumpus_breeze;
            else if (Wumpus && Glitter) Pic.Image = Properties.Resources.wumpus_gold;
            else if (Glitter && Stench) Pic.Image = Properties.Resources.glitter_stench;
            else if (Glitter && Breeze) Pic.Image = Properties.Resources.glitter_breeze;
            else if (Glitter && Pit) Pic.Image = Properties.Resources.pit_glitter;
            else if (Stench && Pit) Pic.Image = Properties.Resources.pit_stench;
            else if (Stench && Breeze) Pic.Image = Properties.Resources.breeze_stench;
            else if (Glitter) Pic.Image = Properties.Resources.glitter;
            else if (Pit) Pic.Image = Properties.Resources.pit;
            else if (Stench) Pic.Image = Properties.Resources.stench;
            else if (Breeze) Pic.Image = Properties.Resources.breeze;
            else if (Wumpus) Pic.Image = Properties.Resources.wumpus;
        }

        public void Modify(String arg)
        {
            if (!Wall)
            {
                switch (arg)
                {
                    case "Stench":
                        Stench = true;
                        break;
                    case "Breeze":
                        Breeze = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
