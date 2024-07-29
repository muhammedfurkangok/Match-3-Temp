using Runtime.Helpers;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LevelCreatorScript))]
    public class LevelCreatorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            LevelCreatorScript levelCreatorScript = (LevelCreatorScript)target;
            
            GUIStyle titleStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 16, 
                alignment = TextAnchor.UpperLeft 
            };


            DrawDefaultInspector();

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate Grid"))
            {
                levelCreatorScript.GenerateLevelData();
            }
            
            // EditorGUILayout.HelpBox("This is an information message.", MessageType.Info);
            
            EditorGUILayout.LabelField("Save/Load Grid", titleStyle);
            
            EditorGUILayout.BeginHorizontal();

            
            if (GUILayout.Button("Save Grid"))
            {
                // levelCreatorScript.SaveLevelData();
            }
            
            if (GUILayout.Button("Load Grid"))
            {
                // levelCreatorScript.LoadLevelData();
            }
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Grid", titleStyle);
            
            if (levelCreatorScript.GetCurrentLevelData() != null && levelCreatorScript.GetCurrentLevelData().Grids != null)
            {
                DrawGrid(levelCreatorScript);
            }
        }

        private void DrawGrid(LevelCreatorScript levelCreatorScript)
        {
            int rows = levelCreatorScript.GetRows();
            int columns = levelCreatorScript.GetColumns();
            

            for (int x = 0; x < rows; x++)
            {
                EditorGUILayout.BeginHorizontal();
                
                for (int y = 0; y < columns; y++)
                {
                    Color originalColor = GUI.backgroundColor;
                    GUI.backgroundColor = levelCreatorScript.GetCurrentLevelData().GetGrid(x, y).isOccupied ? Color.black : Color.gray;

                    if (GUILayout.Button($"{y}x{rows - 1 - x}", GUILayout.Width(50), GUILayout.Height(50)))
                    {
                        levelCreatorScript.ToggleGridOccupancy(x, y);
                    }

                    GUI.backgroundColor = originalColor;
                }
                
                EditorGUILayout.EndHorizontal();
            }
        }
        
        
    }
}