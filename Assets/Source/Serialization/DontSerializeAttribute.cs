using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization
{
    [AttributeUsage (AttributeTargets.All)]
    public class DontSerializeAttribute : Attribute
    {
    }
}
