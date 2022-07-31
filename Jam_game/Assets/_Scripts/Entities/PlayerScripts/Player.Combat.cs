using Abilities.Base;
using UnityEngine;

namespace Entities.PlayerScripts
{
    public partial class Player
    {
        public override Ability ActiveAbility
        {
            set
            {
                base.ActiveAbility = value;
                if (value.customCursor.Enabled)
                {
                    _uiManager.CursorTexture = value.customCursor.Value;
                }
            }
        }

        public override void KillThis()
        {
            Debug.Log("Player was killed!");
            base.KillThis();
        }
    }
}