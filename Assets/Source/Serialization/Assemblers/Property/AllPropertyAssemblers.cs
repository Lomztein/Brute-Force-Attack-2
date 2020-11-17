using Lomztein.BFA2.Serialization.Models.Property;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers.Property
{
    public class AllPropertyAssemblers : IPropertyAssembler
    {
        private static List<IPropertyAssembler> _assemblers;

        public AllPropertyAssemblers ()
        {
            if (_assemblers == null)
            {
                _assemblers = new List<IPropertyAssembler>();
                _assemblers = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IPropertyAssembler>(typeof(AllPropertyAssemblers), typeof (ArrayPropertyAssembler), typeof (ComplexPropertyAssembler), typeof (PrimitivePropertyAssembler)).ToList();
                _assemblers.Add(new ArrayPropertyAssembler());
                _assemblers.Add(new ComplexPropertyAssembler());
                _assemblers.Add(new PrimitivePropertyAssembler());
            }
        }

        public object Assemble(PropertyModel model, Type implicitType)
        {
            return GetAssembler(implicitType).Assemble(model, implicitType);
        }

        public PropertyModel Disassemble(object obj, Type implicitType)
        {
            return GetAssembler(obj != null ? obj.GetType() : implicitType).Disassemble(obj, implicitType);
        }

        public bool CanAssemble(Type type)
        {
            return GetAssembler(type) != null;
        }

        private IPropertyAssembler GetAssembler(Type type)
            => _assemblers.First(x => x.CanAssemble(type));
    }
}
