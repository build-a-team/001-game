using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SceneGUI))]

public class MapEditorObject
{
    public string Name;
    public GameObject PrefabObject;
}

public class MapEditor : Editor
{
    private bool m_bMapEditorMode = false;
    private List<GameObject> m_listCreateObj = new List<GameObject>();
    private List<MapEditorObject> m_listEditorObj = new List<MapEditorObject>();

    private void OnSceneGUI()
    {
        Handles.BeginGUI();

        GUILayout.BeginArea(new Rect(20, 20, 200, 1500));

        var rect = EditorGUILayout.BeginVertical();
//rect.
        GUI.Box(rect, GUIContent.none);

        GUI.color = Color.white;

        GUILayout.BeginHorizontal();
        if (!m_bMapEditorMode)
        {
            if (GUILayout.Button("Start Map Editor"))
            {
                Debug.Log("Start Map Editor");
                m_bMapEditorMode = true;
            }
        }
        GUILayout.EndHorizontal();
        
        if (m_bMapEditorMode)
        {
            GUILayout.Space(10);
            StartMapEditorMode();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("End Map Editor"))
            {
                Debug.Log("Start Map Editor");
                m_bMapEditorMode = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
        }
        
        

        EditorGUILayout.EndVertical();


        GUILayout.EndArea();

        Handles.EndGUI();
    }

    private void StartMapEditorMode()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("AddMap"))
        {

        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Obstacle Type 1"))
        {

        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Obstacle Type 2"))
        {

        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Obstacle Type 3"))
        {

        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Obstacle Type 4"))
        {

        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Item Type 1"))
        {

        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Item Type 2"))
        {

        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
    }

    private void EndMapEditorMode()
    { }
}
