using Runtime.Managers;
using UnityEngine;

namespace Runtime.Entities
{
    public class Item : MonoBehaviour
    {
        public Vector2Int GridPosition { get; private set; }
        public Color ItemColor { get; private set; }

        private GridManager _gridManager;

        public void Init(Vector2Int gridPosition, Color color,GridManager gridManager)
        {
            GridPosition = gridPosition;
            ItemColor = color;
            _gridManager = gridManager;
                
            _gridManager.AddItem(gridPosition, this);
            _gridManager.SetDirty();
        }

        // private void ApplyColor()
        // {
        //     Renderer renderer = GetComponent<Renderer>();
        //     if (renderer != null)
        //     {
        //         renderer.material.color = ItemColor;
        //     }
        // }

        public void MoveItem(Vector2Int newPosition)
        {
            _gridManager.UpdateItemPosition(GridPosition, newPosition);
            GridPosition = newPosition;
            transform.position = _gridManager.GridSpaceToWorldSpace(newPosition);
            _gridManager.SetDirty();
        }
    }
}