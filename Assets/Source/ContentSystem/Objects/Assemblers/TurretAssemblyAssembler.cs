using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Lomztein.BFA2.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.Assemblers
{
    public class TurretAssemblyAssembler
    {
        private const string ASSEMBLY_PREFAB_PATH = "Prefabs/AssemblyPrefab";
        private TurretComponentAssembler _assembler = new TurretComponentAssembler();

        public TurretAssembly Assemble (ObjectModel model)
        {
            GameObject assemblyObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(ASSEMBLY_PREFAB_PATH));
            TurretAssembly assembly = assemblyObject.GetComponent<TurretAssembly>();
            assembly.Name = model.GetValue<string>("Name");
            assembly.Description = model.GetValue<string>("Description");
            _assembler.Assemble (model.GetObject("RootComponent"), null, assembly);
            return assembly;
        }

        public ObjectModel Disassemble (ITurretAssembly assembly)
        {
            return new ObjectModel(null,
                new ObjectField("Name", PropertyModelFactory.Create(assembly.Name)),
                new ObjectField("Description", PropertyModelFactory.Create(assembly.Description)),
                new ObjectField("RootComponent", new ComplexPropertyModel (_assembler.Dissassemble(assembly.GetRootComponent())
                )));
        }
    }
}
