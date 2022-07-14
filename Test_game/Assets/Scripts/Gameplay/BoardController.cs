using UnityEngine;

namespace Gameplay
{
    public class BoardItem
    {
        public enum ItemType
        {
            Player,
            Obstacle,
            Enemy,
        }

        public ItemType Type;

        public BoardItem(ItemType type = ItemType.Player)
        {
            Type = type;
        }
    }
    
    public class BoardField
    {
        public BoardItem Item;
        
        public BoardField(BoardItem item = null)
        {
            Item = item;
        }
    }

    public class Board
    {
        
    }
    
    public class BoardController : MonoBehaviour
    {
        
    }
}
