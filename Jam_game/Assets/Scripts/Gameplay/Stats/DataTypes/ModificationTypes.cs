using System;

namespace Gameplay.Stats.DataTypes
{
    [Serializable]
    public enum ModificationTypes
    {
        Flat = 100,
        PercentAdd = 200,
        PercentMultiply = 300,
    }
}