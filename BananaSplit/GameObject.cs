using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaSplit
{
    public enum Direction { Right, Left }
    public abstract class GameObject
    {
   




        public Direction direction = new Direction();
        public List<SoundEffect> soundEffects;


        //Flere sprites i et array.
        protected Texture2D[] sprites;
        //En sprite.
        protected Texture2D sprite;

        public Texture2D tex;

        public bool isAlive;

        //protected Vector2[] positions;

        //protected Texture2D platform;

        public int health;

        //Frames per seconds, bruges til animation af sprites.
        protected int fps;
        //Bruges til Player og Enemey movement.
        protected float speed;
        //Bruges også til Player og Enemy movement.
        protected Vector2 velocity;
        //Bruges til at holde styr på Player og Enemy position.
        public Vector2 position;
        //Bruges til at finde midtpunket af Player og Enemy sprite.
        protected Vector2 origin;
        //Holder styr på hvilken tast som bliver brugt i øjeblikket.
        protected KeyboardState currentKey;
        //Holder styr på hvilken key som blev brugt sidst.
        protected KeyboardState previousKey;
        //Holder styr på hvor langt man er i et Array.
        private int currentIndex;
        //
        private float timeElapsed;

        public virtual Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, sprite.Width / 2, sprite.Height);
            }
        }
        public virtual Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            }
        }
        public abstract void OnCollision(GameObject other);

        public void CheckCollision(GameObject other)
        {
            if (CollisionBox.Intersects(other.CollisionBox))
            {
                OnCollision(other);
            }
        }


        protected void Animation(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            currentIndex = (int)(timeElapsed * fps);
            sprite = sprites[currentIndex];

            if (currentIndex >= sprites.Length - 1)
            {
                timeElapsed = 0;
                currentIndex = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 1);
        }
        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

    }
}
