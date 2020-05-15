using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models.Property
{
    public interface IPropertyModel : ISerializable
    {
        string Name { get; }
        JToken Value { get; }
    }

    public static class PropertyModelExtensions
    {
        public static IPropertyModel GetProperty(this IEnumerable<IPropertyModel> list, string name) => list.FirstOrDefault(x => x.Name == name);
    }
}
