using Lomztein.BFA2.Enemies;
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
    public class MarathonMutator : Mutator, IHasProperties
    {
        public float Multiplier;

        public void AddPropertiesTo(PropertyMenu menu)
        {
            menu.AddProperty(new NumberDefinition("Multiplier", Multiplier, false, 0f, 100f)).OnValueChanged += MarathonMutator_OnValueChanged;
        }

        private void MarathonMutator_OnValueChanged(object obj)
        {
            Multiplier = float.Parse(obj.ToString());
        }

        public override void Start()
        {
            RoundController.Instance.OnNewWave += Instance_OnNewWave;
        }

        private void Instance_OnNewWave(Enemies.Waves.WaveTimeline obj)
        {
            obj.ForEach(x => x.Amount = Mathf.RoundToInt (x.Amount * Multiplier));
            obj.ForEach(x => x.Length *= Multiplier);
        }
    }
}
