using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Buffs;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Weaponary.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.EventMods
{
    [CreateAssetMenu(fileName = "New " + nameof(AddEnemyBuffOnHit), menuName = "BFA2/Mods/Add Enemy Buff On Hit")]
    public class AddEnemyBuffOnHit : ModBase
    {
        [ModelAssetReference]
        public EventInfo Event;
        [ModelAssetReference]
        public EnemyBuff Buff;
        [ModelAssetReference]
        public StatInfo BuffTimeInfo;
        [ModelAssetReference]
        public StatInfo BuffStacksInfo;
        [ModelAssetReference]
        public StatInfo BuffCoeffecientInfo;
        [ModelAssetReference]
        public StatInfo BuffPowerStatInfo;
        [ModelProperty]
        public float PowerStatFactor;
        [ModelProperty]
        public float BuffTime = -1;
        [ModelProperty]
        public float BuffTimePerStack;
        [ModelProperty]
        public int BuffStacks;
        [ModelProperty]
        public int BuffStacksPerStack;
        [ModelProperty]
        public int BuffCoeffecient;
        [ModelProperty]
        public int BuffCoeffecientPerStack;

        private IStatReference _time;
        private IStatReference _stacks;
        private IStatReference _coeffecient;
        private IStatReference _buffPowerStat;

        public override void ApplyBase(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            events.GetEvent(Event.Identifier).Event.AddListener(OnHit, this);
            _time = stats.AddStat(BuffTimeInfo, BuffTime, this);
            _stacks = stats.AddStat(BuffStacksInfo, BuffStacks, this);
            _coeffecient = stats.AddStat(BuffCoeffecientInfo, BuffCoeffecient, this);
            if (BuffPowerStatInfo != null)
            {
                _buffPowerStat = stats.GetStat(BuffPowerStatInfo.Identifier);
            }
        }

        public override void ApplyStack(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            stats.AddStatElement(BuffTimeInfo.Identifier, new StatElement(this, BuffTimePerStack), this);
            stats.AddStatElement(BuffStacksInfo.Identifier, new StatElement(this, BuffStacksPerStack), this);
            stats.AddStatElement(BuffCoeffecientInfo.Identifier, new StatElement(this, BuffCoeffecientPerStack), this);
        }

        public override void RemoveBase(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            events.GetEvent(Event.Identifier).Event.RemoveListener(OnHit, this);
            stats.RemoveStat(BuffTimeInfo.Identifier, this);
            stats.RemoveStat(BuffStacksInfo.Identifier, this);
            stats.RemoveStat(BuffCoeffecientInfo.Identifier, this);
        }

        public override void RemoveStack(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            stats.RemoveStatElement(BuffTimeInfo.Identifier, this, this);
            stats.RemoveStatElement(BuffStacksInfo.Identifier, this, this);
            stats.RemoveStatElement(BuffCoeffecientInfo.Identifier, this, this);
        }

        protected virtual void OnHit (EventArgs args)
        {
            if (args.TryGetArgs(out HitInfo info) && info.Collider.TryGetComponent(out Enemy enemy))
            {
                EnemyBuff newBuff = Instantiate(Buff);
                if (BuffPowerStatInfo != null)
                {
                    newBuff.Power = _buffPowerStat.GetValue() * PowerStatFactor;
                }
                newBuff.Coeffecient = _coeffecient.GetValue() * Coeffecient;
                enemy.TryAddBuff(newBuff, _time.GetValue(), Mathf.RoundToInt(_stacks.GetValue()));
            }
        }
    }
}
