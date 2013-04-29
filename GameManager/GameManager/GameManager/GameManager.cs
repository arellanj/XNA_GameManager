using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using KinectTracking;
using Microsoft.Kinect;


namespace XNA_GameManager
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameManager : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        enum ManagerState { MENU, GAME }
        ManagerState State = ManagerState.MENU;

        // temporary
        Kinect kinect;
        Texture2D dot;
        //????


        List<GameBase> games;
        Button button1, button2, Quit, endgame;
        int loadedGame;
        
        SpriteFont buttonFont;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content/Manager";
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            IsMouseVisible = true;

            Rectangle window = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);


            games = new List<GameBase>();
            loadedGame = -1;

            Game1 game1 = new Game1(Content, window );
            games.Add((GameBase)game1);

            //Game2 game2 = new Game2(Content, window);
            //games.Add((GameBase)game2);

            Tetris tetris = new Tetris(Content, window);
            games.Add((GameBase)tetris);


            buttonFont = Content.Load<SpriteFont>("buttonText");


            //games.Add(new Game1(Content));
            button1 = new Button(GraphicsDevice, new Rectangle(100, 10, 300, 100), Color.DarkGreen, Color.Green, buttonFont, "GAME 1");
            button2 = new Button(GraphicsDevice, new Rectangle(100, 120, 300, 100), Color.Blue, Color.SkyBlue, buttonFont, "Tetris");
            Quit = new Button(GraphicsDevice, new Rectangle(100, 230, 300, 100), Color.Crimson, Color.OrangeRed, buttonFont, "QUIT");
            endgame = new Button(GraphicsDevice, new Rectangle(100, 10, 300, 100), Color.Crimson, Color.OrangeRed, buttonFont, "ENDGAME");

            kinect = new Kinect();
            kinect.initialize();

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

            dot = Content.Load<Texture2D>("dot");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            switch(State){ // Transition
                case ManagerState.MENU:
                    MouseState mouse = Mouse.GetState();
                    button1.Update(mouse);
                    button2.Update(mouse);
                    Quit.Update(mouse);
                    if (button1.fallingEdge)
                    {
                        State = ManagerState.GAME;

                        loadedGame = 0;
                        games[loadedGame].Initialize();
                    }
                    else if (button2.fallingEdge)
                    {

                        IsMouseVisible = false; ;
                        State = ManagerState.GAME;
                        loadedGame = 1;
                        games[loadedGame].Initialize();
                    }
                    else if (Quit.fallingEdge)
                    {
                        this.Exit();
                    }

                    break;
                case ManagerState.GAME:
                    
                    if(games[loadedGame].Terminated)
                    {
                        games[loadedGame].Unload();
                        //kinect.start();
                        IsMouseVisible = true;
                        State = ManagerState.MENU;
                        loadedGame = -1;
                    }

                    break;
                default:
                    break;

            }
            switch (State) // State Actions
            {
                case ManagerState.MENU:
                    break;
                case ManagerState.GAME:
                    //kinect.pause();
                    games[loadedGame].Update(gameTime);
                    break;
                default:
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
           
            GraphicsDevice.Clear(Color.White);
            
            switch (State) // State Actions
            {
                case ManagerState.MENU:

                    button1.Draw(spriteBatch);
                    button2.Draw(spriteBatch);
                    Quit.Draw(spriteBatch);

                    spriteBatch.Begin();
                    if (kinect.player != null)
                    {
                        foreach (Joint j in kinect.player.Joints)
                        {
                            Vector2 position = new Vector2((((0.5f * j.Position.X) + 0.5f) * (graphics.PreferredBackBufferWidth)), (((-0.5f * j.Position.Y) + 0.5f) * (graphics.PreferredBackBufferWidth)));
                            spriteBatch.Draw(dot, position , Color.White);
                        }
                    }
                    spriteBatch.End();
                    break;
                case ManagerState.GAME:
                    games[loadedGame].Draw(spriteBatch);

                 

            
                    break;
                default:
                    break;

            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
