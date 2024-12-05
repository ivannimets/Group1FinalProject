using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevFinalProj.Screens.Game
{
	public class Player
	{
		public int Direction = 0; //0 = Up, 1 = Down, 2 = Left, 3 = Right
		public int Frame = 0;
		public Point position;
		private int size, cols, rows;
		private Texture2D texture;

		private KeyboardState idk;

		public Player(Point start, int tSize, int tCols, int tRows, GraphicsDevice graphics)
		{
			position = start;
			size = tSize;
			cols = tCols;
			rows = tRows;

			texture = new Texture2D(graphics, 1, 1);
			texture.SetData(new[] { new Color(255, 165, 0) }); ; // ?

			idk = Keyboard.GetState();
		}

		public void Update()
		{
			KeyboardState plz = Keyboard.GetState();

			// 
			if (plz.IsKeyDown(Keys.Up) && !idk.IsKeyDown(Keys.Up) && position.Y > 0)
			{
				Direction = 0;
				position.Y--;
				Frame++;


                if (Frame >= 1)
                {
                    Frame = 0;
                }
            }
			else if (plz.IsKeyDown(Keys.Down) && !idk.IsKeyDown(Keys.Down) && position.Y < rows - 1)
			{
				Direction = 1;
                position.Y++;
                Frame++;

				if (Frame >= 1)
				{
					Frame = 0;
				}
            }
			else if (plz.IsKeyDown(Keys.Left) && !idk.IsKeyDown(Keys.Left) && position.X > 0)
			{
				Direction = 2;
                position.X--;
                Frame++;


                if (Frame >= 1)
                {
                    Frame = 0;
                }
            }
			else if (plz.IsKeyDown(Keys.Right) && !idk.IsKeyDown(Keys.Right) && position.X < cols - 1)
			{
				Direction = 3;
                position.X++;
                Frame++;


                if (Frame >= 1)
                {
                    Frame = 0;
                }
            }

			idk = plz;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Rectangle rectangle = new Rectangle(position.X * size, position.Y * size, size, size);
			spriteBatch.Draw(texture, rectangle, new Color(255, 165, 0)); // ?
		}
		public Point GetPosition()
		{
			return position;
		}
	}
}