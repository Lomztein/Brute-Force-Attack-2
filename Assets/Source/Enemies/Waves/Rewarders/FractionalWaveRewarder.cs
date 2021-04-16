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

        private Action<float> _rewardCallback;

        public void OnFinished()
        {
            Earn (_finishReward);
        }

        public void OnKill(IEnemy enemy)
        {
            Earn(_totalKillReward / _totalCount);
        }

        private void Earn(float value)
        {
            _rewardCallback(value);
        }

        public FractionalWaveRewarder(int total, float finishReward, float totalKillReward, Action<float> callback)
        {
            _totalCount = total;
            _finishReward = finishReward;
            _totalKillReward = totalKillReward;
            _rewardCallback = callback;
        }
    }
}
