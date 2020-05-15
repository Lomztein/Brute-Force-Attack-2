using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GameObjectSerializer : EditorWindow
{
    public GameObject Object;
    public string Path;

    private IGameObjectAssembler _assembler = new GameObjectAssembler();
    
    [MenuItem("BFA2/GameObject Serializer")]
    public static void ShowWindow ()
    {
        GetWindow(typeof(GameObjectSerializer));
    }

    private void OnGUI()
    {
        Object = EditorGUILayout.ObjectField("Object", Object, typeof(GameObject), true) as GameObject;
        Path = EditorGUILayout.TextField("Path", Path);
        if (GUILayout.Button ("Dissasemble!"))
        {
            Dissasemble();
        }

        if (GUILayout.Button("Assemble!"))
        {
            Assemble();
        }
    }

    private void Dissasemble()
    {
        string path = Paths.StreamingAssets;
        IGameObjectModel model = _assembler.Dissassemble(Object);
        string data = model.Serialize().ToString();
        File.WriteAllText(path + Path, data);
    }

    private void Assemble ()
    {
        string path = Paths.StreamingAssets;

        JToken data = DataSerialization.FromFile(path + Path);
        GameObjectModel model = new GameObjectModel();
        model.Deserialize(data);
        _assembler.Assemble(model);
    }
}
