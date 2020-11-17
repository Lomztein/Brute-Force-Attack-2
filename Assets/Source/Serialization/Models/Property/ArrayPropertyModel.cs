using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models.Property
{
    public class ArrayPropertyModel : PropertyModel, IEnumerable<PropertyModel>
    {
        public Type ElementType => PropertyType.GetElementType();

        public PropertyModel[] Elements { get; private set; }
        public PropertyModel this[int i] => Elements[i];
        public int Length => Elements.Length;

        public ArrayPropertyModel (Type type, params PropertyModel[] elements) : this(type, (IEnumerable<PropertyModel>) elements)
        {
        }

        public ArrayPropertyModel (Type type, IEnumerable<PropertyModel> elements)
        {
            PropertyType = type;
            Elements = elements.ToArray();
        }

        public IEnumerator<PropertyModel> GetEnumerator()
        {
            return ((IEnumerable<PropertyModel>)Elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<PropertyModel>)Elements).GetEnumerator();
        }
    }
}
