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
        const int _mapWidth = 0, _mapHeight = 0;
        Form _form;

        public void CreateBoardImage(Form formInstance, int Level)
        {
            // Create Board Image
            BoardImage.Name = "BoardImage";
            BoardImage.SizeMode = PictureBoxSizeMode.AutoSize;
            
            BoardImage.Location = new Point(0, 50);

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
                                          { new Cell(0, 1, true), new Cell(1, 1, false, false, false, true), new Cell(2, 1), new Cell(3, 1), new Cell(4, 1), new Cell(5, 1, true)},
                                          { new Cell(0, 2, true), new Cell(1, 2, false, true), new Cell(2, 2, false, false, false, true, true), new Cell(3, 2), new Cell(4, 2), new Cell(5, 2, true)},
                                          { new Cell(0, 3, true), new Cell(1, 3, false, false, false, true), new Cell(2, 3), new Cell(3, 3, false, false, false, false, false, true), new Cell(4, 3), new Cell(5, 3, true)},
                                          { new Cell(0, 4, true), new Cell(1, 4), new Cell(2, 4, false, false, false, false, false, true), new Cell(3, 4, false, false, true, false, false, false), new Cell(4, 4, false, false, false, false, false, true), new Cell(5, 4, true)},
                                          { new Cell(0, 5, true), new Cell(1, 5, true), new Cell(2, 5, true), new Cell(3, 5, true), new Cell(4, 5, true), new Cell(5, 5,true)},
                                        };
                    break;
                default:

                    break;

            }
            int StartX = 1;
            int StartY = Matrix.GetLength(0) - 2;
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
            for (int y = 0; y < Matrix.GetLength(0); y++)
            {
                for (int x = 0; x < Matrix.GetLength(1); x++)
                {
                   
                   _form.Controls.Add(Matrix[y, x].Pic.Image == null ? null : Matrix[y, x].Pic);

                }
            }

        }
    }
}
