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
        public string Name { get; private set; }
        public PropertyModel Model { get; private set; }

        public ObjectField(string name, PropertyModel model)
        {
            Name = name;
            Model = model;
        }
    }
}
