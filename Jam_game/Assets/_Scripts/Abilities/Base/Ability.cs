using System;
using System.Reflection;
using Abilities.Data;
using Abilities.Weapons;
using Stats.Stat;
using Stats.Stat.Variants;
using Tools;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Abilities.Base
{
	public abstract class Ability : ExpandableScriptableObject, IStatCollection
	{
        public Optional<LayerMask> targetMask;
		
		public Optional<FloatStat> activationDelay; // should be off for player probably?
		public Optional<FloatStat> cooldown = (FloatStat)1f;
        public Optional<FloatStat> usageSelfKnockback = (FloatStat)5f;
		public Optional<Vector2> originOffset;
		
        public Optional<AnimationData> usageAnimation;
		public Optional<Texture2D> customCursor;


		public abstract void UseAbility();

		
		
		
	#if UNITY_EDITOR
		protected static void ConvertObjectToType(Object target, Type type)
		{
			if (target == null)
			{
				Debug.LogWarning("Target weapon was not found?");
				return;
			}
			string path = AssetDatabase.GetAssetPath(target);
            
			// create new weapon
			MeleeWeapon newWeapon = CreateInstance(type) as MeleeWeapon;
			foreach (FieldInfo field in typeof(MeleeWeapon).GetFields())
			{
				field.SetValue(newWeapon, field.GetValue(target)); // copy data over
			}
            
			int undoGroup = Undo.GetCurrentGroup();
            
			Undo.DestroyObjectImmediate(target); // replace with new
            
			AssetDatabase.CreateAsset(newWeapon, path);
			Undo.RegisterCreatedObjectUndo(newWeapon, $"Created {newWeapon!.GetType().Name}");
            
			AssetDatabase.SaveAssets(); // save changes
			AssetDatabase.Refresh();
			Undo.CollapseUndoOperations(undoGroup);
            
			Selection.objects = new Object[] { newWeapon }; // select object again

			Debug.Log($"Converted {path.PathWithoutDirectory()} to {newWeapon!.GetType().Name}"); // success message
		}
	#endif
	}
}
