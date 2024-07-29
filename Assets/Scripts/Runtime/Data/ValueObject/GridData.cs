using UnityEngine;
using Color = System.Drawing.Color;

namespace Runtime.Data.ValueObject
{
    [System.Serializable]
    public struct GridData
    {
        public bool isOccupied;
        public Color32 gridColor;
        public Vector2Int position;
    }
}