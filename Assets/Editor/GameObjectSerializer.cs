using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Assemblers.Turret;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Lomztein.BFA2.Turrets;
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

    private IGameObjectAssembler _assembler = new GameObjectAssembler();
    private GameObjectTurretAssemblyAssembler _assemblyAssembler = new GameObjectTurretAssemblyAssembler();
    
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
        if (GUILayout.Button ("Dissasemble!"))
        {
            Dissasemble();
        }
    }

    private void Dissasemble()
    {
        string path = Paths.StreamingAssets;
        string data = string.Empty;

        switch (Type)
        {
            case TargetType.Assembly:
                data = _assemblyAssembler.Dissasemble(Object.GetComponent<ITurretAssembly>()).Serialize().ToString();
                break;

            case TargetType.GameObject:
                data = _assembler.Disassemble(Object).Serialize().ToString();
                break;
        }

        path = path + Path + Object.name + ".json";
        Debug.Log(path);
        File.WriteAllText(path, data);
    }
}
