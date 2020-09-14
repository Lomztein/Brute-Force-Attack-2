using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models.Property
{
    public class ArrayPropertyModel : IPropertyModel, IEnumerable<IPropertyModel>
    {
        public Type Type { get; private set; }
        public IPropertyModel[] Elements { get; private set; }
        public IPropertyModel this[int i] => Elements[i];
        public int Length => Elements.Length;

        public ArrayPropertyModel (Type type, params IPropertyModel[] elements)
        {
            Type = type;
            Elements = elements;
        }

        public ArrayPropertyModel (Type type, IEnumerable<IPropertyModel> elements)
        {
            Type = type;
            Elements = elements.ToArray();
        }

        public IEnumerator<IPropertyModel> GetEnumerator()
        {
            return ((IEnumerable<IPropertyModel>)Elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<IPropertyModel>)Elements).GetEnumerator();
        }
    }
}
