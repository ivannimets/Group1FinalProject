using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameDevFinalProj.Controllers
{
	public class Button
	{
		public bool _isHovered = false;
		public System.Numerics.Vector2 _position;
		public Texture2D _texture;
		public Texture2D _textureHovered;
		public Texture2D _activeTexture;

		public void Draw(int screenWidth, int screenHeight, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_activeTexture, new Microsoft.Xna.Framework.Rectangle((int)_position.X, (int)_position.Y, _activeTexture.Width, _activeTexture.Height), Microsoft.Xna.Framework.Color.White);
		}
	}
}
