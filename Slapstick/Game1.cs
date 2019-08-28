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
        private Texture2D celebTexture;


        //Classes responsible for managing lots of content and functionality
        private MainMenu mainMenu = new MainMenu();

        private BackgroundManager backgroundManager = new BackgroundManager();
        private PlayerInput playerInput = new PlayerInput();
        private BarrierManager barrierManager = new BarrierManager();
        private UI gameUI = new UI();
        private PersonManager pm = new PersonManager();

        private Celeb celeb;
        double bpmIncreaseTimer;
      

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
            
            font = Content.Load<SpriteFont>("Fonts/Arial");
            celebTexture = Content.Load<Texture2D>("Images/celeb_static_sized");
            celeb = new Celeb(celebTexture, graphics);
            pm.LoadContent(Content);
            sm.LoadContent(Content);
            backgroundManager.LoadContent(Content);
            playerInput.LoadContent(Content);
            if(GameState.BarrierOn)
                barrierManager.LoadContent(Content);
            gameUI.LoadContent(Content);
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
                    bpmIncreaseTimer += gameTime.ElapsedGameTime.TotalSeconds;

                    if (bpmIncreaseTimer >= 20 || (bpmIncreaseTimer >= 10 && GameState.BeatsPerMinute == 180))
                    {
                        GameState.AddBPM();
                        bpmIncreaseTimer = 0;
                    }

                    sm.Update(gameTime, gameUI);
                    if(GameState.BarrierOn)
                        barrierManager.Update(gameTime);
                    playerInput.Update(gameTime, pm.people, barrierManager);
                    pm.update(gameTime, graphics, gameUI, celeb);

                    if (GameState.Lives == 0)
                    {
                        Exit();
                    }
                    break;
                case (GameplayState.RetryScreen):

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
                    pm.draw(spriteBatch);
                    gameUI.Draw(spriteBatch, gameTime, celeb);

                    break;
                case (GameplayState.RetryScreen):

                    break;
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
