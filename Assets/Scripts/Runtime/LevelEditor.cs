using Runtime.Data.UnityObject;
using UnityEngine;
using UnityEditor;
using Runtime.Data.ValueObject;
using UnityEngine.Serialization;
using Grid = Runtime.Data.ValueObject.Grid;

public class LevelEditorWindow : Editor
{
    public CD_LevelData allLevelData;
    private LevelData _currentLevelData;
    
    private int _currentLevelIndex;
    private int _rows = 6;
    private int _columns = 5;
    private float _spaceModifier = 50f;

  

    private void OnEnable()
    {
        _currentLevelData = allLevelData.levelData[_currentLevelIndex - 1];
    }

    private void OnGUI()
    {
        GUILayout.Label("Level Creator", EditorStyles.boldLabel);

        _rows = EditorGUILayout.IntField("Rows", _rows);
        _columns = EditorGUILayout.IntField("Columns", _columns);
        _spaceModifier = EditorGUILayout.FloatField("Space Modifier", _spaceModifier);

        if (GUILayout.Button("Generate Grid"))
        {
            GenerateLevelData();
        }

        if (_currentLevelData != null && _currentLevelData.Grids != null)
        {
            DrawGrid();
        }
    }

    private void GenerateLevelData()
    {
        _currentLevelData.Width = _columns;
        _currentLevelData.Height = _rows;
        _currentLevelData.Grids = new Grid[_columns*_rows];
    }

    private void DrawGrid()
    {
        for (int x = 0; x < _rows; x++)
        {
            GUILayout.BeginHorizontal();
            for (int y = 0; y < _columns; y++)
            {
                Color originalColor = GUI.backgroundColor;
                GUI.backgroundColor = _currentLevelData.GetGrid(x,y).isOccupied ? Color.green : Color.gray;

                if (GUILayout.Button($"{y}x{_rows - 1 - x}", GUILayout.Width(50), GUILayout.Height(50)))
                {
                     var grid = _currentLevelData.GetGrid(x,y);
                     grid.isOccupied = !grid.isOccupied;
                }

                GUI.backgroundColor = originalColor;
            }
            GUILayout.EndHorizontal();
        }
    }

    private Vector2Int WorldSpaceToGridSpace(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x / _spaceModifier);
        int y = Mathf.RoundToInt(worldPosition.z / _spaceModifier);
        return new Vector2Int(x, y);
    }

    private Vector3 GridSpaceToWorldSpace(int x, int y)
    {
        return new Vector3(x * _spaceModifier, 0, y * _spaceModifier);
    }
}
