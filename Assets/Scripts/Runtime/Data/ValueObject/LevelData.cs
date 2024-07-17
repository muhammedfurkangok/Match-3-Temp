using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [System.Serializable]
    public class LevelData
    {
        public int Width;
        public int Height;
        public bool[,] isOccupied;
    }
}