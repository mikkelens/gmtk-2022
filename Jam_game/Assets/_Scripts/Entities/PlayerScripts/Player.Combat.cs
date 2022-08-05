using Abilities;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Entities.PlayerScripts
{
    public partial class Player
    {
        [Header("Player specific")]
        [FoldoutGroup(StatCategory)]
        [SerializeField] protected Optional<AbilityKit> startingKit;
        
        private Ability _lastSelectedAbility;
        
        private AbilityKit _givenKit;
        public AbilityKit ActiveKit
        {
            protected get
            {
                if (_givenKit != null) return _givenKit;
                if (startingKit.Enabled) return startingKit.Value;
                return null;
            }
            set
            {
                _givenKit = value; // to be used by a manager thing? selectable at game start is the idea
            }
        }


        protected override Ability SelectAbility()
        {
            // choose
            Ability newAbility = null;
            if (_pressedPrimarySinceUse || ActiveKit.primary.continuousUsage && _holdingPrimary)
                newAbility = ActiveKit.primary;
            if (ActiveKit.secondary.Enabled &&
                (_pressedSecondarySinceUse || ActiveKit.secondary.Value.continuousUsage && _holdingSecondary))
                newAbility = ActiveKit.secondary.Value;
            
            // maybe switch cursor
            if (newAbility != null  && newAbility != _lastSelectedAbility && newAbility.customCursor.Enabled)
                _uiManager.CursorTexture = newAbility.customCursor.Value;
            
            return _lastSelectedAbility = newAbility;
        }

        protected override void StartAbilityUse()
        {
            // remember that player selected (used) ability
            if (ChosenAbility == ActiveKit.primary) _pressedPrimarySinceUse = false;
            if (ChosenAbility == ActiveKit.secondary.Value) _pressedSecondarySinceUse = false;
            base.StartAbilityUse();
        }
        
        public override void KillThis()
        {
            Debug.Log("Player was killed!");
            base.KillThis();
        }
    }
}
