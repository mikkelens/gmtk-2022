using UnityEngine;

namespace Entities.Players
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