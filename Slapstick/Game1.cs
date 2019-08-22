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
        

        //Classes responsible for managing lots of content and functionality
        PlayerInput playerInput;

        PersonManager pm = new PersonManager();
        List<Person> people = new List<Person>();
        double personTimer;

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

            background = Content.Load<Texture2D>("Images/theatre");
            font = Content.Load<SpriteFont>("Fonts/Arial");


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
            if (personTimer >= 5)
            {
                people.Add(pm.makePerson(graphics));
                personTimer = 0;
            }

            foreach (Person p in people)
            {
                p.Update(gameTime);
            }

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
                spriteBatch.Draw(p.texture, p.position, Color.White);
            {
                // TODO: Add your drawing code here
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
