using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNA_GameManager
{

    /// <summary>
    /// Button Class
    ///     Used as a clickable button using a Rectangle and text
    ///     Button can be checked for risingEdge which will return true when the button transitions from not cliced to clicked
    ///     as well as falling edge for transitioning from clicked to not clicked
    /// </summary>
    class Button
    {
        // Contains the position and dimmensions of the Button
        Rectangle button;

        // normal   - Color of the button when it is not in use
        // hover    - Color of the button when the cursor is over it
        //          NOTE: It should be possible to use normal Color and increase the values a percentage  to give it a Highlited look
        // All buttons turn Grey when the are clicked on
        Color normal, hover;
        
        // GD - required for creating the texture that the button uses
        GraphicsDevice GD;

        
        // Position the Draw Function uses to write the text
        Vector2 message_pos;

        // The string contained in the Button
        // the string can be larger than the Button but the text does not shrink and spill outside the button
        public string message;

        // Font that the message is written in
        SpriteFont font;

        // Button states
        // hovered      - Cursor is within the Button
        // clicked      - Cursor is currently clicking on the Button
        // prev_clicked - Cursor clicked on the button one state previously
        public bool hovered,clicked, prev_clicked;

        // returns true if the button transitioned from not clicked to clicked
        public bool risingEdge 
        { 
            get 
            { 
                return (!prev_clicked) && (clicked) && (hovered); 
            } 
        }


        // returns true if the button transitioned from clicked to not clicked
        public bool fallingEdge 
        { 
            get 
            { 
                return (prev_clicked) && (!clicked) && (hovered); 
            } 
        } 

        // constructor
        public Button( GraphicsDevice GD, Rectangle rect, Color inactive, Color hovered, SpriteFont font, string message = "")
        {

            this.font = font;
            this.message = (string)message;

            //  if no message
            if (message != "") 
            {

                // Find the dimensions of the message
                Vector2 message_size = font.MeasureString(message);

                // get the positon offset to center the message in the button
                message_pos = new Vector2 (rect.Center.X, rect.Center.Y) - .5f*message_size ;
            }
            button = rect;
            normal = inactive;
            hover = hovered;
            this.GD = GD;
        }

        public void Update(MouseState mouse)
        {

            // store previous state
            prev_clicked = clicked;

            
            clicked = (mouse.LeftButton == ButtonState.Pressed);

            // if mouse is inside the Button's boudaries
            if (mouse.X > button.X && mouse.X < button.X + button.Width &&
               mouse.Y > button.Y && mouse.Y < button.Y + button.Height)
            {
                hovered = true;
            }
            else hovered = false;

        }


        public void Draw(SpriteBatch spriteBatch)
        {

            // normal color
            Color color = normal;
            if (clicked && hovered)
            {
                color = Color.Gray;
            }
            else if (hovered)
            {
                color = hover;
            }
            
            // Create texture based on the color of the button
            var rect = new Texture2D(GD, 1, 1);
            rect.SetData(new[] { color });


            spriteBatch.Begin();
            spriteBatch.Draw(rect, button, color);

            // if there is a message
            if (message.Length != 0)
            {
                spriteBatch.DrawString(font, message, message_pos, Color.White);
            }

            spriteBatch.End();
        }
        
    }
}
