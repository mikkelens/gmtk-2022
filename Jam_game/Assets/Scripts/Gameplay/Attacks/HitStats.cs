﻿using System;
using Gameplay.Stats;
using Gameplay.Stats.DataTypes;
using Tools;

namespace Gameplay.Attacks
{
    [Serializable]
    public class HitStats : IStatCollection
    {
        public Optional<IntStat> damage;
        public Optional<FloatStat> knockback;
        // todo: add some effect that can be triggered on hit

        public HitStats(Optional<IntStat> optionalDamage = default, Optional<FloatStat> optionalKnockback = default) // full constructor
        {
            damage = optionalDamage;
            knockback = optionalKnockback;
        }
    }
}