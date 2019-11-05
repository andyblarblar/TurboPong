using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Paddle paddle1;
        private Paddle paddle2;
        private Ball ball;
        private ScoreBoard scoreBoard;
        public int volocity = 2;
        public bool isAI = false;
        
        public Game1(string[] args)
        {
            graphics = new GraphicsDeviceManager(this);

            try
            {
                graphics.PreferredBackBufferWidth = int.Parse(args[0]);
                graphics.PreferredBackBufferHeight = int.Parse(args[1]);
                if (args.Contains("ai")) isAI = true;
                volocity = int.Parse(args[args.Length - 1]);

            }

            catch (Exception)
            {
                Console.WriteLine("usage:\n pong.exe [Width Highth] [ai] [velocity]");
                graphics.PreferredBackBufferWidth = 700;
                graphics.PreferredBackBufferHeight = 500;
                
            }

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
            GraphicsDevice.Viewport = new Viewport(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            var paddleTexture = new Texture2D(GraphicsDevice,30,70);

            Color[] data = new Color[30 * 70];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            paddleTexture.SetData(data);

            var ballTexture = new Texture2D(GraphicsDevice,10,10);

            Color[] data2 = new Color[10 * 10];
            for (int i = 0; i < data2.Length; ++i) data2[i] = Color.White;
            ballTexture.SetData(data2);


            ball = new Ball(ballTexture, this.GraphicsDevice, volocity);
            paddle1 = isAI ? new AiPaddle(paddleTexture, ball, this.GraphicsDevice, true) : new Paddle(paddleTexture, ball, this.GraphicsDevice, true);
                
            paddle2 = isAI ? new AiPaddle(paddleTexture, ball, this.GraphicsDevice, false) : new Paddle(paddleTexture, ball, this.GraphicsDevice, false);

            scoreBoard = new ScoreBoard(GraphicsDevice, this, ball);

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
           
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || Keyboard.GetState().IsKeyDown(
                    Keys.Escape))
                Exit();

            paddle1.Update(Keyboard.GetState());
            paddle2.Update(Keyboard.GetState());
            ball.Update();
           
            scoreBoard.Update();



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            ball.Draw(spriteBatch);
            paddle1.Draw(spriteBatch);
            paddle2.Draw(spriteBatch);
            scoreBoard.Draw(spriteBatch);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
