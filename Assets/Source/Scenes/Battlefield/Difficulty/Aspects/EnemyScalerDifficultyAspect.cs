using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Scalers;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Menus.PropertyMenus;
using Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Scenes.Battlefield.Difficulty.Aspects
{
    public class EnemyScalerDifficultyAspect : IDifficultyAspect, IHasProperties
    {
        public string Name => "Enemy scaling";
        public string Description => "Multipliers for scaling enemy health, armor, and shields.";
        public string CategoryIdentifier => "Core.Enemies";

        [ModelProperty]
        public float HealthMult = 1.5f;
        [ModelProperty]
        public float ArmorMult = 1f;
        [ModelProperty]
        public float ShieldMult = 1f;

        public void Apply()
        {
            EnemyScaleController.Instance.AddEnemyScalers(new EnemyScaler(HealthMult, ArmorMult, ShieldMult));
        }

        public void AddPropertiesTo(PropertyMenu menu)
        {
            menu.AddProperty(new NumberDefinition("Enemy Health Scaler", HealthMult, false, 0.1f, 10f)).OnValueChanged += ShieldMultChanged;
            menu.AddProperty(new NumberDefinition("Enemy Armor Scaler", ArmorMult, false, 0.1f, 10f)).OnValueChanged += ArmorMultChanged;
            menu.AddProperty(new NumberDefinition("Enemy Shield Scaler", ShieldMult, false, 0.1f, 10f)).OnValueChanged += HealthMultChanged;
        }

        private void ShieldMultChanged(object obj)
        {
            ShieldMult = float.Parse(obj.ToString());
        }

        private void ArmorMultChanged(object obj)
        {
            ArmorMult = float.Parse(obj.ToString());
        }

        private void HealthMultChanged(object obj)
        {
            HealthMult = float.Parse(obj.ToString());
        }
    }
}
