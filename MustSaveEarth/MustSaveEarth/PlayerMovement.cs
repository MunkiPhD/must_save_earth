using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MustSaveEarth {
    static class PlayerMovement {
       
        public static MapData CurrentMapData;
        public static Texture2D LevelTexture;
        private static Rectangle _levelRect;
        private static Color[] _playerData;
        private static Color[] _levelData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="currentMap"></param>
        public static void Initialize(Texture2D background) {
           
            //CurrentMapData = currentMap;

            LevelTexture = background;
            _levelRect = new Rectangle(0, 0, LevelTexture.Width, LevelTexture.Height);
            _levelData = new Color[LevelTexture.Width * LevelTexture.Height];
            background.GetData(_levelData);

            _playerData = new Color[Player.Texture.Width * Player.Texture.Height];
            Player.Texture.GetData(_playerData);

        }


        /// <summary>
        /// 
        /// </summary>
        public static void MovePlayerToMapYLocation() {
            if (IntersectPixels(Player.RectangleHitBox, _playerData, _levelRect, _levelData)) {
                while (IntersectPixels(Player.RectangleHitBox, _playerData, _levelRect, _levelData)) {
                    Player.Position = new Vector2(Player.Position.X, Player.Position.Y - 1);
                }
            }
            //foreach(MapLedge ledge in CurrentMapData.MapLedges) {
            //    if((PlayerOne.WorldPosition.X >= ledge.LeftPoint.X) &&
            //        PlayerOne.WorldPosition.X <= ledge.RightPoint.X) {
            //        PlayerOne.Position = new Vector2(PlayerOne.Position.X, ledge.Adjustment.Y - ledge.RightPoint.Y);
            //        return;
            //    }
            //}
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangleA"></param>
        /// <param name="dataA"></param>
        /// <param name="rectangleB"></param>
        /// <param name="dataB"></param>
        /// <returns></returns>
        static bool IntersectPixels(Rectangle rectangleA, Color[] dataA, Rectangle rectangleB, Color[] dataB) {

            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);


            // Check every point within the intersection bounds
            for(int y = top; y < bottom; y++) {
                for(int x = left; x < right; x++) {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) + (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) + (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if(colorA.A != 0 && colorB.A != 0) {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;

        } // end intersects pixels

    }
}
