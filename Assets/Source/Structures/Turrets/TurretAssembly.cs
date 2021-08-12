﻿using Lomztein.BFA2.Purchasing;
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

        public Tier CurrentTeir { get; private set; } = Tier.Initial;
        public UpgradeMap UpgradeMap;

        public float Complexity => GetComponents(CurrentTeir).Sum(x => x.ComputeComplexity());

        public IResourceCost GetCost(Tier tier)
        {
            IEnumerable<IPurchasable> children = GetComponents(tier).Select (x => x as IPurchasable).Where (x => x != null);
            return children.Select(x => x.Cost).Sum();
        }

        public IEnumerable<TurretComponent> GetComponents(Tier tier)
        {
            return GetTierParent(tier).GetComponentsInChildren<TurretComponent>();
        }
        public IEnumerable<TurretComponent> GetComponents() => GetComponents(CurrentTeir);

        public override string ToString()
        {
            return $"{string.Join("\n", GetComponents(CurrentTeir).Select(x => x.ToString()))}";
        }

        public TurretComponent GetRootComponent(Tier tier)
        {
            return GetTierParent(tier).GetComponentInChildren<TurretComponent>();
        }
        public TurretComponent GetRootComponent() => GetRootComponent(CurrentTeir);

        public void AddTier(Transform parent, Tier tier)
        {
            parent.gameObject.name = tier.ToString();
            parent.SetParent(transform);
            parent.transform.position = transform.position;
        }

        public void RemoveTier(Tier tier)
        {
            Transform parent = GetTierParent(tier);
            if (parent)
            {
                Destroy(parent.gameObject);
            }
        }

        public bool HasTier(Tier tier) => GetTierParent(tier) != null;

        public Transform GetTierParent(Tier tier)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name.Equals(tier.ToString()))
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