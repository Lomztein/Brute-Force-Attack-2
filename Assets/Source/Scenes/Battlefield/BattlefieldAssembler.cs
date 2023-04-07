using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Scenes.Battlefield.BattlefieldAssemblerParts;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Scenes.Battlefield
{
    public class BattlefieldAssembler
    {
        private static IBattlefieldAssemblerPart[] _parts;
        private static IBattlefieldAssemblerPart[] Parts
        {
            get
            {
                if (_parts == null)
                    _parts = ReflectionUtils.InstantiateAllOfType<IBattlefieldAssemblerPart>().ToArray();
                return _parts;
            }
        }

        public ObjectModel Disassemble (BattlefieldController controller, DisassemblyContext context)
        {
            var fields = new List<ObjectField>();
            foreach (var part in Parts)
            {
                fields.Add(new ObjectField(part.Identifier, part.DisassemblePart(controller, context)));
            }
            return new ObjectModel(fields.ToArray());
        }

        public void Assemble(BattlefieldController controller, ObjectModel data, AssemblyContext context)
        {
            foreach (var part in data)
            {
                var assembler = Parts.FirstOrDefault(x => x.Identifier == part.Name);
                assembler.AssemblePart(controller, part.Model, context);
            }
        }

    }
}
