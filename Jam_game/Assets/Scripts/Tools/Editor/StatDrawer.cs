using Gameplay.Stats;
using Gameplay.Stats.DataTypes;
using UnityEditor;
using UnityEngine;

namespace Tools.Editor
{
    [CustomPropertyDrawer(typeof(FloatStat)), CustomPropertyDrawer(typeof(IntStat))]
    public class StatDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("baseValue");
            return EditorGUI.GetPropertyHeight(valueProperty);
        }

        public override void OnGUI(Rect totalRect, SerializedProperty property, GUIContent label)
        {
            // refresh current value since we cannot get from property directly
            object target = EditorHelpers.GetTargetObjectOfProperty(property);
            target.GetType().GetProperty("CurrentValue")?.GetValue(target);

            SerializedProperty baseValueProperty = property.FindPropertyRelative("baseValue");
            SerializedProperty currentValueProperty = property.FindPropertyRelative("value");
            SerializedProperty statTypeProperty = property.FindPropertyRelative("type");
            bool showCurrentValue = statTypeProperty.objectReferenceValue != null;
            
            Rect baseRect = totalRect, currentRect = totalRect, statTypeRect = totalRect;

            float spacing = EditorGUIUtility.standardVerticalSpacing;
            float totalWidth = totalRect.width - EditorGUIUtility.labelWidth;

            // relative width
            baseRect.width = (totalWidth / 4f);
            currentRect.width = showCurrentValue ? totalWidth / 8f : 0f;
            statTypeRect.width = totalWidth - (baseRect.width + currentRect.width);
            // position (from left)
            baseRect.width += EditorGUIUtility.labelWidth;
            currentRect.x += baseRect.width + statTypeRect.width;
            statTypeRect.x += baseRect.width;
            // width with spacing
            baseRect.width -= spacing;
            if (showCurrentValue) statTypeRect.width -= spacing;
            
            int indent = EditorGUI.indentLevel;
            EditorGUI.BeginProperty(totalRect, label, property);
            EditorGUI.PropertyField(baseRect, baseValueProperty, label, true); // base value

            EditorGUI.indentLevel = 0;
            EditorGUI.PropertyField(statTypeRect, statTypeProperty, GUIContent.none); // stat type
            if (showCurrentValue)
            {
                
                EditorGUI.indentLevel = 0;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.PropertyField(currentRect, currentValueProperty, GUIContent.none); // current value
                EditorGUI.EndDisabledGroup();
            }
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}