using Lomztein.BFA2.Serialization.DataStruct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerializable
{
    IDataStruct Serialize();

    void Deserialize(IDataStruct data);
}
