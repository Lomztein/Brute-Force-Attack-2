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
        public int StartingCredits = 1000;
        [ModelProperty]
        public int StartingResearch = 0;

        public void Apply()
        {
            var container = Player.Player.Resources;
            container.ChangeResource(Resource.Credits, StartingCredits);
            container.ChangeResource(Resource.Research, StartingResearch);
        }

        public void AddPropertiesTo(PropertyMenu menu)
        {
            menu.AddProperty(new NumberDefinition("Starting Credits", StartingCredits, true, 0, float.MaxValue)).OnValueChanged += StartingCreditsChanged;
            menu.AddProperty(new NumberDefinition("Starting Research", StartingResearch, true, 0, float.MaxValue)).OnValueChanged += StartingResearchChanged; ;
        }

        private void StartingResearchChanged(object obj)
        {
            StartingResearch = int.Parse(obj.ToString());
        }

        private void StartingCreditsChanged(object obj)
        {

            StartingCredits = int.Parse(obj.ToString());
        }
    }
}
