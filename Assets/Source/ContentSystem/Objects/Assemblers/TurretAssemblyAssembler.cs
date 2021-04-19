using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Assemblers
{
    public class TurretAssemblyAssembler
    {
        private const string ASSEMBLY_PREFAB_PATH = "Prefabs/AssemblyPrefab";
        private TurretComponentAssembler _assembler = new TurretComponentAssembler();

        public TurretAssembly Assemble (RootModel model)
        {
            GameObject assemblyObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(ASSEMBLY_PREFAB_PATH));
            TurretAssembly assembly = assemblyObject.GetComponent<TurretAssembly>();
            ObjectModel obj = model.Root as ObjectModel;
            assembly.Name = obj.GetValue<string>("Name");
            assembly.Description = obj.GetValue<string>("Description");
            _assembler.Assemble (obj.GetObject("RootComponent"), null, assembly, new AssemblyContext());
            return assembly;
        }

        public RootModel Disassemble (TurretAssembly assembly)
        {
            DisassemblyContext context = new DisassemblyContext();
            return new RootModel(new ObjectModel(
                new ObjectField("Name", ValueModelFactory.Create(assembly.Name, context)),
                new ObjectField("Description", ValueModelFactory.Create(assembly.Description, context)),
                new ObjectField("RootComponent", _assembler.Dissassemble(assembly.GetRootComponent(), context)
                )));
        }
    }
}
