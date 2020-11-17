using Lomztein.BFA2.Serialization.Models;
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

        public object Assemble (ObjectModel model, Type implicitType)
        {
            object obj = Activator.CreateInstance(model.ImplicitType ? implicitType : model.Type);
            _populator.Populate(obj, model);
            return obj;
        }

        public ObjectModel Disassemble(object obj) => _populator.Extract(obj);
    }
}
