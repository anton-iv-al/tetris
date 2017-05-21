using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    public class Figure
    {
        public static DrawManager DrawManager { get; set; }

        public SquareColor[,] Matrix { get; private set; }

        public int X { get; set; } = 3;
        public int Y { get; set; } = 0;

        public Figure(SquareColor[,] matrix)
        {
            Matrix = matrix;
        }

        public void Draw()
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    DrawManager.DrawSquare(X+i, Y+j, Matrix[i, j]);
                }
            }
        }

        public void RotateRight()
        {
            var newMatrix = new SquareColor[4, 4];
            for(int i=0; i<4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    newMatrix[i, j] = Matrix[3 - j, i];
                }
            }
            Matrix = newMatrix;
        }

        public void RotateLeft()
        {
            var newMatrix = new SquareColor[4, 4];
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    newMatrix[i, j] = Matrix[j, 3 - i];
                }
            }
            Matrix = newMatrix;
        }
    }
}
