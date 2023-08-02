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

    [ MenuItem ( "Window/����Bunle" )]
    static void OpenWindow ( )
    {
        SetBundleInfoEditor window = ( SetBundleInfoEditor ) EditorWindow.GetWindow ( typeof ( SetBundleInfoEditor ) );

        window.Show ( );
    }

    public void OnGUI ( )
    {
        EditorGUILayout.Space ( );
        EditorGUILayout.LabelField ( "ģ��·�� (�����ק�ļ��е�����)" );
        EditorGUILayout.Space ( );
        GUI.SetNextControlName ( "input1" );//������һ���ؼ�������
        modelRect = EditorGUILayout.GetControlRect ( );
        modelPath = EditorGUI.TextField ( modelRect, modelPath );
        EditorGUILayout.Space ( );
        bundleName = EditorGUILayout.TextField ( "Bundle����", bundleName );
        DragFolder ( );

        EditorGUILayout.Space ( );

        if ( GUILayout.Button ( "����Bundle����" ) )
        {
            SetName ( );
        }
    }

    /// <summary>
    /// �����ק�ļ�
    /// </summary>
    void DragFolder()
    {
        //���λ�ڵ�ǰ����
        if (mouseOverWindow == this)
        {
            //���봰��δ�ɿ����
            if (Event.current.type == EventType.DragUpdated)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Generic;//�ı�������
                // �ж�����
                if (modelRect.Contains(Event.current.mousePosition))
                    GUI.FocusControl("input1");
            }
            //���봰�ڲ��ɿ����
            else if (Event.current.type == EventType.DragExited)
            {
                string dragPath = string.Join("", DragAndDrop.paths);
                // �ж�����
                if (modelRect.Contains(Event.current.mousePosition))
                    this.modelPath = dragPath;

                // ȡ������(��ȻGUI����ˢ��)
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

