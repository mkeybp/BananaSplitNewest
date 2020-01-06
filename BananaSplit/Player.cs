using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace BananaSplit
{
    class Player : GameObject
    {
        ContentManager content;
        KeyboardState previousKBState;

        private bool isGrounded = true;
        private bool isAlive = true;

        public Player()
        {
            speed = 50f;
            position = new Vector2(0, 760);
            GameWorld.Instance.health = 30;
            isAlive = true;

        }


        private static Vector2 playerPosition;

    
        public static Vector2 PlayerPosition
        {
            get { return playerPosition; }
            set { playerPosition = value; }
        }

        public override void LoadContent(ContentManager content)
        {
            this.content = content;

            sprites = new Texture2D[3];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>(i + 1 + ("player"));
            }

            fps = 8;

            sprite = sprites[0];
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            Move(gameTime);
            playerPosition = this.position;

            if (GameWorld.Instance.health <= 10)
            {
                isAlive = false;
            }

            if (isGrounded == false)
            {
                Gravity(gameTime);
            }
            isGrounded = false;

            ////Get these values however you like.
            //DateTime productLaunchDateTime = DateTime.Parse("1/7/2020 12:00:01 AM");
            //DateTime startDate = DateTime.Now;

            ////Calculate countdown timer.
            //TimeSpan t = productLaunchDateTime - startDate;
            //string countDown = string.Format("{0} Days, {1} Hours, {2} Minutes, {3} Seconds til launch.", t.Days, t.Hours, t.Minutes, t.Seconds);


   


            ////Debug.WriteLine(countDown);
        }
        private void HandleInput(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            KeyboardState kbState = Keyboard.GetState();

            if (isAlive == true)
            {

                if (kbState.IsKeyDown(Keys.A))
                {
                    velocity += new Vector2(-1, 0);

                }
                if (kbState.IsKeyDown(Keys.D))
                {
                    velocity += new Vector2(1, 0);

                }

                if (kbState.IsKeyDown(Keys.O))
                {

                    //Platform platform = new Platform(content.Load<Texture2D>("platform"));
                    //GameWorld.Instance.gameObjectsToAdd.Add(platform);
                    //platform.position = new Vector2(0,1020);
                }
                if (kbState.IsKeyDown(Keys.O))
                {
                    //Platform platform1 = new Platform(content.Load<Texture2D>("platform"));
                    //GameWorld.Instance.gameObjectsToAdd.Add(platform1);
                    //platform1.position = new Vector2(300, 1020);
                }
                if (kbState.IsKeyDown(Keys.W) && previousKBState.IsKeyUp(Keys.W) && isGrounded == true)
                {
                    velocity += new Vector2(0, -250);

                }

                if (kbState.IsKeyDown(Keys.Space) && previousKBState.IsKeyUp(Keys.Space))
                {
                    Projectile projectile = new Projectile(content.Load<Texture2D>("stone"));
                    GameWorld.Instance.gameObjectsToAdd.Add(projectile);

                    sprite = content.Load<Texture2D>("player_shooting");

                }


                if ((kbState.IsKeyDown(Keys.D)) || (kbState.IsKeyDown(Keys.A)) || kbState.IsKeyDown(Keys.W) || kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.Left) || kbState.IsKeyDown(Keys.Up))
                {
                    Animation(gameTime);
                }

                    previousKBState = kbState;

            }

            // Play again
            if (kbState.IsKeyDown(Keys.Enter))
            {
                GameWorld.Instance.health = 30;
                GameWorld.Instance.bananaCounter = 0;
                Enemy.EnemyPosition = new Vector2(1500, 760);
                isAlive = true;
                position = new Vector2(100, 100);
            }

            Vector2 temp = velocity;
            temp.Y = 0;
            GameWorld.Instance.MoveAll(-temp);
        }
        private void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += ((velocity * speed) * deltaTime);
        }
        private void Gravity(GameTime gameTime)
        {
            if (position.Y < 1200 && !currentKey.IsKeyDown(Keys.W) && !currentKey.IsKeyDown(Keys.Up))
            {
                velocity += new Vector2(0, 10);
            }
            Move(gameTime);

        }

        public override void OnCollision(GameObject @object)
        {
            if (@object is Loot)
            {
                /// Er sat lavere end der står i raporten, da loot ikke bliver fjernet ved collision
                /// Så det går alt for hurtigt med at samle alle bananer op
                GameWorld.Instance.bananaCounter += 100;
                //isAlive = false;
                //GameWorld.Instance.gameObjectsToRemove.Add(Loot);

            }
            else if (@object is Enemy)
            {
                GameWorld.Instance.health -= 1;
            }
            else if (@object is Boost)
            {
                GameWorld.Instance.bananaCounter += 1000;

            }
            if (@object is Platform)
            {

                Platform P = (Platform)@object;
                //Playeren rører venstre side af platformen
                // hvis X værdien af players højre side + fart > platformens venstre side kolliderer objekterne
                // hvis X værdien af platformens venstre side er større end playerens venstre side er der lavet et dobbelttjek for at spilleren er på venstre side af platformen
                // hvis platformens bunds Y værdi er større end playerens top y værdi så er der lavet et tjek for at Playerens top ligger inden for hitboxen af platformen
                // hvis platformens top y værdi er mindre end playerens bunds værdi så er der lavet et tjek for at playerens bund ligger inden for sidehitboxen af platformen

                if (this.Rectangle.Right + this.velocity.X > P.Rectangle.Left && this.Rectangle.Left < P.Rectangle.Left && this.Rectangle.Top < P.Rectangle.Bottom && this.Rectangle.Bottom > P.Rectangle.Top)
                {



                }
                //Playeren rører højre side af platformen - samme metode som ovenfor, men onvendt.
                if (this.Rectangle.Left + this.velocity.X < P.Rectangle.Right && this.Rectangle.Right > P.Rectangle.Right && this.Rectangle.Top < P.Rectangle.Bottom && this.Rectangle.Bottom > P.Rectangle.Top)
                {


                }
                //Playeren rører toppen af platformen
                if (this.Rectangle.Bottom + this.velocity.Y >= P.Rectangle.Top && this.Rectangle.Top < P.Rectangle.Top && this.Rectangle.Left < P.Rectangle.Right && this.Rectangle.Right > P.Rectangle.Left)
                {
                    isGrounded = true;
                }
                //Playeren rører bunden af platformen
                if (this.Rectangle.Top + this.velocity.Y <= P.Rectangle.Bottom && this.Rectangle.Bottom > P.Rectangle.Bottom && this.Rectangle.Left < P.Rectangle.Right && this.Rectangle.Right > P.Rectangle.Left)
                {
                    this.velocity.Y = 0;


                }


            }
            else
                isGrounded = false;
        }

    }
}
