using System;

namespace Gameplay.CustomStatsSystem
{
    [Serializable]
    public enum ModificationTypes
    {
        Flat = 100,
        PercentAdd = 200,
        PercentMultiply = 300,
    }
}