using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Slapstick
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Sprites
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;


        //Classes responsible for managing lots of content and functionality
        private MainMenu mainMenu = new MainMenu();
        private RetryScreen retryScreen = new RetryScreen();

        private BackgroundManager backgroundManager = new BackgroundManager();
        private PlayerInput playerInput = new PlayerInput();
        private BarrierManager barrierManager = new BarrierManager();
        private UI gameUI = new UI();
        private PersonManager pm = new PersonManager();
        private GameplayManager gameplayManager = new GameplayManager();
        private PresentManager presentManager = new PresentManager();
        private Celeb celeb = new Celeb();
      

        SoundManager sm = new SoundManager();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            celeb.Initialize(graphics);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mainMenu.LoadContent(Content);
            retryScreen.LoadContent(Content);
            
            font = Content.Load<SpriteFont>("Fonts/Arial");
            pm.LoadContent(Content);
            sm.LoadContent(Content);
            backgroundManager.LoadContent(Content);
            playerInput.LoadContent(Content);
            if(GameState.BarrierOn)
                barrierManager.LoadContent(Content);
            gameUI.LoadContent(Content);
            celeb.LoadContent(Content);
            gameplayManager.LoadContent(Content);
            presentManager.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            backgroundManager.UnloadContent();
            playerInput.UnloadContent();
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch(GameState.CurrentGameplayState)
            {
                case (GameplayState.MainMenu):
                    mainMenu.Update(gameTime);
                    break;
                case (GameplayState.InGame):
                    backgroundManager.Update(gameTime);
                    sm.Update(gameTime, gameUI);
                    if(GameState.BarrierOn)
                        barrierManager.Update(gameTime);
                    playerInput.Update(gameTime, pm.people, barrierManager);
                    pm.update(gameTime, graphics, gameUI, celeb);
                    gameplayManager.Update(gameTime);
                    celeb.celebUpdate(gameTime);
                    presentManager.Update(gameTime);
                    break;
                case (GameplayState.RetryScreen):
                    retryScreen.Update(gameTime, pm.people, gameplayManager, playerInput, presentManager);
                    break;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            backgroundManager.Draw(spriteBatch, gameTime);

            switch (GameState.CurrentGameplayState)
            {
                case (GameplayState.MainMenu):
                    mainMenu.Draw(spriteBatch, gameTime);
                    break;
                case (GameplayState.InGame):
                    celeb.Draw(spriteBatch);
                    playerInput.Draw(spriteBatch, gameTime);
                    if(GameState.BarrierOn)
                        barrierManager.Draw(spriteBatch, gameTime);
                    presentManager.Draw(spriteBatch);
                    pm.draw(spriteBatch);
                    gameplayManager.Draw(spriteBatch, gameTime);
                    gameUI.Draw(spriteBatch, gameTime, celeb, gameplayManager);

                    break;
                case (GameplayState.RetryScreen):
                    retryScreen.Draw(spriteBatch, gameTime);
                    break;
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
