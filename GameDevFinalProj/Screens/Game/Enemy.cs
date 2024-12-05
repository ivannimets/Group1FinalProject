using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static System.Collections.Specialized.BitVector32;

namespace GameDevFinalProj.Screens.Game
{
	internal class Enemy
	{
        int Frame = 0;
        int EnemyDirection = 0;

		private Point position;
		private int size, cols, rows;
		private Texture2D texture;
		private bool direction = true; //this flips the x and y direction for the enemy movement
		private float elapsedTime;
		private float moveDelay = 0.5f;
        private Random random; // NEW Spawn(s)

        private Texture2D EUp1;
        private Texture2D EDown1;
        private Texture2D ELeft1;
        private Texture2D ERight1;

        private Texture2D EUp2;
        private Texture2D EDown2;
        private Texture2D ELeft2;
        private Texture2D ERight2;

        // FOR Powerup
        public bool IsAlive { get; set; } = true;

        public Enemy(Point start, int tSize, int tCols, int tRows, GraphicsDevice graphics, Game1 parent)
		{
            EUp1 = parent.Content.Load<Texture2D>("E-U1");
            EDown1 = parent.Content.Load<Texture2D>("E-D1");
            ELeft1 = parent.Content.Load<Texture2D>("E-L1");
            ERight1 = parent.Content.Load<Texture2D>("E-R1");

            EUp2 = parent.Content.Load<Texture2D>("E-U2");
            EDown2 = parent.Content.Load<Texture2D>("E-D2");
            ELeft2 = parent.Content.Load<Texture2D>("E-L2");
            ERight2 = parent.Content.Load<Texture2D>("E-R2");

            position = start;
			size = tSize;
			cols = tCols;
			rows = tRows;
            random = new Random();

            texture = new Texture2D(graphics, 1, 1);
			texture.SetData(new[] { Color.Black }); // ?
		}

		public void Update(Point playerPosition, GameTime gameTime)
		{
			if (!IsAlive)
			{
                Respawn(playerPosition); // Respawn IF NOT Alive & Check Player's Position FOR Respawnin'
                return;
			}

            // Update elapsed time
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (elapsedTime >= moveDelay)
			{
				// Reset the timer
				elapsedTime = 0;

				// Perform movement logic
				if (direction && position.X != playerPosition.X)
				{
					position.X += playerPosition.X > position.X ? 1 : -1;
				}
				direction = false;
				if (position.Y != playerPosition.Y)
				{
					position.Y += playerPosition.Y > position.Y ? 1 : -1;

				}
				direction = true;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
            if (IsAlive)
            {
                Rectangle rectangle = new Rectangle(position.X * size, position.Y * size, size, size);
                spriteBatch.Draw(texture, rectangle, Color.Black); // ?

                if (Frame == 0)
                {
                    if (EnemyDirection == 0)
                    {

                        spriteBatch.Draw(EUp1, rectangle, new Color(255, 255, 255)); // ?
                    }

                    if (EnemyDirection == 1)
                    {
                        spriteBatch.Draw(EDown1, rectangle, new Color(255, 255, 255)); // ?
                    }

                    if (EnemyDirection == 2)
                    {
                        spriteBatch.Draw(ELeft1, rectangle, new Color(255, 255, 255)); // ?
                    }


                    if (EnemyDirection == 3)
                    {

                        spriteBatch.Draw(ERight1, rectangle, new Color(255, 255, 255)); // ?
                    }



                }

                if (Frame == 1)
                {
                    if (EnemyDirection == 0)
                    {

                        spriteBatch.Draw(EUp2, rectangle, new Color(255, 255, 255)); // ?
                    }

                    if (EnemyDirection == 1)
                    {
                        spriteBatch.Draw(EDown2, rectangle, new Color(255, 255, 255)); // ?
                    }

                    if (EnemyDirection == 2)
                    {
                        spriteBatch.Draw(ELeft2, rectangle, new Color(255, 255, 255)); // ?
                    }


                    if (EnemyDirection == 3)
                    {

                        spriteBatch.Draw(ERight2, rectangle, new Color(255, 255, 255)); // ?
                    }

                }
            }
        }

		public Point GetPosition()
		{
			return position;
		}

        private void Respawn(Point playerPosition)
        {
            Point newPosition;
            do
            {
                // Spawn ONLY ON "Edge"
                if (random.Next(2) == 0) // Horizontal
                {
                    newPosition = new Point(random.Next(0, cols), random.Next(2) == 0 ? 0 : rows - 1);
                }
                else // Vertical
                {
                    newPosition = new Point(random.Next(2) == 0 ? 0 : cols - 1, random.Next(0, rows));
                }
            }
            while (EazyE(newPosition, playerPosition));

            position = newPosition;
            IsAlive = true; // Respawn
        }

		// SO Enemy CAN'T Spawn ON Player
        private bool EazyE(Point enemyPosition, Point playerPosition)
        {
            int x = Math.Abs(enemyPosition.X - playerPosition.X);
            int y = Math.Abs(enemyPosition.Y - playerPosition.Y);
            return x <= 5 && y <= 5; // Check IF Within 5 Tile(s)
        }
    }
}
