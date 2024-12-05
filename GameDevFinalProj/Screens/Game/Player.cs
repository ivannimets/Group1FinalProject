using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevFinalProj.Screens.Game
{
	public class Player
	{
		public int Direction = 0; //0 = Up, 1 = Down, 2 = Left, 3 = Right
		public int Frame = 0;
		public Point position;
		private int size, cols, rows;

		private Texture2D PUp1;
        private Texture2D PDown1;
        private Texture2D PLeft1;
        private Texture2D PRight1;

        private Texture2D PUp2;
        private Texture2D PDown2;
        private Texture2D PLeft2;
        private Texture2D PRight2;

        private KeyboardState idk;


        public Player(Point start, int tSize, int tCols, int tRows, GraphicsDevice graphics, Game1 parent)
        {
			position = start;
			size = tSize;
			cols = tCols;
			rows = tRows;


            PUp1 = parent.Content.Load<Texture2D>("P-U1");
            PDown1 = parent.Content.Load<Texture2D>("P-D1");
            PLeft1 = parent.Content.Load<Texture2D>("P-L1");
            PRight1 = parent.Content.Load<Texture2D>("P-R1");

            PUp2 = parent.Content.Load<Texture2D>("P-U2");
            PDown2 = parent.Content.Load<Texture2D>("P-D2");
            PLeft2 = parent.Content.Load<Texture2D>("P-L2");
            PRight2 = parent.Content.Load<Texture2D>("P-R2");




            idk = Keyboard.GetState();
		}

		public void Update()
		{
			KeyboardState plz = Keyboard.GetState();

			// 
			if (plz.IsKeyDown(Keys.Up) && !idk.IsKeyDown(Keys.Up) && position.Y > 0)
			{
				Direction = 0;
				position.Y--;
				Frame++;


                if (Frame >= 2)
                {
                    Frame = 0;
                }
            }
			else if (plz.IsKeyDown(Keys.Down) && !idk.IsKeyDown(Keys.Down) && position.Y < rows - 1)
			{
				Direction = 1;
                position.Y++;
                Frame++;

				if (Frame >= 2)
				{
					Frame = 0;
				}
            }
			else if (plz.IsKeyDown(Keys.Left) && !idk.IsKeyDown(Keys.Left) && position.X > 0)
			{
				Direction = 2;
                position.X--;
                Frame++;


                if (Frame >= 2)
                {
                    Frame = 0;
                }
            }
			else if (plz.IsKeyDown(Keys.Right) && !idk.IsKeyDown(Keys.Right) && position.X < cols - 1)
			{
				Direction = 3;
                position.X++;
                Frame++;


                if (Frame >= 2)
                {
                    Frame = 0;
                }
            }

			idk = plz;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Rectangle rectangle = new Rectangle(position.X * size, position.Y * size, size, size);


            if (Frame == 0)
            {
                if (Direction == 0)
                {

                    spriteBatch.Draw(PUp1, rectangle, new Color(255, 255, 255)); // ?
                }

                if (Direction == 1)
                {
                    spriteBatch.Draw(PDown1, rectangle, new Color(255, 255, 255)); // ?
                }

                if (Direction == 2)
                {
                    spriteBatch.Draw(PLeft1, rectangle, new Color(255, 255, 255)); // ?
                }


                if (Direction == 3)
                {
                    
                    spriteBatch.Draw(PRight1, rectangle, new Color(255, 255, 255)); // ?
                }


               
            }

            if (Frame == 1)
            {
                if (Direction == 0)
                {

                    spriteBatch.Draw(PUp2, rectangle, new Color(255, 255, 255)); // ?
                }

                if (Direction == 1)
                {
                    spriteBatch.Draw(PDown2, rectangle, new Color(255, 255, 255)); // ?
                }

                if (Direction == 2)
                {
                    spriteBatch.Draw(PLeft2, rectangle, new Color(255, 255, 255)); // ?
                }


                if (Direction == 3)
                {

                    spriteBatch.Draw(PRight2, rectangle, new Color(255, 255, 255)); // ?
                }

            }


        }
		public Point GetPosition()
		{
			return position;
		}
	}
}