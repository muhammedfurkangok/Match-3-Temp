using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [System.Serializable]
    public struct GridItemData
    {
        public Vector2Int Position { get; private set; }
        public Item Item { get; private set; }

        public GridItemData(Vector2Int position, Item item)
        {
            Position = position;
            Item = item;
        }

        public void UpdatePosition(Vector2Int newPosition)
        {
            Position = newPosition;
        }
    }
}