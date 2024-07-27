using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [System.Serializable]
    public class LevelData
    {
        public int Width;
        public int Height;
        public Grid[] Grids;
    
        public Grid GetGrid(int x, int y)
        {
            return Grids[x*Height + y];
        }
    }
    
}