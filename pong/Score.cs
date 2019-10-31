using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace pong
{
    class Score
    {
        private SpriteFont font;
        private Vector2 position;
        public int currentScore = 0;
        private GraphicsDevice graphics; 
        private Game gameContext;

        public Score(Vector2 position, GraphicsDevice graphics,Game gameContext)
        {
            this.position = position;
            this.graphics = graphics;
            this.gameContext = gameContext;

            font = gameContext.Content.Load<SpriteFont>("ScoreFont");
        }


        public void Update()
        {
            


        }



        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(font, currentScore.ToString(), position, Color.White);


        }












    }
}
