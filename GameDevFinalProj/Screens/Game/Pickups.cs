using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic; // For handling the enemies list

namespace GameDevFinalProj.Screens.Game
{
    internal class Pickups
    {
        private Point position;
        private int size;
        private Texture2D texture;
        private bool isAlive = true;
        private Random random = new Random();

        public Pickups(int col, int rows, int tSize, GraphicsDevice graphics)
        {
            size = tSize;
            Respawn(new Point(col / 2, rows / 2), col, rows); // Random
            texture = new Texture2D(graphics, 1, 1);
            texture.SetData(new[] { new Color(255, 0, 0) }); // ?
        }

        public void Respawn(Point playerPosition, int col, int rows)
        {
            Point newPosition;
            do
            {
                // Random Position WITHIN Screen
                newPosition = new Point(random.Next(0, col), random.Next(0, rows));
            }
            while (COVID19(newPosition, playerPosition)); // NOT TOO Close TOO Player

            position = newPosition;
            isAlive = true; // Respawn Pickup
        }

        private bool COVID19(Point pickupPosition, Point playerPosition)
        {
            int x = Math.Abs(pickupPosition.X - playerPosition.X);
            int y = Math.Abs(pickupPosition.Y - playerPosition.Y);
            return x <= 3 && y <= 3; // WITHIN 3 Tile(s)
        }

        public void CheckCollision(Player player, List<Enemy> enemies, int col, int rows)
        {
            // Check IF Player & Powerup Collide
            if (isAlive && player.GetPosition() == position)
            {
                // KILL ALL Enemy
                foreach (var enemy in enemies)
                {
                    enemy.IsAlive = false;
                }

                // Respawn Powerup
                Respawn(player.GetPosition(), col, rows);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                Rectangle rectangle = new Rectangle(position.X * size, position.Y * size, size, size);
                spriteBatch.Draw(texture, rectangle, Color.Red); // ?
            }
        }

        public Point GetPosition()
        {
            return position;
        }
    }
}
