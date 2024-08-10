using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Managers
{
    public class GridManager : MonoBehaviour
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float SpaceModifier { get; private set; }

        private Dictionary<Vector2Int, Item> _itemDictionary;

        private void Awake()
        {
            _itemDictionary = new Dictionary<Vector2Int, Item>();
        }

        public void Initialize(int width, int height, float spaceModifier)
        {
            Width = width;
            Height = height;
            SpaceModifier = spaceModifier;
        }

        public void AddItem(Vector2Int position, Item item)
        {
            if (!_itemDictionary.ContainsKey(position))
            {
                _itemDictionary[position] = item;
            }
        }

        public void RemoveItem(Vector2Int position)
        {
            if (_itemDictionary.ContainsKey(position))
            {
                _itemDictionary.Remove(position);
            }
        }

        public void UpdateItemPosition(Vector2Int oldPosition, Vector2Int newPosition)
        {
            if (_itemDictionary.ContainsKey(oldPosition))
            {
                Item item = _itemDictionary[oldPosition];
                _itemDictionary.Remove(oldPosition);
                _itemDictionary[newPosition] = item;
            }
        }

        public Item GetItem(Vector2Int position)
        {
            _itemDictionary.TryGetValue(position, out Item item);
            return item;
        }

        public Vector3 GridSpaceToWorldSpace(Vector2Int gridPosition)
        {
            return new Vector3(gridPosition.x * SpaceModifier, 0, gridPosition.y * SpaceModifier);
        }

        public Vector2Int WorldSpaceToGridSpace(Vector3 worldPosition)
        {
            int x = Mathf.RoundToInt(worldPosition.x / SpaceModifier);
            int y = Mathf.RoundToInt(worldPosition.z / SpaceModifier);
            return new Vector2Int(x, y);
        }

        public void SetDirty()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            #endif
        }
    }
}
