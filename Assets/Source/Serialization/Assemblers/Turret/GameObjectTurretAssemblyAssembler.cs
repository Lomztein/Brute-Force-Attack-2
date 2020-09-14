using Lomztein.BFA2.Serialization.Models.Turret;
using Lomztein.BFA2.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers.Turret
{
    public class GameObjectTurretAssemblyAssembler
    {
        private const string ASSEMBLY_PREFAB_PATH = "Prefabs/AssemblyPrefab";
        private GameObjectTurretComponentAssembler _assembler = new GameObjectTurretComponentAssembler();

        public ITurretAssembly Assemble (ITurretAssemblyModel model)
        {
            GameObject assemblyObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(ASSEMBLY_PREFAB_PATH));
            ITurretAssembly assembly = assemblyObject.GetComponent<ITurretAssembly>();
            assembly.Name = model.Name;
            assembly.Description = model.Description;
            _assembler.Assemble (model.RootComponent, null, assembly);
            return assembly;
        }

        public ITurretAssemblyModel Disassemble (ITurretAssembly assembly)
        {
            return new TurretAssemblyModel(assembly.Name, assembly.Description, _assembler.Dissassemble(assembly.GetRootComponent()));
        }
    }
}
