using UnityEngine;
using UnityEditor;
using Runtime.Data.ValueObject;

public class LevelEditorWindow : EditorWindow
{
    private LevelData levelData;
    private int rows = 6;
    private int columns = 5;
    private float spaceModifier = 1f;

    [MenuItem("Tools/LevelCreator")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditorWindow>("Level Creator");
    }

    private void OnEnable()
    {
        levelData = new LevelData(columns, rows, spaceModifier);
    }

    private void OnGUI()
    {
        GUILayout.Label("Level Creator", EditorStyles.boldLabel);

        rows = EditorGUILayout.IntField("Rows", rows);
        columns = EditorGUILayout.IntField("Columns", columns);
        spaceModifier = EditorGUILayout.FloatField("Space Modifier", spaceModifier);

        if (GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }

        if (levelData != null)
        {
            DrawGrid();
        }
    }

    private void GenerateGrid()
    {
        levelData = new LevelData(columns, rows, spaceModifier);
    }

    private void DrawGrid()
    {
        for (int x = 0; x < rows; x++)
        {
            GUILayout.BeginHorizontal();
            for (int y = 0; y < columns; y++)
            {
                Color originalColor = GUI.backgroundColor;
                GUI.backgroundColor = levelData.grids[x, y].isOccupied ? Color.green : Color.gray;

                if (GUILayout.Button($"{y}x{(rows - 1 - x)}", GUILayout.Width(50), GUILayout.Height(50)))
                {
                    levelData.grids[x, y].isOccupied = !levelData.grids[x, y].isOccupied;
                }

                GUI.backgroundColor = originalColor;
            }
            GUILayout.EndHorizontal();
        }
    }
}