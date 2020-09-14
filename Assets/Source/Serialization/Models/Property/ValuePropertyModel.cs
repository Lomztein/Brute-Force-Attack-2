using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Models.Property
{
    public class ValuePropertyModel : IPropertyModel
    {
        public ValuePropertyModel () { }

        public ValuePropertyModel (object value)
        {
            Value = value;
            Type = value.GetType();
        }

        public Type Type { get; private set; }
        public object Value { get; private set; }

        public T GetValue<T>() => (T)Value;
    }
}
