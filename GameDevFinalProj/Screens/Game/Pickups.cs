using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameDevFinalProj.Screens.Game
{
	internal class Pickups
	{
		private Point position;
		private int size;
		private Texture2D texture;
		private bool isAlive = true;
		Random Random = new Random();
		public Pickups(int col, int rows, int tSize, GraphicsDevice graphics)
		{
			position = new Point(Random.Next(0, col), Random.Next(0, rows));
			size = tSize;
			texture = new Texture2D(graphics, 1, 1);
			texture.SetData(new[] { new Color(255, 165, 0) }); ; // ?
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (isAlive)
			{
				Rectangle rectangle = new Rectangle(position.X * size, position.Y * size, size, size);
				spriteBatch.Draw(texture, rectangle, new Color(255, 0, 0)); // ?
			}
		}
		public Point GetPosition()
		{
			return position;
		}

	}
}
