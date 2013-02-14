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
        private Rectangle _hitBox;
        public bool isActive = true;
        private float _lastTimeHit = 0f;
        private bool _drawAsHit = false;

        public Enemy(Texture2D texture, Rectangle initialFrame, Vector2 initialPosition) {
            Position = initialPosition;
            this.SpriteTexture = texture;
            _hitBox = new Rectangle(0, 0, texture.Width, texture.Height); //for the time being all the textures are the stand alone sprite
            HitPoints = 20; // set a default hitpoints
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime) {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += elapsedTime * MaxSpeed * Movement;

            if (HitPoints <= 0)
                isActive = false;


            if (elapsedTime >= _lastTimeHit) {
                _lastTimeHit = elapsedTime;
                _drawAsHit = false;
            } else {
                _lastTimeHit -= elapsedTime;
                _drawAsHit = true;
            }
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) {
            if (isActive) {
                if (_drawAsHit)
                    spriteBatch.Draw(SpriteTexture, Position, Color.Red);
                else
                    spriteBatch.Draw(SpriteTexture, Position, Color.White);

            }
        }



        /// <summary>
        /// Handles being shot
        /// </summary>
        /// <param name="shotDamage"></param>
        public void WasShot(int shotDamage, GameTime gameTime) {
            HitPoints -= shotDamage;
            _lastTimeHit = (float)gameTime.ElapsedGameTime.TotalSeconds + 0.1f; // add a fraction of a second so that it has to tint the sprite to reflect that it was hit
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


        /// <summary>
        /// The hitbox for the enemy on the screen
        /// </summary>
        public Rectangle HitBox {
            get {
                return new Rectangle((int)Position.X, (int)Position.Y, _hitBox.Width, _hitBox.Height);
            }
        }
    }
}
