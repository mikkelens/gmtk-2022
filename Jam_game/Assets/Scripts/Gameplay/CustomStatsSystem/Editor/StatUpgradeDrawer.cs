using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace Gameplay.CustomStatsSystem.Editor
{
    [CustomPropertyDrawer(typeof(Upgrade))]
    public class StatUpgradeDrawer : PropertyDrawer
    {
        // Height is set to height of a value property
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("value");
            return EditorGUI.GetPropertyHeight(valueProperty);
        }

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            // display value and type inline
            SerializedProperty valueProperty = property.FindPropertyRelative("value");
            SerializedProperty typePropety = property.FindPropertyRelative("type");

            rect.width /= 2;
            Rect rectLeft = rect, rectRight = rect;

            EditorGUI.BeginProperty(rectLeft, label, property);
            
            // display value
            EditorGUI.PropertyField(rect, valueProperty, label, true);

            // display type
            rectRight.x += rect.width;
            EditorGUI.PropertyField(rectRight, typePropety, new GUIContent("Upgrade Type"));
            EditorGUI.EndProperty();
        }
    }
}