using Stats.Stat;
using Stats.Stat.Modifier;

namespace Game
{
    public partial class GameManager
    {
        private int _killCount;
       
        public void IncreaseKillcount()
        {
        	_killCount++;
        	UI.UpdateKillCount(_killCount);
        }

        public void AddModifierToStat<T>(Modifier<T> modifier, Stat<T> stat)
        {
            if (modifier.Timer.Enabled)
                StartCoroutine(stat.TimedModifier(modifier));
            else
                stat.AddModifier(modifier);
        }
    }
}