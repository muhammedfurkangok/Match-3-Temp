using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelEditorScript))]
public class LevelEditorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        LevelEditorScript levelEditorScript = (LevelEditorScript)target;

        DrawDefaultInspector();

        EditorGUILayout.Space();

        if (GUILayout.Button("Generate Grid"))
        {
            levelEditorScript.GenerateLevelData();
        }

        if (levelEditorScript.GetCurrentLevelData() != null && levelEditorScript.GetCurrentLevelData().Grids != null)
        {
            DrawGrid(levelEditorScript);
        }
    }

    private void DrawGrid(LevelEditorScript levelEditorScript)
    {
        int rows = levelEditorScript.GetRows();
        int columns = levelEditorScript.GetColumns();

        for (int x = 0; x < rows; x++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int y = 0; y < columns; y++)
            {
                Color originalColor = GUI.backgroundColor;
                GUI.backgroundColor = levelEditorScript.GetCurrentLevelData().GetGrid(x, y).isOccupied ? Color.green : Color.gray;

                if (GUILayout.Button($"{y}x{rows - 1 - x}", GUILayout.Width(50), GUILayout.Height(50)))
                {
                    levelEditorScript.ToggleGridOccupancy(x, y);
                }

                GUI.backgroundColor = originalColor;
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}