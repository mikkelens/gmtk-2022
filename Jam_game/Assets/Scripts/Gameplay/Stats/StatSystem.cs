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
        
        public static IEnumerable<GenericStat> FindAllStatsOnObject(object target)
        {
            List<GenericStat> foundStats = new List<GenericStat>();

            foreach (FieldInfo field in foundStats.GetType().GetFields())
            {
                object value = field.GetValue(target);
                switch (value)
                {
                    case GenericStat stat:
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
    }
}