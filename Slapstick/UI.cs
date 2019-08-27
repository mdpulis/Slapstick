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
        private int points;
        public int beatsPerMinute = 80;
        public const int maxBeatsPerMinute = 240;
        private SpriteFont fontScore;
        private SpriteFont fontTimer;

        public UI()
        {
            score = "000";
            points = 0;
        }

        public void LoadContent(ContentManager Content)
        {
            fontScore = Content.Load<SpriteFont>("Fonts/Score");
            fontTimer = Content.Load<SpriteFont>("Fonts/Timer");
        }
        public void addScore(int scoreToAdd)
        {
            score = score + scoreToAdd;
        }

        public void penalize(int penalty)
        {
            //score = score - penalty;
        }

        public void addBPM()
        {
            beatsPerMinute = beatsPerMinute + 20;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Celeb celeb)
        {
            string playTime = string.Format("Time {0:00}:{1:00}", gameTime.TotalGameTime.Minutes, gameTime.TotalGameTime.Seconds);
            string gameScore = string.Format("Score {0:00000}", GameState.Score);

            spriteBatch.DrawString(fontScore, gameScore, new Vector2(32, 32), Color.Red);
            spriteBatch.DrawString(fontTimer, playTime, new Vector2(32, 64), Color.Red);
            spriteBatch.DrawString(fontTimer, "Lives: " + celeb.GetLives(), new Vector2(32, 96), Color.Red);
        }

    }
}
