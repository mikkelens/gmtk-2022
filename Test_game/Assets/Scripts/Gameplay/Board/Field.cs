using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Board
{
    public enum FieldType
    {
        Flat,
        Hill,
        Void,
    }
    
    // In-engine gameobject component
    public class Field : MonoBehaviour
    {
        // own details
        public FieldType type;
        
        // exterior details
        [AssetsOnly]
        public Item item;
    }
}