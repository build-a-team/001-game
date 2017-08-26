using UnityEngine;
using UnityEditor;
using System.Collections;

public static class TestGUI
{

    static TestGUI()
    {
        SceneView.onSceneGUIDelegate += OnScene;
    }
    static void OnScene(SceneView sceneView)
    {
        // Draw GUI stuff here for Scene window display

        if (GUILayout.Button("Start Map Editor"))
        {
            Debug.Log("Start Map Editor");
        }
    }

}
