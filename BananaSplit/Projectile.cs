using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaSplit
{
    public class Projectile : GameObject
    {

        Vector2 dir;
        public Projectile(Texture2D loadedTexture, Vector2 Dir)
        {
            this.dir = Dir;
            if (direction == Direction.Right)
            {
                this.position = new Vector2(Player.PlayerPosition.X + 180, Player.PlayerPosition.Y + 125);
            }

            else if (direction == Direction.Left)
            {
                this.position = new Vector2(Player.PlayerPosition.X + 10, Player.PlayerPosition.Y + 125);
            }

            sprite = loadedTexture;
            isAlive = true;
            this.speed = 1f;
        }
        private void UpdateProjectiles()
        {
            if (isAlive)
            {
                position += this.dir * speed;

                Rectangle viewPortRect = new Rectangle(0, 0, GameWorld.Instance.graphics.GraphicsDevice.Viewport.Width, GameWorld.Instance.graphics.GraphicsDevice.Viewport.Height);
                if (!viewPortRect.Contains(new Point((int)position.X, (int)position.Y)))
                {
                    isAlive = false;
                    GameWorld.Instance.gameObjectsToRemove.Add(this);
                }
            }
        }


        public override void LoadContent(ContentManager content)
        {

        }
        public override void Update(GameTime gameTime)
        {
            UpdateProjectiles();
            Debug.WriteLine(Player.PlayerPosition.Y);
        }

        public override void OnCollision(GameObject @object)
        {
            // Removes projectile if it hits an enemy
            if (@object is Enemy)
            {
                this.isAlive = false;

                // if collition -> Add last fired projectile to gameObjectsToRemove
                GameWorld.Instance.gameObjectsToRemove.Add(this);
            }
        }

    }
}
