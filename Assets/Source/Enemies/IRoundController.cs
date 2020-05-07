using System;

namespace Lomztein.BFA2.Enemies
{
    public interface IRoundController
    {
        void InvokeDelayed(Action callback, float time);

        void OnEnemyDeath(IEnemy enemy);
    }
}