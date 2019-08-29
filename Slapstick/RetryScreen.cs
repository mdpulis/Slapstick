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
        private SpriteFont directionsFont;

        private Vector2 ohNoTextSize;
        private Vector2 returnTextSize;


        public void LoadContent(ContentManager Content)
        {
            directionsFont = Content.Load<SpriteFont>("Fonts/BigUI");

            ohNoTextSize = directionsFont.MeasureString("Oh no! The celeb left!");
            returnTextSize = directionsFont.MeasureString("Press Backspace to return to the main menu.");
        }

        public void Update(GameTime gameTime, List<Person> people, GameplayManager gameplayManager)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Back))
                ReturnToMainMenu(people, gameplayManager);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(directionsFont, "Oh no! The celeb left!", new Vector2((1920 / 2) - (ohNoTextSize.X / 2), 300), Color.OrangeRed, 0, new Vector2(1, 1), 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(directionsFont, "Press Backspace to return to the main menu.", new Vector2((1920 / 2) - (returnTextSize.X / 2), 500), Color.OrangeRed, 0, new Vector2(1, 1), 1, SpriteEffects.None, 0);
        }

        private void ReturnToMainMenu(List<Person> people, GameplayManager gameplayManager)
        {
            people.Clear(); //destroy all people
            gameplayManager.ResetGameplayVariables();
            GameState.ResetGameState();
            //GameState.ProgressGameplayState();
        }
    }
}
