using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Lomztein.BFA2.World;

namespace Lomztein.BFA2.Structures.Turrets
{
    public class TurretAssembly : Structure
    {
        public override Size Width => GetRootComponent(CurrentTeir)?.Width ?? Size.Medium;
        public override Size Height => GetRootComponent(CurrentTeir)?.Height ?? Size.Medium;
        public override IResourceCost Cost => GetCost(CurrentTeir);

        public int CurrentTeir { get; private set; }
        [SerializeField] private List<Transform> _tierParents;

        public float Complexity => GetComponents(CurrentTeir).Sum(x => x.ComputeComplexity());

        public IResourceCost GetCost(int tier)
        {
            IEnumerable<IPurchasable> children = GetComponents(tier).Select (x => x as IPurchasable).Where (x => x != null);
            return children.Select(x => x.Cost).Sum();
        }

        public TurretComponent[] GetComponents(int tier)
        {
            return GetTierParent(tier).GetComponentsInChildren<TurretComponent>();
        }
        public TurretComponent[] GetComponents() => GetComponents(CurrentTeir);

        public override string ToString()
        {
            return $"{string.Join("\n", GetComponents(CurrentTeir).Select(x => x.ToString()))}";
        }

        public TurretComponent GetRootComponent(int tier)
        {
            return GetTierParent(tier).GetComponentInChildren<TurretComponent>();
        }
        public TurretComponent GetRootComponent() => GetRootComponent(CurrentTeir);

        public void AddTier(Transform parent) => _tierParents.Add(parent);
        public void InsertTier(int tier, Transform parent) => _tierParents.Insert(tier, parent);
        public void RemoveTier(int tier) => _tierParents.RemoveAt(tier);
        public Transform GetTierParent(int tier) => _tierParents[tier];
        public Transform[] GetTiers() => _tierParents.ToArray();
        public int TierAmount => _tierParents.Count();

        public void SetTier (int tier)
        {
            if (CurrentTeir < TierAmount) // In case a higher tier was removed.
            {
                GetTierParent(CurrentTeir).gameObject.SetActive(false);
            }
            CurrentTeir = tier;
            GetTierParent(CurrentTeir).gameObject.SetActive(true);
        }

        public void Start()
        {
            Changed += TurretAssembly_Changed;
            UpdateCollider();
        }

        private void TurretAssembly_Changed(Structure obj)
        {
            UpdateCollider();
        }
        
        private void UpdateCollider ()
        {
            BoxCollider2D col = GetComponent<BoxCollider2D>();
            col.size = new Vector2(World.Grid.SizeOf(Width), World.Grid.SizeOf(Height));
        }
    }
}