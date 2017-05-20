using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.MonoGame;

namespace TetrisGame
{
    public class DrawManager
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameWindow Window;

        private Texture2D _backgroundTexture;
        private Texture2D _squareRed;
        private Texture2D _squareBlue;
        private Texture2D _squareGreen;
        private Texture2D _squareYellow;
        private Texture2D _squareViolet;
        private Texture2D _squareOrange;
        private Texture2D _squareLightBlue;
        private SpriteFont _defaultFont;

        private Rectangle _gameArea;
        private Rectangle _gameAreaForDraw;
        private Point _squareSize;
        private int _frameThickness = 10;

        public int MatrixSizeX { get; set; }
        public int MatrixSizeY { get; set; }

        public DrawManager(GraphicsDeviceManager graphics, GameWindow Window)
        {
            this.graphics = graphics;
            this.Window = Window;
        }

        public void Initialize()
        {
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

            _gameArea = new Rectangle(50, 50, 300, 600);
            _gameAreaForDraw = new Rectangle(50 - 1, 50 - _frameThickness, 300 + _frameThickness, 600 + _frameThickness);
            _squareSize = new Point(30, 30);
        }

        public void LoadContent(SpriteBatch spriteBatch, Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = spriteBatch;
            
            _backgroundTexture = Content.Load<Texture2D>("strips");
            _squareRed = Content.Load<Texture2D>("square_red");
            _squareBlue = Content.Load<Texture2D>("square_blue");
            _squareGreen = Content.Load<Texture2D>("square_green");
            _squareYellow = Content.Load<Texture2D>("square_yellow");
            _squareOrange = Content.Load<Texture2D>("square_orange");
            _squareViolet = Content.Load<Texture2D>("square_violet");
            _squareLightBlue = Content.Load<Texture2D>("square_light_blue");

            _defaultFont = Content.Load<SpriteFont>("default");
        }

        public void DrawBackGround()
        {
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
            Primitives2D.FillRectangle(spriteBatch, _gameArea, Color.Black);
            Primitives2D.DrawRectangle(spriteBatch, _gameAreaForDraw, Color.Gray, _frameThickness);
        }

        public void DrawText(int gameLevel, int linesCount)
        {
            spriteBatch.DrawString(_defaultFont, "level", new Vector2(400, 200), Color.Black);
            spriteBatch.DrawString(_defaultFont, gameLevel.ToString(), new Vector2(440, 260), Color.Black);

            spriteBatch.DrawString(_defaultFont, "lines", new Vector2(400, 400), Color.Black);
            spriteBatch.DrawString(_defaultFont, linesCount.ToString(), new Vector2(440, 460), Color.Black);
        }

        public void DrawSquare(int x, int y, SquareColor color)    // координаты в матрице
        {
            if (x < 0 || x >= MatrixSizeX || y < 0 || y >= MatrixSizeY) return;

            Texture2D squareTexture = _squareRed;
            switch(color)
            {
                case SquareColor.Empty: return; 
                case SquareColor.Red: squareTexture = _squareRed; break;
                case SquareColor.Blue: squareTexture = _squareBlue; break;
                case SquareColor.Green: squareTexture = _squareGreen; break;
                case SquareColor.Yellow: squareTexture = _squareYellow; break;
                case SquareColor.Orange: squareTexture = _squareOrange; break;
                case SquareColor.Violet: squareTexture = _squareViolet; break;
                case SquareColor.LightBlue: squareTexture = _squareLightBlue; break;
                default: throw new Exception("Unknown SquareColor");
            }

            var location = new Point(_gameArea.X + x * _squareSize.X, _gameArea.Y + y * _squareSize.Y);
            spriteBatch.Draw(squareTexture, new Rectangle(location, _squareSize), Color.White);
        }

    }
}
