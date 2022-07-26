using System;
using Gameplay.Entities.Base;
using Sirenix.OdinInspector;

namespace Events
{
    [Serializable]
    public class EntityData
    {
        public float relativeSpawnChance = 1f;
        [AssetsOnly]
        public Entity prefab;
    }
}