using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [System.Serializable]
    public struct GridCell
    {
        public bool isOccupied;
    }

    [System.Serializable]
    public class LevelData
    {
        public int width;
        public int height;
        public GridCell[,] grids;
        public float spaceModifier;

        public LevelData(int width, int height, float spaceModifier)
        {
            this.width = width;
            this.height = height;
            this.spaceModifier = spaceModifier;
            grids = new GridCell[width, height];
        }

        public Vector2Int WorldSpaceToGridSpace(Vector3 worldPosition)
        {
            int x = Mathf.RoundToInt(worldPosition.x / spaceModifier);
            int y = Mathf.RoundToInt(worldPosition.z / spaceModifier);
            return new Vector2Int(x, y);
        }

        public Vector3 GridSpaceToWorldSpace(int x, int y)
        {
            float worldX = x * spaceModifier;
            float worldZ = y * spaceModifier;
            return new Vector3(worldX, 0, worldZ);
        }
    }
}