using Abilities;
using Abilities.Weapons;
using UnityEngine;

namespace Entities.Players
{
    public partial class Player
    {
        public override MeleeWeapon ActiveWeapon
        {
            set
            {
                base.ActiveWeapon = value;
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