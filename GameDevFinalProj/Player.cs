using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevFinalProj
{
    public class Player
    {
        private Point position;
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
                position.Y--;
            }
            else if (plz.IsKeyDown(Keys.Down) && !idk.IsKeyDown(Keys.Down) && position.Y < rows - 1)
            {
                position.Y++;
            }
            else if (plz.IsKeyDown(Keys.Left) && !idk.IsKeyDown(Keys.Left) && position.X > 0)
            {
                position.X--;
            }
            else if (plz.IsKeyDown(Keys.Right) && !idk.IsKeyDown(Keys.Right) && position.X < cols - 1)
            {
                position.X++;
            }

            idk = plz;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new Rectangle(position.X * size, position.Y * size, size, size);
            spriteBatch.Draw(texture, rectangle, new Color(255, 165, 0)); // ?
        }
    }
}