using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong
{
    class Paddle
    {
        public Texture2D sprite;
        public Vector2 position;

        private Ball ball;
        private GraphicsDevice graphics;
        public 
        Paddle(Texture2D sprite, Ball ball, GraphicsDevice graphics, bool isLeft)
        {
            this.sprite = sprite;
            this.ball = ball;
            this.graphics = graphics;
            if (isLeft)
            {
                position.X = graphics.Viewport.Width * 0.33f ;
                position.Y = graphics.Viewport.Height / 2f;
            }
            else
            {
                position.X = graphics.Viewport.Width * 0.66f;//TODO may need to change
                position.Y = graphics.Viewport.Height / 2f;

            }
        }


        public void Update(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Up))
            {
                position.Y += 5;
            }

            if (keyboard.IsKeyDown(Keys.Down))
            {
                position.Y -= 5;
            }

            if (position.Y >= graphics.Viewport.Height)//keep in bounds
            {
                position.Y = graphics.Viewport.Height;
            }
            else if (position.Y <= 0f)
            {
                position.Y = 0f;
            }

            if (ball.position.X - position.X <= 7f && ball.position.Y - position.X <= 7f)//range to accept hit
            {
                ball.hitBy = this;
                ball.isHit = true;
            }

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position);

        }
        











    }
}
