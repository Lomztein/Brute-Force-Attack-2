using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Lomztein.BFA2.World;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Structures.Turrets.Attachment;

namespace Lomztein.BFA2.Structures.Turrets
{
    public class TurretAssembly : Structure
    {
        public override Size Width => GetRootComponent(CurrentTeir)?.Width ?? Size.Medium;
        public override Size Height => GetRootComponent(CurrentTeir)?.Height ?? Size.Medium;
        public override IResourceCost Cost => GetCost(CurrentTeir);

        public Tier CurrentTeir { get; private set; } = Tier.Initial;
        [SerializeField] private List<Tier> _tiers = new List<Tier>();
        public IEnumerable<Tier> Tiers => _tiers;

        public UpgradeMap UpgradeMap;
        public StatInfo ModuleSlotsStatInfo;

        private readonly List<TurretAssemblyModule> _modules = new List<TurretAssemblyModule>();
        public IEnumerable<TurretAssemblyModule> Modules => _modules;

        public float Complexity => GetComponents(CurrentTeir).Sum(x => x.ComputeComplexity());

        public IResourceCost GetCost(Tier tier)
        {
            IEnumerable<IPurchasable> children = GetComponents(tier).Select (x => x as IPurchasable).Where (x => x != null);
            return children.Select(x => x.Cost).Sum().Scale(1f + Complexity);
        }

        public IEnumerable<TurretComponent> GetComponents(Tier tier)
        {
            return GetTierParent(tier).GetComponentsInChildren<TurretComponent>(true);
        }
        public IEnumerable<TurretComponent> GetComponents() => GetComponents(CurrentTeir);

        public override string ToString()
        {
            return $"Free Module Slots: {FreeModuleSlots()}\n{string.Join("\n", GetComponents(CurrentTeir).Select(x => x.ToString()))}";
        }

        public TurretComponent GetRootComponent(Tier tier)
        {
            return GetTierParent(tier).GetComponentInChildren<TurretComponent>();
        }

        public TurretComponent GetRootComponent() => GetRootComponent(CurrentTeir);

        public void SetTiers(IEnumerable<Tier> tiers)
        {
            _tiers.Clear();
            _tiers.AddRange(tiers);
        }

        public void AddTier(Transform parent, Tier tier)
        {
            parent.gameObject.name = tier.ToString();
            parent.SetParent(transform);
            parent.transform.position = transform.position;
            _tiers.Add(tier);
        }

        public void RemoveTier(Tier tier)
        {
            Transform parent = GetTierParent(tier);
            if (parent)
            {
                Destroy(parent.gameObject);
            }
            _tiers.Remove(tier);
        }

        public bool HasTier(Tier tier) => Tiers.Any(x => x.Equals(tier));

        public Transform GetTierParent(Tier tier)
        {
            foreach (Transform child in transform)
            {
                if (Tier.Parse(child.gameObject.name).Equals(tier))
                    return child;
            }

            return null;
        }

        public void SetTier (Tier tier)
        {
            if (HasTier(CurrentTeir))
            {
                GetTierParent(CurrentTeir).gameObject.SetActive(false);
            }
            CurrentTeir = tier;
            GetTierParent(CurrentTeir).gameObject.SetActive(true);
        }

        public void SetTierName(Tier tier, string name)
        {
            tier.Name = name;
        }

        public int FreeModuleSlots ()
        {
            return GetModuleSlots() - _modules.Sum(x => x.Item.ModuleSlots);
        }

        public int GetModuleSlots ()
        {
            // I find humor in suffering..
            int slots = Mathf.RoundToInt(GetComponents(CurrentTeir).Where(x => x.Stats.HasStat(ModuleSlotsStatInfo.Identifier)).Sum(x => x.Stats.GetStat(ModuleSlotsStatInfo.Identifier).GetValue()));
            return slots;
        }

        public bool HasRoomFor (int requiredModuleSlots)
        {
            return FreeModuleSlots() >= requiredModuleSlots;
        }

        public void AddModule(TurretAssemblyModule module)
        {
            _modules.Add(module);
        }

        public void RemoveModule(TurretAssemblyModule module)
        {
            _modules.Remove(module);
        }

        public void Start()
        {
            HierarchyChanged += TurretAssembly_Changed;
            UpdateCollider();
        }

        private void TurretAssembly_Changed(Structure structure, GameObject obj, object source)
        {
            UpdateCollider();
        }

        private void UpdateCollider ()
        {
            BoxCollider2D col = GetComponent<BoxCollider2D>();
            col.size = new Vector2(World.Grid.SizeOf(Width), World.Grid.SizeOf(Height));
        }

        /// <summary>
        /// Loops through all components and reconstructs the connections between attachment points and attachment slots.
        /// </summary>
        public void RebuildComponentAttachments (Tier tier)
        {
            IEnumerable<TurretComponent> components = GetComponents(tier);

            foreach (TurretComponent component in components)
            {
                component.RebuildAttachmentToParent();
                component.RemoveAttachmentsToDeadChildren();
            }
        }

        public void RebuildComponentAttachments()
        {
            foreach (Tier tier in _tiers)
            {
                RebuildComponentAttachments(tier);
            }
        }
    }
}