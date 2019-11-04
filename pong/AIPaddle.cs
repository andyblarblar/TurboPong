using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong
{
    /// <summary>
    /// A paddle controlled by a deterministic ai
    /// </summary>
    public class AiPaddle : Paddle
    {
        private float? nextBallPosition = null;


        /// <inheritdoc />
        public AiPaddle(Texture2D sprite, Ball ball, GraphicsDevice graphics, bool isLeft) : base(sprite, ball, graphics, isLeft)
        {
            
        }


        public override void Update(KeyboardState keyboard)
        {
            if (nextBallPosition.Equals(null))
            {
                nextBallPosition = FindBallPositionR(ball.position, position, ball.rate);//called now to make sure game is actually running
            }

            if (ball.isHit || isLeft && ball.isInGoal)
            {
                if (ball.hitBy == this)
                {
                    Console.WriteLine("in");
                    goto outOfMov;//do nothing if ball is heading away from paddle
                }
                Console.WriteLine("in2 paddle:" + isLeft);
                nextBallPosition = FindBallPositionR(ball.position, position, ball.rate);//find where to move to if hit by other paddle

            }

            #region movement logic

            if (nextBallPosition > position.Y)//if need to move up
            {
                if (Math.Abs(nextBallPosition.Value - position.Y) < 10)//to avoid overshooting
                {
                    position.Y++;
                    goto outOfMov;
                }

                position.Y += 10;
            }

            else if (nextBallPosition < position.Y)//if need to move down
            {
                if (Math.Abs(nextBallPosition.Value - position.Y) < 10)
                {
                    position.Y--;
                    goto outOfMov;
                }

                position.Y -= 10;
            }

            outOfMov:
            #endregion

            if (position.Y >= graphics.Viewport.Height - 60)//keep in bounds
            {
                position.Y = graphics.Viewport.Height - 70;
            }

            else if (position.Y <= 0f)
            {
                position.Y = 0f;
            }

            if (this.hitBox.Intersects(ball.hitBox))
            {
                ball.hitBy = this;
                ball.isHit = true;
            }

            
            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;
        }

        /// <summary>
        /// recursively finds where the ball will be after being hit
        /// </summary>
        private float FindBallPositionR(Vector2 ballPos, Vector2 paddlePos, Vector2 ballRate)
        {
            var newPos = new Vector2();

            if (Math.Abs(ballPos.X - ballPos.X) < 5)//break condition
            {
                return ballPos.Y;
            }


            newPos = ballPos + ballRate; //move

            if (position.Y >= graphics.Viewport.Height || Math.Abs(position.Y) < 20f)//on hitting ceiling
            {

                ballRate.Y *= -1;//invert slope

            }

            return FindBallPositionR(newPos, paddlePos, ballRate);

        }

        
    }




}