using GameDevFinalProj.Controllers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevFinalProj.Screens.PauseGameScreen
{
	internal class ContinueButton : Button
	{
		public ContinueButton(Game1 parent)
		{
			_texture = parent.Content.Load<Texture2D>("Continue Button");
			_textureHovered = parent.Content.Load<Texture2D>("Continue Button Hover");
			_activeTexture = _texture;
			_position = new System.Numerics.Vector2(
					(parent.screenWidth - _activeTexture.Width) / 2,
					(parent.screenHeight - _activeTexture.Height) * 2 / 3
				);
		}
		public void Update(Game1 parent)
		{
			MouseState ms = Mouse.GetState();
			if ((ms.X > _position.X && ms.X < _position.X + _activeTexture.Width) && (ms.Y > _position.Y && ms.Y < _position.Y + _activeTexture.Height))
			{
				_activeTexture = _textureHovered;
				if (ms.LeftButton == ButtonState.Pressed && parent.leftMouseButtonPressed == false)
				{
					parent._activeScreen = ScreenConditions.Game;
					parent.IsMouseVisible = false;
					parent.leftMouseButtonPressed = true;
				}
			}
			else
			{
				_activeTexture = _texture;
			}
			if (ms.LeftButton == ButtonState.Released && parent.leftMouseButtonPressed == true)
			{
				parent.leftMouseButtonPressed = false;
			}
		}
	}
}
