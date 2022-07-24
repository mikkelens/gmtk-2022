using System;
using System.Linq;
using Gameplay.Stats;
using Gameplay.Stats.DataTypes;
using UnityEditor;
using UnityEngine;

namespace Tools.Editor
{
    [CustomPropertyDrawer(typeof(FloatStat)), CustomPropertyDrawer(typeof(IntStat))]
    public class StatPropertyDrawer : PropertyDrawer
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
            
            // compensation for optional variables
            float optionalCompensation = 0;
            SerializedProperty parentProperty = property.GetParent();
            Type type = EditorHelpers.GetTargetObjectOfProperty(parentProperty)?.GetType();
            if (type is { IsGenericType: true } && type.GetGenericTypeDefinition() == typeof(Optional<>))
            {
                optionalCompensation = EditorGUI.GetPropertyHeight(parentProperty.FindPropertyRelative("enabled"));
                Debug.Log($"Compensation: {optionalCompensation}");
            }

            // relative width
            
            const float baseValueDivision = 4f;
            const float currentValueDivision = 8f;

            totalWidth += optionalCompensation;
            
            baseRect.width = totalWidth / baseValueDivision;
            currentRect.width = showCurrentValue ? (totalWidth - optionalCompensation) / currentValueDivision : 0f;
            statTypeRect.width = totalWidth - (optionalCompensation + baseRect.width + currentRect.width);
            
            // position (from left)
            baseRect.width += EditorGUIUtility.labelWidth;
            statTypeRect.x += baseRect.width;
            currentRect.x += baseRect.width + statTypeRect.width;
            
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