using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Tools.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(Possible<>))]
    public class PossiblePropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("value");
            return EditorGUI.GetPropertyHeight(valueProperty, true);
        }

        public override void OnGUI(Rect totalRect, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("value");
            SerializedProperty chanceProperty = property.FindPropertyRelative("chance");

            Rect valueRect = totalRect, chanceRect = totalRect;
            float totalWidth = totalRect.width - EditorGUIUtility.labelWidth;
            float spacing = EditorGUIUtility.standardVerticalSpacing;

            // 2 parts
            float chanceShare = 0.5f;
            chanceRect.width = totalWidth * chanceShare;
            valueRect.width = totalWidth - chanceRect.width;

            valueRect.width += EditorGUIUtility.labelWidth;
            chanceRect.x += valueRect.width; // fixing centering issue
            valueRect.width -= spacing;
            
            // subpart
            float labelShare = 0.5f;
            Rect chanceLabelRect = chanceRect, chancePropertyRect = chanceRect;
            chanceLabelRect.width = chanceRect.width * labelShare;
            chancePropertyRect.width = chanceRect.width - chanceLabelRect.width;
            
            chancePropertyRect.x += chanceLabelRect.width;
            chanceLabelRect.width -= spacing;
            
            
            int indent = EditorGUI.indentLevel;
            EditorGUI.BeginProperty(totalRect, label, property);

            // value field
            EditorGUI.PropertyField(valueRect, valueProperty, label);

            // chance field
            EditorGUI.indentLevel = 0;
            EditorGUI.LabelField(chanceLabelRect, $"{chanceProperty.displayName} (%)");
            EditorGUI.PropertyField(chancePropertyRect, chanceProperty, GUIContent.none);
            EditorGUI.EndProperty();
            EditorGUI.indentLevel = indent;
        }
    }
}