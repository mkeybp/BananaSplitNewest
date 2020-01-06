using Microsoft.Xna.Framework;
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
    class Platform : GameObject
    {
        public Platform(Vector2 position)
        {
            this.position = position;
        }

        public void Initialize(GameTime gameTime)
        {
            //platforms.Add(new Platform());
        }
        public override void LoadContent(ContentManager content)
        {



            // Et spritearray Instantieres
            sprites = new Texture2D[1];

            // Sprites bliver loadet ind i arrayet
            sprites[0] = content.Load<Texture2D>("Platform");
            sprite = sprites[0];


            //sprite = content.Load<Texture2D>("platform");
            //this.position = new Vector2(0, 1020);
            //sprite = content.Load<Texture2D>("platform");
            //this.position = new Vector2(200, 1020);
            //sprite = content.Load<Texture2D>("platform");
            //this.position = new Vector2(400, 1020);
            //sprite = content.Load<Texture2D>("platform");
            //this.position = new Vector2(600, 1020);
            //sprite = content.Load<Texture2D>("platform");
            //this.position = new Vector2(800, 1020);
        }

        public override void Update(GameTime gameTime)
        {
            //foreach()
        }

        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            }
        }
        public override void OnCollision(GameObject @object)
        {

        }
    }
}
