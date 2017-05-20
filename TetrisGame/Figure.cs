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
        public int Y { get; set; } = -4;

        public Figure(SquareColor[,] matrix)
        {
            Matrix = matrix.Clone() as SquareColor[,];
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
    }
}
