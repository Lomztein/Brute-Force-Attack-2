using Lomztein.BFA2.Abilities.Effects;
using Lomztein.BFA2.Abilities.Placements;
using Lomztein.BFA2.Abilities.Visualizers;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Abilities
{
    public class CooldownAbility : Ability
    {
        [ModelProperty] public int MaxCooldown;
        [ModelProperty] public int MaxCharges = 1;

        [ModelProperty] public int CurrentCooldown;
        [ModelProperty] public int CurrentCharges;

        public override float CooldownProgress => CurrentCooldown / (float)MaxCooldown;
        public override int Charges => CurrentCharges;

        public override void Initialize()
        {
            base.Initialize();
            RoundController.Instance.OnWaveFinished += OnWaveFinished;
        }

        public override void End()
        {
            RoundController.Instance.OnWaveFinished -= OnWaveFinished;
        }

        private void OnWaveFinished(int arg1, Enemies.Waves.WaveHandler arg2)
        {
            Cooldown(1);
        }

        public override AbilityVisualizer InstantiateVisualizer()
        {
            if (Visualizer != null)
            {
                return Visualizer.Instantiate().GetComponent<AbilityVisualizer>();
            }
            return null;
        }

        public virtual void Cooldown(int amount)
        {
            CurrentCooldown = Mathf.Max(0, CurrentCooldown - amount);
            if (CurrentCooldown == 0 && CurrentCharges < MaxCharges)
            {
                CurrentCooldown = MaxCooldown;
                CurrentCharges++;
            }
            if (CurrentCharges >= MaxCharges)
            {
                CurrentCooldown = 0;
                CurrentCharges = MaxCharges;
            }
        }

        public override IEnumerable<string> GetUnavailableReasons()
        {
            foreach (var baseReason in base.GetUnavailableReasons())
            {
                yield return baseReason;
            }
            if (CurrentCharges == 0)
            {
                yield return "Cooldown: " + CurrentCooldown;
            }
        }

        public override void Activate()
        {
            if (CurrentCooldown != 0 || CurrentCharges == MaxCharges)
            {
                CurrentCooldown = MaxCooldown;
            }
            if (MaxCooldown != 0) // The ability does not have a cooldown, so charges should essentially be ignored.
            {
                CurrentCharges--;
            }
            base.Activate();
        }
    }
}
