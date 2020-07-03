using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.World.Tiles
{
    public struct TileTypeReference : ISerializable
    {
        public string WallType;

        public TileTypeReference(string type)
        {
            WallType = type;
        }

        public void Deserialize(JToken source)
        {
            WallType = source.ToObject<string>();
        }

        public JToken Serialize()
        {
            return new JValue(WallType);
        }
    }
}