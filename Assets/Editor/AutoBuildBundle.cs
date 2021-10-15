using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;

public class AutoBuildBundle : EditorWindow
{
    private Vector2 scrollPos;

    private static string srcDir = "C:/Users/iori7/Desktop/TestBundle";

    private static string tgtDir;


    [MenuItem ( "Window/一键打包" )]
    static void OpenWindow ( )
    {
        srcDir = Application.dataPath;
        tgtDir = Application.dataPath.Replace ("Assets", "Output" );
        //tgtDir = Application.streamingAssetsPath;

        AutoBuildBundle window = ( AutoBuildBundle ) EditorWindow.GetWindow ( typeof ( AutoBuildBundle ) );
        window.Show ( );
    }

    private void OnGUI ( )
    {
        if ( Event.current.type == EventType.KeyDown || Event.current.type == EventType.KeyUp )
        {
            return;
        }

        scrollPos = EditorGUILayout.BeginScrollView ( scrollPos );

        GUILayout.Space ( 5 );

        EditorGUILayout.BeginVertical ( );
        GUILayout.Label ( "文件路径: " );
        GUILayout.Space ( 5 );
        srcDir = GUILayout.TextField ( srcDir );
        EditorGUILayout.EndVertical ( );

        GUILayout.Space ( 5 );

        EditorGUILayout.BeginVertical ( );
        GUILayout.Label ( "目标路径: " );
        GUILayout.Space ( 5 );
        tgtDir = GUILayout.TextField ( tgtDir );
        EditorGUILayout.EndVertical ( );

        GUILayout.Space ( 5 );

        if ( GUILayout.Button ( "选择源路径" ) )
        {
            SelectSrcDir ( );
        }

        GUILayout.Space ( 5 );

        if ( GUILayout.Button ( "选择目标路径" ) )
        {
            SelectTgtDir ( );
        }

        GUILayout.Space ( 5 );

        if ( GUILayout.Button ( "打包" ) )
        {
            StartBuildBundle ( );
        }

        GUILayout.Space ( 5 );


        EditorGUILayout.EndScrollView ( );
    }

    private void SelectSrcDir ( )
    {
        //string path = EditorUtility.OpenFilePanel ("Overwrite with png" , Application.dataPath , "xlsx");
        string path = EditorUtility.OpenFolderPanel ( "Select Folder" , Application.dataPath , "" );
        if ( path.Length != 0 )
        {
            srcDir = path;

            Debug.LogError ( "Select Source Dir   " + srcDir );
        }
    }

    private void SelectTgtDir ( )
    {
        //string path = EditorUtility.OpenFilePanel ("Overwrite with png" , Application.dataPath , "xlsx");
        string path = EditorUtility.OpenFolderPanel ( "Select Folder" , Application.dataPath , "" );
        if ( path.Length != 0 )
        {
            tgtDir = path;

            Debug.LogError ( "Select Target Dir   " + tgtDir );
        }
    }

    private void StartBuildBundle ( )
    {
        if ( Directory.Exists ( tgtDir ) )
        {
            Directory.Delete ( tgtDir , true );
        }

        Directory.CreateDirectory ( tgtDir );


        string _assetPath = CopyAsset ( );
        BuildBundle ( _assetPath );

        DeleteAssets ( _assetPath );
    }

    void BuildBundle( string _assetPath )
    {
        foreach ( string item in Directory.GetFiles ( _assetPath ) )
        {
            if ( item.Contains ( ".meta" ) )
            {
                continue;
            }

            List<AssetBundleBuild> _buildList = new List<AssetBundleBuild> ( );
            AssetBundleBuild _build = new AssetBundleBuild ( );
            FileInfo _fi = new FileInfo ( item );
            _build.assetBundleName = _fi.Name.Substring ( 0 , _fi.Name.LastIndexOf ( _fi.Extension ) );
            _build.assetNames = new string[] { item.Remove ( 0 , item.IndexOf ( "Assets" ) ) };

            _buildList.Add ( _build );

            BuildPipeline.BuildAssetBundles ( tgtDir , _buildList.ToArray ( ) ,
                BuildAssetBundleOptions.UncompressedAssetBundle , BuildTarget.StandaloneWindows );
        }
        AssetDatabase.Refresh ( );
    }

    string CopyAsset ( )
    {
        string _dirName = srcDir.Remove (0, srcDir.LastIndexOf("/") + 1);

        string _targetPath = Path.Combine (Application.dataPath , _dirName );

        if ( Directory.Exists ( _targetPath ) )
        {
            return _targetPath;
        }

        CopyDirToDir ( srcDir, _targetPath );

        AssetDatabase.Refresh ( );
        return _targetPath;
    }

    public void CopyDirToDir ( string srcdir , string dstdir , bool overwrite = true )
    {
        if ( !Directory.Exists ( dstdir ) )
        {
            Directory.CreateDirectory ( dstdir );
        }

        foreach ( var s in Directory.GetFiles ( srcdir ) )
        {
            if ( File.Exists ( Path.Combine ( dstdir , Path.GetFileName ( s ) ) ) && !overwrite )
            {
            }
            else
            {
                File.Copy ( s , Path.Combine ( dstdir , Path.GetFileName ( s ) ) , overwrite );
            }
        }

        foreach ( var s in Directory.GetDirectories ( srcdir ) )
        {
            CopyDirToDir ( s , Path.Combine ( dstdir , Path.GetFileName ( s ) ) , overwrite );
        }
    }

    void DeleteAssets ( string _assetPath )
    {
        if ( Directory.Exists ( _assetPath ) )
        {
            Directory.Delete ( _assetPath , true );
        }

        if (File.Exists( string.Concat( _assetPath, ".meta") ) )
        {
            File.Delete ( string.Concat ( _assetPath , ".meta" ) );
        }

        AssetDatabase.Refresh ( );
    }
 }
