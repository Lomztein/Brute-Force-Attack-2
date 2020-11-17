using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssemblable
{
    PropertyModel Disassemble();

    void Assemble(PropertyModel source);
}
