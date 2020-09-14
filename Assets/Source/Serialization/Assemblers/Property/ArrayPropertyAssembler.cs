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

        public object Assemble(IPropertyModel model, Type type)
        {
            ArrayPropertyModel array = model as ArrayPropertyModel;
            Type elementType = type.GetElementType();

            object list = Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
            MethodInfo add = list.GetType().GetMethod("Add");
            MethodInfo toArray = list.GetType().GetMethod("ToArray");
            // I barfed a little writing that. Blame Unity for being outdated.

            for (int i = 0; i < array.Length; i++) 
            {
                add.Invoke(list, new object[] { _elementAssembler.Assemble(array[i], elementType) });
            }
            return toArray.Invoke(list, new object[] { });
        }

        public IPropertyModel Disassemble(object obj, Type type)
        {
            List<IPropertyModel> models = new List<IPropertyModel>();
            Type elementType = type.GetElementType();
            IEnumerable enumerable = obj as IEnumerable;

            foreach (object element in enumerable)
            {
                models.Add(_elementAssembler.Disassemble(element, elementType));
            }
            return new ArrayPropertyModel(type, models.ToArray());
        }

        public bool CanAssemble(Type type)
            => type.IsArray;
    }
}
