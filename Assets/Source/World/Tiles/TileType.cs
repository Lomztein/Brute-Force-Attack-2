using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.World.Tiles
{
    public class TileType
    {
        public string Name;

        public TileType(string name)
        {
            Name = name;
        }

        public static TileType Empty = new TileType("Empty"); // Player can build, allows enemies.
        public static TileType PlayerWall = new TileType("PlayerWall"); // Player can build, blocks enemies
        public static TileType BlockingWall = new TileType("BlockingWall"); // Player cannot build, blocks enemies.
        public static TileType NoBuildZone = new TileType("NoBuildZone"); // Player cannot build, allows enemies.
    }
}