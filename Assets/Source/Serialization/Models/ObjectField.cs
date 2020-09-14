using Lomztein.BFA2.Serialization.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models
{
    public class ObjectField
    {
        public Type Type { get; private set; }
        public string Name { get; private set; }
        public IPropertyModel Model { get; private set; }

        public ObjectField(string name, IPropertyModel model)
        {
            Name = name;
            Model = Model;
            Type = Model.Type;
        }
    }
}
