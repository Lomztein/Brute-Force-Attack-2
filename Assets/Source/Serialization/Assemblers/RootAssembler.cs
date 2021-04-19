using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class RootAssembler
    {
        private IValueAssembler _rootAssembler;

        public RootAssembler (IValueAssembler rootAssembler)
        {
            _rootAssembler = rootAssembler;
        }

        public RootAssembler() : this(new ValueAssembler())
        {
        }

        public RootModel Disassemble (object obj, bool isExplicit)
        {
            Type expectedType = isExplicit ? typeof(object) : obj.GetType();
            DisassemblyContext context = new DisassemblyContext();
            ValueModel value = _rootAssembler.Disassemble(obj, expectedType, context);
            context.ReturnGuidRequests();
            return new RootModel(value, context.GetSharedReferences());
        }

        public object Assemble(RootModel model, Type expectedType)
        {
            return null;
        }

        public RootModel Disassemble(object obj) => Disassemble(obj, false);

        public T Assemble<T>(RootModel model) => (T)Assemble(model, typeof(T));
    }
}
