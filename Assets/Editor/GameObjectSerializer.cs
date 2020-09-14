using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Assemblers.Turret;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Lomztein.BFA2.Serialization.Serializers.GameObject;
using Lomztein.BFA2.Serialization.Serializers.Turret;
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
        IGameObjectAssembler _assembler = new GameObjectAssembler();
        GameObjectModelSerializer goSerializer = new GameObjectModelSerializer();

        GameObjectTurretAssemblyAssembler _assemblyAssembler = new GameObjectTurretAssemblyAssembler();
        TurretAssemblyModelSerializer tSerializer = new TurretAssemblyModelSerializer();

        string path = Paths.StreamingAssets;
        string data = string.Empty;

        switch (Type)
        {
            case TargetType.Assembly:
                data = tSerializer.Serialize (_assemblyAssembler.Disassemble(Object.GetComponent<ITurretAssembly>())).ToString();
                break;

            case TargetType.GameObject:
                data = goSerializer.Serialize(_assembler.Disassemble(Object)).ToString();
                break;
        }

        path = path + Path + Object.name + ".json";
        Debug.Log(path);
        File.WriteAllText(path, data);
    }
}
