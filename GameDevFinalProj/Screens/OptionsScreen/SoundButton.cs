using GameDevFinalProj.Controllers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevFinalProj.Screens.OptionsScreen
{
	internal class SoundButton : Button
	{
		public Texture2D _textureOff;
		public Texture2D _textureOffHovered;
		public Texture2D _textureOn;
		public Texture2D _textureOnHovered;

		public SoundButton(Game1 parent)
		{
			_textureOn = parent.Content.Load<Texture2D>("Sound On Button");
			_textureOnHovered = parent.Content.Load<Texture2D>("Sound On Button Hover");
			_textureOff = parent.Content.Load<Texture2D>("Sound Off Button");
			_textureOffHovered = parent.Content.Load<Texture2D>("Sound Off Button Hover");
			_texture = _textureOn;
			_textureHovered = _textureOnHovered;
			_activeTexture = _texture;
			_position = new System.Numerics.Vector2(
					(parent.screenWidth - _activeTexture.Width) / 2,
					(parent.screenHeight - _activeTexture.Height) / 3
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
					if (_texture == _textureOn)
					{
						_texture = _textureOff;
						_textureHovered = _textureOffHovered;
					}
					else
					{
						_texture = _textureOn;
						_textureHovered = _textureOnHovered;
					}
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
