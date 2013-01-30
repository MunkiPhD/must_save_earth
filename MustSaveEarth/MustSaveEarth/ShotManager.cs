using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MustSaveEarth {
    static class ShotManager {
        static public Texture2D Texture;
        static private Rectangle _defaultShot;
        static private Vector2 _defaultShotVector = new Vector2(0, -4f);
        static private List<Particle> _shots = new List<Particle>();
        static private Viewport _view;

        static private float _timeSinceLastShot;
        static private float _shotDelay = .25f;
        static private SpriteFont _spriteFont; // throw this into a debug class

        public static void Initialize(ContentManager content, Viewport view) {
            Texture = content.Load<Texture2D>("WeaponShots");
            _spriteFont = content.Load<SpriteFont>("SpriteFont1");
            _defaultShot = new Rectangle(0, 0, 3, 8);
            _view = view;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        public static void FireShot(Vector2 position){
            _shots.Add(new Particle(Texture, _defaultShot, position, _defaultShotVector));
        }




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool CanFireShot() {
            if(_timeSinceLastShot >= _shotDelay) {
                _timeSinceLastShot = 0;
                return true;
            } else {
                return false;
            }
        }


        /// <summary>
        /// Update!
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime) {
            _timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;


            for(int i = 0; i < _shots.Count ; i++) {
                if(_shots[i].isActive) {
                    _shots[i].Update(gameTime);


                    // check to see if it's still in the viewable area
                    if(_view.TitleSafeArea.Intersects(_shots[i].PositionRectangle))
                        _shots[i].isActive = true;
                    else
                        _shots[i].isActive = false;

                } else {
                    _shots.RemoveAt(i);
                }
            }
        }



        /// <summary>
        /// Draw!
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch) {
            foreach(Particle particle in _shots) {
                if(particle.isActive)
                    particle.Draw(spriteBatch);
            }
            spriteBatch.DrawString(_spriteFont, _shots.Count.ToString(), new Vector2(10, 10), Color.White);
        }
    }
}
