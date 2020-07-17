using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrafuAlgoritmai
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public enum Screen { PPLOT, PMJM, KMJM, Main };
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Main main;
        PPLOTmedis pplotmedis;
        KruskalioMJM kruskaliomjm;
        PrimoMJM primomjm;

        
        Screen screen;

        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            screen = new Screen();
            screen = Screen.Main;


            main = new Main(screen,Window);
            pplotmedis = new PPLOTmedis(screen);
            kruskaliomjm = new KruskalioMJM(screen);
            primomjm = new PrimoMJM(screen);

            IsMouseVisible = true;
            
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

            main.Initialize(spriteBatch, GraphicsDevice,Content);
            pplotmedis.Initialize(spriteBatch, GraphicsDevice, Content);
            kruskaliomjm.Initialize(spriteBatch, GraphicsDevice, Content);
            primomjm.Initialize(spriteBatch, GraphicsDevice, Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            switch(screen)
            {
                case Screen.Main:
                    main.Update(out screen,screen);
                    break;
                case Screen.PPLOT:
                    pplotmedis.Update(out screen, screen);
                    break;
                case Screen.KMJM:
                    kruskaliomjm.Update(out screen, screen);
                    break;
                case Screen.PMJM:
                    primomjm.Update(out screen,screen);
                    break;

            }
            
           
            
           

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            switch (screen)
            {
                case Screen.Main:
                    main.Draw();
                    break;
                case Screen.PPLOT:
                    pplotmedis.Draw();
                    break;
                case Screen.KMJM:
                    kruskaliomjm.Draw();
                    break;
                case Screen.PMJM:
                    primomjm.Draw();
                    break;

            }
            
            base.Draw(gameTime);
        }
    }
}
