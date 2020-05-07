using Lomztein.BFA2.Modification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Turrets.ExpansionCards
{
    public interface IExpansionCard
    {
        bool ApplyTo(IExpansionCardAcceptor target);

        bool RemoveFrom(IExpansionCardAcceptor target);

        bool CompatableWith(ModdableAttribute[] target);
    }
}
