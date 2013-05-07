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
using Coding4Fun.Kinect.KinectService.Common;
using Coding4Fun.Kinect.KinectService.WpfClient;
using KinectTracking;
//using Microsoft.Kinect;


namespace XNA_GameManager
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameManager : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        SkeletonClient skeletonClient;
        Skeleton player =null;

        enum ManagerState { MENU, GAME }
        ManagerState State = ManagerState.MENU;

        int loadedGame;
        List<string> gamelist;

        // temporary
        Texture2D dot;
        //????


        Button button1, button2, Quit, endgame;
  
        
        SpriteFont buttonFont;
        int socket = 4530;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content/Manager";
            gamelist = new List<string>();
            loadedGame = -1;
            

            skeletonClient = new SkeletonClient();
            skeletonClient.SkeletonFrameReady += client_SkeletonFrameReady;
            skeletonClient.Connect("127.0.0.1", socket);
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

            //gamelist.Add("C:/Users/Jonatan/Documents/Visual Studio 2010/Projects/WindowsGame2/WindowsGame2/WindowsGame2/bin/x86/Debug/WindowsGame2.exe");
            //gamelist.Add("C:/Users/Jonatan/Documents/Visual Studio 2010/Projects/KinectGame/KinectGame/KinectGame/bin/x86/Debug/KinectGame.exe");
            gamelist.Add("notepad.exe");
            gamelist.Add("notepad.exe");


            buttonFont = Content.Load<SpriteFont>("buttonText");


            //games.Add(new Game1(Content));
            button1 = new Button(GraphicsDevice, new Rectangle(100, 10, 300, 100), Color.DarkGreen, Color.Green, buttonFont, "NOTEPAD");
            button2 = new Button(GraphicsDevice, new Rectangle(100, 120, 300, 100), Color.Blue, Color.SkyBlue, buttonFont, "NOTEPAD");
            Quit = new Button(GraphicsDevice, new Rectangle(100, 230, 300, 100), Color.Crimson, Color.OrangeRed, buttonFont, "QUIT");
            endgame = new Button(GraphicsDevice, new Rectangle(100, 10, 300, 100), Color.Crimson, Color.OrangeRed, buttonFont, "ENDGAME");
            
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
            switch (State)
            { // Transition
                case ManagerState.MENU:
                    MouseState mouse = Mouse.GetState();
                    button1.Update(mouse);
                    button2.Update(mouse);
                    Quit.Update(mouse);
                    if (button1.fallingEdge)
                    {
                        //State = ManagerState.GAME;

                        loadedGame = 0;
                        loadGame(gamelist[loadedGame]);
                    }
                    else if (button2.fallingEdge)
                    {

                        //IsMouseVisible = false; ;
                       // State = ManagerState.GAME;
                        loadedGame = 1;
                        loadGame(gamelist[loadedGame]);
                    }
                    else if (Quit.fallingEdge)
                    {
                        this.Exit();
                    }

                    break;
                case ManagerState.GAME:

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
                    if (player != null)
                    {
                        foreach (Joint j in player.Joints)
                        {
                            Vector2 position = new Vector2((((0.5f * j.Position.X) + 0.5f) * (graphics.PreferredBackBufferWidth)), (((-0.5f * j.Position.Y) + 0.5f) * (graphics.PreferredBackBufferWidth)));
                            spriteBatch.Draw(dot, position , Color.White);
                        }
                    }
                    spriteBatch.End();
                    break;
                 
                default:
                    break;

            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void loadGame(string command)
        {
            execute(command);
        }

        private void execute(string command, string args = "")
        {
            try {
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
        
                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                Console.WriteLine(result);
            }
            catch (Exception objException)
            {
                // Log the exception
            }
        }
        
        void client_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton skeleton = (from s in e.SkeletonFrame.Skeletons
                                 where s.TrackingState == SkeletonTrackingState.Tracked
                                 select s).FirstOrDefault();

            if (skeleton == null)
                return;
            player = skeleton;
        }
    }
}
