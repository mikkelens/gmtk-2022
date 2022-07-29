using System;
using Entities.Base;
using Sirenix.OdinInspector;

namespace Events
{
    [Serializable]
    public class WaveEntityData
    {
        [AssetsOnly]
        public Entity prefab;
        public float relativeSpawnChance = 1f;
    }
}