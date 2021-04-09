using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.FacadeComponents
{
    public abstract class FacadeComponent
    {
        public abstract bool Active { get; }

        public abstract void Init(Facade facade);
    }
}
