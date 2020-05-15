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
        private IPropertyAssembler _elementAssembler = new DefaultPropertyAssemblers();

        public object Assemble(JToken model, Type type)
        {
            JArray array = model as JArray;
            Type elementType = type.GetElementType();

            Type listType = typeof(List<>).MakeGenericType(elementType);
            dynamic list = Activator.CreateInstance(listType);
            foreach (JToken token in array)
            {
                dynamic element = _elementAssembler.Assemble(token, elementType);
                list.Add(element);
            }
            return list.ToArray();
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
