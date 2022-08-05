using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using UnityEditor;

namespace Tools.Editor
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class EditorHelpers
    {
    #region Serialized property family
        public static SerializedProperty GetParent(this SerializedProperty aProperty)
        {
            string path = aProperty.propertyPath;
            int i = path.LastIndexOf('.');
            if (i < 0)
                return null;
            return aProperty.serializedObject.FindProperty(path.Substring(0, i));
        }
        public static SerializedProperty FindSiblingProperty(this SerializedProperty aProperty, string aPath)
        {
            SerializedProperty parent = aProperty.GetParent();
            if (parent == null)
                return aProperty.serializedObject.FindProperty(aPath);
            return parent.FindPropertyRelative(aPath);
        }
    #endregion
        
    #region To object from serialized property
        /// <summary>
        /// Gets the object the property represents.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static object GetTargetObjectOfProperty(this SerializedProperty property)
        {
            if (property == null) return null;

            string path = property.propertyPath.Replace(".Array.data[", "[");
            object obj = property.serializedObject.targetObject;
            string[] elements = path.Split('.');
            foreach (string element in elements)
            {
                if (element.Contains("["))
                {
                    string elementName = element.Substring(0, element.IndexOf("["));
                    int index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }
        private static object GetValue_Imp(object source, string name)
        {
            if (source == null)
                return null;
            Type type = source.GetType();

            while (type != null)
            {
                FieldInfo f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(source);

                PropertyInfo p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }
            return null;
        }
        private static object GetValue_Imp(object source, string name, int index)
        {
            if (GetValue_Imp(source, name) is not IEnumerable enumerable) return null;
            IEnumerator enm = enumerable.GetEnumerator();
            //while (index-- >= 0)
            //    enm.MoveNext();
            //return enm.Current;

            for (int i = 0; i <= index; i++)
            {
                if (!enm.MoveNext()) return null;
            }
            return enm.Current;
        }
    #endregion
    }
}
