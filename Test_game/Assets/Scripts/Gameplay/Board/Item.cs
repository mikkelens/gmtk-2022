using UnityEngine;

namespace Gameplay.Board
{
    public enum ItemType
    {
        Player,
        Obstacle,
        Enemy,
    }

    // In-engine gameobject component
    public class Item : MonoBehaviour
    {
        public ItemType type;
    }
}