﻿using System;
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
    public partial class Form1 : Form
    {
        public static GameBoard gameboard = new GameBoard();  
        public static Agent pacman = new Agent();  
        public static Player player = new Player();
        public static HighScore highscore = new HighScore();
        private static FormElements formelements = new FormElements();

        public Form1()
        {
            
            InitializeComponent();
            
           
            SetupGame(1);
        }

        public void SetupGame(int Level)
        {
            // Create Game Board
            gameboard.CreateBoardImage(this, Level);

            // Create Board Matrix
            Tuple<int, int> PacmanStartCoordinates = gameboard.InitialiseBoardMatrix(Level);

            // Create Player
            player.CreatePlayerDetails(this);
            player.CreateLives(this);

            // Create Form Elements
            formelements.CreateFormElements(this);

            // Create High Score
            highscore.CreateHighScore(this);


            // Create Pacman
            pacman.CreateAgentImage(this, PacmanStartCoordinates.Item1, PacmanStartCoordinates.Item2);
        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    base.OnKeyDown(e);
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Up: pacman.nextDirection = 1; pacman.MovePacman(1); break;
        //        case Keys.Right: pacman.nextDirection = 2; pacman.MovePacman(2); break;
        //        case Keys.Down: pacman.nextDirection = 3; pacman.MovePacman(3); break;
        //        case Keys.Left: pacman.nextDirection = 4; pacman.MovePacman(4); break;
        //    }
        //}
        


    }
}
