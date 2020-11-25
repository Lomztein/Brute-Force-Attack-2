using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Utilities
{
    // TODO: Somehow mark as non-assembalable.
    public class ObjectCloner
    {
        private ValueModel _modelCache;
        private ValueAssembler _assembler = new ValueAssembler();

        public T Clone<T>(T original)
        {
            if (_modelCache == null)
            {
                _modelCache = _assembler.Disassemble(original, typeof(T));
            }

            return (T)_assembler.Assemble(_modelCache, typeof(T));
        }
    }
}
