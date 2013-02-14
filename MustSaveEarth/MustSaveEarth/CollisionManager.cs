using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MustSaveEarth {
    static class CollisionManager {
        static public EnemyManager enemyManager;

        public static void Initialize(EnemyManager enemyMan) {
            enemyManager = enemyMan;
        }

        public static void Update(GameTime gameTime) {
            for (int i = 0; i < ShotManager.Shots.Count; i++) {
                for (int enemyIndex = 0; enemyIndex < enemyManager.EnemyList.Count; enemyIndex++) {

                    // check to make sure the shot is active
                    if (ShotManager.Shots[i].isActive) {
                        if (ShotManager.Shots[i].PositionRectangle.Intersects(enemyManager.EnemyList[enemyIndex].HitBox)) {
                            enemyManager.EnemyList[enemyIndex].WasShot(5, gameTime);
                            ShotManager.Shots[i].isActive = false;
                        }
                    }
                }

            }
        }
    }
}
