using Lomztein.BFA2.Serialization.Assemblers.Custom;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class ObjectAssembler
    {
        private ObjectPopulator _populator = new ObjectPopulator();
        private ICustomObjectAssembler[] _customAssemblers;

        private ICustomObjectAssembler[] GetCustomAssemblers ()
        {
            if (_customAssemblers == null)
            {
                _customAssemblers = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<ICustomObjectAssembler>().ToArray();
            }
            return _customAssemblers;
        }

        private ICustomObjectAssembler GetCustomAssembler(Type type) => GetCustomAssemblers().FirstOrDefault(x => x.CanAssemble(type));

        public object Assemble (ObjectModel model, Type implicitType)
        {
            Type type = model.ImplicitType ? implicitType : model.Type;
            ICustomObjectAssembler custom = GetCustomAssembler(type);
            if (custom != null)
            {
                return custom.Assemble(model, implicitType);
            }
            else
            {
                object obj = Activator.CreateInstance(model.ImplicitType ? implicitType : model.Type);
                _populator.Populate(obj, model);
                return obj;
            }
        }

        public ObjectModel Disassemble(object obj)
        {
            Type type = obj.GetType();
            ICustomObjectAssembler custom = GetCustomAssembler(type);
            if (custom != null)
            {
                return custom.Disassemble(obj);
            }
            else
            {
                return _populator.Extract(obj);
            }
        }
    }
}
