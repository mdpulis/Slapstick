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


        public Celeb(Texture2D tex, GraphicsDeviceManager gdm)
        {
            texture = tex;
            position = new Vector2((gdm.PreferredBackBufferWidth - texture.Width)/2,
               700);
            Debug.WriteLine("position: " + position.X);
        }

        public void LoadContent(ContentManager Content)
        {
            clearThroatSFX = Content.Load<SoundEffect>("Sounds/clear_throat");

            heart = Content.Load<Texture2D>("Images/heart");
            anger = Content.Load<Texture2D>("Images/anger");

            textureWidthDifference = (int)(heart.Width * 1.5f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

            

            if(GameState.Lives == 3)
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
            return position.X + (150 / 2);
        }


    }
}
