using System;
using System.Collections.Generic;
using System.Reflection;
using Gameplay.Stats.DataTypes;

namespace Gameplay.Stats
{
    public static class StatSystem
    {
        public static bool IsStat(object objectToIdentify)
        {
            return objectToIdentify is IntStat or FloatStat;
        }
        
        public static List<GenericStat> FindAllStatsOnObject(object target)
        {
            List<GenericStat> foundStats = new List<GenericStat>();

            foreach (FieldInfo field in foundStats.GetType().GetFields())
            {
                if (field.GetValue(target) is GenericStat stat)
                // add found stat to list
                    foundStats.Add(stat);
                else if (field.GetValue(target) is StatCollection statCollection)
                // go down a level to find stats and pass them back up, recursively
                    foundStats.AddRange(FindAllStatsOnObject(statCollection));
            }
            return foundStats;
        }
    }
}