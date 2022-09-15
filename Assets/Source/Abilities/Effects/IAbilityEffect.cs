using Lomztein.BFA2.Abilities.Placements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Abilities.Effects
{
    public interface IAbilityEffect
    {
        public void Activate(AbilityPlacement placement);
    }
}
