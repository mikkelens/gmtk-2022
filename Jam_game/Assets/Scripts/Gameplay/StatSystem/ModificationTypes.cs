using System;

namespace Gameplay.StatSystem
{
    [Serializable]
    public enum ModificationTypes
    {
        Flat = 100,
        PercentAdd = 200,
        PercentMultiply = 300,
    }
}