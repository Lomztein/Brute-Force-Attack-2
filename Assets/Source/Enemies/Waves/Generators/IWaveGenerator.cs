using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Enemies.Waves.Generators
{
    public interface IWaveGenerator
    {
        bool CanGenerate(int wave);

        SpawnInterval Generate(float startTime, float length, float baseFrequency, int wave, int seed);
    }
}
