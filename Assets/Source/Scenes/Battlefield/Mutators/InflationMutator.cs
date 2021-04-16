using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Menus.PropertyMenus;
using Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Mutators
{
    public class InflationMutator : Mutator, IHasProperties
    {
        [ModelProperty]
        public float StartingCredits = 2f;
        [ModelProperty]
        public float StartingResearch = 2f;

        public void AddPropertiesTo(PropertyMenu menu)
        {
            menu.AddProperty(new NumberDefinition("Credits", StartingCredits, false, 0, float.MaxValue)).OnValueChanged += SetCredits;
            menu.AddProperty(new NumberDefinition("Research", StartingResearch, false, 0, float.MaxValue)).OnValueChanged += SetResearch;
        }

        private void SetResearch(object obj)
        {
            StartingCredits = float.Parse(obj.ToString());
        }

        private void SetCredits(object obj)
        {
            StartingResearch = float.Parse(obj.ToString());
        }

        public override void Start()
        {
            IResourceContainer container = Player.Player.Resources;
            container.SetResource(Resource.Credits, Mathf.RoundToInt(StartingCredits));
            container.SetResource(Resource.Research, Mathf.RoundToInt(StartingResearch));
            container.OnResourceChanged += OnResourceChanged;
        }

        private void OnResourceChanged(Resource arg1, int arg2, int arg3)
        {
            throw new System.NotImplementedException();
        }
    }
}
