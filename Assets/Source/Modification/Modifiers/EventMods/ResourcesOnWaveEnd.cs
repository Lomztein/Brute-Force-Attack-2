using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.EventMods
{
    [CreateAssetMenu(fileName = "New Resources On Wave End Mod", menuName = "BFA2/Mods/Resources On Wave End")]
    public class ResourcesOnWaveEnd : ModBase
    {
        [ModelProperty]
        public ResourceCost Resources;

        public override void ApplyBase(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            // TODO: Add OnWaveFinished as event to player, use player event container instead.
            RoundController.Instance.OnWaveFinished += Instance_OnWaveFinished;
        }

        public override void ApplyStack(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            RoundController.Instance.OnWaveFinished -= Instance_OnWaveFinished;
        }

        public override void RemoveBase(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            RoundController.Instance.OnWaveFinished -= Instance_OnWaveFinished;
        }

        public override void RemoveStack(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            RoundController.Instance.OnWaveFinished -= Instance_OnWaveFinished;
        }

        private void Instance_OnWaveFinished(int arg1, Enemies.Waves.WaveHandler arg2)
        {
            foreach (var element in Resources.Elements)
            {
                Player.Player.Instance.Earn(element.Resource, element.Value);
            }
        }
    }
}
