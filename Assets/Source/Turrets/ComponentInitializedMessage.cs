using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Turrets
{
    public class ComponentInitializedMessage
    {
        public ITurretComponent Component { get; private set; }

        public ComponentInitializedMessage (ITurretComponent component)
        {
            Component = component;
        }
    }
}
