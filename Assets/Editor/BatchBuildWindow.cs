using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEditor.Build.Reporting;

public class BatchBuildWindow : EditorWindow
{
    private Vector2 scrollPos;

    [MenuItem ("Window/BatchBuild")]
    static void OpenWindow ()
    {
        BatchBuildWindow window = (BatchBuildWindow)EditorWindow.GetWindow (typeof (BatchBuildWindow));
        window.Show ();
    }

    private void OnGUI ()
    {
        scrollPos = EditorGUILayout.BeginScrollView (scrollPos);
        
        GUILayout.Space (5);

        if (GUILayout.Button ("A"))
        {
            BuildA ();
        }

        GUILayout.Space (5);

        if (GUILayout.Button ("B"))
        {
            BuildB ();
        }
        GUILayout.Space (5);

        if (GUILayout.Button ("C"))
        {
            BuildC ();
        }
        GUILayout.Space (5);

        if (GUILayout.Button ("D"))
        {
            //ExcelToJson ();
            BuildD ();
        }
        GUILayout.Space (5);

        if (GUILayout.Button ("WXTS"))
        {
            BuildWXTS ();
        }
        GUILayout.Space (5);

        if (GUILayout.Button ("All"))
        {
            BuildAll ();
        }
        GUILayout.Space (5);

        EditorGUILayout.EndScrollView ();
    }
    
    //得到工程中所有场景名称
    static string[] targetScenes = null;

    static void BuildA ()
    {
        BulidTarget ("A");
    }

    static void BuildB ()
    {
        BulidTarget ("B");
    }

    static void BuildC ()
    {
        BulidTarget ("C");
    }

    static void BuildD ()
    {
        BulidTarget ("D");
    }

    static void BuildWXTS ()
    {
        BulidTarget ("D");
    }

    static void BuildAll ()
    {
        BulidTarget ("A");
        BulidTarget ("B");
        BulidTarget ("C");
        BulidTarget ("D");
    }

    //这里封装了一个简单的通用方法。
    static void BulidTarget (string targetName)
    {
        BuildTargetGroup targetGroup = BuildTargetGroup.Standalone;
        BuildTarget buildTarget = BuildTarget.StandaloneWindows;

        string applicationPath = Application.dataPath.Replace ("/Assets" , "");

        string target_dir = applicationPath + "/Output/" + targetName;
        string target_name = targetName + ".exe";
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
                //PlayerSettings.applicationIdentifier = "com.game.qq";
                //PlayerSettings.bundleVersion = "v0.0.1";
                //PlayerSettings.SetScriptingDefineSymbolsForGroup (targetGroup , "QQ");

                targetScenes = GetEditorScenes ("A");
                break;
            case "B":
                //PlayerSettings.applicationIdentifier = "com.game.uc";
                //PlayerSettings.bundleVersion = "v0.0.1";
                //PlayerSettings.SetScriptingDefineSymbolsForGroup (targetGroup , "UC");

                targetScenes = GetEditorScenes ("B");
                break;
            case "C":
                //PlayerSettings.applicationIdentifier = "com.game.cmcc";
                //PlayerSettings.bundleVersion = "v0.0.1";
                //PlayerSettings.SetScriptingDefineSymbolsForGroup (targetGroup , "CMCC");
                targetScenes = GetEditorScenes ("C");
                break;
            case "D":
                //PlayerSettings.applicationIdentifier = "com.game.cmcc";
                //PlayerSettings.bundleVersion = "v0.0.1";
                //PlayerSettings.SetScriptingDefineSymbolsForGroup (targetGroup , "CMCC");
                targetScenes = GetEditorScenes ("D");
                break;
        }

        GenericBuild (targetScenes , target_dir + "/" + target_name , targetGroup , buildTarget , BuildOptions.None);
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
        List<string> EditorScenes = new List<string> ();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            int _startIdx = scene.path.LastIndexOf ('/');
            int _endIdx = scene.path.LastIndexOf ('.');

            string _str = scene.path.Substring (_startIdx + 1 , _endIdx - _startIdx - 1);

            scene.enabled = false;
            foreach (string item in _targetScenes)
            {
                if (item.Equals (_str))
                {
                    scene.enabled = true;
                    EditorScenes.Add (scene.path);
                    break;
                }
            }
        }
        return EditorScenes.ToArray ();
    }

    static void GenericBuild (string[] scenes , string target_dir , BuildTargetGroup build_group , BuildTarget build_target , BuildOptions build_options)
    {
        if (EditorUserBuildSettings.activeBuildTarget == build_target)
        {
            Debug.LogError ("Not Change Target");
            BuildReport _report = BuildPipeline.BuildPlayer (scenes , target_dir , build_target , build_options);
            Debug.LogError (_report);

            //string _reportMSG = LitJson.JsonMapper.ToJson (_report.summary);

            string _reportMSG = JsonFx.Json.JsonWriter.Serialize (_report.summary);

            SaveTextToPath (Path.Combine ("D:/Work/MySVN/DemoProject/Output/", "BuildReport.json"), _reportMSG);
        }
        else
        {
            Debug.LogError ("Start Change Target" + DateTime.Now.ToString());
            if (EditorUserBuildSettings.SwitchActiveBuildTarget (build_group , build_target))
            {
                Debug.LogError ("End Change Target" + DateTime.Now.ToString ());
                BuildPipeline.BuildPlayer (scenes , target_dir , build_target , build_options);
            }
        }
    }

    static void SaveTextToPath (string fileName , string text)
    {
        using (FileStream stream = new FileStream (fileName , FileMode.OpenOrCreate))
        {
            stream.Seek (0 , SeekOrigin.Begin);
            //清空文本里数据
            stream.SetLength (0);
            using (StreamWriter writer = new StreamWriter (stream , System.Text.Encoding.UTF8))
            {
                writer.Write (text);
                writer.Flush ();
                writer.Close ();
            }
            stream.Close ();
        }
    }
}