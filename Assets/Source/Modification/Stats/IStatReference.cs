using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Stats
{
    public interface IStatReference
    {
        string Identifier { get; }
        float GetValue();
        event Action OnChanged;
    }
}
