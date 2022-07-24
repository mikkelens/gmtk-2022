using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gameplay.Stats;
using Gameplay.Stats.DataTypes;
using UnityEngine;

namespace Management
{
    public static class StatSystem
    {
        public static bool IsStat(this object objectToIdentify)
        {
            return objectToIdentify is IntStat or FloatStat;
        }
        
        public static List<Stat> FindAllStatsOnObject(this object target)
        {
            List<Stat> foundStats = new List<Stat>();

            foreach (FieldInfo field in foundStats.GetType().GetFields())
            {
                object value = field.GetValue(target);
                switch (value)
                {
                    case Stat stat:
                        // add found stat to list
                        foundStats.Add(stat);
                        break;
                    case IStatCollection statCollection:
                        // go down a level to find stats and pass them back up recursively
                        foundStats.AddRange(FindAllStatsOnObject(statCollection));
                        break;
                    case List<IStatCollection> statCollections:
                        // go down multiple ways to find stats and pass them back up recursively
                        statCollections.ForEach(collection => foundStats.AddRange(FindAllStatsOnObject(collection)));
                        break;
                }
            }
            return foundStats;
        }
        
        public static void ApplyModifiers(this List<Stat> stats, List<StatModifier> modifiers)
        {
            GameManager manager = GameManager.Instance;
            if (manager == null)
            {
                Debug.LogWarning("No game manager found! Could not apply upgrades!");
                return; // we need a manager (monobehaviour) in order to use StartCoroutines
            }
            
            // upgrade appropriate stats
            foreach (StatModifier modifier in modifiers)
            {
                // find targets with these upgrades
                List<StatType> typeTargets = modifier.TypeTargets;
                if (typeTargets == null) continue;
                
                // get list appropriate to modifier target types
                List<Stat> appropriateStats = stats.Where(stat => stat != null && typeTargets.Contains(stat.type)).ToList();
                appropriateStats.ForEach(stat => manager.AddModifierToStat(modifier, stat));
            }
        }
    }
}