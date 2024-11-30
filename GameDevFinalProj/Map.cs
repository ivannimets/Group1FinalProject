using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevFinalProj
{
    public class Map
    {
        private int cols, rows, size;
        private Texture2D un, deux;

        public Map(int tCols, int tRows, int tSize, GraphicsDevice graphics)
        {
            cols = tCols;
            rows = tRows;
            size = tSize;

            un = new Texture2D(graphics, 1, 1);
            un.SetData(new[] { new Color(92, 188, 73) }); // ?
            deux = new Texture2D(graphics, 1, 1);
            deux.SetData(new[] { new Color(99, 199, 77) }); // ?
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    Texture2D texture = (x + y) % 2 == 0 ? un : deux;

                    Rectangle rectangle = new Rectangle(x * size, y * size, size, size);

                    spriteBatch.Draw(texture, rectangle, Color.White);
                }
            }
        }
    }
}