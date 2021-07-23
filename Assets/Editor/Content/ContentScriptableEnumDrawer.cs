using Lomztein.BFA2.ContentSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Lomztein.BFA2.Editor.Content
{
    [CustomPropertyDrawer(typeof(ContentScriptableEnumAttribute))]
    public class ContentScriptableEnumDrawer : PropertyDrawer
    {
        private static readonly string _contentPath = Path.Combine(Application.streamingAssetsPath, "Content");
        private int _selected;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ContentScriptableEnumAttribute contentScriptableEnum = attribute as ContentScriptableEnumAttribute;
            string[] files = GetFiles(contentScriptableEnum.Path);
            _selected = EditorGUI.Popup(position, _selected, files.Select(x => Path.GetFileName(x)).ToArray());
        }

        private string[] GetFiles (string attributePath)
        {
            return Directory.GetDirectories(_contentPath).SelectMany(x => Directory.GetFiles(Path.Combine(x, attributePath))).ToArray();
        }
    }
}
