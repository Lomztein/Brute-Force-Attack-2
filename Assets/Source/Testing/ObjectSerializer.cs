using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectSerializer : MonoBehaviour
{
    public GameObject Object;
    public string Path;
    
    public void DEW_IT()
    {
        string path = Paths.StreamingAssets;
        IGameObjectModel model = GameObjectModel.Create(Object);
        string data = model.Serialize().ToString();
        File.WriteAllText(path + Path + Object.name + ".json", data);
    }
}
