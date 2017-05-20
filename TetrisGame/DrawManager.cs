using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisGame
{
    public class DrawManager
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private GameWindow Window;

        private Texture2D _wallTexture;
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

        private int _thickness = 10;

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
            _gameAreaForDraw = new Rectangle(50 - 1, 50 - _thickness, 300 + _thickness, 600 + _thickness);
            _squareSize = new Point(30, 30);
        }

        public void LoadContent(SpriteBatch spriteBatch, Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = spriteBatch;
            
            _wallTexture = Content.Load<Texture2D>("wall_1");
            _squareRed = Content.Load<Texture2D>("square_red");
            _squareBlue = Content.Load<Texture2D>("square_blue");
            _squareGreen = Content.Load<Texture2D>("square_green");
            _squareYellow = Content.Load<Texture2D>("square_yellow");
            _squareOrange = Content.Load<Texture2D>("square_orange");
            _squareViolet = Content.Load<Texture2D>("square_violet");
            _squareLightBlue = Content.Load<Texture2D>("square_light_blue");

            _defaultFont = Content.Load<SpriteFont>("default");
        }

        public void Draw()
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            spriteBatch.Draw(_wallTexture, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
            C3.MonoGame.Primitives2D.FillRectangle(spriteBatch, _gameArea, Color.Black);
            C3.MonoGame.Primitives2D.DrawRectangle(spriteBatch, _gameAreaForDraw, Color.Blue, _thickness);
            spriteBatch.DrawString(_defaultFont, "test_text", new Vector2(200, 200), Color.White);
            spriteBatch.Draw(_squareRed, new Rectangle(new Point(50, 50), _squareSize), Color.White);
            spriteBatch.Draw(_squareRed, new Rectangle(new Point(320, 620), _squareSize), Color.White);
            spriteBatch.End();
        }

    }
}
