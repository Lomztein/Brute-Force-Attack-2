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

namespace Lomztein.BFA2.Mutators
{
    public class StartingResourceMultiplierMutator : Mutator, IHasProperties
    {
        [ModelProperty]
        public float CreditsMultiplier = 2f;
        [ModelProperty]
        public float ResearchMultiplier = 2f;

        public void AddPropertiesTo(PropertyMenu menu)
        {
            menu.AddProperty(new NumberDefinition("Credits Mult", CreditsMultiplier, false, 0, float.MaxValue)).OnValueChanged += SetCredits;
            menu.AddProperty(new NumberDefinition("Research Mult", ResearchMultiplier, false, 0, float.MaxValue)).OnValueChanged += SetResearch;
        }

        private void SetResearch(object obj)
        {
            ResearchMultiplier = float.Parse(obj.ToString());
        }

        private void SetCredits(object obj)
        {
            CreditsMultiplier = float.Parse(obj.ToString());
        }

        public override void Start()
        {
            IResourceContainer container = Player.Player.Resources;
            container.SetResource(Resource.Credits, Mathf.RoundToInt(container.GetResource(Resource.Credits) * CreditsMultiplier));
            container.SetResource(Resource.Research, Mathf.RoundToInt(container.GetResource(Resource.Research) * ResearchMultiplier));
        }
    }
}
