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
    class Game2 : GameBase
    {

        Texture2D cursor;
        Vector2 pos;

        public Game2(ContentManager content, Rectangle window)
            : base(content, window)
        {
            Content.RootDirectory = "Content/Game2";
        }

        public override void Initialize()
        {
            cursor = Content.Load<Texture2D>("mouse");
        }
        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            pos = new Vector2(mouse.X, mouse.Y);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch SB)
        {
            SB.Begin();
            SB.Draw(cursor, pos, Color.White);
            SB.End();
        }

    }
}
