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
        public Rectangle currentFrame;
        private double animTimer;
        private int frameCounter = 0;
        private bool noisy;
        private int speed;
        public Texture2D texture;
        public Vector2 position;
        public SpriteEffects spriteEffects;
        public void Initialize(Direction dir, bool n, int spd, Texture2D tex, GraphicsDeviceManager gdm)
        {
            direction = dir;
            noisy = n;
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

            for(int i = 0; i < 30; i++)
            {
                frames[i] = new Rectangle(150 * (i % 5), 200 * (i / 5), 150, 200);
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
            if(animTimer > .0333) //every 1/30 seconds
            {
                frameCounter++;
                if(frameCounter >= 30)
                {
                    frameCounter = 0;
                }
                currentFrame = frames[frameCounter];
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
    
    }
}
