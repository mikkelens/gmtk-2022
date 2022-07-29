using System;
using UnityEngine;

namespace Abilities.Data
{
    [Serializable]
    public class AnimationData
    {
        [Tooltip("This *has* to match the name of the animation. alternatives: 'Charge', 'MeleeAlternate'")]
        public string name = "Melee";
        [Tooltip("Should be true for attacks that can be directional")]
        public bool isDirectional = false;
    }
}