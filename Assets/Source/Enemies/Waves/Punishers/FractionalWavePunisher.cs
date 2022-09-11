using Lomztein.BFA2.Player.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Enemies.Waves.Punishers
{
    public class FractionalWavePunisher : IWavePunisher
    {
        private const float TotalDamage = 100f;
        private int _total;

        public void Punish(Enemy enemy)
        {
            Player.Player.Instance.TakeDamage(TotalDamage / _total, enemy);
        }

        public FractionalWavePunisher(int total)
        {
            _total = total;
        }
    }
}
