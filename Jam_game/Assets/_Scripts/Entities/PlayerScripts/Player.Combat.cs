using Abilities;
using UnityEngine;

namespace Entities.PlayerScripts
{
    public partial class Player
    {
        protected override Ability ActiveAbility
        {
            set
            {
                base.ActiveAbility = value;
                if (value == null) return;
                if (!value.customCursor.Enabled) return;
                _uiManager.CursorTexture = value.customCursor.Value;
            }
        }

        public override void KillThis()
        {
            Debug.Log("Player was killed!");
            base.KillThis();
        }
    }
}