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
using UnityEngine;

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
                {
                    _parts = ReflectionUtils.InstantiateAllOfType<IBattlefieldAssemblerPart>().ToArray();
                    Array.Sort(_parts, (x, y) => x.AssemblyOrder - y.AssemblyOrder);
                }
                return _parts;
            }
        }

        public ObjectModel Disassemble (BattlefieldController controller, DisassemblyContext context)
        {
            var fields = new List<ObjectField>();
            foreach (var part in Parts)
            {
                try
                {
                    var field = new ObjectField(part.Identifier, part.DisassemblePart(controller, context));
                    fields.Add(field);
                }catch (Exception exc)
                {
                    Debug.Log($"Something went wrong during save disassembly of part '{part.Identifier}'");
                    Debug.LogError(exc);
                }
            }
            return new ObjectModel(fields.ToArray());
        }

        public void Assemble(BattlefieldController controller, ObjectModel data, AssemblyContext context)
        {
            foreach (var part in data)
            {
                try
                {
                    var assembler = Parts.FirstOrDefault(x => x.Identifier == part.Name);
                    assembler.AssemblePart(controller, part.Model, context);
                }catch (Exception exc)
                {
                    Debug.Log($"Something went wrong during save assembly of part '{part.Name}'");
                    Debug.LogError(exc);
                }
            }
        }

    }
}
