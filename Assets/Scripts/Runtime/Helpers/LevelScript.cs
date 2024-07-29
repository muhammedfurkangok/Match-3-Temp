using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Helpers
{
    [ExecuteInEditMode]
    public class LevelCreatorScript : MonoBehaviour
    {
        [Header("References")]
        public CD_LevelData allLevelData;
        public CD_GameColor colorData;

        [Header("Level Data")]
        public GameColors gameColor;
        public int currentLevelIndex;

        private LevelData _currentLevelData;
        private int _rows;
        private int _columns;
        private float _spaceModifier = 50f;

        private void OnEnable()
        {
            if (allLevelData != null)
            {
                SetCurrentLevelData();
            }
        }

        public void GenerateLevelData()
        {
            _columns = allLevelData.levelData.Width;
            _rows = allLevelData.levelData.Height;
            _currentLevelData = new LevelData
            {
                Width = _columns,
                Height = _rows,
                Grids = new GridData[_rows * _columns]
            };

            for (int x = 0; x < _rows; x++)
            {
                for (int y = 0; y < _columns; y++)
                {
                    _currentLevelData.Grids[x * _columns + y] = new GridData
                    {
                        isOccupied = false,
                        position = new Vector2Int(x, y)
                    };
                }
            }

            Debug.Log("Grid generated.");
        }

        private void SetCurrentLevelData()
        {
            _currentLevelData = allLevelData.levelData;
            _rows = _currentLevelData.Height;
            _columns = _currentLevelData.Width;
        }

        public void ToggleGridOccupancy(int x, int y)
        {
            var grid = _currentLevelData.GetGrid(x, y);
            grid.isOccupied = !grid.isOccupied;
            _currentLevelData.SetGrid(x, y, grid);
        }

        public void SetGridColor(int x, int y, Color color)
        {
            var grid = _currentLevelData.GetGrid(x, y);
            grid.gridColor = color;
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

        public Color GetSelectedGridColor()
        {
            foreach (var data in colorData.gameColorsData)
            {
                if (data.gameColor == gameColor)
                {
                    return data.color;
                }
            }
            Debug.Log("Default color returned: Color.white");
            return Color.white;
        }

        public void SaveLevelData()
        {
            allLevelData.levelData = _currentLevelData;
            Debug.Log("Level data saved.");
        }

        public void LoadLevelData()
        {
            SetCurrentLevelData();
            Debug.Log("Level data loaded.");
        }

        public void ResetGridData()
        {
            for (int x = 0; x < _rows; x++)
            {
                for (int y = 0; y < _columns; y++)
                {
                    _currentLevelData.SetGrid(x, y, new GridData
                    {
                        isOccupied = false,
                        gridColor = Color.white,
                        position = new Vector2Int(x, y)
                    });
                }
            }

            SaveLevelData(); // Ensures the ScriptableObject is also reset
            Debug.Log("Grid reset.");
        }
    }
}
