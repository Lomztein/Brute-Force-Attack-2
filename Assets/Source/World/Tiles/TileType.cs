using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.World.Tiles
{
    [System.Serializable]
    public class TileType
    {
        public string Name;

        public TileType(string name)
        {
            Name = name;
        }

        public static TileType Empty = new TileType("Empty"); // Player can build, allows enemies.
        public static TileType PlayerWall = new TileType("PlayerWall"); // Player can build walls on on them, blocks enemies
        public static TileType PlaceableWall = new TileType("PlaceableWall"); // Player cannot build walls, but can build on them, blocks enemies
        public static TileType BlockingWall = new TileType("BlockingWall"); // Player cannot build, blocks enemies.
        public static TileType NoBuild = new TileType("NoBuild"); // Player cannot build, allows enemies.
    }
}