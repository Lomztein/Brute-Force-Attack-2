using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Models
{
    [Serializable]
    public class ArrayModel : ValueModel, IEnumerable<ValueModel>
    {
        public Type ElementType => GetModelType().GetElementType();
        [SerializeReference]
        public ValueModel[] Elements;
        public ValueModel this[int i] => Elements[i];
        public int Length => Elements.Length;

        public ArrayModel (params ValueModel[] elements) : this((IEnumerable<ValueModel>) elements)
        {
        }

        public ArrayModel (IEnumerable<ValueModel> elements)
        {
            Elements = elements.ToArray();
        }

        public IEnumerator<ValueModel> GetEnumerator()
        {
            return ((IEnumerable<ValueModel>)Elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ValueModel>)Elements).GetEnumerator();
        }

        public override string ToString()
        {
            return base.ToString() + $" [{Length}]";
        }
    }
}
