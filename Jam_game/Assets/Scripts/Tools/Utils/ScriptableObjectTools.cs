using System;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools
{
	public static class ScriptableObjectTools
	{
	#if UNITY_EDITOR
        public static void ConvertToType(this ScriptableObject oldObject, Type newType)
        {
        	int undoGroup = Undo.GetCurrentGroup();
        	if (oldObject == null)
        	{
        		Debug.LogWarning("Target ability was not found?");
        		return;
        	}
        	string path = AssetDatabase.GetAssetPath(oldObject);
            
            // create instance
            ScriptableObject newObject = ScriptableObject.CreateInstance(newType);
            
        	// transfer fields
            foreach (FieldInfo field in newType.GetFields())
        	{
        		field.SetValue(newObject, field.GetValue(oldObject)); // copy data over
        	}
            
            // make instance replace old object asset
            Undo.DestroyObjectImmediate(oldObject); // replace with new
            AssetDatabase.CreateAsset(newObject, path);
        	Undo.RegisterCreatedObjectUndo(newObject, $"Created {newType.Name}");
            
        	AssetDatabase.SaveAssets(); // save changes
        	AssetDatabase.Refresh();
        	Undo.CollapseUndoOperations(undoGroup);
            
        	Selection.objects = new Object[] { newObject }; // select object again

        	Debug.Log($"Converted {path.PathWithoutDirectory()} to {newType.Name}"); // success message
        }
    #endif
	}
}