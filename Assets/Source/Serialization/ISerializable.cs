using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerializable
{
    JToken Serialize();

    void Deserialize(JToken source);
}
