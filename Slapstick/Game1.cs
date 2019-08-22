using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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

        Texture2D background;
        SpriteFont font;
        private SpriteFont fontScore;
        private SpriteFont fontTimer;
        private UI gameUI;
        

        //Classes responsible for managing lots of content and functionality
        PlayerInput playerInput;

        PersonManager pm = new PersonManager();
        List<Person> people = new List<Person>();
        double personTimer;
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
            playerInput = new PlayerInput();
            gameUI = new UI();

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
            pm.LoadContent(Content);
            sm.LoadContent(Content);
            background = Content.Load<Texture2D>("Images/theatre");
            font = Content.Load<SpriteFont>("Fonts/Arial");
            fontScore = Content.Load<SpriteFont>("Fonts/Score");
            fontTimer = Content.Load<SpriteFont>("Fonts/Timer");


            playerInput.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

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


            personTimer += gameTime.ElapsedGameTime.TotalSeconds;
            bpmIncreaseTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (personTimer >= 6 - gameUI.beatsPerMinute * 2.0 / 100)
            {
                people.Add(pm.makePerson(graphics, gameUI.beatsPerMinute));
                personTimer = 0;
            }
            if(bpmIncreaseTimer >= 30)
            {
                gameUI.addBPM();
                bpmIncreaseTimer = 0;
            }

            int peopleIndexToDelete = -1;
            foreach (Person p in people)
            {
                p.Update(gameTime);
                if (p.position.X < 0 || p.position.X > 1920)
                {
                    peopleIndexToDelete = people.IndexOf(p);
                }
            }
            if(peopleIndexToDelete != -1)
            {
                people.RemoveAt(peopleIndexToDelete);
            }

            sm.Update(gameTime, gameUI);

            playerInput.Update(gameTime, people);


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
            
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White);

            playerInput.Draw(spriteBatch, gameTime);
            //spriteBatch.Draw(shuttle, new Vector2(450, 240), Color.White);
            foreach (Person p in people)       
            {
                spriteBatch.Draw(p.texture, p.position, Color.White);
            }

            spriteBatch.DrawString(fontScore, "Score : " + gameUI.score, new Vector2(0, 0), Color.Red);
            spriteBatch.DrawString(fontTimer, "Time " + gameTime.TotalGameTime.Minutes + ":" + gameTime.TotalGameTime.Seconds, new Vector2(0, 0), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
