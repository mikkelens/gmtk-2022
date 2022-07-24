using Gameplay.Stats.DataTypes;

namespace Management
{
    public partial class GameManager
    {
        public void AddModifierToStat(StatModifier modifier, Stat stat)
        {
            if (modifier.Timer.Enabled)
                StartCoroutine(stat.TimedModifier(modifier));
            else
                stat.AddModifier(modifier);
        }
    }
}