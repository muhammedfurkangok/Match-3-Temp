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
        [Header("Grid Settings")]
        public int Width;
        public int Height;
        [Range( 50f, 100f)]
        public float _spaceModifier = 50f;
        [Range( 50f, 100f)]
        public float _gridSize = 50f;
        
        
        [Header("References")]
        public CD_LevelData LevelData;
        public CD_GameColor colorData;

        [Header("Level Data")]
        public GameColors gameColor;
        
        private LevelData _currentLevelData;
       
       

        private void OnEnable()
        {
            if (LevelData != null)
            {
                SetCurrentLevelData();
            }
        }

        public void GenerateLevelData()
        {
            Height = LevelData.levelData.Width;
            Width = LevelData.levelData.Height;
            _currentLevelData = new LevelData
            {
                Width = Height,
                Height = Width,
                Grids = new GridData[Width * Height]
            };

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _currentLevelData.Grids[x * Height + y] = new GridData
                    {
                        isOccupied = false,
                        position = new Vector2Int(x, y)
                    };
                }
            }

            Debug.Log("Grid generated.");
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

        private void SetCurrentLevelData()
        {
            if (LevelData.levelData.Grids == null || LevelData.levelData.Grids.Length != _currentLevelData.Grids.Length)
            {
                LevelData.levelData.Grids = new GridData[_currentLevelData.Grids.Length];
            }
            
            _currentLevelData = new LevelData
            {
                Width = LevelData.levelData.Width,
                Height = LevelData.levelData.Height,
                Grids = new GridData[LevelData.levelData.Grids.Length]
            };

            for (int i = 0; i < LevelData.levelData.Grids.Length; i++)
            {
                _currentLevelData.Grids[i] = new GridData
                {
                    isOccupied = LevelData.levelData.Grids[i].isOccupied,
                    gridColor = LevelData.levelData.Grids[i].gridColor,
                    position = LevelData.levelData.Grids[i].position
                };
            }

            Width = _currentLevelData.Height;
            Height = _currentLevelData.Width;
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
            return Width;
        }

        public int GetColumns()
        {
            return Height;
        }

  

        public void SaveLevelData()
        {
            if (LevelData.levelData.Grids == null || LevelData.levelData.Grids.Length != _currentLevelData.Grids.Length)
            {
                LevelData.levelData.Grids = new GridData[_currentLevelData.Grids.Length];
            }
                
            for (int i = 0; i < _currentLevelData.Grids.Length; i++)
            {
                LevelData.levelData.Grids[i] = new GridData
                {
                    isOccupied = _currentLevelData.Grids[i].isOccupied,
                    gridColor = _currentLevelData.Grids[i].gridColor,
                    position = _currentLevelData.Grids[i].position
                };
            }

            LevelData.levelData.Width = _currentLevelData.Width;
            LevelData.levelData.Height = _currentLevelData.Height;
            Debug.Log("Level data saved.");
        }

        public void LoadLevelData()
        {
            SetCurrentLevelData();
            Debug.Log("Level data loaded.");
        }

        public void ResetGridData()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    //ScriptableObject
                     LevelData.levelData.SetGrid(x, y, new GridData
                     {
                         isOccupied = false,
                         gridColor = Color.white,
                         position = new Vector2Int(x, y)
                     });
                    //Editor
                    _currentLevelData.SetGrid(x, y, new GridData
                    {
                        isOccupied = false,
                        gridColor = Color.white,
                        position = new Vector2Int(x, y)
                    });
                }
            }

            Debug.Log("Grid reset.");
        }

        public Color GetGridColor(Vector2Int position)
       {
            var grid = _currentLevelData.GetGrid(position.x, position.y);
            return grid.isOccupied ? grid.gridColor : Color.white;
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
           
            return Color.white;
        }
        
        public void SetGridColor(int x, int y, Color color)
        {
            var grid = _currentLevelData.GetGrid(x, y);
            grid.gridColor = color;
            _currentLevelData.SetGrid(x, y, grid);
        }
    }
}
