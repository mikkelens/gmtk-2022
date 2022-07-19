using Gameplay.Entities.Stats;
using UnityEngine;

namespace Gameplay.Entities.PlayerScripts
{
    public partial class Player
    {
        

        public override void KillThis()
        {
            base.KillThis();
            Debug.Log("Player was killed!");
        }
    }
}