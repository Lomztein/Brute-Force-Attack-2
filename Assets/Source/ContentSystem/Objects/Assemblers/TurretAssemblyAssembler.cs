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
            ArrayModel tiers = obj.GetArray("Tiers");
            ObjectModel children = obj.GetObject("Children");
            UpgradeMap upgrades = _valueAssembler.Assemble(obj.GetObject("UpgradeMap"), typeof(UpgradeMap), new AssemblyContext()) as UpgradeMap;

            foreach (var tierProperty in children.GetProperties())
            {
                Transform childObj = AssembleTier(tierProperty.Model as ObjectModel, assembly).transform;
                Tier tier = Tier.Parse(tierProperty.Name);

                childObj.gameObject.SetActive(false);
                childObj.gameObject.name = tier.ToString();
            }

            ArrayModelAssembler assembler = new ArrayModelAssembler();
            assembly.SetTiers(assembler.Assemble(tiers, typeof(Tier[]), new AssemblyContext()) as Tier[]);
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
            List<ObjectField> children = new List<ObjectField>();
            foreach (Tier tier in assembly.Tiers)
            {
                children.Add(new ObjectField (tier.ToString(), DisassembleTier(assembly, tier, context)));
            }

            return new RootModel(new ObjectModel(
                new ObjectField("Name", ValueModelFactory.Create(assembly.Name, context)),
                new ObjectField("Description", ValueModelFactory.Create(assembly.Description, context)),
                new ObjectField("Tiers", ValueModelFactory.Create(assembly.Tiers, context)),
                new ObjectField("UpgradeMap", _valueAssembler.Disassemble(assembly.UpgradeMap, typeof(UpgradeMap), context)),
                new ObjectField("Children", new ObjectModel(children.ToArray()))
                ));
        }

        private ObjectModel DisassembleTier (TurretAssembly assembly, Tier tier, DisassemblyContext context)
        {
            return _assembler.Dissassemble(assembly.GetRootComponent(tier), context);
        }
    }
}
