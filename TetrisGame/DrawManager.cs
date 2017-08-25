using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    class DrawManager
    {
        public static DrawManager Instance = new DrawManager();

        public readonly IDrawAdapter Adapter = new MonoGameDrawAdapter();

        private DrawManager()
        {
        }
    }
}
