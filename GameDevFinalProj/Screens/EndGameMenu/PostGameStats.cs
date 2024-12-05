using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevFinalProj.Screens.EndGameMenu
{
    internal class PostGameStats
    {
        private double timer; // Time survived
        private int score;    // Total score
        private bool isGameOver; // Game state
        private SpriteFont font; // Font for displaying text

        public PostGameStats(SpriteFont font)
        {
            this.font = font;
            Reset();
        }

        public void Update(GameTime gameTime, bool gameOverCondition)
        {
            if (!isGameOver)
            {
                // Increment the timer
                timer += gameTime.ElapsedGameTime.TotalSeconds;

                // Check for game-over condition
                if (gameOverCondition)
                {
                    isGameOver = true;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 timerPosition, Vector2 scorePosition, Vector2 gameOverPosition)
        {
            if (!isGameOver)
            {
                // Draw timer and score during gameplay
                spriteBatch.DrawString(font, $"Time: {timer:F2} sec", timerPosition, Color.White);
                spriteBatch.DrawString(font, $"Score: {score}", scorePosition, Color.White);
            }
            else
            {
                // Draw post-game stats
                spriteBatch.DrawString(font, "Game Over", gameOverPosition, Color.Red);
                spriteBatch.DrawString(font, $"Time Survived: {timer:F2} sec", timerPosition, Color.AliceBlue);
                spriteBatch.DrawString(font, $"Total Score: {score}", scorePosition, Color.AliceBlue);
            }
        }

        public void AddScore(int points)
        {
            if (!isGameOver)
            {
                score += points;
            }
        }

        public void Reset()
    {
        timer = 0;
        score = 0;
        isGameOver = false;
    }

    public bool IsGameOver => isGameOver; // Read-only property
    }
}
