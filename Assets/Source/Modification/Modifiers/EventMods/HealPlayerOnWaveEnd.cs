using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.EventMods
{
    [CreateAssetMenu(fileName = "New HealPlayerOnWaveEnd", menuName = "BFA2/Mods/HealPlayerOnWaveEnd")]
    public class HealPlayerOnWaveEnd : ModBase
    {
        public StatInfo HealAmountInfo;
        public float HealAmountBase;
        public float HealAmountStack;
        private IStatReference _healAmount;


        public override void ApplyBase(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            _healAmount = stats.AddStat(HealAmountInfo, HealAmountBase, this);
            RoundController.Instance.OnWaveFinished += Instance_OnWaveFinished;
        }

        private void Instance_OnWaveFinished(int arg1, Enemies.Waves.WaveHandler arg2)
        {
            Player.Player.Instance.Heal(_healAmount.GetValue(), this);
        }

        public override void ApplyStack(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            stats.AddStatElement(HealAmountInfo.Identifier, new StatElement(this, HealAmountStack), this);
        }

        public override void RemoveBase(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            stats.RemoveStatElement(HealAmountInfo.Identifier, this, this);
        }

        public override void RemoveStack(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            RoundController.Instance.OnWaveFinished -= Instance_OnWaveFinished;
        }
    }
}
