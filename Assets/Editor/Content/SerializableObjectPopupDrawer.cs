using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Lomztein.BFA2.Editor.Content
{
    [CustomPropertyDrawer(typeof(SerializeableObjectPopupAttribute))]
    public class SerializableObjectPopupDrawer : PropertyDrawer
    {
        private static readonly string _contentPath = Path.Combine(Application.streamingAssetsPath, "Content");
        private int _selected;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();

            SerializeableObjectPopupAttribute contentScriptableEnum = attribute as SerializeableObjectPopupAttribute;
            string[] files = GetFiles(contentScriptableEnum.Path);

            string currentPath = GetSelectedAssetPath(property);

            string path = Path.ChangeExtension(BackslashTheSucker(currentPath), null);
            _selected = files.ToList().FindIndex(x => BackslashTheSucker(x).Contains(path));
 
            _selected = EditorGUI.Popup(position, label, _selected, files.Select(x => new GUIContent (Path.GetFileNameWithoutExtension(x))).ToArray());

            if (EditorGUI.EndChangeCheck())
            {
                Type parentType = property.serializedObject.targetObject.GetType(); // Stole from StackOverflow lol.
                FieldInfo info = parentType.GetField(property.propertyPath);

                string assetPath = Path.ChangeExtension("Assets" + files[_selected].Substring(Application.streamingAssetsPath.Length), ".asset");
                UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(assetPath, info.FieldType);

                property.objectReferenceValue = asset;
            }

            EditorGUI.EndProperty();
        }

        private string BackslashTheSucker (string input)
        {
            return input.Replace("/", "\\");
        }

        private string[] GetFiles (string attributePath)
        {
            return Directory.GetDirectories(_contentPath).SelectMany(x => {
                string path = Path.Combine(x, attributePath);
                if (Directory.Exists(path))
                    return Directory.GetFiles(path);
                else
                    return Array.Empty<string>();
                }).Where(x => !x.EndsWith(".meta") && !x.StartsWith("CompilationType")).Select(x => BackslashTheSucker(x)).ToArray(); // Ignore meta files.
        }

        private string GetSelectedAssetPath (SerializedProperty property)
        {
            return AssetDatabase.GetAssetPath(property.objectReferenceValue);
        }
    }
}
