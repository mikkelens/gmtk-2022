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

            Rect baseRect = totalRect, currentRect = totalRect, statTypeRect = totalRect;
            
            // compensation for optional variables
            bool showDetails = true;
            SerializedProperty parentProperty = property.GetParent();
            Type type = EditorHelpers.GetTargetObjectOfProperty(parentProperty)?.GetType();
            if (type is { IsGenericType: true } && type.GetGenericTypeDefinition() == typeof(Optional<>))
            {
                if (!parentProperty.FindPropertyRelative("enabled").boolValue) showDetails = false;
            }
            bool showCurrentValue = showDetails && statTypeProperty.objectReferenceValue != null;

            float spacing = EditorGUIUtility.standardVerticalSpacing;
            float totalWidth = totalRect.width - EditorGUIUtility.labelWidth;

            // relative widths
            float baseValueShare = 0.40f;
            float currentValueShare = 0.10f;
            
            // shrink stat type field if nothing is assigned
            if (!showDetails)
                baseValueShare = 1f;
            else if (!showCurrentValue)
                baseValueShare += currentValueShare;

            baseRect.width = totalWidth * baseValueShare;
            currentRect.width = showCurrentValue ? totalWidth * currentValueShare : 0f;
            statTypeRect.width = totalWidth - (baseRect.width + currentRect.width);
            
            // position (from left)
            baseRect.width += EditorGUIUtility.labelWidth;
            currentRect.x += baseRect.width;
            statTypeRect.x += baseRect.width + currentRect.width;
            
            // width with spacing
            if (showDetails) baseRect.width -= spacing;
            if (showCurrentValue) currentRect.width -= spacing;
            
            int indent = EditorGUI.indentLevel;
            EditorGUI.BeginProperty(totalRect, label, property);
            EditorGUI.PropertyField(baseRect, baseValueProperty, label, true); // base value

            if (showDetails)
            {
                if (showCurrentValue)
                {
                    EditorGUI.indentLevel = 0;
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUI.PropertyField(currentRect, currentValueProperty, GUIContent.none); // current value
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUI.indentLevel = 0;
                EditorGUI.PropertyField(statTypeRect, statTypeProperty, GUIContent.none); // stat type
            }
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}