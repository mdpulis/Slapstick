using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slapstick
{
    public enum BarrierPositions
    {
        Left = 0,
        LeftMid = 1,
        Mid = 2,
        RightMid = 3,
        Right = 4,
    }

    /// <summary>
    /// Manages the barrier that protects the celebrity from aerial enemies
    /// </summary>
    public class BarrierManager
    {
        private BarrierPositions barrierPosition = BarrierPositions.Mid;

        private Texture2D barrier;

        private const int BARRIER_HEIGHT = 30;
        private const int BARRIER_WIDTH = 150;

        private const int BARRIER_HEIGHT_LOCATION = 550;
        private const int BARRIER_X_POSITION_OFFSET = 50;
        private const int BARRIER_Y_POSITION_OFFSET = 25;


        public void LoadContent(ContentManager Content)
        {
            barrier = Content.Load<Texture2D>("Images/square_150x30");
        }


        public void Update(GameTime gameTime)
        {

        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            int screenCenter = (1920 / 2);

            switch (barrierPosition)
            {
                case (BarrierPositions.Left):
                    spriteBatch.Draw(barrier, new Rectangle(screenCenter - BARRIER_X_POSITION_OFFSET * 2, BARRIER_HEIGHT_LOCATION + BARRIER_Y_POSITION_OFFSET * 2, barrier.Width, BARRIER_HEIGHT), null, Color.White, MathHelper.Pi * -0.3f, new Vector2(barrier.Width / 2, barrier.Height / 2), SpriteEffects.None, 1);
                    break;
                case (BarrierPositions.LeftMid):
                    spriteBatch.Draw(barrier, new Rectangle(screenCenter - BARRIER_X_POSITION_OFFSET, BARRIER_HEIGHT_LOCATION + BARRIER_Y_POSITION_OFFSET, barrier.Width, BARRIER_HEIGHT), null, Color.White, MathHelper.Pi * -0.15f, new Vector2(barrier.Width / 2, barrier.Height / 2), SpriteEffects.None, 1);
                    break;
                case (BarrierPositions.Mid):
                    spriteBatch.Draw(barrier, new Rectangle(screenCenter, BARRIER_HEIGHT_LOCATION, barrier.Width, BARRIER_HEIGHT), null, Color.White, 0, new Vector2(barrier.Width / 2, barrier.Height / 2), SpriteEffects.None, 1);
                    break;
                case (BarrierPositions.RightMid):
                    spriteBatch.Draw(barrier, new Rectangle(screenCenter + BARRIER_X_POSITION_OFFSET, BARRIER_HEIGHT_LOCATION + BARRIER_Y_POSITION_OFFSET, barrier.Width, BARRIER_HEIGHT), null, Color.White, MathHelper.Pi * 0.15f, new Vector2(barrier.Width / 2, barrier.Height / 2), SpriteEffects.None, 1);
                    break;
                case (BarrierPositions.Right):
                    spriteBatch.Draw(barrier, new Rectangle(screenCenter + BARRIER_X_POSITION_OFFSET * 2, BARRIER_HEIGHT_LOCATION + BARRIER_Y_POSITION_OFFSET * 2, barrier.Width, BARRIER_HEIGHT), null, Color.White, MathHelper.Pi * 0.3f, new Vector2(barrier.Width / 2, barrier.Height / 2), SpriteEffects.None, 1);
                    break;
            }

        }


        public Vector2 RotatePoint(float x, float y, float turnAngle)
	    {
            Vector2 result = new Vector2();
            result.X = x * (float) Math.Cos(turnAngle) - y * (float) Math.Sin(turnAngle);
            result.Y = y * (float) Math.Sin(turnAngle) + y * (float) Math.Cos(turnAngle);
	        return result;
	    }

	    public Vector2 RotatePoint(Vector2 vectorToRotate, float turnAngle)
	    {
            Vector2 result = new Vector2();
            result.X = vectorToRotate.X* (float) Math.Cos(turnAngle) - vectorToRotate.Y * (float) Math.Sin(turnAngle);
            result.Y = vectorToRotate.X* (float) Math.Sin(turnAngle) + vectorToRotate.Y * (float) Math.Cos(turnAngle);
	        return result;
	    }

        public void MoveBarrier(bool isRight)
        {
            if(isRight)
            {
                switch (barrierPosition)
                {
                    case (BarrierPositions.Left):
                        barrierPosition = BarrierPositions.LeftMid;
                        break;
                    case (BarrierPositions.LeftMid):
                        barrierPosition = BarrierPositions.Mid;
                        break;
                    case (BarrierPositions.Mid):
                        barrierPosition = BarrierPositions.RightMid;
                        break;
                    case (BarrierPositions.RightMid):
                        barrierPosition = BarrierPositions.Right;
                        break;
                    case (BarrierPositions.Right):
                        barrierPosition = BarrierPositions.Left;
                        break;
                }
            }
            else //is left
            {
                switch (barrierPosition)
                {
                    case (BarrierPositions.Right):
                        barrierPosition = BarrierPositions.RightMid;
                        break;
                    case (BarrierPositions.RightMid):
                        barrierPosition = BarrierPositions.Mid;
                        break;
                    case (BarrierPositions.Mid):
                        barrierPosition = BarrierPositions.LeftMid;
                        break;
                    case (BarrierPositions.LeftMid):
                        barrierPosition = BarrierPositions.Left;
                        break;
                    case (BarrierPositions.Left):
                        barrierPosition = BarrierPositions.Right;
                        break;
                }
            }
            
        }


        /// <summary>
        /// Gets the current barrier position
        /// </summary>
        public BarrierPositions GetBarrierPosition()
        {
            return barrierPosition;
        }

    }
}
