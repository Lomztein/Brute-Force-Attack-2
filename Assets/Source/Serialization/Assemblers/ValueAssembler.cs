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
                // Add all specific assemblers first.
                _assemblers = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IValueAssembler>(typeof(ValueAssembler), typeof (ArrayModelAssembler), typeof (ObjectAssembler), typeof (PrimitiveModelAssembler)).ToList();

                // Add "generic" assemblers as fallbacks.
                _assemblers.Add(new ArrayModelAssembler());
                _assemblers.Add(new ObjectAssembler());
                _assemblers.Add(new PrimitiveModelAssembler());
                
            }
        }

        public object Assemble(ValueModel model, Type expectedType, AssemblyContext context)
        {
            if (model is NullModel)
                return null;

            Type type = model.IsTypeImplicit ? expectedType : model.GetModelType();
            return GetAssembler(type).Assemble(model, type, context);
        }

        public ValueModel Disassemble(object obj, Type expectedType, DisassemblyContext context)
        {
            if (obj == null)
                return new NullModel();

            var model = GetAssembler(obj.GetType()).Disassemble(obj, expectedType, context);
            if (obj.GetType() != expectedType)
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
