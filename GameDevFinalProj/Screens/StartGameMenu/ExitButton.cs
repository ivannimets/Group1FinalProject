using GameDevFinalProj.Controllers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevFinalProj.Screens.StartGameMenu
{
	internal class ExitButton : Button
	{
		public ExitButton(Game1 parent)
		{
			_texture = parent.Content.Load<Texture2D>("Exit Button");
			_textureHovered = parent.Content.Load<Texture2D>("Exit Button Hover");
			_activeTexture = _texture;
			_position = new System.Numerics.Vector2(
					(parent.screenWidth - _activeTexture.Width) / 20,
					(parent.screenHeight - _activeTexture.Height) / 20
				);
		}
		public void Update(Game1 parent)
		{
			MouseState ms = Mouse.GetState();
			if ((ms.X > _position.X && ms.X < _position.X + _activeTexture.Width) && (ms.Y > _position.Y && ms.Y < _position.Y + _activeTexture.Height))
			{
				_activeTexture = _textureHovered;
				if (ms.LeftButton == ButtonState.Pressed)
				{
					parent.Exit();
				}
			}
			else
			{
				_activeTexture = _texture;
			}
		}
	}
}
