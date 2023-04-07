using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Collectables;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield.BattlefieldAssemblerParts
{
    public class LootAssemblerPart : IBattlefieldAssemblerPart
    {
        public string Identifier => "Core.Loot";
        public int AssemblyOrder => 0;

        public void AssemblePart(BattlefieldController controller, ValueModel partData, AssemblyContext context)
        {
            GameObjectAssembler assembler = new GameObjectAssembler();
            ArrayModel array = partData as ArrayModel;
            foreach (var collectable in array)
            {
                GameObject newObject = assembler.Assemble(collectable, typeof(GameObject), context) as GameObject;
                newObject.SetActive(true);
            }
        }

        public ValueModel DisassemblePart(BattlefieldController controller, DisassemblyContext context)
        {
            Collectable[] collectables = UnityEngine.Object.FindObjectsOfType<Collectable>(false);
            GameObjectAssembler assembler = new GameObjectAssembler();
            var list = new List<ValueModel>();
            foreach (var collectable in collectables)
            {
                collectable.Effect.transform.SetParent(null); // Temporarily remove the effect so that it is not stored in the model when disassembling the collectable.
                ValueModel model = assembler.Disassemble(collectable.gameObject, typeof(GameObject), context);
                collectable.Effect.transform.SetParent(collectable.transform);
                list.Add(model);
            }
            return new ArrayModel(list);
        }
    }
}
