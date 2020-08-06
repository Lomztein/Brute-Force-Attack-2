using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers.Property
{
    public class ArrayPropertyAssembler : IPropertyAssembler
    {
        private IPropertyAssembler _elementAssembler = new AllPropertyAssemblers();

        public object Assemble(JToken model, Type type)
        {
            JArray array = model as JArray;
            Type elementType = type.GetElementType();

            object list = Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
            MethodInfo add = list.GetType().GetMethod("Add");
            MethodInfo toArray = list.GetType().GetMethod("ToArray");
            // I barfed a little writing that. Blame Unity for being outdated.

            for (int i = 0; i < array.Count; i++) 
            {
                add.Invoke(list, new object[] { _elementAssembler.Assemble(array[i], elementType) });
            }
            return toArray.Invoke(list, new object[] { });
        }

        public JToken Dissassemble(object obj, Type type)
        {
            List<JToken> tokens = new List<JToken>();
            Type elementType = type.GetElementType();
            IEnumerable enumerable = obj as IEnumerable;

            foreach (object element in enumerable)
            {
                tokens.Add(_elementAssembler.Dissassemble(element, elementType));
            }
            return new JArray(tokens.ToArray());
        }

        public bool Fits(Type type)
            => type.IsArray;
    }
}
