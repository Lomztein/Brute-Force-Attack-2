using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models.Property
{
    public class ObjectPropertyModel : IPropertyModel
    {
        public Type Type { get; private set; }
        public IObjectModel Model { get; private set; }

        public ObjectPropertyModel (IObjectModel model)
        {
            Model = model;
            Type = Model.Type;
        }
    }
}
