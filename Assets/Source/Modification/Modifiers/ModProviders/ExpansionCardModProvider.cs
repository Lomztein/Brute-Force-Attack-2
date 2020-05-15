using Lomztein.BFA2.Content.References;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.ExpansionCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.ModProviders
{
    public class ExpansionCardModProvider : MonoBehaviour, IExpansionCard, IPurchasable
    {
        private IMod _mod;

        [SerializeField] [ModelProperty] private string _name;
        public string Name => _name;

        [SerializeField] [ModelProperty] private string _description;
        public string Description => _description;

        [SerializeField] [ModelProperty] private ResourceCost _cost;
        public IResourceCost Cost => _cost;

        [SerializeField] [ModelProperty] private ContentSprite _sprite;
        public Sprite Sprite => _sprite.Get();

        private void Awake()
        {
            _mod = GetComponent<IMod>();
        }

        public void ApplyTo (IModdable obj)
        {
            obj.Mods.AddMod(_mod);
        }

        public void RemoveFrom (IModdable obj)
        {
            obj.Mods.RemoveMod(_mod);
        }

        public bool ApplyTo(IExpansionCardAcceptor target)
        {
            if (target is IModdable moddable)
            {
                ApplyTo(moddable);
                return true;
            }
            return false;
        }

        public bool RemoveFrom(IExpansionCardAcceptor target)
        {
            if (target is IModdable moddable)
            {
                RemoveFrom(moddable);
                return true;
            }
            return false;
        }

        public bool CompatableWith(ModdableAttribute[] attributes)
        {
            return _mod.CompatableWith(attributes);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
