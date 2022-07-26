using System;
using System.Collections.Generic;
using Gameplay.Stats.Stat;
using Gameplay.Stats.Stat.Modifier;
using Gameplay.Stats.Stat.Variants;
using Tools;

namespace Gameplay.Attacks
{
    [Serializable]
    public class ImpactData : IStatCollection
    {
        public Optional<IntStat> damage;
        public Optional<FloatStat> knockback;
        
        public Optional<List<Modifier<float>>> floatEffects; // stun etc
        public Optional<List<Modifier<int>>> intEffects; // health etc
        public Optional<List<Modifier<bool>>> boolEffects; // stun etc
        
        // todo: add some visual that can be triggered on hit
    }
}