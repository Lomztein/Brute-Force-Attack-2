using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Scenes.Battlefield.Difficulty.Aspects
{
    public interface IDifficultyAspect
    {
        public string Name { get; }
        public string Description { get; }
        public string CategoryIdentifier { get; }

        public void Apply();
    }
}
