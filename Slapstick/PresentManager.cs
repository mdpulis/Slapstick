using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace Slapstick
{
    public class PresentManager
    {
        public List<Present> presents = new List<Present>();
        Texture2D presentTexture;
        Texture2D goldPresentTexture;
        double presentTimer = 0;
        int presentIndexToDelete = -1;
        Random random = new Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private SpriteFont balloonFont;
        private SoundEffect balloonPopSFX;
        private const float FONT_SCALE = 1.5f;
        private const float FONT_DOWN_HEIGHT = 360.0f;

        private const int PRESENT_BASE_SCORE = 400;


        public void makePresent()
        {
            Present p = new Present();

            int goldRandomizer = random.Next(0, 5);
            if (goldRandomizer == 0)
                p.Initialize(goldPresentTexture, true);
            else
                p.Initialize(presentTexture, false);

            p.position.Y = -400;
            p.position.X = random.Next(934);
            presents.Add(p);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Present p in presents)
            {
                Vector2 balloonFontSize = balloonFont.MeasureString(chars[p.letterNumber].ToString());
                spriteBatch.Draw(p.texture, p.position, Color.White);
                spriteBatch.DrawString(balloonFont, chars[p.letterNumber].ToString(), new Vector2(p.position.X + (p.texture.Width / 2) - (balloonFontSize.X * FONT_SCALE / 2), p.position.Y + FONT_DOWN_HEIGHT - (balloonFontSize.Y * FONT_SCALE/ 2)), Color.Red, 0.0f, new Vector2(0,0), FONT_SCALE, SpriteEffects.None, 1f);
            }
        }

        public void LoadContent(ContentManager Content)
        {
            presentTexture = Content.Load<Texture2D>("Images/Balloon_Present");
            goldPresentTexture = Content.Load<Texture2D>("Images/Balloon_Present_Gold");
            balloonFont = Content.Load<SpriteFont>("Fonts/UI");
            balloonPopSFX = Content.Load<SoundEffect>("Sounds/balloon_pop");
        }

        public void Update(GameTime gameTime)
        {
            presentTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (presentTimer > 15)
            {
                makePresent();
                presentTimer = 0;
            }

            presentIndexToDelete = -1;
            foreach (Present p in presents)
            {
                p.Update(gameTime);
                if (p.position.Y > 400)
                {
                    presentIndexToDelete = presents.IndexOf(p);
                }
                foreach(Keys K in Keyboard.GetState().GetPressedKeys())
                {
                    if (chars[p.letterNumber].ToString() == K.ToString())
                    {
                        presentIndexToDelete = presents.IndexOf(p);
                        balloonPopSFX.Play();

                        if(p.IsGold()) //is gold balloon
                        {
                            if(GameState.Lives >= 3) //add points if at max lives
                            {
                                GameState.Score += (int)(PRESENT_BASE_SCORE * 1.5f);
                            }
                            else
                            {
                                GameState.Lives++; //add lives to game state for gold balloon
                            }
                        }
                        else //is normal balloon
                        {
                            if (p.position.Y <= 100)
                            {
                                GameState.Score += (int)(PRESENT_BASE_SCORE * 1.0f);
                            }
                            else if (p.position.Y > 100 && (p.position.Y < 200))
                            {
                                GameState.Score += (int)(PRESENT_BASE_SCORE * 0.8f);
                            }
                            else if (p.position.Y > 200 && (p.position.Y < 300))
                            {
                                GameState.Score += (int)(PRESENT_BASE_SCORE * 0.6f);
                            }
                            else
                            {
                                GameState.Score += (int)(PRESENT_BASE_SCORE * 0.4f);
                            }
                        }
                        
                    }
                }
            }
            if (presentIndexToDelete != -1)
            {
                presents.RemoveAt(presentIndexToDelete);
            }
        }


        /// <summary>
        /// Clears out all the presents and resets parameters
        /// </summary>
        public void ResetPresents()
        {
            presentTimer = 0;
            presents.Clear();
        }

    }
}
