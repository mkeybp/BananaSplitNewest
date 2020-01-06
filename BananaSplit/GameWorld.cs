﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BananaSplit
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        public List<GameObject> gameObjects = new List<GameObject>();
        public List<GameObject> gameObjectsToAdd = new List<GameObject>();
        public List<GameObject> gameObjectsToRemove = new List<GameObject>();

        public static GameWorld Instance;
        Song song;

        private Texture2D background;
        private Texture2D heartFull;
        private Texture2D bananaPoints;
        public int bananaCounter;
        public int health;
        private SpriteFont text;



        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


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

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            gameObjects = new List<GameObject>();
            gameObjects.Add(new Player());
            //gameObjects.Add(new Enemy());
            //gameObjects.Add(new Platform());
            //gameObjects.Add(new Loot());
            //gameObjects.Add(new Boost());

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

            //test = Content.Load<Texture2D>("test");
            background = Content.Load<Texture2D>("background");
            heartFull = Content.Load<Texture2D>("heartfull");
            bananaPoints = Content.Load<Texture2D>("smallBanana");
            text = Content.Load<SpriteFont>("gameOver");
            song = Content.Load<Song>("By the Fire");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            //Platform
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

            gameObjects.Add(new Platform(new Vector2(1770, 500)));
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

            //Enemy
            gameObjects.Add(new Enemy(new Vector2(1000, 900)));
            gameObjects.Add(new Enemy(new Vector2(1000, 100)));
            gameObjects.Add(new Enemy(new Vector2(1000, 500)));
            gameObjects.Add(new Enemy(new Vector2(1000, 300)));

            //Loot
            gameObjects.Add(new Loot(new Vector2(3000, 900)));
            gameObjects.Add(new Loot(new Vector2(9000, 100)));
            gameObjects.Add(new Loot(new Vector2(9050, 150)));
            gameObjects.Add(new Loot(new Vector2(9100, 200)));
            gameObjects.Add(new Loot(new Vector2(9150, 250)));
            gameObjects.Add(new Loot(new Vector2(9200, 300)));
            gameObjects.Add(new Loot(new Vector2(9250, 350)));
            gameObjects.Add(new Loot(new Vector2(9300, 400)));
            gameObjects.Add(new Loot(new Vector2(9350, 450)));
            gameObjects.Add(new Loot(new Vector2(9400, 500)));

            //Boost
            gameObjects.Add(new Boost(new Vector2(1000, 900)));


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
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);


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



            spriteBatch.Draw(bananaPoints, new Vector2(10, 70), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(text, ": " + bananaCounter, new Vector2(65, 75), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            int bananasNeeded = 40000 - bananaCounter;
            Debug.WriteLine(Player.PlayerPosition);
            // GameoverTxT
            if (health <= 0 || Player.PlayerPosition.Y >= 1100)
            {
                //isAlive = false;
                spriteBatch.DrawString(text,
                                       "You only needed " + bananasNeeded + " more bananas, to remove banana-food-waste for today.\n See you again tomorrow for 40.000 more \n BUT you gathered enough bananas to produce {x_amount} of ice cream \n\n PRESS ENTER TO PLAY AGAIN",
                                       new Vector2(150, graphics.GraphicsDevice.Viewport.Height / 2),
                                       Color.White,
                                       0,
                                       Vector2.Zero,
                                       1,
                                       SpriteEffects.None,
                                       0);
            }
            if (bananaCounter >= 40000)
            {
                bananaCounter = 40000;
            }
            if (bananaCounter == 40000 && health > 0)
            {

                //spriteBatch.Draw(background, new Vector2(0, 0), null, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1f);


                spriteBatch.DrawString(text,
                                 "Congrats! You made it to level 2! \n In England they discard around 1.500.000 bananas EVERY day!\n Collect all 1.500.000 to advance to win the game \n and to produce {x_amount} of ice cream!"
,
                                 new Vector2(150, graphics.GraphicsDevice.Viewport.Height / 2),
                                 Color.White,
                                 0,
                                 Vector2.Zero,
                                 1,
                                 SpriteEffects.None,
                                 0);
            }



            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}