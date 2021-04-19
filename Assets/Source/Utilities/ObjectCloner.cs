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
        private RootModel _modelCache;
        private RootAssembler _assembler = new RootAssembler();

        public T Clone<T>(T original)
        {
            if (_modelCache == null)
            {
                _modelCache = _assembler.Disassemble(original);
            }

            return _assembler.Assemble<T>(_modelCache);
        }
    }

    public static class CloneExtensions
    {
        /// <summary>
        /// Deeply clones an object using the polymorphic deep object serialization system. Probably quite slow. For repeated clones, use an ObjectCloner instead.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="original">Original</param>
        /// <returns>Clone</returns>
        public static T DeepClone<T>(this T original)
        {
            ObjectCloner cloner = new ObjectCloner();
            return cloner.Clone(original);
        }
    }
}
