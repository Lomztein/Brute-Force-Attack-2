using System.Collections;
using System.Collections.Generic;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
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

        public void Assemble(ValueModel source, AssemblyContext context)
        {
            TileType = (source as PrimitiveModel).ToObject<string>();
        }

        public ValueModel Disassemble(DisassemblyContext context)
        {
            return new PrimitiveModel(TileType);
        }

        public bool IsType(TileType type) => TileType != null && TileType == type?.Name;
    }
}