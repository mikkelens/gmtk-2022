using System;
using Gameplay.Stats.DataTypes;
using Tools;

namespace Gameplay.Stats.Attacks
{
    [Serializable]
    public class HitStats : IStatCollection
    {
        public Optional<IntStat> damage;
        public Optional<FloatStat> knockback;
        public Optional<StatModifier> effect;
        // todo: add some effect that can be triggered on hit
    }
}