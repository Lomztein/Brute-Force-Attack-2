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

    public static class PropertyModelExtensions
    {
        public static IPropertyModel GetProperty(this IEnumerable<IPropertyModel> list, string name) => list.FirstOrDefault(x => x.Name == name);
    }
}
