using Lomztein.BFA2.ContentSystem.Assemblers;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Utilities;
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
        GameObject, Assembly, Object
    }

    public GameObject Object;
    public string Path;
    public TargetType Type;

    private string _objectTypeName;

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

        if (Type == TargetType.Object)
        {
            _objectTypeName = EditorGUILayout.TextField("Object type", _objectTypeName);
        }

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
        string name = string.Empty;
        string data = "";

        switch (Type)
        {
            case TargetType.Assembly:
                data = ObjectPipeline.SerializeObject(_assemblyAssembler.Disassemble(Object.GetComponent<TurretAssembly>())).ToString();
                name = Object.name;
                break;

            case TargetType.GameObject:
                data = ObjectPipeline.SerializeObject(_assembler.Disassemble(Object)).ToString();
                name = Object.name;
                break;

            case TargetType.Object:
                data = ObjectPipeline.UnbuildObject(Activator.CreateInstance(ReflectionUtils.GetType(_objectTypeName)), false).ToString();
                name = _objectTypeName.Replace(".", "");
                break;
        }

        path = path + Path + name + ".json";


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
