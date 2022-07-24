using System;
using Gameplay.Stats.DataTypes;
using Sirenix.OdinInspector;

namespace Gameplay.Stats
{
    [Serializable]
    public class StatModifier
    {
        public float value = 0;
        public ModificationTypes modificationType = ModificationTypes.Flat;
        
        // [InfoBox("You need to assign some form of stat type in order for this to work.", InfoMessageType.Warning, VisibleIf = "@BothNull")]
        
        private bool BothNull => StatNull && StatCollectionNull;
        private bool StatNull => targetStatType == null;
        private bool StatCollectionNull => targetStatTypeCollection == null;
        
        // top option
        [VerticalGroup("group/yes")]
        [EnableIf("StatCollectionNull")]
        public StatType targetStatType; // scriptable object
        
        // bottom option
        [VerticalGroup("group/yes")]
        [EnableIf("StatNull")]
        public StatTypeCollection targetStatTypeCollection; // scriptable object with other scriptable objects

        [ButtonGroup("group")]
        [DisableIf("BothNull")]
        [Button("Reset", ButtonHeight = 38)]
        public void ResetTypes()
        {
            targetStatType = null;
            targetStatTypeCollection = null;
        }
        
        public int Order => (int)modificationType;
    }
}