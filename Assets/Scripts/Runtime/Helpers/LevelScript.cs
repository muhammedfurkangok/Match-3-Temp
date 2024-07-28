using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using UnityEngine;
using Grid = Runtime.Data.ValueObject.Grid;

[ExecuteInEditMode]
public class LevelEditorScript : MonoBehaviour
{
    public CD_LevelData allLevelData;
    public int currentLevelIndex;

    private LevelData _currentLevelData;
    private int _rows;
    private int _columns;
    private float _spaceModifier = 50f;

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
        allLevelData.levelData[currentLevelIndex].Grids = new Grid[_rows * _columns];
        
        for (int x = 0; x < _rows; x++)
        {
            for (int y = 0; y < _columns; y++)
            {
                allLevelData.levelData[currentLevelIndex].Grids[x * _columns + y] = new Grid
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

    public void SetGridSize(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;
    }

    public void SetSpaceModifier(float spaceModifier)
    {
        _spaceModifier = spaceModifier;
    }

    public void ToggleGridOccupancy(int x, int y)
    {
        var grid = _currentLevelData.GetGrid(x, y);
        grid.isOccupied = !grid.isOccupied;
        _currentLevelData.SetGrid(x, y, grid);
    }

    public void SetAllLevelData(CD_LevelData data)
    {
        allLevelData = data;
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

    public float GetSpaceModifier()
    {
        return _spaceModifier;
    }
}
