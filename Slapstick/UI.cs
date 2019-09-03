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
            uiFont = Content.Load<SpriteFont>("Fonts/BigUI");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Celeb celeb, GameplayManager gameplayManager)
        {
            int minutes = (int)(gameplayManager.GetElapsedGameTime() / 60);
            int seconds = (int)(gameplayManager.GetElapsedGameTime() % 60);

            Vector2 uiFontSize = uiFont.MeasureString("Score: {0:00000000}");

            string playTime = string.Format("Time {0:00}:{1:00}", minutes, seconds);
            string gameScore = string.Format("Score: {0:00000000}", GameState.Score);

            spriteBatch.DrawString(uiFont, playTime, new Vector2(32, 32), Color.OrangeRed);
            spriteBatch.DrawString(uiFont, gameScore, new Vector2((1920 / 2) - (uiFontSize.X / 2), 32), Color.OrangeRed);
            //spriteBatch.DrawString(uiFont, "Lives: " + GameState.Lives, new Vector2(32, 64), Color.White);
        }

    }
}
