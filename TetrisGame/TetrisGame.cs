using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TetrisGame
{
    public class TetrisGame : Game
    {
        private SpriteBatch spriteBatch;
        private DrawManager drawManager;

        private int _gameLevel = 1;
        private int _gameSpeed = 1;
        private int _linesCount = 0;

        private bool _isRun = true;

        private Field _field;
        private FigureTypes _figureTypes = new FigureTypes();
        private Figure _currentFigure;

        public TetrisGame()
        {
            try
            {
                drawManager = new DrawManager(new GraphicsDeviceManager(this), Window);
                _field = new Field(drawManager);
                Figure.DrawManager = drawManager;

                Content.RootDirectory = "Content";
            }
            catch (System.Exception e)
            {
                _isRun = false;
                ShowExceptionMessage(e);
            }
        }

        protected override void Initialize()
        {
            try
            {
                drawManager.Initialize();
                CreateNewFigure();
            }
            catch (System.Exception e)
            {
                _isRun = false;
                ShowExceptionMessage(e);
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {           
            try
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);
                drawManager.LoadContent(spriteBatch, Content);
            }
            catch (System.Exception e)
            {
                _isRun = false;
                ShowExceptionMessage(e);
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            if (!_isRun) return;

            try
            {

            }
            catch (System.Exception e)
            {
                _isRun = false;
                ShowExceptionMessage(e);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!_isRun) return;
            spriteBatch.Begin();

            try
            {
                drawManager.DrawBackGround();
                drawManager.DrawText(_gameLevel, _linesCount);
                _field.Draw();
                _currentFigure.Draw();
            }
            catch (System.Exception e)
            {
                _isRun = false;
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

        private void CreateNewFigure()
        {
            _currentFigure = new Figure(_figureTypes.GetRandomMatrix());
        }
    }
}
