using GameDevFinalProj.Controllers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Drawing;

namespace GameDevFinalProj.Screens.StartGameMenu
{
	internal class PlayButton : Button
	{
		public PlayButton(Game1 parent)
		{
			_texture = parent.Content.Load<Texture2D>("Play Button");
			_textureHovered = parent.Content.Load<Texture2D>("Play Button Hover");
			_activeTexture = _texture;
		}
		public void Update(Game1 parent)
		{
			MouseState ms = Mouse.GetState();
			if ((ms.X > _position.X && ms.X < _position.X + _activeTexture.Width) && (ms.Y > _position.Y && ms.Y < _position.Y + _activeTexture.Height))
			{
				_activeTexture = _textureHovered;
				if (ms.LeftButton == ButtonState.Pressed)
				{
					parent._activeScreen = ScreenConditions.Game;
				}
			}
			else
			{
				_activeTexture = _texture;
			}
		}
	}
}
