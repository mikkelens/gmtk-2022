using System;
using Stats.Stat;
using Stats.Stat.Modifier;
using Stats.Stat.Variants;
using Tools;

namespace Abilities.Data
{
    [Serializable]
    public class ImpactData : IStatCollection
    {
        public Optional<IntStat> damage = (IntStat)1;
        public Optional<IntStat> healing = (IntStat)0;
        public Optional<FloatStat> knockback = (FloatStat)5f;

        public Optional<Effect> effects;
    }
}