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
    /// Handles the main menu
    /// </summary>
    public class MainMenu
    {
        private Texture2D gameLogo;
        private SpriteFont directionsFont;

        private Vector2 startTextSize;


        public void LoadContent(ContentManager Content)
        {
            gameLogo = Content.Load<Texture2D>("Images/slapstick_logo");
            directionsFont = Content.Load<SpriteFont>("Fonts/BigUI");

            startTextSize = directionsFont.MeasureString("Press Enter to Begin!");
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                PlayGame();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int logoPosition = (1920 / 2) - (gameLogo.Width / 2);
            spriteBatch.Draw(gameLogo, new Vector2(logoPosition, 300), Color.Orange);
            spriteBatch.DrawString(directionsFont, "Press Enter to Begin!", new Vector2((1920 / 2) - (startTextSize.X / 2), 500), Color.OrangeRed, 0, new Vector2(1, 1), 1, SpriteEffects.None, 0);

        }

        private void PlayGame()
        {
            GameState.ProgressGameplayState();
        }

    }


}
