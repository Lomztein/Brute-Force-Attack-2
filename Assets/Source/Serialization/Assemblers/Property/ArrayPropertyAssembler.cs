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

        public object Assemble(PropertyModel model, Type implicitType)
        {
            ArrayPropertyModel array = model as ArrayPropertyModel;
            Type elementType = implicitType.GetElementType();

            object list = Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
            MethodInfo add = list.GetType().GetMethod("Add");
            MethodInfo toArray = list.GetType().GetMethod("ToArray");
            // I barfed a little writing that.

            for (int i = 0; i < array.Length; i++) 
            {
                add.Invoke(list, new object[] { _elementAssembler.Assemble(array[i], array.ImplicitType ? elementType : array[i].PropertyType) });
            }
            return toArray.Invoke(list, new object[] { });
        }

        public PropertyModel Disassemble(object obj, Type implicitType)
        {
            List<PropertyModel> models = new List<PropertyModel>();
            IEnumerable enumerable = obj as IEnumerable;

            foreach (object element in enumerable)
            {
                PropertyModel model = _elementAssembler.Disassemble(element, implicitType);
                if (element.GetType () != obj.GetType().GetElementType())
                {
                    model.MakeExplicit();
                }
                models.Add(model);
            }
            return new ArrayPropertyModel(obj.GetType(), models.ToArray());
        }

        public bool CanAssemble(Type type) => IsArray(type);

        public static bool IsArray(Type type) => type.IsArray;
    }
}
