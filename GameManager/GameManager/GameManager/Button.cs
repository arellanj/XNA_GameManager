using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNA_GameManager
{
    class Button
    {
        Rectangle button;
        Color normal, hover;
        GraphicsDevice GD;

        Vector2 message_pos;
        public string message;
        SpriteFont font;

        public bool hovered,clicked, prev_clicked;

        public Button()
        {
        }
        public Button( GraphicsDevice GD, Rectangle rect, Color inactive, Color hovered, SpriteFont font, string message = "")
        {
            this.font = font;
            this.message = (string)message;
            if (message != "")
            {
                Vector2 message_size = font.MeasureString(message);
                message_pos = new Vector2 (rect.Center.X, rect.Center.Y) - .5f*message_size ;
            }
            button = rect;
            normal = inactive;
            hover = hovered;
            this.GD = GD;
        }

        public void Update(MouseState mouse)
        {

            prev_clicked = clicked;
            clicked = (mouse.LeftButton == ButtonState.Pressed);
            if (mouse.X > button.X && mouse.X < button.X + button.Width &&
               mouse.Y > button.Y && mouse.Y < button.Y + button.Height)
            {
                hovered = true;
            }
            else hovered = false;

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Color color = normal;
            if (clicked && hovered)
                color = Color.Gray;
            else if (hovered)
                color = hover;
            
            var rect = new Texture2D(GD, 1, 1);
            rect.SetData(new[] { color });
            spriteBatch.Begin();
            spriteBatch.Draw(rect, button, color);
            if (message.Length != 0)
            {
                spriteBatch.DrawString(font, message, message_pos, Color.White);
            }
            spriteBatch.End();
        }
        
        public bool risingEdge { get { return ( !prev_clicked ) && (clicked) && (hovered); } }

        public bool fallingEdge { get { return ( prev_clicked ) && (clicked) && (hovered); } } 
    }
}
