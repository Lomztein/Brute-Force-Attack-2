using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.World.Tiles
{
    // TODO: Rename walls to tiles and specify which tiles are walls instead.
    public class TileType
    {
        public string Name;
        public string Description;

        public TileType(string name, string desc)
        {
            Name = name;
            Description = desc;
        }

        public static TileType Empty = new TileType("Empty", "Completely empty tile.");
        public static TileType PlayerWall = new TileType("PlayerWall", "Player made walls. Structures can be built on these, enemies cannot move through.");
        public static TileType BlockingWall = new TileType("BlockingWall", "Totally blocking wall. Does not support structures nor enemy movement.");
        public static TileType NoBuildZone = new TileType("NoBuildZone", "Blocks building of structures, but enemies may pass through.");
    }
}