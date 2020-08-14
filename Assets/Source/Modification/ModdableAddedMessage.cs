using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification
{
    public class ModdableAddedMessage
    {
        public IModdable Moddable { get; private set; }

        public ModdableAddedMessage (IModdable moddable)
        {
            Moddable = moddable;
        }
    }
}
