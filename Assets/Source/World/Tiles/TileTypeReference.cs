using System.Collections;
using System.Collections.Generic;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.World.Tiles
{
    [System.Serializable]
    public struct TileTypeReference : IAssemblable
    {
        [ModelProperty]
        public string TileType;

        public TileTypeReference(string type)
        {
            TileType = type;
        }

        public void Assemble(PropertyModel source)
        {
            TileType = (source as PrimitivePropertyModel).ToObject<string>();
        }

        public PropertyModel Disassemble()
        {
            return new PrimitivePropertyModel(TileType);
        }

        public bool IsType(TileType type) => TileType != null && TileType == type?.Name;
    }
}