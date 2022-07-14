using UnityEngine;

namespace Gameplay.Board
{
    public enum ItemType
    {
        Obstacle,
        Enemy,
        Player,
    }

    // In-engine gameobject component
    public class Item : MonoBehaviour
    {
        public ItemType type;
    }
}