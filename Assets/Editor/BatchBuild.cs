using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class BatchBuild : EditorWindow
{
    static string configFileName = "BuildConfig.json";

    private Vector2 scrollPos;

    bool isRelease;

    bool loadExternalAudio;

    //得到工程中所有场景名称
    EditorBuildSettingsScene[] targetScenes = null;

    string[] a_targetScenes = new string[] {
        "Assets/Scenes/A.unity",
    };

    string[] b_targetScenes = new string[] {
        "Assets/Scenes/B.unity",
    };

    string[] c_targetScenes = new string[] {
       "Assets/Scenes/C.unity",
    };

    string[] d_targetScenes = new string[] {
        "Assets/Scenes/D.unity",
    };
    

    [MenuItem ("Window/一键打包")]
    static void OpenWindow ()
    {
        BatchBuild window = (BatchBuild)EditorWindow.GetWindow (typeof (BatchBuild));
        window.Show ();
    }

    private void OnGUI ()
    {
        scrollPos = EditorGUILayout.BeginScrollView (scrollPos);

        GUILayout.Space (5);

        EditorGUILayout.BeginHorizontal ();

        isRelease = GUILayout.Toggle (isRelease, "Release", GUILayout.ExpandWidth(false));

        loadExternalAudio = GUILayout.Toggle (loadExternalAudio , "LoadExternalAudio" , GUILayout.ExpandWidth (false));

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5);

        EditorGUILayout.BeginHorizontal ();

        if (GUILayout.Button ("A"))
        {
            BuildA ();
        }       

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5);

        EditorGUILayout.BeginHorizontal ();

        if (GUILayout.Button ("B"))
        {
            BuildB ();
        }

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5);

        EditorGUILayout.BeginHorizontal ();

        if (GUILayout.Button ("C"))
        {
            BuildC ();
        }

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5);

        EditorGUILayout.BeginHorizontal ();

        if (GUILayout.Button ("D"))
        {
            BuildD ();
        }

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5); 

        EditorGUILayout.BeginHorizontal ();

        if (GUILayout.Button ("All"))
        {
            BuildAll ();
        }

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5);

        EditorGUILayout.EndScrollView ();
    }


    //一系列批量build的操作
    // [MenuItem ("BatchBuild/Build A")]
    void BuildA ()
    {
        BuildStart ();

        BulidTarget ("A");

        BuildEnd ();
    }

    void BuildB ()
    {
        BuildStart ();

        BulidTarget ("B");

        BuildEnd ();
    }

    void BuildC ()
    {
        BuildStart ();

        BulidTarget ("C");

        BuildEnd ();
    }

    void BuildD ()
    {
        BuildStart ();

        BulidTarget ("D");

        BuildEnd ();
    }

    void BuildAll ()
    {
        BuildStart ();

        BulidTarget ("A");
        BulidTarget ("B");
        BulidTarget ("C");
        BulidTarget ("D");

        BuildEnd ();
    }

    void BuildStart ()
    {
        if (loadExternalAudio)
        {
            string _src = Path.Combine (Application.dataPath , "Resources/Sounds");
            string _stream = Path.Combine (Application.streamingAssetsPath , "Sounds");

            MoveAudio (_src , _stream);
        }       
    }

    void BuildEnd ()
    {
        if (loadExternalAudio)
        {
            string _src = Path.Combine (Application.dataPath , "Resources/Sounds");
            string _stream = Path.Combine (Application.streamingAssetsPath , "Sounds");

            MoveAudio (_stream , _src);
            DeleteStreamingAssetAudio (_stream);
        }       
    }

    void CopyAudioToStreamingAsset (string _srcdir , string _dstdir)
    {
        if (!Directory.Exists (_dstdir))
        {
            Directory.CreateDirectory (_dstdir);
        }

        foreach (var s in Directory.GetFiles (_srcdir))
        {
            if (Path.GetFileName (s).EndsWith (".prefab"))
            {
                continue;
            }

            if (File.Exists (Path.Combine (_dstdir , Path.GetFileName (s))))
            {
                File.Delete (Path.Combine (_dstdir , Path.GetFileName (s)));
            }

            File.Copy (s , Path.Combine (_dstdir , Path.GetFileName (s)));

            Debug.LogError ($"Copy File {Path.GetFileName (s)} Success !");
        }

        foreach (var s in Directory.GetDirectories (_srcdir))
        {
            Debug.LogError ($"Copy Directory {s} Success !");
            CopyAudioToStreamingAsset (s , Path.Combine (_dstdir , Path.GetFileName (s)));
        }
    }
    void CopyAudioToTgtDir (string _srcdir , string _dstdir)
    {
        if (!Directory.Exists (_dstdir))
        {
            Directory.CreateDirectory (_dstdir);
        }

        foreach (var s in Directory.GetFiles (_srcdir))
        {
            if (Path.GetFileName (s).EndsWith (".prefab"))
            {
                continue;
            }

            if (File.Exists (Path.Combine (_dstdir , Path.GetFileName (s))))
            {
                File.Delete (Path.Combine (_dstdir , Path.GetFileName (s)));
            }

            File.Copy (s , Path.Combine (_dstdir , Path.GetFileName (s)));

            Debug.LogError ($"Copy File {Path.GetFileName (s)} Success !");
        }

        foreach (var s in Directory.GetDirectories (_srcdir))
        {
            Debug.LogError ($"Copy Directory {s} Success !");
            CopyAudioToTgtDir (s , Path.Combine (_dstdir , Path.GetFileName (s)));
        }
    }

    void DeleteResourceAudio (string _srcdir)
    {
        foreach (var s in Directory.GetDirectories (_srcdir))
        {
            Debug.LogError ($"Copy Directory {s} Success !");
            Directory.Delete (s , true);
        }
    }

    void DeleteStreamingAssetAudio (string _srcdir)
    {
        Directory.Delete (_srcdir , true);
    }

    void MoveAudio (string _srcdir , string _dstdir)
    {
        if (!Directory.Exists (_dstdir))
        {
            Directory.CreateDirectory (_dstdir);
        }

        foreach (var s in Directory.GetFiles (_srcdir))
        {
            if (Path.GetFileName (s).EndsWith (".prefab"))
            {
                continue;
            }

            if (File.Exists (Path.Combine (_dstdir , Path.GetFileName (s))))
            {
                File.Delete (Path.Combine (_dstdir , Path.GetFileName (s)));
                File.Copy (s , Path.Combine (_dstdir , Path.GetFileName (s)));
            }
            else
            {
                File.Move (s , Path.Combine (_dstdir , Path.GetFileName (s)));
            }
            Debug.LogError ($"Move File {Path.GetFileName (s)} Success !");
        }

        foreach (var s in Directory.GetDirectories (_srcdir))
        {
            Debug.LogError ($"Move Directory {s} Success !");
            MoveAudio (s , Path.Combine (_dstdir , Path.GetFileName (s)));
        }
    }

    //这里封装了一个简单的通用方法。
    void BulidTarget (string targetName)
    {
        string target_dir = null;

        BuildTargetGroup targetGroup = BuildTargetGroup.Standalone;
        BuildTarget buildTarget = BuildTarget.StandaloneWindows;

        string applicationPath = Application.dataPath.Replace ("/Assets" , "");

        
        target_dir = applicationPath + "/Output/"+ targetName;
        
        targetGroup = BuildTargetGroup.Standalone;

        //每次build删除之前的残留
        if (Directory.Exists (target_dir))
        {
            Directory.Delete (target_dir, true);
        }
        else
        {
            Directory.CreateDirectory (target_dir);
        }
        
        switch (targetName)
        {
            case "A":
                PlayerSettings.applicationIdentifier = "com.lnv.bmw_a";
                //PlayerSettings.bundleVersion = "v0.0.1";
                //PlayerSettings.SetScriptingDefineSymbolsForGroup (targetGroup , "UC");

                targetScenes = SetBuildScenes (a_targetScenes);
                break;
            case "B":
                //PlayerSettings.applicationIdentifier = "com.game.uc";
                //PlayerSettings.bundleVersion = "v0.0.1";

                PlayerSettings.applicationIdentifier = "com.lnv.bmw_b";
                targetScenes = SetBuildScenes (b_targetScenes);

                break;
            case "C":
                //PlayerSettings.applicationIdentifier = "com.game.cmcc";
                //PlayerSettings.bundleVersion = "v0.0.1";
                //PlayerSettings.SetScriptingDefineSymbolsForGroup (targetGroup , "CMCC");
                PlayerSettings.applicationIdentifier = "com.lnv.bmw_c";
                targetScenes = SetBuildScenes (c_targetScenes);

                break;
            case "D":
                //PlayerSettings.applicationIdentifier = "com.game.cmcc";
                //PlayerSettings.bundleVersion = "v0.0.1";
                //PlayerSettings.SetScriptingDefineSymbolsForGroup (targetGroup , "CMCC");
                PlayerSettings.applicationIdentifier = "com.lnv.bmw_d";
                targetScenes = SetBuildScenes (d_targetScenes);

                break;           
        }

        AssetDatabase.Refresh ();

        GenericBuild (targetScenes , target_dir + "/" + string.Concat("BMW_VR_", targetName, ".exe") , targetGroup , buildTarget , BuildOptions.None);
    }

    private string[] FindEnabledEditorScenes ()
    {
        List<string> EditorScenes = new List<string> ();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled)
                continue;
            EditorScenes.Add (scene.path);
        }
        return EditorScenes.ToArray ();
    }

    List<EditorBuildSettingsScene> cacheScenes;
    private EditorBuildSettingsScene[] SetBuildScenes (params string[] _targetScenes)
    {
        cacheScenes = new List<EditorBuildSettingsScene> (EditorBuildSettings.scenes);

        List<EditorBuildSettingsScene> _resultScenes = new List<EditorBuildSettingsScene> ();

        bool _found = false;
        foreach (string _scenePath in _targetScenes)
        {
            foreach (EditorBuildSettingsScene _editorScene in cacheScenes)
            {
                if (_editorScene.path.Equals (_scenePath))
                {
                    _editorScene.enabled = true;
                    _resultScenes.Add (_editorScene);

                    _found = true;
                    break;
                }
            }
            if (!_found)
            {
                _resultScenes.Add (new EditorBuildSettingsScene (_scenePath , true));
            }
        }
        EditorBuildSettings.scenes = _resultScenes.ToArray ();
        return _resultScenes.ToArray ();
    }

    void GenericBuild (EditorBuildSettingsScene[] scenes , string target_dir , BuildTargetGroup build_group,BuildTarget build_target , BuildOptions build_options)
    {
        if (EditorUserBuildSettings.activeBuildTarget == build_target)
        {
            BuildPipeline.BuildPlayer (scenes , target_dir , build_target , build_options);
        }
        else
        {
            if (EditorUserBuildSettings.SwitchActiveBuildTarget (build_group , build_target))
            {
                BuildPipeline.BuildPlayer (scenes , target_dir , build_target , build_options);
            }
        }        
    }   
}