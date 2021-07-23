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

namespace Lomztein.BFA2.Scenes.Battlefield.Difficulty.Aspects
{
    public class ResourcesDifficultyAspect : IDifficultyAspect, IHasProperties
    {
        public string Name => "Resources";
        public string Description => "Specifies the amount of resources you start with.";
        public string CategoryIdentifier => "Core.Resources";

        [ModelProperty]
        public ResourceAmount[] Amount;

        public void Apply()
        {
            var container = Player.Player.Resources;
            foreach (ResourceAmount amount in Amount)
            {
                Resource resource = Resource.GetResource(amount.ResourceIdentifier);
                container.ChangeResource(resource, amount.Value);
            }
        }

        public void AddPropertiesTo(PropertyMenu menu)
        {
            foreach (ResourceAmount amount in Amount)
            {
                Resource resource = Resource.GetResource(amount.ResourceIdentifier);
                menu.AddProperty(new NumberDefinition("Starting " + resource.Name, amount.Value, true, 0, float.MaxValue)).OnValueChanged += (x) => StartingResourceChanged(resource.Identifier, x);
            }
        }

        private void StartingResourceChanged(string identifier, object value)
        {
            Amount.First(x => x.ResourceIdentifier == identifier).Value = int.Parse(value.ToString());
        }

        [System.Serializable]
        public class ResourceAmount
        {
            [ModelProperty]
            public string ResourceIdentifier;
            [ModelProperty]
            public int Value;
        }
    }
}
