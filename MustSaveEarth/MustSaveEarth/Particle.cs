using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MustSaveEarth {
    class Particle {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public Rectangle SourceRectangle;
        public bool isActive = true;

        public Particle(Texture2D texture, Rectangle sourceRect, Vector2 position, Vector2 velocity) {
            this.Texture = texture;
            this.SourceRectangle = sourceRect;
            this.Position = position;
            this.Velocity = velocity;
        }

        public void Update(GameTime gameTime) {
            Position += Velocity;
        }


        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Position, SourceRectangle, Color.White);
        }


        /// <summary>
        /// 
        /// </summary>
        public Rectangle PositionRectangle {
            get {
                return new Rectangle((int)Position.X, (int)Position.Y, SourceRectangle.Width, SourceRectangle.Height);
            }
        }
    }
}
