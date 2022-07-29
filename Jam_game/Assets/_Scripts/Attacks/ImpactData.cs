﻿using System;
using System.Collections.Generic;
using Stats.Stat;
using Stats.Stat.Modifier;
using Stats.Stat.Variants;
using Tools;

namespace Attacks
{
    [Serializable]
    public class ImpactData : IStatCollection
    {
        public Optional<IntStat> damage = (IntStat)1;
        public Optional<FloatStat> knockback = (FloatStat)5f;
        
        public Optional<List<Modifier<float>>> floatEffects; // stun etc
        public Optional<List<Modifier<int>>> intEffects; // health etc
        public Optional<List<Modifier<bool>>> boolEffects; // stun etc
        
        // todo: add some visual that can be triggered on hit
    }
}