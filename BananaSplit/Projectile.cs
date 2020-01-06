﻿using Microsoft.Xna.Framework;
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
    class Projectile : GameObject
    {

        public Projectile(Texture2D loadedTexture)
        {
            // If direction right use this
            if (direction == Direction.Right)
            {
                this.position = new Vector2(Player.PlayerPosition.X + 180, Player.PlayerPosition.Y + 125);
            }


            else if (direction == Direction.Left)
            {
                this.position = new Vector2(Player.PlayerPosition.X + 10, Player.PlayerPosition.Y + 125);
            }


            velocity = new Vector2(10, 0);
            sprite = loadedTexture;
            isAlive = true;
        }

        private void UpdateProjectiles()
        {
            if (isAlive)
            {
                if (direction == Direction.Right)
                {

                }
                position += velocity;
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

        }

        public override void OnCollision(GameObject @object)
        {
            // Removes projectile if it hits an enemy
            if (@object is Enemy)
            {
                GameWorld.Instance.gameObjectsToRemove.Add(this);
            }
        }

    }
}
