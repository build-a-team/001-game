using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public struct TextureToSpriteStruct
{
    public string PackingTag;
    public Object PackingObject;
}

public class TextureToSprite : EditorWindow
{
    [MenuItem("Custom/Texture to Sprite")]
    public static void ShowWindow()
    {
        TextureToSprite window = (TextureToSprite)EditorWindow.GetWindow(typeof(TextureToSprite));
    }

    string sTxt = "";
    string sAnimationName = "";
    bool bLoop = false;

    public Object Folder = null;
    public List<TextureToSpriteStruct> FolderList = new List<TextureToSpriteStruct>();

    void OnGUI()
    {
        GUILayout.Label("Packing Tag");
        sTxt = GUILayout.TextField(sTxt);
        GUILayout.Label("Sprite Folder");
        Folder = EditorGUILayout.ObjectField(Folder, typeof(Object));

        string sPath = "";

        for (int i = 0; i < FolderList.Count; i++)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(AssetDatabase.GetAssetPath(FolderList[i].PackingObject) + " / " + FolderList[i].PackingTag);
            if (GUILayout.Button("X",GUILayout.Width(20)))
            {
                FolderList.RemoveAt(i);
            }

            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add"))
        {
            if (sTxt != string.Empty)
            {
                TextureToSpriteStruct ttss = new TextureToSpriteStruct();
                ttss.PackingTag = sTxt;
                ttss.PackingObject = Folder;
                FolderList.Add(ttss);
            }
        }

        if (GUILayout.Button("Change"))
        {
            if (Folder == null || sTxt == "")
                return;

            for (int i = 0; i < FolderList.Count; i++)
            {

                sPath = AssetDatabase.GetAssetPath(FolderList[i].PackingObject);

                string[] files = Directory.GetFiles(sPath, "*.png");

                for (int j = 0; j < files.Length; j++)
                {
                    TextureImporter importer = TextureImporter.GetAtPath(files[j]) as TextureImporter;
                    importer.textureType = TextureImporterType.Sprite;
                    importer.spritePackingTag = FolderList[i].PackingTag;
                    importer.mipmapEnabled = false;
                    importer.SaveAndReimport();
                    //importer.spritesheet
                    EditorUtility.DisplayProgressBar("Progress Bar", "Now : " + (j + 1).ToString() + " Changing.....", (float)j / (float)files.Length);

                    if (i == files.Length - 1)
                    {
                        EditorUtility.ClearProgressBar();
                    }
                }

                Resources.UnloadUnusedAssets();
                System.GC.Collect();
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            EditorUtility.ClearProgressBar();
        }
        if (GUILayout.Button("Refresh"))
        {
            FolderList.Clear();
        }
    }
}
