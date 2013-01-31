using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MustSaveEarth {
    static class PlayerMovement {
        public static Player PlayerOne;
        public static MapData CurrentMapData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="currentMap"></param>
        public static void Initialize(Player player, MapData currentMap) {
            PlayerOne = player;
            CurrentMapData = currentMap;
        }


        /// <summary>
        /// 
        /// </summary>
        public static void MovePlayerToMapYLocation(){
            foreach(MapLedge ledge in CurrentMapData.MapLedges) {
                if((PlayerOne.WorldPosition.X >= ledge.LeftPoint.X) &&
                    PlayerOne.WorldPosition.X <= ledge.RightPoint.X) {
                        PlayerOne.Position = new Vector2(PlayerOne.Position.X, ledge.Adjustment.Y - ledge.RightPoint.Y);
                        return;
                }
            }
        }
    }
}
