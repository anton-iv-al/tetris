using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TetrisGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TetrisGame : Game
    {
        private SpriteBatch spriteBatch;
        private DrawManager drawManager;

        private int _gameLevel = 1;
        private int _gameSpeed = 1;
        private int _linesCount = 0;

        private Field _field;

        public TetrisGame()
        {
            drawManager = new DrawManager(new GraphicsDeviceManager(this), Window);
            _field = new Field(drawManager);

            Content.RootDirectory = "Content";           
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            drawManager.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.            
            try
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);
                drawManager.LoadContent(spriteBatch, Content);
            }
            catch (System.Exception e)
            {
                ShowExceptionMessage(e);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            try
            {

            }
            catch (System.Exception e)
            {
                ShowExceptionMessage(e);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            try
            {
                drawManager.DrawBackGround();
                drawManager.DrawText(_gameLevel, _linesCount);
                _field.Draw();
                drawManager.DrawFigure();
            }
            catch (System.Exception e)
            {
                ShowExceptionMessage(e);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void ShowExceptionMessage(System.Exception e)
        {
            System.Windows.Forms.MessageBox.Show(
                    e.GetType().Name + "\n\n" +
                    e.Message + "\n\n" +
                    e.StackTrace
                , "Error");
        }
    }
}
