using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wumpus.Classes
{
    public class Agent
    {
        // Initialise variables
        public int xCoordinate = 0;
        public int yCoordinate = 0;
        private int xStart = 0;
        private int yStart = 0;
        public int currentDirection = 2;
        private int _arrowsNum = 1;

        public PictureBox AgentImage = new PictureBox();
       
        private ImageList AgentImages = new ImageList();


        public PictureBox ArrowImage = new PictureBox();

        private Timer timer = new Timer();
        private Timer timerArrow = new Timer();

        private int imageOn = 0;




        private KnowledgeBase _knowledgeBase;
        private const int _updateTime = 2000;
        private const int _updateArrowTime = _updateTime/2;

        private Form _formInstance;



        private bool _scream = false;
        public bool Scream { get => _scream; set => _scream = value; }

        public Agent()
        {
            timer.Interval = _updateTime;
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);

            AgentImages.Images.Add(Properties.Resources.AgentUp);



            AgentImages.Images.Add(Properties.Resources.AgentRight);



            AgentImages.Images.Add(Properties.Resources.AgentDown);

            AgentImages.Images.Add(Properties.Resources.AgentLeft);


            AgentImages.ImageSize = new Size(32,32);

            
        }

        public void CreateAgentImage(Form formInstance, int StartXCoordinate, int StartYCoordinate)
        {
            // Create Pacman Image
            xStart = StartXCoordinate;
            yStart = StartYCoordinate;
            AgentImage.Name = "AgentImage";
            AgentImage.SizeMode = PictureBoxSizeMode.AutoSize;
            Set_Agent();

            _formInstance = formInstance;
            formInstance.Controls.Add(AgentImage);

            AgentImage.BackColor = Color.Transparent;
            AgentImage.BringToFront();

            Form1.player.AgentStatusText.Text = "Exploring";
            _knowledgeBase = new KnowledgeBase(Form1.gameboard.Matrix.GetLength(1), Form1.gameboard.Matrix.GetLength(0), new Point(xStart, yStart));
            
          
        }

        public void MovePacman(int direction)
        {
            // Move Pacman
            
            currentDirection = direction; 

         
            switch (direction)
            {
                 case 1: AgentImage.Top -= 32; yCoordinate--; break;
                 case 2: AgentImage.Left += 32; xCoordinate++; break;
                 case 3: AgentImage.Top += 32; yCoordinate++; break;
                 case 4: AgentImage.Left -= 32; xCoordinate--; break;
            }
            UpdatePacmanImage();
          
        }

        //private void CheckPacmanPosition()
        //{
        //    // Check Pacmans position
        //    switch (Form1.gameboard.Matrix[yCoordinate, xCoordinate])
        //    {
        //        case 1: Form1.food.EatFood(yCoordinate, xCoordinate); break;
        //        case 2: Form1.food.EatSuperFood(yCoordinate, xCoordinate); break;
        //    }
        //}

        private void UpdatePacmanImage()
        {
            // Update Pacman image
            if(currentDirection != 0)
            {
                Form1.highscore.UpdateHighScore(-1);
                AgentImage.Image = AgentImages.Images[(currentDirection - 1)];
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            
            Cell current = Form1.gameboard.Matrix[yCoordinate, xCoordinate];

            

            _knowledgeBase.PerceiveData(xCoordinate, yCoordinate, currentDirection, current.Stench, current.Breeze, current.Glitter, Scream);
            if (_knowledgeBase.WumpusRegonized && _arrowsNum > 0)
            {
                Shoot(xCoordinate, yCoordinate, _knowledgeBase.WumpusDir);

            }


            if (!_knowledgeBase.Stop)
            {
                currentDirection = _knowledgeBase.GetStep(xCoordinate, yCoordinate);
            }
            else
            {
                currentDirection = 0;
            }

            if (current.Glitter)
            {
                current.Glitter = false;
                current.LoadResource();
            }

            if (Scream)
            {
                Scream = false;
            }
            
            MovePacman(currentDirection);
            
        }

        public void Set_Agent()
        {
            // Place Pacman in board

            AgentImage.Image = Properties.Resources.AgentRight;
            //PacmanImage.SendToBack();
            currentDirection = 2;
            xCoordinate = xStart;
            yCoordinate = yStart;
            AgentImage.Location = new Point(xStart * 32, yStart * 32 + 80);
        }


        private int _arrowX, _arrowY, _arrowDir;

        

        private void Shoot(int x, int y, int dir)
        {
            --_arrowsNum;
            _arrowDir = dir;
            _arrowX = x;
            _arrowY = y;

            if (dir == 1) ArrowImage.Image = Properties.Resources.ArrowUp;
            else if(dir == 2) ArrowImage.Image = Properties.Resources.ArrowRight;
            else if(dir == 3) ArrowImage.Image = Properties.Resources.ArrowDown;
            else if(dir == 4) ArrowImage.Image = Properties.Resources.ArrowLeft;
            
            ArrowImage.Location = new Point(x * 32 + ((dir-1) % 2 == 0 ? 14 : 5), y * 32 + 80 + ((dir - 1) % 2 == 0 ? 5 : 14));
            _formInstance.Controls.Add(ArrowImage);
            ArrowImage.SizeMode = PictureBoxSizeMode.AutoSize;
            ArrowImage.BringToFront();

            timerArrow.Interval = _updateArrowTime;
            timerArrow.Enabled = true;
            timerArrow.Tick += new EventHandler(ShootTick);
        }


        private void ShootTick(object sender, EventArgs e)
        {

            switch (_arrowDir)
            {
                case 1: ArrowImage.Top -= 32; _arrowY--; break;
                case 2: ArrowImage.Left += 32; _arrowX++; break;
                case 3: ArrowImage.Top += 32; _arrowY++; break;
                case 4: ArrowImage.Left -= 32; _arrowX--; break;
            }

            if(Form1.gameboard.Matrix[_arrowY, _arrowX].Wumpus)
            {
                Form1.gameboard.Matrix[_arrowY, _arrowX].Wumpus = false;
                Form1.gameboard.Matrix[_arrowY, _arrowX].Stench = true;
                Form1.gameboard.Matrix[_arrowY, _arrowX].LoadResource();
                Scream = true;
                _formInstance.Controls.Remove(ArrowImage);
                timerArrow.Stop();
                timerArrow.Dispose();
            }

            
        }

        
    }
}
