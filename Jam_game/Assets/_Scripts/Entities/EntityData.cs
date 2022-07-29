using System;
using Entities.Base;
using Sirenix.OdinInspector;

namespace Entities
{
    [Serializable]
    public class EntityData
    {
        [AssetsOnly]
        public Entity prefab;
        public float relativeSpawnChance = 1f;
    }
}