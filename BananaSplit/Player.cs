using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        public Player()
        {
            speed = 50f;
            position = new Vector2(0, 760);
            GameWorld.Instance.health = 30;
            isAlive = true;
            soundEffects = new List<SoundEffect>();

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


            soundEffects.Add(content.Load<SoundEffect>("gunshot"));

            //var instance = soundEffects[0].CreateInstance();

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

            //#if DEBUG

            //            switch (direction)
            //            {
            //                case Direction.Right:
            //                    for (int i = 0; i < sprites.Length; i++)
            //                    {
            //                        sprites[i] = content.Load<Texture2D>(i + 1 + ("player_debug"));
            //                    }
            //                    break;
            //                case Direction.Left:
            //                    for (int i = 0; i < sprites.Length; i++)
            //                    {
            //                        sprites[i] = content.Load<Texture2D>(i + 1 + ("player_left_debug"));
            //                    }
            //                    break;
            //            }
            //#else
            //            switch (direction)
            //            {
            //                case Direction.Right:
            //                    for (int i = 0; i < sprites.Length; i++)
            //                    {
            //                        sprites[i] = content.Load<Texture2D>(i + 1 + ("player"));
            //                    }
            //                    break;
            //                case Direction.Left:
            //                    for (int i = 0; i < sprites.Length; i++)
            //                    {
            //                        sprites[i] = content.Load<Texture2D>(i + 1 + ("player_left"));
            //                    }
            //                    break;
            //            }
            //#endif


            switch (direction)
            {
                case Direction.Right:
                    for (int i = 0; i < sprites.Length; i++)
                    {
                        sprites[i] = content.Load<Texture2D>(i + 1 + ("player"));
                    }
                    break;
                case Direction.Left:
                    for (int i = 0; i < sprites.Length; i++)
                    {
                        sprites[i] = content.Load<Texture2D>(i + 1 + ("player_left"));
                    }
                    break;

            }
        }

        private void HandleInput(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            KeyboardState kbState = Keyboard.GetState();

            if (isAlive == true)
            {
                // TEST TO SWITCH BETWEEN LEVELS
                if (kbState.IsKeyDown(Keys.L))
                {
                    GameWorld.Instance.level = false;
                }
                if (kbState.IsKeyDown(Keys.T))
                {
                    GameWorld.Instance.level = true;
                }

                //Debug.WriteLine(GameWorld.Instance.level);

                if (kbState.IsKeyDown(Keys.A))
                {
                    velocity += new Vector2(-1, 0);
                    direction = Direction.Left;
                }
                if (kbState.IsKeyDown(Keys.D))
                {
                    velocity += new Vector2(1, 0);
                    direction = Direction.Right;

                }


                if (kbState.IsKeyDown(Keys.W) && previousKBState.IsKeyUp(Keys.W) && isGrounded == true)
                {
                    velocity += new Vector2(0, -250);

                }

                if (kbState.IsKeyDown(Keys.Space) && previousKBState.IsKeyUp(Keys.Space))
                {
                    Vector2 projectileDirection;

                    if (direction == Direction.Right)
                    {
                        sprite = content.Load<Texture2D>("player_shooting");
                        projectileDirection = new Vector2(10, 0);
                    }
                    else
                    {
                        sprite = content.Load<Texture2D>("player_shooting_left");
                        projectileDirection = new Vector2(-10, 0);
                    }

                    Projectile projectile = new Projectile(content.Load<Texture2D>("stone"), projectileDirection);
                    GameWorld.Instance.gameObjectsToAdd.Add(projectile);

                    soundEffects[0].CreateInstance().Play();
                    SoundEffect.MasterVolume = 0.5f;


                    //#if DEBUG
                    //                    if (direction == Direction.Right)
                    //                    {
                    //                        sprite = content.Load<Texture2D>("player_shooting_debug");
                    //                        projectileDirection = new Vector2(10, 0);
                    //                    }
                    //                    else
                    //                    {
                    //                        sprite = content.Load<Texture2D>("player_shooting_left_debug");
                    //                        projectileDirection = new Vector2(-10, 0);
                    //                    }
                    //#else
                    //                    if (direction == Direction.Right)
                    //                    {
                    //                        sprite = content.Load<Texture2D>("player_shooting");
                    //                    }
                    //                    else
                    //                        sprite = content.Load<Texture2D>("player_shooting_left");
                    //#endif

                }


                if ((kbState.IsKeyDown(Keys.D)) || (kbState.IsKeyDown(Keys.A)) || kbState.IsKeyDown(Keys.W) || kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.Left) || kbState.IsKeyDown(Keys.Up))
                {
                    Animation(gameTime);
                }

                previousKBState = kbState;

            }


            // Play again

            if (GameWorld.Instance.health <= 0 || Player.PlayerPosition.Y >= 1100 || GameWorld.Instance.bananaCounter == 40000 || GameWorld.Instance.timer <= 0)
            {
                isAlive = false;
                if (kbState.IsKeyDown(Keys.Enter))
                {
                    GameWorld.Instance.health = 30;
                    GameWorld.Instance.bananaCounter = 0;
                    Enemy.EnemyPosition = new Vector2(1500, 760);
                    isAlive = true;
                    position = new Vector2(100, 100);
                    GameWorld.Instance.timer = 240000f;
                    MediaPlayer.Volume += 0.1f;

                }
            }

            // ## Stands at the same position, but keeps moving ##

            //Vector2 temp = velocity - new Vector2((float)-1.4, 0);


            Vector2 temp = velocity;
            temp.Y = 0;
            GameWorld.Instance.MoveAll(-temp);

            //Debug.WriteLine(temp);
            //Debug.WriteLine(Player.PlayerPosition);

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
                GameWorld.Instance.bananaCounter += 1000;
            }
            else if (@object is Enemy)
            {
                GameWorld.Instance.health -= 1;
            }
            else if (@object is Boost)
            {
                GameWorld.Instance.bananaCounter += 40000;
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
                  //this.position += new Vector2(10, 0);
 
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



                //if (this.Rectangle.Bottom > P.Rectangle.Top && this.Rectangle.Bottom < P.Rectangle.Bottom) // Object is above
                //{
                //    isGrounded = true;
                //    //this.position += new Vector2(0, P.Rectangle.Top - this.Rectangle.Bottom);
                //}
                //if (this.Rectangle.Top < P.Rectangle.Bottom && this.Rectangle.Top > P.Rectangle.Top) // Object below
                //    this.position += new Vector2(0, 10);

                //if (this.Rectangle.Left < P.Rectangle.Right && this.Rectangle.Left > P.Rectangle.Left) // Object to the left
                //    this.position += new Vector2(10, 0);

                //if (this.Rectangle.Right > P.Rectangle.Left && this.Rectangle.Right < P.Rectangle.Right) // Object to the right
                //    this.position += new Vector2(-10, 0);



            }
            else
                isGrounded = false;
        }

    }
}
