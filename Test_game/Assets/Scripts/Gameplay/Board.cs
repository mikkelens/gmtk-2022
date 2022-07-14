using System;

namespace Gameplay
{
    [Serializable]
    public class Board
    {
        // Definition
        [Serializable]
        public class Item
        {
            [Serializable]
            public enum ItemType
            {
                Player,
                Obstacle,
                Enemy,
            }

            public ItemType type;

            public Item(ItemType type = ItemType.Player)
            {
                this.type = type;
            }
        }
        [Serializable]
        public class Field
        {
            public Item item;
            
            public Field(Item item = null)
            {
                this.item = item;
            }
        }

        // Instances
        public Field[,] Fields;
    }
}