using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class ArrayModelAssembler : IValueAssembler
    {
        private IValueAssembler _elementAssembler = new ValueAssembler();

        public object Assemble(ValueModel model, Type expectedType, AssemblyContext context)
        {
            ArrayModel array = model as ArrayModel;
            Type elementType = GetElementType (expectedType);
            bool isList = IsList(expectedType);

            object list = Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
            MethodInfo add = list.GetType().GetMethod("Add");
            // I barfed a little writing that.

            for (int i = 0; i < array.Length; i++) 
            {
                add.Invoke(list, new object[] { _elementAssembler.Assemble(array[i], array.IsTypeImplicit ? elementType : array[i].GetModelType(), context) });
            }

            if (isList)
            {
                return list;
            }
            else
            {
                MethodInfo toArray = list.GetType().GetMethod("ToArray");
                return toArray.Invoke(list, new object[] { });
            }
        }

        private Type GetElementType(Type type)
            => type.GetElementType() ?? type.GetGenericArguments()[0];

        public ValueModel Disassemble(object obj, Type expectedType, DisassemblyContext context)
        {
            List<ValueModel> models = new List<ValueModel>();
            IEnumerable enumerable = obj as IEnumerable;

            if (obj != null)
            {
                foreach (object element in enumerable)
                {
                    ValueModel model = _elementAssembler.Disassemble(element, element.GetType(), context);
                    if (element.GetType() != GetElementType(obj.GetType()))
                    {
                        model.MakeExplicit(element.GetType());
                    }
                    models.Add(model);
                }
            }

            var modelsArray = models.ToArray();
            return context.MakeReferencable (modelsArray, new ArrayModel(modelsArray));
        }

        public bool CanAssemble(Type type) => IsArray(type) || IsList(type);

        public static bool IsArray(Type type) => type.IsArray;
        public static bool IsList(Type type) => typeof(IList).IsAssignableFrom(type) && type.IsGenericType;
    }
}
