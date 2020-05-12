using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization
{
    public interface IComponentModel : ISerializable
    {
        Type Type { get; }

        IPropertyModel[] GetProperties();
    }
}
