﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using KinectTracking;

namespace XNA_GameManager
{
    class GameBase
    {
        // This is the Class your games will be based on to allow easier management of multiple games
        // 

        public Kinect kinect;
        public ContentManager Content;
        public Rectangle window;

        public GameBase( ContentManager content, Rectangle window ) {
            this.window = window;
            Content = new ContentManager(content.ServiceProvider);
            kinect = new Kinect();
            kinect.initialize();
        }

        public virtual void Initialize() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch SB) { }
        
        public  virtual void Unload() { }
    }
}
