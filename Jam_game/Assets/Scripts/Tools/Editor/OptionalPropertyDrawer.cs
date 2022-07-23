using UnityEditor;
using UnityEngine;

namespace Tools.Editor
{
    [CustomPropertyDrawer(typeof(Optional<>))]
    public class OptionalPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("value");
            return EditorGUI.GetPropertyHeight(valueProperty);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("value");
            SerializedProperty enabledProperty = property.FindPropertyRelative("enabled");

            Rect valueRect = position, enabledRect = position;
            
            enabledRect.height = enabledRect.width = EditorGUI.GetPropertyHeight(enabledProperty);
            valueRect.width = position.width - enabledRect.width;
            
            enabledRect.x = valueRect.width;
            
            EditorGUI.BeginProperty(position, label, property);
            int indent = EditorGUI.indentLevel;
            
            // value field
            EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue); // greyed out if not enabled
            EditorGUI.PropertyField(valueRect, valueProperty, label, true);
            EditorGUI.EndDisabledGroup();

            // enabled checkbox
            EditorGUI.indentLevel = 0;
            EditorGUI.PropertyField(enabledRect, enabledProperty, GUIContent.none);
            
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}