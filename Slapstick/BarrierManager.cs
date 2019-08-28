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

        private const int BARRIER_HEIGHT_LOCATION = 500;
        private const int BARRIER_X_POSITION_OFFSET = 50;
        private const int BARRIER_Y_POSITION_OFFSET = 30;


        public void LoadContent(ContentManager Content)
        {
            barrier = Content.Load<Texture2D>("Images/square_150x150_white");
        }


        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int screenCenter = (1920 / 2) - (barrier.Width / 2);

            float leftRotation = BARRIER_HEIGHT * (MathHelper.Pi * -0.2f);
            float leftMidRotation = BARRIER_HEIGHT * (MathHelper.Pi * -0.1f);
            float midRotation = BARRIER_HEIGHT * (MathHelper.Pi * 0.0f);
            float rightMidRotation = BARRIER_HEIGHT * (MathHelper.Pi * 0.1f);
            float rightRotation = BARRIER_HEIGHT * (MathHelper.Pi * 0.2f);

            switch (barrierPosition)
            {
                case (BarrierPositions.Left):
                    spriteBatch.Draw(barrier, new Rectangle(screenCenter, BARRIER_HEIGHT_LOCATION + (int)rightRotation, barrier.Width, BARRIER_HEIGHT), null, Color.White, MathHelper.Pi * -0.2f, new Vector2(0, 0), SpriteEffects.None, 1);
                    //spriteBatch.Draw(barrier, new Rectangle(screenCenter - BARRIER_X_POSITION_OFFSET * 2, BARRIER_HEIGHT_LOCATION + Math.Abs((int)leftRotation), barrier.Width, BARRIER_HEIGHT), null, Color.White, MathHelper.Pi * -0.2f, new Vector2(0, 0), SpriteEffects.None, 1);
                    break;
                case (BarrierPositions.LeftMid):
                    spriteBatch.Draw(barrier, new Rectangle(screenCenter, BARRIER_HEIGHT_LOCATION + (int)rightMidRotation, barrier.Width, BARRIER_HEIGHT), null, Color.White, MathHelper.Pi * -0.1f, new Vector2(0, 0), SpriteEffects.None, 1);
                    break;
                case (BarrierPositions.Mid):
                    spriteBatch.Draw(barrier, new Rectangle(screenCenter, BARRIER_HEIGHT_LOCATION, barrier.Width, BARRIER_HEIGHT), null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1);
                    break;
                case (BarrierPositions.RightMid):
                    spriteBatch.Draw(barrier, new Rectangle(screenCenter + (int)rightMidRotation, BARRIER_HEIGHT_LOCATION - (int)rightMidRotation, barrier.Width, BARRIER_HEIGHT), null, Color.White, MathHelper.Pi * 0.1f, new Vector2(0, 0), SpriteEffects.None, 1);
                    break;
                case (BarrierPositions.Right):
                    spriteBatch.Draw(barrier, new Rectangle(screenCenter + (int)rightRotation, BARRIER_HEIGHT_LOCATION - (int)rightRotation, barrier.Width, BARRIER_HEIGHT), null, Color.White, MathHelper.Pi * 0.2f, new Vector2(0, 0), SpriteEffects.None, 1);
                    break;
            }





            //spriteBatch.Draw(barrier, new Rectangle(screenCenter, 300, barrier.Width, barrier.Height), null, Color.White, MathHelper.Pi, new Vector2(0, 0), SpriteEffects.None, 1);

            //spriteBatch.Draw(barrier, new Vector2(aPadLocation, PAD_HEIGHT_LOCATION), Color.White);

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
