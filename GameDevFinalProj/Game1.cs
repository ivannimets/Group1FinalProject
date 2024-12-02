using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;

namespace GameDevFinalProj
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Map _map;
        private Player _player;
        private Enemy _enemy;

        private Texture2D[] _img;
        private Random _rnd;
        private int _i;

        private bool start = true;

        public Game1(int? idk = null) // Seed
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            _rnd = idk.HasValue ? new Random(idk.Value) : new Random();
        }

        protected override void Initialize()
        {
            // Screen
            int width = 960;
            int height = 540;

            int cols = 15;
            int rows = 9;
            int size = width / cols;

            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = size * rows;
            _graphics.ApplyChanges();

            _map = new Map(cols, rows, size, GraphicsDevice);
            _player = new Player(new Point(cols / 2, rows / 2), size, cols, rows, GraphicsDevice); // Center
            _enemy = new Enemy(new Point(0, 0), size, cols, rows, GraphicsDevice);

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
            if (start)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                if (Keyboard.GetState().GetPressedKeys().Length > 0)
                {
                    start = false;
                }

                return;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update();
            _enemy.Update(_player.GetPosition(), gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (start)
            {
                GraphicsDevice.Clear(Color.Transparent);

                _spriteBatch.Begin();

                _map.Draw(_spriteBatch); // ?

                Texture2D i = _img[3]; // _img[_i]
                System.Numerics.Vector2 pos = new System.Numerics.Vector2(
                    (GraphicsDevice.Viewport.Width - 626) / 2,
                    (GraphicsDevice.Viewport.Height - 212) / 2
                );

                _spriteBatch.Draw(i, new Rectangle((int)pos.X, (int)pos.Y, 626, 212), Color.White);

                _spriteBatch.End();
                return;
            }

            GraphicsDevice.Clear(Color.Transparent);

            _spriteBatch.Begin();
            _map.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);
            _enemy.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}