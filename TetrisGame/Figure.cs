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

        private SquareColor[,] _matrix;
        public SquareColor[,] Matrix
        {
            get
            {
                return _matrix.Clone() as SquareColor[,];
            }
            private set { _matrix = value; }
        }

        public int X { get; set; } = 3;
        public int Y { get; set; } = 23;

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
    }
}
