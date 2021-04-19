using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers.PropertyAssembler
{
    public class ReferencePropertyAssembler : PropertyAssemblerBase
    {
        public override Type AttributeType => typeof(ModelReferenceAttribute);

        public override object Assemble(ValueModel model, Type expectedType, AssemblyContext context)
        {
            throw new NotImplementedException();
            // TODO: Implement
        }

        public override void Disassemble(ObjectField field, object obj, Type expectedType, DisassemblyContext context)
        {
            context.RequestGuid(obj, x => field.Model = new ReferenceModel(x));
        }
    }
}
