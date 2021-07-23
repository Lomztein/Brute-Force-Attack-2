using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Menus.PropertyMenus;
using Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield.Mutators
{
    public class StartingResourceMultiplierMutator : Mutator, IHasProperties
    {
        [ModelProperty]
        public float DefaultMultiplier = 2f;
        [ModelProperty]
        private List<Multiplier> _multipliers;

        public void AddPropertiesTo(PropertyMenu menu)
        {
            foreach (Multiplier multiplier in GetMultipliers())
            {
                Resource resource = Resource.GetResource(multiplier.ResourceIdentifier);
                menu.AddProperty(new NumberDefinition(resource.Shorthand + " Mult", multiplier.Value, false, 0, float.MaxValue)).OnValueChanged += (x) => SetResource(multiplier.ResourceIdentifier, x);
            }
        }

        private void SetResource(string resourceIdentifier, object x)
        {
            GetMultipliers().Find(x => x.ResourceIdentifier == resourceIdentifier).Value = (float)x;
        }

        public override void Start()
        {
            IResourceContainer container = Player.Player.Resources;

            foreach (Multiplier multiplier in GetMultipliers())
            {
                Resource resource = Resource.GetResource(multiplier.ResourceIdentifier);
                container.SetResource(resource, Mathf.RoundToInt(container.GetResource(resource) * multiplier.Value));
            }
        }

        private List<Multiplier> GetMultipliers ()
        {
            if (_multipliers == null)
            {
                _multipliers = new List<Multiplier>();
                foreach (Resource resource in Resource.GetResources())
                {
                    _multipliers.Add(new Multiplier(resource.Identifier, DefaultMultiplier));
                }
            }
            return _multipliers;
        }

        [System.Serializable]
        public class Multiplier
        {
            [ModelProperty]
            public string ResourceIdentifier;
            [ModelProperty]
            public float Value;

            public Multiplier(string resourceIdentifier, float value)
            {
                ResourceIdentifier = resourceIdentifier;
                Value = value;
            }
        }
    }
}
