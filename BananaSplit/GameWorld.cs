using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace BananaSplit
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    public class GameWorld : Game
    {
        public List<GameObject> gameObjects = new List<GameObject>();
        public List<GameObject> gameObjectsToAdd = new List<GameObject>();
        public List<GameObject> gameObjectsToRemove = new List<GameObject>();
        public GameState gameState = new GameState();

        public static GameWorld Instance;
        Song song;

        private Texture2D background;
        private Texture2D heartFull;
        private Texture2D bananaPoints;
        private Texture2D timerImage;

        private Texture2D gameLogo;
        public int bananaCounter;
        public int health;
        private SpriteFont text;

        public bool level = true;

        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public float timer = 1000f;   // Milliseconds


        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Instance = this;
            bananaCounter = 0;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        /// 
        public static Platform pl;

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            gameObjects.Add(new Player());

            //gameObjects.Add(new Platform(pl.position));

            //Platform



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

            background = Content.Load<Texture2D>("background");
            heartFull = Content.Load<Texture2D>("heartfull");
            bananaPoints = Content.Load<Texture2D>("smallBanana");
            text = Content.Load<SpriteFont>("gameOver");
            song = Content.Load<Song>("By the Fire");
            gameLogo = Content.Load<Texture2D>("bananasplit_logo");
            timerImage = Content.Load<Texture2D>("clock");


            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.IsRepeating = true;

            #region Spawn platforms, enemys, loot
            gameObjects.Add(new Platform(new Vector2(-295, 1000)));
            gameObjects.Add(new Platform(new Vector2(-295, 900)));
            gameObjects.Add(new Platform(new Vector2(-295, 800)));
            gameObjects.Add(new Platform(new Vector2(-295, 700)));
            gameObjects.Add(new Platform(new Vector2(-295, 600)));
            gameObjects.Add(new Platform(new Vector2(-295, 500)));

            gameObjects.Add(new Platform(new Vector2(0, 1000)));
            gameObjects.Add(new Platform(new Vector2(295, 1000)));
            gameObjects.Add(new Platform(new Vector2(590, 1000)));
            gameObjects.Add(new Platform(new Vector2(885, 1000)));
            gameObjects.Add(new Platform(new Vector2(1180, 1000)));
            gameObjects.Add(new Platform(new Vector2(1475, 1000)));

            gameObjects.Add(new Platform(new Vector2(1800, 400)));
            gameObjects.Add(new Platform(new Vector2(1770, 900)));
            gameObjects.Add(new Platform(new Vector2(2065, 800)));
            gameObjects.Add(new Platform(new Vector2(2360, 750)));
            gameObjects.Add(new Platform(new Vector2(2655, 700)));
            gameObjects.Add(new Platform(new Vector2(2950, 800)));
            gameObjects.Add(new Platform(new Vector2(3245, 900)));

            gameObjects.Add(new Platform(new Vector2(3800, 900)));
            gameObjects.Add(new Platform(new Vector2(4400, 900)));
            gameObjects.Add(new Platform(new Vector2(4700, 800)));
            gameObjects.Add(new Platform(new Vector2(5000, 600)));
            gameObjects.Add(new Platform(new Vector2(5300, 400)));

            gameObjects.Add(new Platform(new Vector2(6500, 900)));
            gameObjects.Add(new Platform(new Vector2(6700, 900)));
            gameObjects.Add(new Platform(new Vector2(6900, 900)));
            gameObjects.Add(new Platform(new Vector2(7500, 700)));
            gameObjects.Add(new Platform(new Vector2(8200, 1000)));
            gameObjects.Add(new Platform(new Vector2(8600, 900)));
            gameObjects.Add(new Platform(new Vector2(8600, 700)));
            gameObjects.Add(new Platform(new Vector2(8600, 500)));
            gameObjects.Add(new Platform(new Vector2(8600, 300)));
            gameObjects.Add(new Platform(new Vector2(8600, 100)));

            gameObjects.Add(new Platform(new Vector2(10900, 900)));
            gameObjects.Add(new Platform(new Vector2(11000, 900)));
            gameObjects.Add(new Platform(new Vector2(11500, 800)));
            gameObjects.Add(new Platform(new Vector2(11900, 750)));
            gameObjects.Add(new Platform(new Vector2(12200, 700)));

            gameObjects.Add(new Platform(new Vector2(13200, 1000)));
            gameObjects.Add(new Platform(new Vector2(13500, 900)));

            gameObjects.Add(new Platform(new Vector2(13800, 800)));
            gameObjects.Add(new Platform(new Vector2(14100, 800)));
            gameObjects.Add(new Platform(new Vector2(14400, 800)));
            gameObjects.Add(new Platform(new Vector2(14700, 800)));
            gameObjects.Add(new Platform(new Vector2(15000, 800)));
            gameObjects.Add(new Platform(new Vector2(15300, 800)));
            gameObjects.Add(new Platform(new Vector2(15600, 800)));


            //Enemy
            gameObjects.Add(new Enemy(new Vector2(2000, 600)));
            gameObjects.Add(new Enemy(new Vector2(2365, 500)));

            gameObjects.Add(new Enemy(new Vector2(4700, 500)));
            gameObjects.Add(new Enemy(new Vector2(5300, 100)));

            gameObjects.Add(new Enemy(new Vector2(8200, 600)));
            gameObjects.Add(new Enemy(new Vector2(8400, 500)));

            gameObjects.Add(new Enemy(new Vector2(11500, 500)));

            gameObjects.Add(new Enemy(new Vector2(15600, 0)));
            gameObjects.Add(new Enemy(new Vector2(15600, 100)));
            gameObjects.Add(new Enemy(new Vector2(15600, 200)));
            gameObjects.Add(new Enemy(new Vector2(15600, 300)));
            gameObjects.Add(new Enemy(new Vector2(15600, 400)));
            gameObjects.Add(new Enemy(new Vector2(15600, 500)));
            gameObjects.Add(new Enemy(new Vector2(15600, 600)));
            gameObjects.Add(new Enemy(new Vector2(15600, 700)));
            gameObjects.Add(new Enemy(new Vector2(15600, 800)));


            //gameObjects.Add(new Enemy(new Vector2(1050, 750)));
            //gameObjects.Add(new Enemy(new Vector2(1400, 750)));


            //Loot
            gameObjects.Add(new Loot(new Vector2(2065, 720)));
            gameObjects.Add(new Loot(new Vector2(2700, 150)));

            gameObjects.Add(new Loot(new Vector2(5850, 200)));
            gameObjects.Add(new Loot(new Vector2(6100, 500)));

            gameObjects.Add(new Loot(new Vector2(6900, 720)));
            gameObjects.Add(new Loot(new Vector2(7500, 520)));

            gameObjects.Add(new Loot(new Vector2(9000, 25)));
            gameObjects.Add(new Loot(new Vector2(9150, 100)));
            gameObjects.Add(new Loot(new Vector2(9300, 150)));
            gameObjects.Add(new Loot(new Vector2(9450, 200)));
            gameObjects.Add(new Loot(new Vector2(9600, 250)));
            gameObjects.Add(new Loot(new Vector2(9750, 300)));
            gameObjects.Add(new Loot(new Vector2(9900, 350)));
            gameObjects.Add(new Loot(new Vector2(10050, 400)));
            gameObjects.Add(new Loot(new Vector2(10200, 450)));
            gameObjects.Add(new Loot(new Vector2(10350, 500)));
            gameObjects.Add(new Loot(new Vector2(10500, 550)));
            gameObjects.Add(new Loot(new Vector2(10650, 600)));
            gameObjects.Add(new Loot(new Vector2(10800, 650)));

            gameObjects.Add(new Loot(new Vector2(14000, 720)));
            gameObjects.Add(new Loot(new Vector2(14256, 562)));
            gameObjects.Add(new Loot(new Vector2(14648, 582)));
            gameObjects.Add(new Loot(new Vector2(15046, 456)));
            gameObjects.Add(new Loot(new Vector2(15206, 555)));
            gameObjects.Add(new Loot(new Vector2(14384, 685)));
            gameObjects.Add(new Loot(new Vector2(14956, 322)));
            gameObjects.Add(new Loot(new Vector2(15345, 645)));
            gameObjects.Add(new Loot(new Vector2(15100, 370)));
            gameObjects.Add(new Loot(new Vector2(15622, 460)));

            //Boost
            gameObjects.Add(new Boost(new Vector2(1500, 200)));

            gameObjects.Add(new Boost(new Vector2(7800, 100)));

            gameObjects.Add(new Boost(new Vector2(16200, 500)));
            gameObjects.Add(new Boost(new Vector2(16400, 700)));
            gameObjects.Add(new Boost(new Vector2(16600, 900)));

            // Level 2 TO COME


            #endregion Spawn platforms, enemys, loot


            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }

        }


        public void MoveAll(Vector2 velocity)
        {
            foreach (GameObject go in gameObjects)
            {
                if (!(go is Player) && !(go is Projectile))
                {
                    go.position += velocity * 15;
                }
            }
        }




        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            // Countdown timer

            if (gameState == GameState.StartGame)
            {
                timer -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (timer <= 0)
            {
                timer = 0;

            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
            }
            foreach (GameObject gameObject in gameObjects)
            {
                foreach (GameObject gameObject1 in gameObjects)
                {
                    if (gameObject == gameObject1)
                        continue;

                    gameObject.CheckCollision(gameObject1);
                }
            }
            base.Update(gameTime);
            gameObjects.AddRange(gameObjectsToAdd);
            gameObjects.RemoveAll(go => gameObjectsToRemove.Contains(go));
            gameObjectsToRemove.Clear();



            gameObjectsToAdd.Clear();
        }





        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {



            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            spriteBatch.Draw(background, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);


            if (!(gameState == GameState.StartScreen))
            {
                if (health >= 10 || health >= 20 || health >= 30)
                {
                    spriteBatch.Draw(heartFull, new Vector2(10, 15), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
                if (health >= 20 || health >= 30)
                {
                    spriteBatch.Draw(heartFull, new Vector2(50, 15), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
                if (health >= 30)
                {
                    spriteBatch.Draw(heartFull, new Vector2(90, 15), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

                }
                // Timer
                spriteBatch.Draw(timerImage, new Vector2(10, 130), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.1f);

                if (timer < 4000)
                {

                    spriteBatch.DrawString(text, ": " + string.Format("{0:0:00:000}", timer), new Vector2(67, 130), Color.Yellow, 0, Vector2.Zero, 1, SpriteEffects.None, 1f);

                }
                else
                    spriteBatch.DrawString(text, ": " + string.Format("{0:0:00:000}", timer), new Vector2(67, 130), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1f);

                spriteBatch.Draw(bananaPoints, new Vector2(10, 75), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.1f);
                spriteBatch.DrawString(text, ": " + bananaCounter, new Vector2(65, 65), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.1f);
            }


            if (health <= 0 || Player.PlayerPosition.Y >= 1100 || timer <= 0)
            {
                gameState = GameState.GameOver;
            }
            if (bananaCounter >= 40000 && health > 0)
            {
                gameState = GameState.WonLevel;

            }

            switch (gameState)
            {
                case GameState.StartScreen:
                    //timer = 0;
                    spriteBatch.Draw(gameLogo,
                                   new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 200, graphics.GraphicsDevice.Viewport.Height / 2 - 450),
                                   null,
                                   Color.White,
                                   0,
                                   Vector2.Zero,
                                   1.5f,
                                   SpriteEffects.None,
                                   1f);
                    spriteBatch.DrawString(text,
                                     "VELCOME TO BANANASPLIT! \n" +
                                     "Collect 40,000 bananas in the first level, and do it\n as fast as possible!!\n\n" +
                                     "If you collect dadles you get 4 x bananas!\n" +
                                     "How it works:\n\n" +
                                     "Press W to jump\n" +
                                     "Press A to move left\n" +
                                     "Press D to move Right\n\n" +
                                     "PRESS ENTER TO START\n\n\n" +
                                     "GL HF!"
                                     ,
                                     new Vector2(150, graphics.GraphicsDevice.Viewport.Height / 4),
                                     Color.White,
                                     0,
                                     Vector2.Zero,
                                     1,
                                     SpriteEffects.None,
                                     0.8f);
                    spriteBatch.Draw(background, new Vector2(0, 0), null, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);


                    break;

                case GameState.WonLevel:
                    if (bananaCounter >= 40000)
                    {
                        gameState = GameState.Level1;
                        bananaCounter = 40000;
                    }
                    else
                        gameState = GameState.Level2;


                    spriteBatch.DrawString(text,
                                     "Congrats! You made it to level 2! \n In England they discard around 1.500.000 bananas EVERY day!\n Collect all 1.500.000 to win the game \n and to produce {x_amount} of ice cream!"
                                     ,
                                     new Vector2(150, graphics.GraphicsDevice.Viewport.Height / 2),
                                     Color.White,
                                     0,
                                     Vector2.Zero,
                                     1,
                                     SpriteEffects.None,
                                     1f);
                    spriteBatch.Draw(background, new Vector2(0, 0), null, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);


                    break;
                case GameState.GameOver:

                    MediaPlayer.Volume -= 0.1f;
                    int bananasNeeded = 40000 - bananaCounter;

                    spriteBatch.DrawString(text,
                                               "You only needed " + string.Format("{0:0,0}", bananasNeeded) + " more bananas, to remove banana-food-waste for today.\n See you again tomorrow for 40,000 more \n BUT you gathered enough bananas to produce {x_amount} of ice cream \n\n PRESS ENTER TO PLAY AGAIN",
                                               new Vector2(150, graphics.GraphicsDevice.Viewport.Height / 2),
                                               Color.White,
                                               0,
                                               Vector2.Zero,
                                               1,
                                               SpriteEffects.None,
                                               1f);


                    spriteBatch.Draw(background, new Vector2(0, 0), null, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);


                    break;
            }






            // GameoverTxT
            //if (health <= 0 || Player.PlayerPosition.Y >= 1100 || timer <= 0)
            //{
            //    MediaPlayer.Volume -= 0.1f;

            //    spriteBatch.DrawString(text,
            //                           "You only needed " + string.Format("{0:0,0}", bananasNeeded) + " more bananas, to remove banana-food-waste for today.\n See you again tomorrow for 40,000 more \n BUT you gathered enough bananas to produce {x_amount} of ice cream \n\n PRESS ENTER TO PLAY AGAIN",
            //                           new Vector2(150, graphics.GraphicsDevice.Viewport.Height / 2),
            //                           Color.White,
            //                           0,
            //                           Vector2.Zero,
            //                           1,
            //                           SpriteEffects.None,
            //                           1f);


            //    spriteBatch.Draw(background, new Vector2(0, 0), null, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);

            //}
            //if (bananaCounter >= 40000)
            //{
            //    bananaCounter = 40000;
            //}

            //if (bananaCounter == 40000 && health > 0)
            //{

            //    spriteBatch.DrawString(text,
            //                     "Congrats! You made it to level 2! \n In England they discard around 1.500.000 bananas EVERY day!\n Collect all 1.500.000 to win the game \n and to produce {x_amount} of ice cream!"
            //                     ,
            //                     new Vector2(150, graphics.GraphicsDevice.Viewport.Height / 2),
            //                     Color.White,
            //                     0,
            //                     Vector2.Zero,
            //                     1,
            //                     SpriteEffects.None,
            //                     1f);
            //    spriteBatch.Draw(background, new Vector2(0, 0), null, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);

            //}


            if (gameState == GameState.StartGame)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Draw(spriteBatch);
                }
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
