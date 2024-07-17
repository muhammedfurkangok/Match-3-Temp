using UnityEngine;
using UnityEditor;
using Runtime.Data.ValueObject;

public class LevelEditorWindow : EditorWindow
{
    private LevelData _levelData;
    private int _rows = 6;
    private int _columns = 5;
    private float _spaceModifier = 50f;

    [MenuItem("Tools/LevelCreator")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditorWindow>("Level Creator");
    }

    private void OnEnable()
    {
        _levelData = new LevelData();
        GenerateGrid();
    }

    private void OnGUI()
    {
        GUILayout.Label("Level Creator", EditorStyles.boldLabel);

        _rows = EditorGUILayout.IntField("Rows", _rows);
        _columns = EditorGUILayout.IntField("Columns", _columns);
        _spaceModifier = EditorGUILayout.FloatField("Space Modifier", _spaceModifier);

        if (GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }

        if (_levelData != null && _levelData.isOccupied != null)
        {
            DrawGrid();
        }
    }

    private void GenerateGrid()
    {
        _levelData.Width = _columns;
        _levelData.Height = _rows;
        _levelData.isOccupied = new bool[_columns, _rows];
    }

    private void DrawGrid()
    {
        for (int x = 0; x < _rows; x++)
        {
            GUILayout.BeginHorizontal();
            for (int y = 0; y < _columns; y++)
            {
                Color originalColor = GUI.backgroundColor;
                GUI.backgroundColor = _levelData.isOccupied[y, _rows - 1 - x] ? Color.green : Color.gray;

                if (GUILayout.Button($"{y}x{_rows - 1 - x}", GUILayout.Width(50), GUILayout.Height(50)))
                {
                    _levelData.isOccupied[y, _rows - 1 - x] = !_levelData.isOccupied[y, _rows - 1 - x];
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
