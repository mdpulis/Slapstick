using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slapstick
{
    public class Celeb
    {
        public Texture2D texture;
        public Vector2 position;

        private SoundEffect clearThroatSFX;

        private Texture2D heart;
        private Texture2D anger;

        private int textureWidthDifference = 0;

        private const int HEALTH_HEIGHT_ABOVE_CELEB = 100;
        private double animTimer;
        private Rectangle currentFrame;
        private Rectangle[] frames = new Rectangle[180];
        private int frameCounter = 0;
        private float frameTime = .05f;
        private Vector2 zeroVector = new Vector2(0,0);
        public int scale = 1;



        public Celeb()
        {

        }

        public void Initialize(GraphicsDeviceManager gdm)
        {
            position = new Vector2((gdm.PreferredBackBufferWidth- 150) / 2,
                           700);
            for (int i = 0; i < 180; i++)
            {
                frames[i] = new Rectangle(175 * (i % 11), 175 * (i / 14), 175, 175);

            }
            currentFrame = frames[frameCounter];
        }

        public void celebUpdate(GameTime gameTime)
        {
            animTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (animTimer > frameTime) //every 1/30 seconds for normals, 1/60 for gigas
            {
                frameCounter++;
                if (frameCounter >= 180)
                {
                    frameCounter = 0;
                }
                currentFrame = frames[frameCounter];
                animTimer = 0;
            }
            
        }

        public void LoadContent(ContentManager Content)
        {
            clearThroatSFX = Content.Load<SoundEffect>("Sounds/clear_throat");

            texture = Content.Load<Texture2D>("Images/CelebWave");
            heart = Content.Load<Texture2D>("Images/heart");
            anger = Content.Load<Texture2D>("Images/anger");

            textureWidthDifference = (int)(heart.Width * 1.5f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, currentFrame, Color.White, 0.0f, zeroVector,scale, SpriteEffects.None, 0.0f);
            
            if (GameState.Lives == 3)
            {
                spriteBatch.Draw(heart, new Vector2(position.X - textureWidthDifference, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), Color.White);
                spriteBatch.Draw(heart, new Vector2(position.X, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), Color.White);
                spriteBatch.Draw(heart, new Vector2(position.X + textureWidthDifference, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), Color.White);
            }
            else if(GameState.Lives == 2)
            {
                spriteBatch.Draw(anger, new Vector2(position.X - textureWidthDifference, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), Color.White);
                spriteBatch.Draw(heart, new Vector2(position.X, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), Color.White);
                spriteBatch.Draw(heart, new Vector2(position.X + textureWidthDifference, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), Color.White);
            }
            else if(GameState.Lives == 1)
            {
                spriteBatch.Draw(anger, new Vector2(position.X - textureWidthDifference, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), Color.White);
                spriteBatch.Draw(anger, new Vector2(position.X, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), Color.White);
                spriteBatch.Draw(heart, new Vector2(position.X + textureWidthDifference, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), Color.White);
            }
            else
            {
                spriteBatch.Draw(anger, new Vector2(position.X - textureWidthDifference, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), Color.White);
                spriteBatch.Draw(anger, new Vector2(position.X, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), Color.White);
                spriteBatch.Draw(anger, new Vector2(position.X + textureWidthDifference, position.Y - HEALTH_HEIGHT_ABOVE_CELEB), Color.White);
            }

        }

        public void collision(bool isNoisy)
        {
            if (isNoisy)
            {
                GameState.Lives--;
                clearThroatSFX.Play();
            }
            else
            {
                GameState.Score += 50;
            }
        }

        public float getCenterX()
        {
            return position.X + (175 / 2);
        }


    }
}
