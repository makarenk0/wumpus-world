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

namespace Wumpus
{
    public class GameBoard
    {
        public PictureBox BoardImage = new PictureBox();
        public Cell[,] Matrix;
        const int _mapWidth = 0, _mapHeight = 0;

        public void CreateBoardImage(Form formInstance, int Level)
        {
            // Create Board Image
            BoardImage.Name = "BoardImage";
            BoardImage.SizeMode = PictureBoxSizeMode.AutoSize;
            BoardImage.Location = new Point(0, 50);
           
            //BoardImage.Image = Properties.Resources.Board_1; //background map image
            
            formInstance.Controls.Add(BoardImage);
        }

        public Tuple<int,int> InitialiseBoardMatrix(int Level)
        {
            // Initialise Game Board Matrix
            
            switch (Level)
            {
                //for testing on not random map
                case 1:
                    Matrix = new Cell[,] {{ new Cell(true), new Cell(true), new Cell(true), new Cell(true), new Cell(true), new Cell(true)},
                                          { new Cell(true), new Cell(), new Cell(), new Cell(), new Cell(), new Cell(true)},
                                          { new Cell(true), new Cell(), new Cell(), new Cell(), new Cell(), new Cell(true)},
                                          { new Cell(true), new Cell(), new Cell(), new Cell(), new Cell(), new Cell(true)},
                                          { new Cell(true), new Cell(), new Cell(), new Cell(), new Cell(), new Cell(true)},
                                          { new Cell(true), new Cell(true), new Cell(true), new Cell(true), new Cell(true), new Cell(true)},
                                        };
                    break;
                default:

                    break;

            }
            int StartX = 1;
            int StartY = Matrix.GetLength(0) - 1;
            FillMap();

            //for (int y=0; y< Matrix.GetLength(0); y++)
            //{
            //    for (int x=0; x< Matrix.GetLength(1); x++)
            //    {
            //        if (Matrix[y, x] == 3) { StartX = x; StartY = y;}
            //    }
            //}
            Tuple<int,int> StartLocation = new Tuple<int,int> (StartX, StartY);
            return StartLocation;
        }

        private void FillMap()
        {
            
        }
    }
}
