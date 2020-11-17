using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models.Property
{
    public class ComplexPropertyModel : PropertyModel
    {
        public ObjectModel Model { get; private set; }

        public ComplexPropertyModel (ObjectModel model)
        {
            Model = model;
            PropertyType = model.Type;
        }
    }
}
