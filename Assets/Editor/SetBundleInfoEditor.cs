using System.IO;
using UnityEngine;
using UnityEditor;

public class SetBundleInfoEditor : EditorWindow
{
    public string modelPath = "Assets/Model/G38";
    public Rect modelRect;

    public string bundleVariant = "unity3d";

    private static SetBundleInfoEditor _window;

    [SerializeField]
    public string bundleName = "g38";

    [ MenuItem ( "Window/设置Bunle" )]
    static void OpenWindow ( )
    {
        SetBundleInfoEditor window = ( SetBundleInfoEditor ) EditorWindow.GetWindow ( typeof ( SetBundleInfoEditor ) );

        window.Show ( );
    }

    public void OnGUI ( )
    {
        EditorGUILayout.Space ( );
        EditorGUILayout.LabelField ( "模型路径 (鼠标拖拽文件夹到这里)" );
        EditorGUILayout.Space ( );
        GUI.SetNextControlName ( "input1" );//设置下一个控件的名字
        modelRect = EditorGUILayout.GetControlRect ( );
        modelPath = EditorGUI.TextField ( modelRect, modelPath );
        EditorGUILayout.Space ( );
        bundleName = EditorGUILayout.TextField ( "Bundle名字", bundleName );
        DragFolder ( );

        EditorGUILayout.Space ( );

        if ( GUILayout.Button ( "设置Bundle名字" ) )
        {
            SetName ( );
        }
    }

    /// <summary>
    /// 获得拖拽文件
    /// </summary>
    void DragFolder()
    {
        //鼠标位于当前窗口
        if (mouseOverWindow == this)
        {
            //拖入窗口未松开鼠标
            if (Event.current.type == EventType.DragUpdated)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Generic;//改变鼠标外观
                // 判断区域
                if (modelRect.Contains(Event.current.mousePosition))
                    GUI.FocusControl("input1");
            }
            //拖入窗口并松开鼠标
            else if (Event.current.type == EventType.DragExited)
            {
                string dragPath = string.Join("", DragAndDrop.paths);
                // 判断区域
                if (modelRect.Contains(Event.current.mousePosition))
                    this.modelPath = dragPath;

                // 取消焦点(不然GUI不会刷新)
                GUI.FocusControl(null);
            }
        }
    }

    void SetName ( )
    {
        string[] _modelPath = AssetDatabase.FindAssets("t:GameObject", new string[] { modelPath });
        foreach ( string item in _modelPath )
        {            
            string filePath = AssetDatabase.GUIDToAssetPath ( item );
            AssetImporter _impoter = AssetImporter.GetAtPath ( filePath );
            _impoter.assetBundleName = bundleName;
            _impoter.assetBundleVariant = bundleVariant;

            //Debug.LogError ($"Model==={filePath}=={bundleName}=={bundleVariant}" );
        }
        string[] _matPath = AssetDatabase.FindAssets ( "t:Material", new string[] { modelPath } );
        foreach ( string item in _matPath )
        {
            string filePath = AssetDatabase.GUIDToAssetPath ( item );
            AssetImporter _impoter = AssetImporter.GetAtPath ( filePath );
            _impoter.assetBundleName = bundleName;
            _impoter.assetBundleVariant = bundleVariant;
            //Debug.LogError ( $"Material==={filePath}=={bundleName}=={bundleVariant}" );
        }
        string[] _texPath = AssetDatabase.FindAssets ( "t:Texture2D", new string[] { modelPath } );
        foreach ( string item in _texPath )
        {
            string filePath = AssetDatabase.GUIDToAssetPath ( item );
            AssetImporter _impoter = AssetImporter.GetAtPath ( filePath );
            _impoter.assetBundleName = bundleName;
            _impoter.assetBundleVariant = bundleVariant;
            //Debug.LogError ( $"Texture2D==={filePath}=={bundleName}=={bundleVariant}" );
        }
        string[] _animPath = AssetDatabase.FindAssets ( "t:AnimationClip", new string[] { modelPath } );
        foreach ( string item in _animPath )
        {
            string filePath = AssetDatabase.GUIDToAssetPath ( item );
            AssetImporter _impoter = AssetImporter.GetAtPath ( filePath );
            _impoter.assetBundleName = bundleName;
            _impoter.assetBundleVariant = bundleVariant;
            //Debug.LogError ( $"AnimationClip==={filePath}=={bundleName}=={bundleVariant}" );
        }

        Debug.LogError ($"Set=={modelPath}==Bundel Name=={bundleName}==Success" );
    }
}

