using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssemblable
{
    ValueModel Disassemble();

    void Assemble(ValueModel source);
}
