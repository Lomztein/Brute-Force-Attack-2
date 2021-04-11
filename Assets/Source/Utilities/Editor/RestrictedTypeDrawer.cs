using UnityEditor;
using UnityEngine;
using static Util.Editor.SerializedPropertyHelper;

namespace Util.Editor
{
    [CustomPropertyDrawer(typeof(RestrictedType<>))]
    public class RestrictedTypePropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var wrappedProp = property.FindPropertyRelative("wrapped");

            var isArray = IsListType(fieldInfo.FieldType);
            var fieldType = isArray ? GetElementTypeOfListType(fieldInfo.FieldType) : fieldInfo.FieldType;
            var genericType = fieldType.GenericTypeArguments[0];

            EditorGUI.BeginChangeCheck();
            var newVal = EditorGUI.ObjectField(position, label, wrappedProp.objectReferenceValue, genericType, true);
            if (EditorGUI.EndChangeCheck()) {
                wrappedProp.objectReferenceValue = newVal;
            }

            EditorGUI.EndProperty();
        }
    }
}