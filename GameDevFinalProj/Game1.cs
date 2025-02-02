﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;
using GameDevFinalProj.Screens.Game;
using GameDevFinalProj.Screens.StartGameMenu;
using GameDevFinalProj.Screens.EndGameMenu;
using GameDevFinalProj.Screens.PauseGameScreen;
using GameDevFinalProj.Screens.OptionsScreen;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace GameDevFinalProj
{
	public class Game1 : Game
	{
		public ScreenConditions _activeScreen;

		public bool leftMouseButtonPressed;

		int Frame;

		public bool SoundOn = true;

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private Map _map;
		private Player _player;
		private Enemy _enemy;
		private Pickups _pickups;
		private PostGameStats postGameStats;

		private Texture2D[] _img;
		private Random _rnd;
		private int _i;

		// Start Screen
		public int screenWidth;
		public int screenHeight;
		public int cols;
		public int rows;
		public int size;

		private PlayButton _playButton;
		private OptionsButton _optionsButton;
		private ExitButton _exitButton;

		private SpriteFont _font;

		// End Screen

		private RestartButton _restartButton;

		// PauseScreen

		private ContinueButton _continueButton;

		// OptionsScreen

		private BackButton _backButton;
		private SoundButton _soundButton;

		//sounds
		private SoundEffect bombPickupSound;
		private SoundEffect gameoverSound;

		public Game1(int? idk = null) // Seed
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			_rnd = idk.HasValue ? new Random(idk.Value) : new Random();
		}

		protected override void Initialize()
		{
			leftMouseButtonPressed = false;

			// Screen
			screenWidth = 1800;
			screenHeight = 900;
			size = 100;
			cols = screenWidth / size;
			rows = screenHeight / size;


			if (cols % 2 == 0) cols -= 1;
			if (rows % 2 == 0) rows -= 1;

			_graphics.PreferredBackBufferWidth = size * cols;
			_graphics.PreferredBackBufferHeight = size * rows;
			_graphics.ApplyChanges();

			_map = new Map(cols, rows, size, GraphicsDevice);
			InitialiseGameScreen();

			//Start Screen
			_playButton = new PlayButton(this);
			_optionsButton = new OptionsButton(this);
			_exitButton = new ExitButton(this);

			//End Screen
			_restartButton = new RestartButton(this);

			// PauseScreen

			_continueButton = new ContinueButton(this);

			// OptionsScreen

			_backButton = new BackButton(this);
			_soundButton = new SoundButton(this);

			_activeScreen = ScreenConditions.StartGameMenu;

			_rnd = new Random();
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_font = Content.Load<SpriteFont>("ScoreFont");
			postGameStats = new PostGameStats(_font);
			bombPickupSound = Content.Load<SoundEffect>("8bit_bomb_explosion");
			gameoverSound = Content.Load<SoundEffect>("losetrumpet");
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
				IsMouseVisible = true;
				_playButton.Update(this);
				_optionsButton.Update(this);
				_exitButton.Update(this);
			}
			else if (_activeScreen == ScreenConditions.Game)
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				{
					_activeScreen = ScreenConditions.PauseMenu;
					leftMouseButtonPressed = false;
					return;
				}

				_player.Update();
				_enemy.Update(_player.GetPosition(), gameTime);

				// Update PostGameStats timer
				postGameStats.Update(gameTime, gameOverCondition: false);

				// Check collision with enemy
				if (GameState.CheckForCollision(_player.GetPosition(), _enemy.GetPosition()))
				{
					if (SoundOn)
					{
						gameoverSound.Play();
					}
					postGameStats.Update(gameTime, gameOverCondition: true); // Mark game over
					_activeScreen = ScreenConditions.EndGameMenu;
				}

				// Player & Pickup Collision
				if (_pickups.CheckCollision(_player, new List<Enemy> { _enemy }, cols, rows))
				{
					if (SoundOn)
					{

						bombPickupSound.Play();
					}
					_pickups.Kill(new List<Enemy> { _enemy });
					postGameStats.AddScore(1);
					_enemy.moveDelay *= 0.95f;// Increment score via PostGameStats
					_pickups.Respawn(_player.GetPosition(), cols, rows);
				}
			}
			if (_activeScreen == ScreenConditions.PauseMenu)
			{
				IsMouseVisible = true;
				_continueButton.Update(this);
				_exitButton.Update(this);
				_optionsButton.Update(this);
			}
			if (_activeScreen == ScreenConditions.EndGameMenu)
			{
				IsMouseVisible = true;
				_restartButton.Update(this);
				_exitButton.Update(this);

				if (_restartButton.IsPressed)
				{
					postGameStats.Reset(); // Reset stats
					InitialiseGameScreen(); // Reset game objects
					_restartButton.IsPressed = false; // Reset the pressed state
					_activeScreen = ScreenConditions.Game; // Transition back to the Game screen
				}

			}
			if (_activeScreen == ScreenConditions.OptionsMenu)
			{
				IsMouseVisible = true;
				_soundButton.Update(this);
				_backButton.Update(this);
				_exitButton.Update(this);
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
				_exitButton.Draw(screenWidth, screenHeight, _spriteBatch);
				_optionsButton.Draw(screenWidth, screenHeight, _spriteBatch);

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
				postGameStats.Draw(_spriteBatch, new Microsoft.Xna.Framework.Vector2(10, 0), new Microsoft.Xna.Framework.Vector2(10, 40), Microsoft.Xna.Framework.Vector2.Zero);

				_spriteBatch.End();

			}
			if (_activeScreen == ScreenConditions.EndGameMenu)
			{
				GraphicsDevice.Clear(Color.Transparent);

				_spriteBatch.Begin();

				// Draw background or map if necessary
				_map.Draw(_spriteBatch);

				// Draw UI buttons
				_restartButton.Draw(screenWidth, screenHeight, _spriteBatch);
				_exitButton.Draw(screenWidth, screenHeight, _spriteBatch);

				// Display final game stats
				postGameStats.Draw(
					_spriteBatch,
					new Microsoft.Xna.Framework.Vector2(screenWidth / 2 - 215, 130), // Timer position
					new Microsoft.Xna.Framework.Vector2(screenWidth / 2 - 145, 70), // Score position
					new Microsoft.Xna.Framework.Vector2(screenWidth / 2 - 115, 10) // Game over message position
				);

				_spriteBatch.End();
			}
			if (_activeScreen == ScreenConditions.OptionsMenu)
			{
				GraphicsDevice.Clear(Color.Transparent);

				_spriteBatch.Begin();

				_map.Draw(_spriteBatch); // ?

				_soundButton.Draw(screenWidth, screenHeight, _spriteBatch);
				_backButton.Draw(screenWidth, screenHeight, _spriteBatch);
				_exitButton.Draw(screenWidth, screenHeight, _spriteBatch);

				//Texture2D i = _img[3]; // _img[_i]
				//System.Numerics.Vector2 pos = new System.Numerics.Vector2(
				//    (GraphicsDevice.Viewport.Width - 626) / 2,
				//    (GraphicsDevice.Viewport.Height - 212) / 2
				//);

				//_spriteBatch.Draw(i, new Rectangle((int)pos.X, (int)pos.Y, 626, 212), Color.White);

				_spriteBatch.End();
			}
			if (_activeScreen == ScreenConditions.PauseMenu)
			{
				GraphicsDevice.Clear(Color.Transparent);

				_spriteBatch.Begin();

				_map.Draw(_spriteBatch); // ?

				_continueButton.Draw(screenWidth, screenHeight, _spriteBatch);
				_exitButton.Draw(screenWidth, screenHeight, _spriteBatch);
				_optionsButton.Draw(screenWidth, screenHeight, _spriteBatch);

				//Texture2D i = _img[3]; // _img[_i]
				//System.Numerics.Vector2 pos = new System.Numerics.Vector2(
				//    (GraphicsDevice.Viewport.Width - 626) / 2,
				//    (GraphicsDevice.Viewport.Height - 212) / 2
				//);

				//_spriteBatch.Draw(i, new Rectangle((int)pos.X, (int)pos.Y, 626, 212), Color.White);

				_spriteBatch.End();
			}
			base.Draw(gameTime);
		}


		public void InitialiseGameScreen()
		{
			Random random = new Random();
			Point newPosition;
			if (random.Next(2) == 0) // Horizontal
			{
				newPosition = new Point(random.Next(0, cols), random.Next(2) == 0 ? 0 : rows - 1);
			}
			else // Vertical
			{
				newPosition = new Point(random.Next(2) == 0 ? 0 : cols - 1, random.Next(0, rows));
			}


			_player = new Player(new Point(cols / 2, rows / 2), size, cols, rows, GraphicsDevice, this); // Center
			_enemy = new Enemy(newPosition, size, cols, rows, GraphicsDevice, this);
			_pickups = new Pickups(cols, rows, size, GraphicsDevice, this);
		}
	}

	public enum ScreenConditions
	{
		StartGameMenu,
		Game,
		EndGameMenu,
		PauseMenu,
		OptionsMenu,
	}
}