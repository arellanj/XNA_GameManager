using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace XNA_GameManager
{
    class Game1 : GameBase
    {

        Texture2D ball;
        Vector2 pos, D;

        public Game1( ContentManager content, Rectangle window):base(content, window)
        {
            Content.RootDirectory = "Content/Game1";
        }

        
        public override void Initialize()
        {
            ball = Content.Load<Texture2D>("ball");
            pos = new Vector2(100, 100);
            Random r = new Random(1);
            D = new Vector2(r.Next(2, 3), r.Next(1, 3));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            pos += D * gameTime.ElapsedGameTime.Milliseconds;

            if (pos.X < 0)
            {
                D.X = -D.X;
                pos.X = 0;
            }
            else if (pos.X + ball.Width > window.Width)
            {
                D.X = -D.X;
                pos.X = window.Width - ball.Width;
            }
            if (pos.Y < 0)
            {
                D.Y = -D.Y;
                pos.Y = 0;
            }
            else if (pos.Y + ball.Height > window.Height)
            {
                D.Y = -D.Y;
                pos.Y = window.Height - ball.Height;
            }
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape)) this.Exit();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch SB)
        {
            SB.Begin();
            SB.Draw(ball, pos, Color.White);
            SB.End();
        }

    }
}
