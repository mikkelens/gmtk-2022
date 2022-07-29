using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Stats.Stat;
using Stats.Stat.Modifier;
using Stats.Type;
using UnityEngine;

namespace Management
{
    public static class StatSystem
    {
        public static List<Stat<T>> FindAllStatsOnObject<T>(this object target)
        {
            List<Stat<T>> foundStats = new List<Stat<T>>();

            foreach (FieldInfo field in foundStats.GetType().GetFields())
            {
                object value = field.GetValue(target);
                switch (value)
                {
                    case Stat<T> stat:
                        // add found stat to list
                        foundStats.Add(stat);
                        break;
                    case IStatCollection statCollection:
                        // go down a level to find stats and pass them back up recursively
                        foundStats.AddRange(FindAllStatsOnObject<T>(statCollection));
                        break;
                    case List<IStatCollection> statCollections:
                        // go down multiple ways to find stats and pass them back up recursively
                        statCollections.ForEach(collection => foundStats.AddRange(FindAllStatsOnObject<T>(collection)));
                        break;
                }
            }
            return foundStats;
        }
        
        public static void ApplyModifiers<T>(this List<Stat<T>> stats, List<Modifier<T>> modifiers)
        {
            GameManager manager = GameManager.Instance;
            if (manager == null)
            {
                Debug.LogWarning("No game manager found! Could not apply upgrades!");
                return; // we need a manager (monobehaviour) in order to use StartCoroutines
            }
            
            // upgrade appropriate stats
            foreach (Modifier<T> modifier in modifiers)
            {
                // find targets with these upgrades
                List<StatType> typeTargets = modifier.GetAllTargets();
                if (typeTargets == null) continue;
                
                // get list appropriate to modifier target types
                List<Stat<T>> appropriateStats = stats.Where(stat => stat != null && typeTargets.Contains(stat.type)).ToList();
                appropriateStats.ForEach(stat => manager.AddModifierToStat(modifier, stat));
            }
        }
    }
}