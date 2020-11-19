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

        public PictureBox AgentImage = new PictureBox();
        private ImageList AgentImages = new ImageList(); 
        private Timer timer = new Timer();

        private int imageOn = 0;




        private KnowledgeBase _knowledgeBase;
        private const int _updateTime = 2000;



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
            formInstance.Controls.Add(AgentImage);
            AgentImage.BackColor = Color.Transparent;
            AgentImage.BringToFront();

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
            currentDirection = direction;
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
            AgentImage.Image = AgentImages.Images[(currentDirection - 1)];
        }

        //private bool check_direction(int direction)
        //{
        //    // Check if pacman can move to space
        //    switch (direction)
        //    {
        //        case 1: return direction_ok(xCoordinate, yCoordinate - 1);
        //        case 2: return direction_ok(xCoordinate + 1, yCoordinate);
        //        case 3: return direction_ok(xCoordinate, yCoordinate + 1);
        //        case 4: return direction_ok(xCoordinate - 1, yCoordinate);
        //        default: return false;
        //    }
        //}

        //private bool direction_ok(int x, int y)
        //{
        //    // Check if board space can be used
        //    if (x < 0) { xCoordinate = 27; PacmanImage.Left = 2 * 429; return true ; }
        //    if (x > 27) { xCoordinate = 0; PacmanImage.Left = -5; return true; }
        //    if (Form1.gameboard.Matrix[y, x] < 4) { return true; } else { return false; }
        //}

        private void timer_Tick(object sender, EventArgs e)
        {
            // Keep moving pacman
            Cell current = Form1.gameboard.Matrix[yCoordinate, xCoordinate];
            _knowledgeBase.PerceiveData(xCoordinate, yCoordinate, currentDirection, current.Stench, current.Breeze, current.Glitter, current.Scream);
            currentDirection = _knowledgeBase.GetStep(xCoordinate, yCoordinate);
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
    }
}
