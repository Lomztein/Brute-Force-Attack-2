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
            ArrayModel tiers = obj.GetArray("Tiers");
            for (int i = 0; i < tiers.Length; i++)
            {
                Transform tier = AssembleTier(tiers[i], assembly).transform;
                tier.gameObject.SetActive(false);
                assembly.InsertTier(i, tier);
            }
            assembly.SetTier(0);
            return assembly;
        }

        private TurretComponent AssembleTier (ValueModel model, TurretAssembly assembly)
        {
            return _assembler.Assemble(model as ObjectModel, null, assembly, new AssemblyContext());
        }

        public RootModel Disassemble (TurretAssembly assembly)
        {
            DisassemblyContext context = new DisassemblyContext();
            ObjectModel[] tiers = new ObjectModel[assembly.TierAmount];
            for (int i = 0; i < tiers.Length; i++)
            {
                tiers[i] = DisassembleTier(assembly, i, context);
            }

            return new RootModel(new ObjectModel(
                new ObjectField("Name", ValueModelFactory.Create(assembly.Name, context)),
                new ObjectField("Description", ValueModelFactory.Create(assembly.Description, context)),
                new ObjectField("Tiers", new ArrayModel(tiers)
                )));
        }

        private ObjectModel DisassembleTier (TurretAssembly assembly, int tier, DisassemblyContext context)
        {
            return _assembler.Dissassemble(assembly.GetRootComponent(tier), context);
        }
    }
}
