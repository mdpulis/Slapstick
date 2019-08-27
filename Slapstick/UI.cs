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

    public class UI
    {

        public string score;
        private SpriteFont uiFont;

        public UI()
        {
        }

        public void LoadContent(ContentManager Content)
        {
            uiFont = Content.Load<SpriteFont>("Fonts/UI");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Celeb celeb)
        {
            string playTime = string.Format("Time {0:00}:{1:00}", gameTime.TotalGameTime.Minutes, gameTime.TotalGameTime.Seconds);
            string gameScore = string.Format("Score {0:00000}", GameState.Score);

            spriteBatch.DrawString(uiFont, gameScore, new Vector2(32, 32), Color.White);
            spriteBatch.DrawString(uiFont, playTime, new Vector2(32, 64), Color.White);
            spriteBatch.DrawString(uiFont, "Lives: " + GameState.Lives, new Vector2(32, 96), Color.White);
        }

    }
}
