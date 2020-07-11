using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Enemies.Waves.Punishers
{
    public interface IWavePunisher
    {
        void Punish(IEnemy enemy);
    }
}
