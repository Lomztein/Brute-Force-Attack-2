using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Structures.Turrets;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Assemblers
{
    public class TurretAssemblyAssembler
    {
        private const string ASSEMBLY_PREFAB_PATH = "Prefabs/AssemblyPrefab";
        private TurretComponentAssembler _assembler = new TurretComponentAssembler();
        private ValueAssembler _valueAssembler = new ValueAssembler();

        public TurretAssembly Assemble (RootModel model)
        {
            GameObject assemblyObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(ASSEMBLY_PREFAB_PATH));
            TurretAssembly assembly = assemblyObject.GetComponent<TurretAssembly>();
            ObjectModel obj = model.Root as ObjectModel;
            assembly.Name = obj.GetValue<string>("Name");
            assembly.Description = obj.GetValue<string>("Description");
            ObjectModel tiers = obj.GetObject("Tiers");
            UpgradeMap upgrades = _valueAssembler.Assemble(obj.GetObject("UpgradeMap"), typeof(UpgradeMap), new AssemblyContext()) as UpgradeMap;

            foreach (var tierProperty in tiers.GetProperties())
            {
                Transform tierObj = AssembleTier(tierProperty.Model as ObjectModel, assembly).transform;
                Tier tier = Tier.Parse(tierProperty.Name);

                tierObj.gameObject.SetActive(false);
                tierObj.gameObject.name = tier.ToString();
                
                assembly.AddTier(tierObj, tier);
            }

            assembly.UpgradeMap = upgrades;
            assembly.SetTier(Tier.Initial);
            return assembly;
        }

        private TurretComponent AssembleTier (ObjectModel model, TurretAssembly assembly)
        {
            return _assembler.Assemble(model, null, assembly, new AssemblyContext());
        }

        public RootModel Disassemble (TurretAssembly assembly)
        {
            DisassemblyContext context = new DisassemblyContext();
            List<ObjectField> tiers = new List<ObjectField>();
            foreach (Transform tier in assembly.transform)
            {
                tiers.Add(new ObjectField (tier.name, DisassembleTier(assembly, Tier.Parse(tier.gameObject.name), context)));
            }

            return new RootModel(new ObjectModel(
                new ObjectField("Name", ValueModelFactory.Create(assembly.Name, context)),
                new ObjectField("Description", ValueModelFactory.Create(assembly.Description, context)),
                new ObjectField("Tiers", new ObjectModel(tiers.ToArray())),
                new ObjectField("UpgradeMap", _valueAssembler.Disassemble(assembly.UpgradeMap, typeof(UpgradeMap), new DisassemblyContext())
                )));
        }

        private ObjectModel DisassembleTier (TurretAssembly assembly, Tier tier, DisassemblyContext context)
        {
            return _assembler.Dissassemble(assembly.GetRootComponent(tier), context);
        }
    }
}
