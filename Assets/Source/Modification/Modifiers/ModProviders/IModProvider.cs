using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Modifiers.ModProviders
{
    public interface IModProvider
    {
        IMod Mod { get; }
    }
}
