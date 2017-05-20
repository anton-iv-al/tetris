using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    public class Field
    {
        private static DrawManager DrawManager { get; set; }

        private const int _matrixSizeX = 10;
        private const int _matrixSizeY = 20;
        private SquareColor[,] _matrix = new SquareColor[_matrixSizeX, _matrixSizeY];

        public Field(DrawManager drawManager)
        {
            DrawManager = drawManager;
            DrawManager.MatrixSizeX = _matrixSizeX;
            DrawManager.MatrixSizeY = _matrixSizeY;
        }

        public void Draw()
        {
            for(int i=0; i < _matrixSizeX; ++i)
            {
                for (int j = 0; j < _matrixSizeY; ++j)
                {
                    DrawManager.DrawSquare(i, j, _matrix[i, j]);
                }
            }
        }
    }
}
