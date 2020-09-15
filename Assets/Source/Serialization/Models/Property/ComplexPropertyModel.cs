using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models.Property
{
    public class ComplexPropertyModel : IPropertyModel
    {
        public Type Type => Model.Type;
        public IObjectModel Model { get; private set; }

        public ComplexPropertyModel (IObjectModel model)
        {
            Model = model;
        }
    }
}
