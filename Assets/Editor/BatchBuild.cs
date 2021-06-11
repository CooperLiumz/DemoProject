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

    static bool isRelease;

    static bool loadExternalAudio;

    //得到工程中所有场景名称
    static string[] targetScenes = null;

    static string[] a_targetScenes = new string[] {
        "StartUp",
        "Loading",
        "Login",
        "Guide",
        "MainMenu",
        "i3MainA",
        "i3Main_A_RepairSummary",
    };

    static string[] b_targetScenes = new string[] {
        "StartUp",
        "Loading",
        "Login",
        "Guide",
        "MainMenu",
        "i3MainB",
        "i3Main_B_RepairSummary",
    };

    static string[] c_targetScenes = new string[] {
        "StartUp",
        "Loading",
        "Login",
        "Guide",
        "MainMenu",
        "I12_C",
        "I12_C_RepairSummary",
    };

    static string[] d_targetScenes = new string[] {
        "StartUp",
        "Loading",
        "Login",
        "Guide",
        "MainMenu",
        "I12_D",
        "I12_D_RepairSummary",
    };

    static string[] i01_wxgy_targetScenes = new string[] {
        "StartUp",
        "Loading",
        "Login",
        "i3Ani",
    };

    static string[] i12_wxgy_targetScenes = new string[] {
        "StartUp",
        "Loading",
        "Login",
        "I12Ani",
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

        if (GUILayout.Button ("A Hub"))
        {
            BuildA (true);
        }

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5);

        EditorGUILayout.BeginHorizontal ();

        if (GUILayout.Button ("B"))
        {
            BuildB ();
        }

        if (GUILayout.Button ("B Hub"))
        {
            BuildB (true);
        }

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5);

        EditorGUILayout.BeginHorizontal ();

        if (GUILayout.Button ("C"))
        {
            BuildC ();
        }

        if (GUILayout.Button ("C Hub"))
        {
            BuildC (true);
        }

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5);

        EditorGUILayout.BeginHorizontal ();

        if (GUILayout.Button ("D"))
        {
            BuildD ();
        }

        if (GUILayout.Button ("D Hub"))
        {
            BuildD (true);
        }

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5);

        EditorGUILayout.BeginHorizontal ();

        if (GUILayout.Button ("I01 WXGY"))
        {
            BuildI01WXGY ();
        }

        if (GUILayout.Button ("I01 WXGY Hub"))
        {
            BuildI01WXGY (true);
        }

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5);

        EditorGUILayout.BeginHorizontal ();

        if (GUILayout.Button ("I12 WXGY"))
        {
            BuildI12WXGY ();
        }

        if (GUILayout.Button ("I12 WXGY Hub"))
        {
            BuildI12WXGY (true);
        }

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5);

        EditorGUILayout.BeginHorizontal ();

        if (GUILayout.Button ("All"))
        {
            BuildAll ();
        }

        if (GUILayout.Button ("All Hub"))
        {
            BuildAll (true);
        }

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5);

        EditorGUILayout.BeginHorizontal ();

        if (GUILayout.Button ("All & Hub"))
        {
            BuildAll ();
            BuildAll (true);
        }

        EditorGUILayout.EndHorizontal ();

        GUILayout.Space (5);

        EditorGUILayout.EndScrollView ();
    }


    //一系列批量build的操作
    // [MenuItem ("BatchBuild/Build A")]
    static void BuildA (bool startFromHub = false)
    {
        BuildStart ();

        BulidTarget ("A", startFromHub);

        BuildEnd ();
    }

    static void BuildB (bool startFromHub = false)
    {
        BuildStart ();

        BulidTarget ("B" , startFromHub);

        BuildEnd ();
    }

    static void BuildC (bool startFromHub = false)
    {
        BuildStart ();

        BulidTarget ("C" , startFromHub);

        BuildEnd ();
    }

    static void BuildD (bool startFromHub = false)
    {
        BuildStart ();

        BulidTarget ("D" , startFromHub);

        BuildEnd ();
    }

    static void BuildI01WXGY (bool startFromHub = false)
    {
        BuildStart ();

        BulidTarget ("I01WXGY" , startFromHub);

        BuildEnd ();
    }

    static void BuildI12WXGY (bool startFromHub = false)
    {
        BuildStart ();

        BulidTarget ("I12WXGY" , startFromHub);

        BuildEnd ();
    }

    static void BuildAll (bool startFromHub = false)
    {
        BuildStart ();

        BulidTarget ("A" , startFromHub);
        BulidTarget ("B" , startFromHub);
        BulidTarget ("C" , startFromHub);
        BulidTarget ("D" , startFromHub);
        BulidTarget ("I01WXGY" , startFromHub);
        BulidTarget ("I12WXGY" , startFromHub);

        BuildEnd ();
    }

    static void BuildStart ()
    {
        if (loadExternalAudio)
        {
            string _src = Path.Combine (Application.dataPath , "Resources/Sounds");
            string _stream = Path.Combine (Application.streamingAssetsPath , "Sounds");

            MoveAudio (_src , _stream);
        }       
    }

    static void BuildEnd ()
    {
        if (loadExternalAudio)
        {
            string _src = Path.Combine (Application.dataPath , "Resources/Sounds");
            string _stream = Path.Combine (Application.streamingAssetsPath , "Sounds");

            MoveAudio (_stream , _src);
            DeleteStreamingAssetAudio (_stream);
        }       
    }

    static void CopyAudioToStreamingAsset (string _srcdir , string _dstdir)
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
    static void CopyAudioToTgtDir (string _srcdir , string _dstdir)
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

    static void DeleteResourceAudio (string _srcdir)
    {
        foreach (var s in Directory.GetDirectories (_srcdir))
        {
            Debug.LogError ($"Copy Directory {s} Success !");
            Directory.Delete (s , true);
        }
    }

    static void DeleteStreamingAssetAudio (string _srcdir)
    {
        Directory.Delete (_srcdir , true);
    }

    static void MoveAudio (string _srcdir , string _dstdir)
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
    static void BulidTarget (string targetName , bool startFromHub = false)
    {
        string target_dir = null;

        BuildTargetGroup targetGroup = BuildTargetGroup.Standalone;
        BuildTarget buildTarget = BuildTarget.StandaloneWindows;

        string applicationPath = Application.dataPath.Replace ("/Assets" , "");

        
        target_dir = applicationPath + "/Output/"+ targetName;

        if (startFromHub)
        {
            target_dir = target_dir + "_Hub";
        }

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

                targetScenes = GetEditorScenes (a_targetScenes);
                break;
            case "B":
                //PlayerSettings.applicationIdentifier = "com.game.uc";
                //PlayerSettings.bundleVersion = "v0.0.1";

                PlayerSettings.applicationIdentifier = "com.lnv.bmw_b";
                targetScenes = GetEditorScenes (b_targetScenes);

                break;
            case "C":
                //PlayerSettings.applicationIdentifier = "com.game.cmcc";
                //PlayerSettings.bundleVersion = "v0.0.1";
                //PlayerSettings.SetScriptingDefineSymbolsForGroup (targetGroup , "CMCC");
                PlayerSettings.applicationIdentifier = "com.lnv.bmw_c";
                targetScenes = GetEditorScenes (c_targetScenes);

                break;
            case "D":
                //PlayerSettings.applicationIdentifier = "com.game.cmcc";
                //PlayerSettings.bundleVersion = "v0.0.1";
                //PlayerSettings.SetScriptingDefineSymbolsForGroup (targetGroup , "CMCC");
                PlayerSettings.applicationIdentifier = "com.lnv.bmw_d";
                targetScenes = GetEditorScenes (d_targetScenes);

                break;
            case "I01WXGY":
                //PlayerSettings.applicationIdentifier = "com.game.cmcc";
                //PlayerSettings.bundleVersion = "v0.0.1";
                //PlayerSettings.SetScriptingDefineSymbolsForGroup (targetGroup , "CMCC");
                PlayerSettings.applicationIdentifier = "com.lnv.bmw_i01wxgy";
                targetScenes = GetEditorScenes (i01_wxgy_targetScenes);

                break;

            case "I12WXGY":
                //PlayerSettings.applicationIdentifier = "com.game.cmcc";
                //PlayerSettings.bundleVersion = "v0.0.1";
                //PlayerSettings.SetScriptingDefineSymbolsForGroup (targetGroup , "CMCC");
                PlayerSettings.applicationIdentifier = "com.lnv.bmw_i12wxgy";
                targetScenes = GetEditorScenes (i12_wxgy_targetScenes);

                break;
        }

        AssetDatabase.Refresh ();

        GenericBuild (targetScenes , target_dir + "/" + string.Concat("BMW_VR_", targetName, ".exe") , targetGroup , buildTarget , BuildOptions.None);
    }

    private static string[] FindEnabledEditorScenes ()
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

    private static string[] GetEditorScenes (params string[] _targetScenes)
    {
        List<string> _editorScenes = new List<string> ();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene == null || string.IsNullOrEmpty(scene.path) )
            {
                continue;
            }
            int _startIdx = scene.path.LastIndexOf ('/');
            int _endIdx = scene.path.LastIndexOf ('.');

            if (_endIdx - _startIdx < 1)
            {
                continue;
            }

            string _str = scene.path.Substring (_startIdx + 1 , _endIdx - _startIdx - 1);

            scene.enabled = false;
            foreach (string item in _targetScenes)
            {
                if (item.Equals (_str))
                {
                    scene.enabled = true;
                    _editorScenes.Add (scene.path);
                    break;
                }
            }
        }
        return _editorScenes.ToArray ();
    }

    static void GenericBuild (string[] scenes , string target_dir , BuildTargetGroup build_group,BuildTarget build_target , BuildOptions build_options)
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