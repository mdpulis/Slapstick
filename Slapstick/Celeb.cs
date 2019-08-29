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

        private Rectangle[] frames = new Rectangle[60];
        private int frameCounter = 0;
        public Rectangle currentFrame;
        private double animTimer;

        public Texture2D celebTexture;

        public Celeb()
        {
            for (int i = 0; i < 60; i++)
            {
                frames[i] = new Rectangle(75 * (i % 6), 150 * (i / 6), 75, 150);
            }
            currentFrame = frames[frameCounter];
        }


        public void Initialize(GraphicsDeviceManager gdm)
        {
            position = new Vector2((gdm.PreferredBackBufferWidth) / 2,
               700);

        }

        public void LoadContent(ContentManager Content)
        {
            clearThroatSFX = Content.Load<SoundEffect>("Sounds/clear_throat");
            celebTexture = Content.Load<Texture2D>("Images/Celeb_Wave_SpriteSheet");
            heart = Content.Load<Texture2D>("Images/heart");
            anger = Content.Load<Texture2D>("Images/anger");

            textureWidthDifference = (int)(heart.Width * 1.5f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(celebTexture,position,currentFrame, Color.White, 0.0f, new Vector2(0, 0), 1.3f,SpriteEffects.None, 0.0f);

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



        public void update(GameTime gameTime)
        {
            animTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animTimer > .0333)
            {
                frameCounter++;
                if (frameCounter >= 60)
                {
                    frameCounter = 0;
                }
                currentFrame = frames[frameCounter];
                animTimer = 0;
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
            return position.X + (75 / 2);
        }


    }
}
