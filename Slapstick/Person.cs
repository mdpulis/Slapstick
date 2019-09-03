using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slapstick
{
    public class Person
    {
        private Direction direction;
        private Rectangle[] frames = new Rectangle[30];
        private Rectangle[] paparazziFrames = new Rectangle[144];
        public Rectangle currentFrame;
        private double animTimer;
        private int frameCounter = 0;
        private bool noisy, giga;
        private double speed;
        public int lives = 1, scale = 1;
        private float frameTime = .0333f;
        public Texture2D texture;
        public Vector2 position;
        public SpriteEffects spriteEffects;

        private const int NORMIE_SPRITE_FRAME_WIDTH = 200;
        private const int PAPARAZZI_SPRITE_FRAME_WIDTH = 200;

        public void Initialize(Direction dir, bool n, bool g, int spd, Texture2D tex, GraphicsDeviceManager gdm)
        {
            direction = dir;
            noisy = n;
            giga = g;
            speed = spd;
            texture = tex;
          
            if (dir == Direction.left)
            {
                position = new Vector2(gdm.PreferredBackBufferWidth,
                700);
                spriteEffects = SpriteEffects.None;
            }
            else
            {
                position = new Vector2(0,
                700);
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            if (giga)
            {
                position.Y -= 400;
                speed /= 3;
                frameTime *= 1.3f;
                lives = 3;
                scale = 3;
            }

            for (int i = 0; i < 30; i++)
            {
                frames[i] = new Rectangle(NORMIE_SPRITE_FRAME_WIDTH * (i % 5), 200 * (i / 5), NORMIE_SPRITE_FRAME_WIDTH, 200);
            }
            for (int i = 0; i < 144; i++)
            {
                paparazziFrames[i] = new Rectangle(200 * (i % 10), 200 * (i / 10), PAPARAZZI_SPRITE_FRAME_WIDTH, 200);
            }
            currentFrame = frames[frameCounter];

        }


        public void Update(GameTime gameTime)
        {
            if (direction == Direction.left)
            {
                position.X -= (float)speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                position.X += (float)speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            
            animTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if(animTimer > frameTime) //every 1/30 seconds for normals, 1/60 for gigas
            {
                frameCounter++;
                if (!noisy)
                {
                    if (frameCounter >= 30)
                    {
                        frameCounter = 0;
                    }
                    currentFrame = frames[frameCounter];
                }
                else
                {
                    if (frameCounter >= 144)
                    {
                        frameCounter = 0;
                    }
                    currentFrame = paparazziFrames[frameCounter];
                }
                animTimer = 0;
            }
        }


        public void Draw()
        {

        }

        public float getCenterX()
        {
            if(noisy)
            {
                return position.X + (PAPARAZZI_SPRITE_FRAME_WIDTH * scale / 2);
            }
            else
            {
                return position.X + (NORMIE_SPRITE_FRAME_WIDTH * scale / 2);
            }
        }

        public bool isNoisy()
        {
            return noisy;
        }

        public bool isGiga()
        {
            return giga;
        }

        public Direction getDirection()
        {
            return direction;
        }

        /// <summary>
        /// Gets the weight of the character
        /// </summary>
        public int GetPersonSpawnWeight()
        {
            if (scale == 1)
                return 1;
            else if (scale == 2)
                return 2;
            else if (scale >= 3)
                return 3;
            else
                return 1;
        }
    
    }
}
