using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization
{
    public interface IPropertyModel : ISerializable
    {
        string Name { get; }
        object Value { get; }
    }
}
