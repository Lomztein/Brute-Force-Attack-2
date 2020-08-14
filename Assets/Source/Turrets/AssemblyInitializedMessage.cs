using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Turrets
{
    public class AssemblyInitializedMessage
    {
        public ITurretAssembly Assembly { get; private set; }

        public AssemblyInitializedMessage(ITurretAssembly _assembly)
        {
            Assembly = _assembly;
        }
    }
}
