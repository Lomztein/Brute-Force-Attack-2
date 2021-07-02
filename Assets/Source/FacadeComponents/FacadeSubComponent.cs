using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.FacadeComponents
{
    public abstract class FacadeSubComponent<T> : IFacadeComponent where T : IFacadeComponent
    {
        public bool Active => GetParentComponent().Active;

        public abstract void Init();

        protected T GetParentComponent() => Facade.GetComponent<T>();
    }
}
