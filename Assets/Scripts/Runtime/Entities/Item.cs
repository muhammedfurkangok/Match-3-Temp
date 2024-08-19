using Runtime.Managers;
using UnityEngine;

namespace Runtime.Entities
{
    public class Item : MonoBehaviour
    {
        public Vector2Int GridPosition { get; private set; }
        public Color ItemColor { get; private set; }

        

        public void Init(Vector2Int gridPosition, Color color,GridManager gridManager)
        {
            GridPosition = gridPosition;
            ItemColor = color;
                
            gridManager.AddItem(this);
            gridManager.SetDirty();
        }

        // private void ApplyColor()
        // {
        //     Renderer renderer = GetComponent<Renderer>();
        //     if (renderer != null)
        //     {
        //         renderer.material.color = ItemColor;
        //     }
        // }

       
    }
}