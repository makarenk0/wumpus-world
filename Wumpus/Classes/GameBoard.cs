using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wumpus.Classes;

namespace Wumpus.Classes
{
    public class GameBoard
    {
        public PictureBox BoardImage = new PictureBox();
        public Cell[,] Matrix;


        public const int _mapWidth = 20, _mapHeight = 20;
        const float _pitProbability = 0.001f;


        Form _form;

        public void CreateBoardImage(Form formInstance, int Level)
        {
            // Create Board Image
            BoardImage.Name = "BoardImage";
            BoardImage.SizeMode = PictureBoxSizeMode.AutoSize;
            
            BoardImage.Location = new Point(0, 60);

            //BoardImage.Image = Properties.Resources.Board_1; //background map image
            _form = formInstance;
            formInstance.BackColor = Color.DarkGray;
            //formInstance.Controls.Add(BoardImage);
        }

        public Tuple<int,int> InitialiseBoardMatrix(int Level)
        {
            // Initialise Game Board Matrix
            
            switch (Level)
            {
                //for testing on not random map
                case 1:
                    Matrix = new Cell[,] {{ new Cell(0, 0, true), new Cell(1, 0, true), new Cell(2, 0, true), new Cell(3, 0, true), new Cell(4, 0, true), new Cell(5, 0, true)},
                                          { new Cell(0, 1, true), new Cell(1, 1, false, false, false, true), new Cell(2, 1), new Cell(3, 1, false, false, false, false, false, true), new Cell(4, 1, false, false, true), new Cell(5, 1, true)},
                                          { new Cell(0, 2, true), new Cell(1, 2, false, true), new Cell(2, 2, false, false, false, true, true, true), new Cell(3, 2, false, false, true), new Cell(4, 2, false, false, false, false, false, true), new Cell(5, 2, true)},
                                          { new Cell(0, 3, true), new Cell(1, 3, false, false, false, true), new Cell(2, 3), new Cell(3, 3, false, false, false, false, false, true), new Cell(4, 3), new Cell(5, 3, true)},
                                          { new Cell(0, 4, true), new Cell(1, 4), new Cell(2, 4, false, false, false, false, false, true), new Cell(3, 4, false, false, true, false, false, false), new Cell(4, 4, false, false, false, false, false, true), new Cell(5, 4, true)},
                                          { new Cell(0, 5, true), new Cell(1, 5, true), new Cell(2, 5, true), new Cell(3, 5, true), new Cell(4, 5, true), new Cell(5, 5,true)},
                                        };
                    break;
                default:
                    GenerateRandomMap(_mapWidth, _mapHeight);

                    break;

            }
            int StartX = 1;
            int StartY = Matrix.GetLength(0) - 2;
            FillMap();
            Tuple<int,int> StartLocation = new Tuple<int,int> (StartX, StartY);
            return StartLocation;
        }

        private void GenerateRandomMap(int mapWidth, int mapHeight)
        {
            Matrix = new Cell[mapHeight, mapWidth];
            for (int y = 0; y < Matrix.GetLength(0); y++)
            {
                for (int x = 0; x < Matrix.GetLength(1); x++)
                {
                    Matrix[y, x] = new Cell(x, y);
                }
            }

            for (int y = 0; y < Matrix.GetLength(0); y++)
            {
                for (int x = 0; x < Matrix.GetLength(1); x++)
                {
                    if ((y == mapHeight - 2 && x == 1) || (y == mapHeight - 3 && x == 1) || (y == mapHeight - 2 && x == 2)) continue;  //start player position and cells near it
                    if (y == 0 || y == mapHeight - 1 || x == 0 || x == mapWidth - 1)
                    {
                        Matrix[y, x].Wall = true;
                    }
                    else
                    {
                        if(Utilities.ChooseRandomly(0, 11) < _pitProbability * 10)  //probability to spawn the pit
                        {
                            Matrix[y, x].Pit = true;
                            PutEffectsAround(x, y, "Breeze");
                        }
                    }
                }
            }
            int wumpusX = Utilities.ChooseRandomly(1, mapWidth - 1);
            int wumpusY = Utilities.ChooseRandomly(1, mapHeight - 1);
            //Console.WriteLine(String.Concat("Wumpus", wumpusX, ",", wumpusY));
            if ((wumpusY == mapHeight - 2 && wumpusX == 1) ||
                (wumpusY == mapHeight - 3 && wumpusX == 1) ||
                (wumpusY == mapHeight - 2 && wumpusX == 2)
                )
            {
                wumpusX += 2;
            }

            Matrix[wumpusY, wumpusX].Wumpus = true;
            PutEffectsAround(wumpusX, wumpusY, "Stench");



            int goldX = Utilities.ChooseRandomly(1, mapWidth - 1);
            int goldY = Utilities.ChooseRandomly(1, mapHeight - 1);
            //Console.WriteLine(String.Concat("Wumpus", goldX, ",", goldY));
            if (goldY == mapHeight - 2 && goldX == 1)
            {
                goldX += mapWidth - 3;
            }
            Matrix[goldY, goldX].Glitter = true;

        }

        private void PutEffectsAround(int x, int y, string v)
        {
            Matrix[y - 1, x].Modify(v);
            Matrix[y + 1, x].Modify(v);
            Matrix[y, x - 1].Modify(v);
            Matrix[y, x + 1].Modify(v);
        }

        private void FillMap()
        {
            for (int y = 0; y < Matrix.GetLength(0); y++)
            {
                for (int x = 0; x < Matrix.GetLength(1); x++)
                {
                    Matrix[y, x].LoadResource();
                   _form.Controls.Add(Matrix[y, x].Pic.Image == null ? null : Matrix[y, x].Pic);

                }
            }

        }
    }
}
