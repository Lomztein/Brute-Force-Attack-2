using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssemblable
{
    ValueModel Disassemble(DisassemblyContext context);

    void Assemble(ValueModel source, AssemblyContext context);
}
