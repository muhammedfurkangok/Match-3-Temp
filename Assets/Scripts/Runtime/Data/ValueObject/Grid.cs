using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [System.Serializable]
    public struct Grid
    {
        public bool isOccupied;
        public Vector2Int position;
    }
}