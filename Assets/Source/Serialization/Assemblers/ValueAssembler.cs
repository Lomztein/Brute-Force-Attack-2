using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class ValueAssembler : IValueAssembler
    {
        private static List<IValueAssembler> _assemblers;

        public ValueAssembler ()
        {
            if (_assemblers == null)
            {
                _assemblers = new List<IValueAssembler>();
                _assemblers = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IValueAssembler>(typeof(ValueAssembler), typeof (ArrayModelAssembler), typeof (ObjectAssembler), typeof (PrimitiveModelAssembler)).ToList();
                _assemblers.Add(new ArrayModelAssembler());
                _assemblers.Add(new ObjectAssembler());
                _assemblers.Add(new PrimitiveModelAssembler());
            }
        }

        public object Assemble(ValueModel model, Type implicitType)
        {
            return GetAssembler(implicitType).Assemble(model, implicitType);
        }

        public ValueModel Disassemble(object obj, Type implicitType)
        {
            var model = GetAssembler(obj != null ? obj.GetType() : implicitType).Disassemble(obj, implicitType);
            if (obj.GetType() != implicitType)
                model.MakeExplicit(obj.GetType());
            return model;
        }

        public bool CanAssemble(Type type)
        {
            return GetAssembler(type) != null;
        }

        private IValueAssembler GetAssembler(Type type)
            => _assemblers.First(x => x.CanAssemble(type));
    }
}
