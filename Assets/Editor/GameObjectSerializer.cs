using Lomztein.BFA2.Content.Assemblers;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Structures.Turrets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GameObjectSerializer : EditorWindow
{
    public enum TargetType
    {
        GameObject, Assembly
    }

    public GameObject Object;
    public string Path;
    public TargetType Type;

    [MenuItem("BFA2/GameObject Serializer")]
    public static void ShowWindow ()
    {
        GetWindow(typeof(GameObjectSerializer));
    }

    private void OnGUI()
    {
        Object = EditorGUILayout.ObjectField("Object", Object, typeof(GameObject), true) as GameObject;
        Path = EditorGUILayout.TextField("Path", Path);
        Type = (TargetType)EditorGUILayout.EnumPopup("Type", Type);
        if (GUILayout.Button ("Disasemble!"))
        {
            Disassemble();
        }
        if (GUILayout.Button("Assemble!"))
        {
            Assemble();
        }
    }

    private void Disassemble()
    {
        GameObjectAssembler _assembler = new GameObjectAssembler();
        TurretAssemblyAssembler _assemblyAssembler = new TurretAssemblyAssembler();

        string path = Paths.StreamingAssets;
        string data = "";

        switch (Type)
        {
            case TargetType.Assembly:
                data = ObjectPipeline.SerializeObject(_assemblyAssembler.Disassemble(Object.GetComponent<TurretAssembly>())).ToString();
                break;

            case TargetType.GameObject:
                data = ObjectPipeline.SerializeObject(_assembler.Disassemble(Object)).ToString();
                break;
        }

        path = path + Path + Object.name + ".json";


        File.WriteAllText(path, data);
    }

    private void Assemble()
    {
        GameObjectAssembler _assembler = new GameObjectAssembler();

        string path = Paths.StreamingAssets;
        path += Path;

        JToken data = JToken.Parse(File.ReadAllText(path));

        switch (Type)
        {
            case TargetType.GameObject:
                _assembler.Assemble(ObjectPipeline.DeserializeObject(data) as ObjectModel).SetActive(true);
                break;
        }

        Debug.Log(path);
    }
}
