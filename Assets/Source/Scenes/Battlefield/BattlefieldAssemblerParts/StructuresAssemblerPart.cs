using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.Assemblers;
using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.StructureManagement;
using Lomztein.BFA2.Structures.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield.BattlefieldAssemblerParts
{
    // Stores what structures have been placed on the map and whatever data they contain such as settings and modules.
    public class StructuresAssemblerPart : IBattlefieldAssemblerPart
    {
        public string Identifier => "Core.Structures";
        public int AssemblyOrder => 50;

        private const string ASSEMBLIES_PATH = "*/Assemblies/*";
        private const string STRUCTURES_PATH = "*/Structures/*";

        public void AssemblePart(BattlefieldController controller, ValueModel partData, AssemblyContext context)
        {
            ArrayModel arrayModel = partData as ArrayModel;
            ValueAssembler valueAssembler = new ValueAssembler();

            var dict = Enumerable.Concat(
                Content.GetAll<IContentCachedPrefab>(ASSEMBLIES_PATH),
                Content.GetAll<IContentCachedPrefab>(STRUCTURES_PATH)
                ).ToDictionary(x => x.GetCache().GetComponent<Structure>().Identifier);

            foreach (var element in arrayModel)
            {
                ObjectModel structureModel = element as ObjectModel;
                string identifier = structureModel.GetProperty<PrimitiveModel>("Identifier").Value;
                Vector3 position = (Vector3)valueAssembler.Assemble(structureModel.GetProperty("Position"), typeof(Vector3), context);
                Vector3 eulerAngles = (Vector3)valueAssembler.Assemble(structureModel.GetProperty("Rotation"), typeof(Vector3), context);
                GameObject newStructure = dict[identifier].Instantiate();
                newStructure.transform.position = position;
                newStructure.transform.eulerAngles = eulerAngles;

                Structure structure = newStructure.GetComponent<Structure>();
                structure.AssembleData(structureModel.GetProperty("Data"), context);
                
                StructureManager.AddStructure(structure, this);
            }
        }

        public ValueModel DisassemblePart(BattlefieldController controller, DisassemblyContext context)
        {
            var list = new List<ObjectModel>();

            var structures = StructureManager.GetStructures();
            foreach (var structure in structures)
            {
                ObjectModel structureModel = new ObjectModel(
                    new ObjectField("Position", ValueModelFactory.Create(structure.transform.position, context)),
                    new ObjectField("Rotation", ValueModelFactory.Create(structure.transform.eulerAngles, context)),
                    new ObjectField("Identifier", ValueModelFactory.Create(structure.Identifier, context)),
                    new ObjectField("Data", structure.DisassembleData(context))
                    );
                list.Add(structureModel);
            }

            return new ArrayModel(list);
        }
    }
}
