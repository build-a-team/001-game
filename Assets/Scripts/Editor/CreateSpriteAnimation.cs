using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Threading;

public class CreateSpriteAnimationStruct
{
    public string PackingTag { get; set; }
    public Object PackingObject { get; set; }
    public bool Loop { get; set; }
}

public class CreateSpriteAnimation : EditorWindow
{
    [MenuItem("Custom/Create Sprite Animation")]
    public static void ShowWindow()
    {
        CreateSpriteAnimation window = (CreateSpriteAnimation)EditorWindow.GetWindow(typeof(CreateSpriteAnimation));
    }

    string sTxt = "";
    string sAnimationName = "";
    bool bLoop = false;
    Object Folder = null;
    List<CreateSpriteAnimationStruct> FolderList = new List<CreateSpriteAnimationStruct>();
    bool bUseTagName = false;
    int nFrameRate = 30;

    bool EnterDown = false;

    void OnGUI()
    {
        if (GUILayout.Button("Get Sprite Editor Data"))
        {
            GetTextureToSpriteData();
        }

        GUILayout.Label("Name");
        sTxt = GUILayout.TextField(sTxt);
        GUILayout.Label("Sprite Folder");
        Folder = EditorGUILayout.ObjectField(Folder,typeof(Object));
        
        bLoop = GUILayout.Toggle(bLoop, "Loop");
        string sPath = "";
        bUseTagName = GUILayout.Toggle(bUseTagName, "Use Tag Name");
        nFrameRate = int.Parse(GUILayout.TextField(nFrameRate.ToString()));
        for (int i = 0; i < FolderList.Count; i++)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(AssetDatabase.GetAssetPath(FolderList[i].PackingObject) + " / " + FolderList[i].PackingTag + " / " + FolderList[i].Loop);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                FolderList.RemoveAt(i);
            }
            bool bEachLoop = FolderList[i].Loop;

            bEachLoop = GUILayout.Toggle(bEachLoop, "Loop");

            if (bEachLoop != FolderList[i].Loop)
                FolderList[i].Loop = bEachLoop;

            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add") || EnterDown)
        {
            EnterDown = false;
            if (sTxt != string.Empty)
            {
                CreateSpriteAnimationStruct ttss = new CreateSpriteAnimationStruct();

                if (bUseTagName)
                {
                    sPath = AssetDatabase.GetAssetPath(Folder);

                    string[] files = Directory.GetFiles(sPath, "*.png");

                    TextureImporter importer = TextureImporter.GetAtPath(files[0]) as TextureImporter;
                    sTxt = importer.spritePackingTag;
                }

                ttss.PackingTag = sTxt;
                ttss.PackingObject = Folder;
                ttss.Loop = bLoop;
                FolderList.Add(ttss);
            }
        }



        if (GUILayout.Button("Create"))
        {
            if (Folder == null || sTxt == "")
                return;

            for (int j = 0; j < FolderList.Count; j++)
            {

                sPath = AssetDatabase.GetAssetPath(FolderList[j].PackingObject);

                string[] files = Directory.GetFiles(sPath, "*.png");

                List<Sprite> listSprite = new List<Sprite>();

                for (int i = 0; i < files.Length; i++)
                {
                    //string sName = files[i].Split('\'')
                    listSprite.Add(AssetDatabase.LoadAssetAtPath<Sprite>(files[i]));
                }

                Animation anim = new Animation();
                AnimationClip clip = new AnimationClip();
                AnimationClipSettings sts = AnimationUtility.GetAnimationClipSettings(clip);
                clip.frameRate = nFrameRate;
                if (FolderList[j].Loop)
                {
                    clip.wrapMode = WrapMode.Loop;
                    sts.loopTime = true;
                    AnimationUtility.SetAnimationClipSettings(clip, sts);
                }

                sTxt = FolderList[j].PackingTag;
                
                //System.IO.
                EditorCurveBinding spriteBinding = EditorCurveBinding.PPtrCurve("", typeof(UnityEngine.UI.Image), "m_Sprite");



                //spriteBinding.type = typeof(SpriteRenderer);
                //spriteBinding.path = "";
                //spriteBinding.propertyName = "m_Sprite"; 



                ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[listSprite.Count];

                Debug.Log(sPath + "/" + " :: " + files.Length + " listSprite.Length] :: " + listSprite.Count);

                for (int i = 0; i < (listSprite.Count); i++)
                {

                    spriteKeyFrames[i] = new ObjectReferenceKeyframe();
                    spriteKeyFrames[i].time = i / clip.frameRate;
                    spriteKeyFrames[i].value = listSprite[i];
                }
                AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, spriteKeyFrames);
                //clip.setc

                //폴더명 CreatedAnimation
                AssetDatabase.CreateAsset(clip, "assets/CreatedAnimation/" + sTxt + ".anim");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                EditorUtility.DisplayProgressBar("Progress Bar", "Now : " + (j + 1).ToString() + " Changing.....", (float)j / (float)FolderList.Count);
            }
            EditorUtility.ClearProgressBar();
        }

    }

    private void GetTextureToSpriteData()
    {
        TextureToSprite tts = EditorWindow.GetWindow<TextureToSprite>();
        FolderList.Clear();

        for (int i = 0; i < tts.FolderList.Count; i++)
        {
            CreateSpriteAnimationStruct csa = new CreateSpriteAnimationStruct();
            csa.Loop = true;
            csa.PackingObject = tts.FolderList[i].PackingObject;
            csa.PackingTag = tts.FolderList[i].PackingTag;
            FolderList.Add(csa);
        }
    }

}
