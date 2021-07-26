using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Menus.PropertyMenus;
using Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Scenes.Battlefield.Mutators
{
    public class StartingWaveMutator : Mutator, IHasProperties
    {
        [ModelProperty]
        public int StartingWave;

        public void AddPropertiesTo(PropertyMenu menu)
        {
            menu.AddProperty(new NumberDefinition("Staring Wave", StartingWave, true, 0, 1024)).OnValueChanged += OnStartingWaveChanged; ;
        }

        private void OnStartingWaveChanged(object obj)
        {
            StartingWave = int.Parse(obj.ToString());
        }

        public override void Start()
        {
            RoundController.Instance.NextIndex = StartingWave;
        }
    }
}
