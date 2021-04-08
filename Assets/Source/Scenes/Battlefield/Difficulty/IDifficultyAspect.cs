using Lomztein.BFA2.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Battlefield.Difficulty
{
    public interface IDifficultyAspect : INamed
    {
        DifficultyAspectCategory Category { get; }

        void Apply();
    }
}
