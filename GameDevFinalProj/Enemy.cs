using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevFinalProj
{
    internal class Enemy
    {
        private Point position;
        private int size, cols, rows;
        private Texture2D texture;
        private bool direction = true; //this flips the x and y direction for the enemy movement
        private float elapsedTime; 
        private float moveDelay = 0.5f;
        public Enemy(Point start, int tSize, int tCols, int tRows, GraphicsDevice graphics)
        {
            position = start;
            size = tSize;
            cols = tCols;
            rows = tRows;

            texture = new Texture2D(graphics, 1, 1);
            texture.SetData(new[] {Color.Black}); 
        }

        public void Update(Point playerPosition, GameTime gameTime)
        {
            // Update elapsed time
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= moveDelay)
            {
                // Reset the timer
                elapsedTime = 0;

                // Perform movement logic
                if (direction)
                {
                    position.X += playerPosition.X > position.X ? 1 : -1;
                    direction = false;
                }
                else
                {
                    position.Y += playerPosition.Y > position.Y ? 1 : -1;
                    direction = true;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new Rectangle(position.X * size, position.Y * size, size, size);
            spriteBatch.Draw(texture, rectangle, new Color(0, 0, 0)); // ?
        }
    }
}
