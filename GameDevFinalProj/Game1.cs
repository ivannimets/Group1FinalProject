using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;
using GameDevFinalProj.Screens.Game;
using GameDevFinalProj.Screens.StartGameMenu;

namespace GameDevFinalProj
{
	public class Game1 : Game
    {
        public ScreenConditions _activeScreen;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Map _map;
        private Player _player;
        private Enemy _enemy;
        private Pickups _pickups;

        private Texture2D[] _img;
        private Random _rnd;
        private int _i;

        // Start Screen
        private int screenWidth;
        private int screenHeight;
        private PlayButton _playButton;






        public Game1(int? idk = null) // Seed
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _rnd = idk.HasValue ? new Random(idk.Value) : new Random();
        }

        protected override void Initialize()
        {
			// Screen
			screenWidth = 960;
			screenHeight = 540;

            int cols = 20;
            int rows = 11;
            int size = screenWidth / cols;

            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = size * rows;
            _graphics.ApplyChanges();

            _map = new Map(cols, rows, size, GraphicsDevice);
            _player = new Player(new Point(cols / 2, rows / 2), size, cols, rows, GraphicsDevice); // Center
            _enemy = new Enemy(new Point(0, 0), size, cols, rows, GraphicsDevice);
            _pickups = new Pickups(cols, rows, size, GraphicsDevice);

            //Start Screen
            _playButton = new PlayButton(this);

			_activeScreen = ScreenConditions.StartGameMenu;

			_rnd = new Random();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _img = new Texture2D[4];
            _img[0] = Content.Load<Texture2D>("Uno");
            _img[1] = Content.Load<Texture2D>("Due");
            _img[2] = Content.Load<Texture2D>("Tre");
            _img[3] = Content.Load<Texture2D>("Img");

            _i = _rnd.Next(_img.Length);
        }

        protected override void Update(GameTime gameTime)
        {
            if (_activeScreen == ScreenConditions.StartGameMenu)
            {
                _playButton.Update(this);
				if (Keyboard.GetState().IsKeyDown(Keys.Escape))
					Exit();
				return;
			}
            if (_activeScreen == ScreenConditions.Game)
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Escape))
					Exit();

				_player.Update();
				_enemy.Update(_player.GetPosition(), gameTime);
				if (GameState.CheckForCollision(_player.GetPosition(), _enemy.GetPosition()))
				{
					// GameOver(); IMPLEMENT THIS
					Exit();
				}
                return;
			}
			if (_activeScreen == ScreenConditions.PauseMenu)
			{

			}
			if (_activeScreen == ScreenConditions.EndGameMenu)
			{

			}
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (_activeScreen == ScreenConditions.StartGameMenu)
            {
                GraphicsDevice.Clear(Color.Transparent);

                _spriteBatch.Begin();

                _map.Draw(_spriteBatch); // ?

                _playButton.Draw(screenWidth, screenHeight, _spriteBatch);

                //Texture2D i = _img[3]; // _img[_i]
                //System.Numerics.Vector2 pos = new System.Numerics.Vector2(
                //    (GraphicsDevice.Viewport.Width - 626) / 2,
                //    (GraphicsDevice.Viewport.Height - 212) / 2
                //);

                //_spriteBatch.Draw(i, new Rectangle((int)pos.X, (int)pos.Y, 626, 212), Color.White);

                _spriteBatch.End();
            }
			if (_activeScreen == ScreenConditions.Game)
            {

				GraphicsDevice.Clear(Color.Transparent);

				_spriteBatch.Begin();
				_map.Draw(_spriteBatch);
				_player.Draw(_spriteBatch);
				_enemy.Draw(_spriteBatch);
				_pickups.Draw(_spriteBatch);
				_spriteBatch.End();
			}
            
            base.Draw(gameTime);
        }
	}

	public enum ScreenConditions
	{
		StartGameMenu,
		Game,
		EndGameMenu,
		PauseMenu,
	}
}