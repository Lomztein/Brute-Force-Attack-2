using Lomztein.BFA2.Enemies.Waves.Punishers;
using Lomztein.BFA2.Enemies.Waves.Rewarders;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Enemies.Waves
{
    [Serializable]
    public class WaveTimeline : IEnumerable<SpawnInterval>
    {
        public float Time => _intervals.Sum(x => x.Length);
        public float EndTime => _intervals.Max(x => x.EndTime);
        public int Amount => _intervals.Sum(x => x.Amount);
        public int IntervalAmount => _intervals.Count;

        // TODO: Refactor into a seperate Wave container class that contains Rewarders, Punishiers, and Timeline.
        // WaveTimeline is intended as a model only, having behaviour here goes against this.
        [ModelProperty, SerializeReference, SR]
        public IWaveRewarder Rewarder;
        [ModelProperty, SerializeReference, SR]
        public IWavePunisher Punisher;

        private List<SpawnInterval> _intervals = new List<SpawnInterval>();

        public IEnumerator<SpawnInterval> GetEnumerator()
        {
            return ((IEnumerable<SpawnInterval>)_intervals).GetEnumerator();
        }

        public SpawnInterval[] GetIntervals() => _intervals.ToArray();
        public void ForEach (Action<SpawnInterval> action)
        {
            foreach (SpawnInterval interval in _intervals)
            {
                action(interval);
            }
        }

        public IDictionary<string, int> GetEnemySpawnAmount ()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            var groups = _intervals.GroupBy(x => x.EnemyIdentifier);
            foreach (var id in groups)
            {
                result.Add(id.Key, id.Sum(x => x.Amount));
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<SpawnInterval>)_intervals).GetEnumerator();
        }

        public void AddSpawn (SpawnInterval spawn)
        {
            _intervals.Add(spawn);
        }

        public void RemoveSpawn (SpawnInterval spawn)
        {
            _intervals.Remove(spawn);
        }
    }
}
