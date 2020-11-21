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

        public object Assemble(ValueModel model, Type implicitType)
        {
            ArrayModel array = model as ArrayModel;
            Type elementType = implicitType.GetElementType();

            object list = Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
            MethodInfo add = list.GetType().GetMethod("Add");
            MethodInfo toArray = list.GetType().GetMethod("ToArray");
            // I barfed a little writing that.

            for (int i = 0; i < array.Length; i++) 
            {
                add.Invoke(list, new object[] { _elementAssembler.Assemble(array[i], array.IsTypeImplicit ? elementType : array[i].GetModelType()) });
            }
            return toArray.Invoke(list, new object[] { });
        }

        public ValueModel Disassemble(object obj, Type implicitType)
        {
            List<ValueModel> models = new List<ValueModel>();
            IEnumerable enumerable = obj as IEnumerable;

            if (obj != null)
            {
                foreach (object element in enumerable)
                {
                    ValueModel model = _elementAssembler.Disassemble(element, element.GetType());
                    if (element.GetType() != obj.GetType().GetElementType())
                    {
                        model.MakeExplicit(element.GetType());
                    }
                    models.Add(model);
                }
            }
            return new ArrayModel(models.ToArray());
        }

        public bool CanAssemble(Type type) => IsArray(type);

        public static bool IsArray(Type type) => type.IsArray;
    }
}
