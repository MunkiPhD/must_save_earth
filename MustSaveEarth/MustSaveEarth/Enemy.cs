using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MustSaveEarth {
    class Enemy {
        public Texture2D SpriteTexture;
        public Vector2 Position;
        public Vector2 Movement = new Vector2(0, 1);
        public int HitPoints;
        public float MaxSpeed = 45f;

        public Enemy(Texture2D texture, Rectangle initialFrame, Vector2 initialPosition) {
            Position = initialPosition;
            this.SpriteTexture = texture;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime) {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += elapsedTime * MaxSpeed * Movement;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(SpriteTexture, Position, Color.White);
        }



        /// <summary>
        /// The center of the sprite in form of a vector
        /// </summary>
        public Vector2 RelativeCenter {
            get {
                return new Vector2(SpriteTexture.Width / 2, SpriteTexture.Height / 2);
            }
        }



        /// <summary>
        /// The location of this sprite in the world, to the center of the sprite
        /// </summary>
        public Vector2 WorldCenter {
            get {
                return new Vector2(RelativeCenter.X + Position.X, RelativeCenter.Y + Position.Y);
            }
        }

    }
}
