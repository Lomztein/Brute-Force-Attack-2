﻿using Lomztein.BFA2.Content.References;
using Lomztein.BFA2.Modification.ModProviders.ExpansionCards;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.ModProviders.ModProviders
{
    public class ExpansionCard : MonoBehaviour, IExpansionCard, IPurchasable
    {
        public IMod Mod { get; private set; }

        [SerializeField] [ModelProperty] private string _name;
        public string Name => _name;

        [SerializeField] [ModelProperty] private string _description;
        public string Description => _description;

        [SerializeField] [ModelProperty] private ResourceCost _cost;
        public IResourceCost Cost => _cost;

        [SerializeField] [ModelProperty] private ContentSpriteReference _sprite;
        public Sprite Sprite => _sprite.Get();

        [SerializeField] [ModelProperty] private string _uniqueIdentifier;
        public string UniqueIdentifier => _uniqueIdentifier;

        private void Awake()
        {
            Mod = GetComponent<IMod>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
