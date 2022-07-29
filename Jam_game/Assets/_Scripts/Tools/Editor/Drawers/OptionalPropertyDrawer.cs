using UnityEditor;
using UnityEngine;

namespace Tools.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(Optional<>))]
    public class OptionalPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("value");
            return EditorGUI.GetPropertyHeight(valueProperty, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("value");
            SerializedProperty enabledProperty = property.FindPropertyRelative("enabled");

            Rect valueRect = position, enabledRect = position;
            float spacing = EditorGUIUtility.standardVerticalSpacing;

            enabledRect.width = enabledRect.height = EditorGUI.GetPropertyHeight(enabledProperty, false);
            valueRect.width = position.width - enabledRect.width;
            enabledRect.x += valueRect.width + spacing; // fixing centering issue
            valueRect.width -= spacing * 2f;

            EditorGUI.BeginProperty(position, label, property);
            
            // value field
            EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue); // greyed out if not enabled
            EditorGUI.PropertyField(valueRect, valueProperty, label, true);
            EditorGUI.EndDisabledGroup();

            // enabled checkbox
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            EditorGUI.PropertyField(enabledRect, enabledProperty, GUIContent.none);
            
            EditorGUI.EndProperty();
            EditorGUI.indentLevel = indent;
        }
    }
}