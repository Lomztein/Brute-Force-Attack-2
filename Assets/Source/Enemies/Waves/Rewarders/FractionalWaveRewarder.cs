using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves.Rewarders
{
    public class FractionalWaveRewarder : IWaveRewarder
    {
        private float _finishReward;
        private float _totalKillReward;
        private int _totalCount;

        private float _earnings;
        private IResourceContainer _rewardTarget;

        public void OnFinished()
        {
            _rewardTarget.ChangeResource(Resource.Credits, Mathf.RoundToInt(_finishReward));
        }

        public void OnKill(IEnemy enemy)
        {
            Earn(_totalKillReward / _totalCount);
        }

        private void Earn(float value)
        {
            _earnings += value;
            int floored = Mathf.FloorToInt(_earnings);
            _rewardTarget.ChangeResource(Resource.Credits, floored);
            _earnings -= floored;
        }

        public FractionalWaveRewarder (int total, float finishReward, float totalKillReward, IResourceContainer container)
        {
            _totalCount = total;
            _finishReward = finishReward;
            _totalKillReward = totalKillReward;
            _rewardTarget = container;
        }
    }
}
