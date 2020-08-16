using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Inventory;
using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.ModProviders.ExpansionCards;
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

namespace Lomztein.BFA2.Turrets
{
    public class TurretAssembly : MonoBehaviour, ITurretAssembly, INamed, IGridObject, IPurchasable, IModdable, IExpansionCardAcceptor, IContextMenuOptionProvider
    {
        public IStatContainer Stats = new StatContainer ();
        public IEventContainer Events = new EventContainer();
        public IExpansionCardContainer ExpansionCards { get; } = new ExpansionCardContainer();

        public IModContainer Mods { get; private set; }

        [ModelProperty][SerializeField] private string _name;
        public string Name { get => _name; set => _name = value; }
        [ModelProperty][SerializeField] private string _description;
        public string Description { get => _description; set => _description = value; }

        public IResourceCost Cost => GetCost();

        private IResourceCost GetCost()
        {
            IEnumerable<IPurchasable> children = GetComponentsInChildren<ITurretComponent>().Select (x => x as IPurchasable).Where (x => x != null);
            return children.Select(x => x.Cost).Sum();
        }

        public Sprite Sprite => Iconography.GenerateSprite(gameObject);

        public Size Width => GetRootComponent().Width;
        public Size Height => GetRootComponent().Height;


        [SerializeField] [ModelProperty] ModdableAttribute[] _modAttributes;
        
        // Start is called before the first frame update
        void Start()
        {
            InitStats();

            ExpansionCards.OnCardAdded += ExpansionCards_OnCardAdded;
            ExpansionCards.OnCardRemoved += ExpansionCards_OnCardRemoved;

            SceneAssemblyManager.Instance.AddAssembly(this);
            GlobalUpdate.BroadcastUpdate(new ModdableAddedMessage(this));
        }

        private void ExpansionCards_OnCardRemoved(IExpansionCard obj)
        {
            foreach (var component in GetComponentsInChildren<IModdable>())
            {
                if (component.IsCompatableWith(obj.Mod))
                {
                    component.Mods.RemoveMod(obj.Mod.Identifier);
                }
            }
        }

        private void ExpansionCards_OnCardAdded(IExpansionCard obj)
        {
            foreach (var component in GetComponentsInChildren<IModdable>())
            {
                if (component.IsCompatableWith(obj.Mod))
                {
                    component.Mods.AddMod(obj.Mod);
                }
            }
        }

        public void OnAssemblyUpdated ()
        {
            RefreshExpansionCards();
        }

        private void RefreshExpansionCards ()
        {
            RemoveAllExpansionCardMods();
            AddAllExpansionCardMods();
        }

        private void RemoveAllExpansionCardMods ()
        {
            foreach (var component in GetComponentsInChildren<IModdable>())
            {
                foreach (IExpansionCard card in ExpansionCards.CurrentCards)
                {
                    if (component.IsCompatableWith(card.Mod))
                    {
                        component.Mods.RemoveMod(card.Mod.Identifier);
                    }
                }
            }
        }

        private void AddAllExpansionCardMods ()
        {
            foreach (var component in GetComponentsInChildren<IModdable>())
            {
                foreach (IExpansionCard card in ExpansionCards.CurrentCards)
                {
                    if (component.IsCompatableWith(card.Mod))
                    {
                        component.Mods.AddMod(card.Mod);
                    }
                }
            }
        }

        private IModdable[] GetModdableChildren => GetComponentsInChildren<IModdable>();

        void OnDestroy ()
        {
            SceneAssemblyManager.Instance.RemoveAssembly(this);
        }

        void InitStats ()
        {
            Mods = new ModContainer(Stats, Events);
        }

        public ITurretComponent[] GetComponents()
        {
            return GetComponentsInChildren<ITurretComponent>();
        }

        public override string ToString()
        {
            return Name;
        }

        public ITurretComponent GetRootComponent()
        {
            return GetComponentInChildren<ITurretComponent>();
        }

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
            => ExpansionCards.CurrentCards.Select(x => new ContextMenuOption($"Remove '{x.Name}'", "Remove an expansion card.", x.Sprite, () => ReturnCardToInventory(x), () => true));

        // WHAT. WHAT THE FUCK.
        // TODO: Move removal of expansions cards to a different class.
        // Should perhaps be unneccesary if items were not dependant on GameObjects.
        private bool ReturnCardToInventory (IExpansionCard card)
        {
            IContentCachedPrefab item = Content.Content.GetAll<IContentCachedPrefab>("*/Items/")
                .Where(x => x.GetCache().GetComponent<ExpansionCardItem>() != null)
                .FirstOrDefault(x => x.GetCache().GetComponent<ExpansionCardItem>().GetPrefab().GetComponent<IExpansionCard>().UniqueIdentifier == card.UniqueIdentifier);
            if (item != null)
            {
                GetComponent<IInventory>().AddItem(item.Instantiate().GetComponent<Item>());
            }
            ExpansionCards.RemoveCard(card);
            return true;
        }

        public bool IsCompatableWith(IMod mod)
            => mod.ContainsRequiredAttributes(_modAttributes);

        public bool IsCompatableWith(IExpansionCard card)
            => GetComponentsInChildren<IModdable>().Any(x => x.IsCompatableWith(card.Mod));
    }
}