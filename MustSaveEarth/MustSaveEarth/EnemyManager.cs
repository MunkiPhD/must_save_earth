using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MustSaveEarth {
    class EnemyManager {
        private Texture2D _baseTexture;
        private Texture2D _baseTexture2;
        private Viewport _view;
        private ContentManager _contentManager;
        private float _elapsedTime = 0f;
        public List<Enemy> EnemyList = new List<Enemy>();
        private Random _rand = new Random();

        public EnemyManager(ContentManager contentManager, Viewport viewport) {
            this._contentManager = contentManager;
            this._view = viewport;

            _baseTexture = _contentManager.Load<Texture2D>("enemy_1");
            _baseTexture2 = _contentManager.Load<Texture2D>("enemy_2");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime) {
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_elapsedTime >= 5) {
                AddEnemy();
                _elapsedTime = 0f;
            }

            for(int i = 0; i < EnemyList.Count; i++) {
                EnemyList[i].Update(gameTime); // remove it from the game world

                if((EnemyList[i].Position.Y > _view.TitleSafeArea.Height) || (EnemyList[i].isActive == false))
                    EnemyList.RemoveAt(i);
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) {
            foreach(Enemy enemy in EnemyList)
                enemy.Draw(spriteBatch);

        }



        /// <summary>
        /// Adds an enemy to the game
        /// </summary>
        private void AddEnemy() {
            int xLocation = _rand.Next(0, _view.TitleSafeArea.Width - _baseTexture.Width);
            if(xLocation % 2 == 0)
                EnemyList.Add(new Enemy(_baseTexture, new Rectangle(0, 0, 0, 0), new Vector2(xLocation,-_baseTexture.Height),10, 5));
            else
                EnemyList.Add(new Enemy(_baseTexture2, new Rectangle(0, 0, 0, 0), new Vector2(xLocation, -_baseTexture2.Height), 20, 10));
        }
    }
}
