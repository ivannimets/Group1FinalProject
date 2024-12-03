using Microsoft.Xna.Framework;

namespace GameDevFinalProj
{
    class GameState
    {
        public static bool CheckForCollision(Point playerPosition, Point EnemyPosition)
        {
            if (playerPosition == EnemyPosition)
            {
                return true;
            }
            return false;
        }
    }
}
