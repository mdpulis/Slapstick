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
        private int speed;
        private float frameTime = .0333f;
        public Texture2D texture;
        public Vector2 position;
        public SpriteEffects spriteEffects;
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
                speed /= 2;
                frameTime *= 1.2f;
            }

            for (int i = 0; i < 30; i++)
            {
                frames[i] = new Rectangle(150 * (i % 5), 200 * (i / 5), 150, 200);
            }
            for (int i = 0; i < 144; i++)
            {
                paparazziFrames[i] = new Rectangle(200 * (i % 10), 200 * (i / 10), 150, 200);
            }
            currentFrame = frames[frameCounter];

        }


        public void Update(GameTime gameTime)
        {
            if (direction == Direction.left)
            {
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            return position.X + (150 / 2);
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
    
    }
}
