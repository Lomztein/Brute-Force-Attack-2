using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Assemblers
{
    public class TurretAssemblyAssembler
    {
        private const string ASSEMBLY_PREFAB_PATH = "Prefabs/AssemblyPrefab";
        private TurretComponentAssembler _componentAssembler = new TurretComponentAssembler();
        private ValueAssembler _valueAssembler = new ValueAssembler();

        public TurretAssembly Assemble (RootModel model)
        {
            GameObject assemblyObject = Object.Instantiate(Resources.Load<GameObject>(ASSEMBLY_PREFAB_PATH));
            TurretAssembly assembly = assemblyObject.GetComponent<TurretAssembly>();
            try
            {
                ObjectModel obj = model.Root as ObjectModel;
                assembly.Name = obj.GetValue<string>("Name");
                assembly.Description = obj.GetValue<string>("Description");
                assembly.Identifier = obj.GetValue<string>("Identifier");
                ArrayModel tiers = obj.GetArray("Tiers");
                ObjectModel children = obj.GetObject("Children");
                UpgradeMap upgrades = _valueAssembler.Assemble(obj.GetObject("UpgradeMap"), typeof(UpgradeMap), new AssemblyContext()) as UpgradeMap;

                foreach (var tierProperty in children.GetProperties())
                {
                    Tier tier = Tier.Parse(tierProperty.Name);

                    GameObject tierParent = new GameObject(tier.ToString());
                    tierParent.transform.SetParent(assembly.transform);
                    tierParent.transform.localPosition = Vector3.zero;
                    tierParent.gameObject.SetActive(false);

                    Transform childObj = AssembleTier(tierProperty.Model as ObjectModel, assembly).transform;

                    childObj.transform.SetParent(tierParent.transform);
                    childObj.transform.localPosition = Vector3.zero;
                }

                ArrayModelAssembler assembler = new ArrayModelAssembler();
                assembly.SetTiers(assembler.Assemble(tiers, typeof(Tier[]), new AssemblyContext()) as Tier[]);
                assembly.UpgradeMap = upgrades;
                assembly.SetTier(Tier.Initial);

                ReflectionUtils.DynamicBroadcastInvoke(assembly.gameObject, "OnAssembled", true);
                ReflectionUtils.DynamicBroadcastInvoke(assembly.gameObject, "OnPostAssembled", true);

                assembly.RebuildComponentAttachments();

                return assembly;
            }catch (System.Exception exc)
            {
                Debug.LogException(exc);
                throw new TurretAssemblyException($"Something went wrong during turret assembly of '{assembly.Name}'.", exc);
            }
        }

        private TurretComponent AssembleTier (ObjectModel model, TurretAssembly assembly)
        {
            return _componentAssembler.Assemble(model, null, assembly, new AssemblyContext());
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
                new ObjectField("Identifier", ValueModelFactory.Create(assembly.Identifier, context)),
                new ObjectField("Tiers", ValueModelFactory.Create(assembly.Tiers, context)),
                new ObjectField("UpgradeMap", _valueAssembler.Disassemble(assembly.UpgradeMap, typeof(UpgradeMap), context)),
                new ObjectField("Children", new ObjectModel(children.ToArray()))
                ));
        }

        private ObjectModel DisassembleTier (TurretAssembly assembly, Tier tier, DisassemblyContext context)
        {
            return _componentAssembler.Dissassemble(assembly.GetRootComponent(tier), context);
        }
    }

    public class TurretAssemblyException : System.Exception
    {
        public TurretAssemblyException(string message, System.Exception inner) : base(message, inner) { }
    }
}
