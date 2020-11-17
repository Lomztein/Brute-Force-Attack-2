using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers.Custom
{
    public interface ICustomObjectAssembler
    {
        ObjectModel Disassemble(object obj);

        object Assemble(ObjectModel model, Type implicitType);

        bool CanAssemble(Type type);
    }
}
