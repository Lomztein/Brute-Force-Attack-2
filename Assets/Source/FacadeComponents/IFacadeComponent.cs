using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.FacadeComponents
{
    public interface IFacadeComponent
    {
        public bool Active { get; }

        public void Init();
    }
}
