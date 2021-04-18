using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Inventory;
using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.ModBroadcasters.ExpansionCards;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI;
using Lomztein.BFA2.UI.ContextMenu;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Lomztein.BFA2.World;

namespace Lomztein.BFA2.Structures.Turrets
{
    public class TurretAssembly : Structure
    {
        public override Size Width => GetRootComponent().Width;
        public override Size Height => GetRootComponent().Height;

        public IExpansionCardContainer ExpansionCards { get; } = new ExpansionCardContainer();
        public override IResourceCost Cost => GetCost();
        public float Complexity => GetComponents().Sum(x => x.ComputeComplexity());

        private IResourceCost GetCost()
        {
            IEnumerable<IPurchasable> children = GetComponentsInChildren<TurretComponent>().Select (x => x as IPurchasable).Where (x => x != null);
            return children.Select(x => x.Cost).Sum();
        }
        
        public TurretComponent[] GetComponents()
        {
            return GetComponentsInChildren<TurretComponent>();
        }

        public override string ToString()
        {
            return $"{string.Join("\n", GetComponents().Select(x => x.ToString()))}";
        }

        public TurretComponent GetRootComponent()
        {
            return GetComponentInChildren<TurretComponent>();
        }
    }
}