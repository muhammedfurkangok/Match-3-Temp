using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Helpers
{
    [ExecuteInEditMode]
    public class LevelCreatorScript : MonoBehaviour
    {
        [Header("References")]
        public CD_LevelData allLevelData;
        public CD_GameColor color;
        
        [Header("Level Data")]
        public GameColors gameColor ;
        public int currentLevelIndex;

        private LevelData _currentLevelData;
        private int _rows;
        private int _columns;
        private float _spaceModifier = 50f;
        
        private Color selectedColor => GetSelectedColor();

        private void OnEnable()
        {
            if (allLevelData != null && allLevelData.levelData.Length > 0)
            {
                SetCurrentLevelData();
            }
        }

        public void GenerateLevelData()
        {
            _columns = allLevelData.levelData[currentLevelIndex].Width;
            _rows = allLevelData.levelData[currentLevelIndex].Height;
            allLevelData.levelData[currentLevelIndex].Grids = new GridData[_rows * _columns];
        
            for (int x = 0; x < _rows; x++)
            {
                for (int y = 0; y < _columns; y++)
                {
                    allLevelData.levelData[currentLevelIndex].Grids[x * _columns + y] = new GridData
                    {
                        isOccupied = false,
                        position = new Vector2Int(x, y)
                    };
                }
            }
        
            SetCurrentLevelData();
        }

        private void SetCurrentLevelData()
        {
            _currentLevelData = allLevelData.levelData[currentLevelIndex];
            _rows = _currentLevelData.Height;
            _columns = _currentLevelData.Width;
        }

        public Vector3 GridSpaceToWorldSpace(int x, int y)
        {
            return new Vector3(x * _spaceModifier, 0, y * _spaceModifier);
        }

        public Vector2Int WorldSpaceToGridSpace(Vector3 worldPosition)
        {
            int x = Mathf.RoundToInt(worldPosition.x / _spaceModifier);
            int y = Mathf.RoundToInt(worldPosition.z / _spaceModifier);
            return new Vector2Int(x, y);
        }


        public void ToggleGridOccupancy(int x, int y)
        {
            var grid = _currentLevelData.GetGrid(x, y);
            grid.isOccupied = !grid.isOccupied;
            _currentLevelData.SetGrid(x, y, grid);
        }

        public LevelData GetCurrentLevelData()
        {
            return _currentLevelData;
        }

        public int GetRows()
        {
            return _rows;
        }

        public int GetColumns()
        {
            return _columns;
        }

        

    }
}
