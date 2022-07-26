using Gameplay.Stats.Stat;
using Gameplay.Stats.Stat.Modifier;

namespace Management
{
    public partial class GameManager
    {
        public void AddModifierToStat<T>(Modifier<T> modifier, Stat<T> stat)
        {
            if (modifier.Timer.Enabled)
                StartCoroutine(stat.TimedModifier(modifier));
            else
                stat.AddModifier(modifier);
        }
    }
}