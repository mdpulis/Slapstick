using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slapstick
{
    /// <summary>
    /// The game over/retry screen
    /// </summary>
    public class RetryScreen
    {
        private Texture2D newspaper;
        private SpriteFont directionsFont;

        private Vector2 ohNoTextSize;
        private Vector2 returnTextSize;

        private Vector2 startTextSize;

        private float textScale = 1.0f;

        private const float MIN_TEXT_SCALE = 1.0f;
        private const float MAX_TEXT_SCALE = 1.2f;

        private bool textSizeIncreasing = true;

        private bool newspaperIntro = true;
        private float newspaperRotation = 0.0f;
        private float newspaperScale = 0.0f;


        public void LoadContent(ContentManager Content)
        {
            newspaper = Content.Load<Texture2D>("Images/newspaper3");
            directionsFont = Content.Load<SpriteFont>("Fonts/BigUI");

            ohNoTextSize = directionsFont.MeasureString("GAME OVER");
            returnTextSize = directionsFont.MeasureString("Press Backspace to return to the main menu.");
        }

        public void Update(GameTime gameTime, List<Person> people, GameplayManager gameplayManager, PlayerInput playerInput, PresentManager presentManager)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Back))
                ReturnToMainMenu(people, gameplayManager, playerInput, presentManager);

            if (textSizeIncreasing)
                textScale += 0.01f;
            else
                textScale -= 0.01f;


            if (textSizeIncreasing && textScale >= MAX_TEXT_SCALE)
            {
                textSizeIncreasing = false;
            }
            else if (!textSizeIncreasing && textScale <= MIN_TEXT_SCALE)
            {
                textSizeIncreasing = true;
            }


            if (newspaperIntro)
            {
                newspaperScale += 0.01f * 4;
                newspaperRotation += 0.019f * 4;

                if(newspaperRotation >= 1.9f)
                {
                    newspaperScale = 1.0f;
                    newspaperRotation = 1.9f;
                    newspaperIntro = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(newspaper, new Rectangle(1920 / 2, 1080 / 2, (int)(newspaper.Width * newspaperScale), (int)(newspaper.Height * newspaperScale)), null, Color.White, MathHelper.Pi * newspaperRotation, new Vector2(newspaper.Width * newspaperScale / 2, newspaper.Height * newspaperScale / 2), SpriteEffects.None, 1);

            spriteBatch.DrawString(directionsFont, "GAME OVER", new Vector2((1920 / 2) - (ohNoTextSize.X / 2), 300), Color.OrangeRed, 0, new Vector2(1, 1), 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(directionsFont, "Press Backspace to return to the main menu.", new Vector2((1920 / 2) - (returnTextSize.X * textScale / 2), 500), Color.OrangeRed, 0, new Vector2(1, 1), textScale, SpriteEffects.None, 0);

        }

        private void ReturnToMainMenu(List<Person> people, GameplayManager gameplayManager, PlayerInput playerInput, PresentManager presentManager)
        {
            newspaperIntro = true;
            newspaperScale = 0.0f;
            newspaperRotation = 0.0f;

            people.Clear(); //destroy all people
            gameplayManager.ResetGameplayVariables();
            playerInput.ResetSlappers();
            presentManager.ResetPresents();
            GameState.ResetGameState();
            //GameState.ProgressGameplayState();
        }
    }
}
