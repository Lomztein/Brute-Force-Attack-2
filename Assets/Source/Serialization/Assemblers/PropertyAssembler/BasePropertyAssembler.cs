using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lomztein.BFA2.Serialization.Assemblers.ObjectPopulator;

namespace Lomztein.BFA2.Serialization.Assemblers.PropertyAssembler
{
    // I'm so sorry if you have to witness this naming I've been working for like 10 hours straight on this.
    public class BasePropertyAssembler : PropertyAssemblerBase
    {
        private IValueAssembler _assembler = new ValueAssembler();

        public override Type AttributeType => typeof(ModelPropertyAttribute);

        public override void Disassemble(ObjectField field, object obj, Type expectedType, DisassemblyContext context)
        {
            field.Model = _assembler.Disassemble(obj, expectedType, context);
        }

        public override void Assemble(object obj, IAssignableMemberInfo member, ValueModel model, Type expectedType, AssemblyContext context)
        {
            member.SetValue(obj, _assembler.Assemble(model, expectedType, context));
        }
    }
}
