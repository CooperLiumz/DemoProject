using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class AutoBuild
{
    //得到工程中所有场景名称
    static string[] targetScenes = null;

    //一系列批量build的操作
    [MenuItem ("Custom/Build A")]
    static void PerformPCABuild ()
    {
        BulidTarget ("A" , "A");
    }

    [MenuItem ("Custom/Build B")]
    static void PerformPCBBuild ()
    {
        BulidTarget ("B" , "B");
    }

    [MenuItem ("Custom/Build C")]
    static void PerformPCCBuild ()
    {
        BulidTarget ("C" , "C");
    }

    [MenuItem ("Custom/Build D")]
    static void PerformPCDBuild ()
    {
        BulidTarget ("D" , "D");
    }

    [MenuItem ("Custom/Build All")]
    static void PerformPCALLBuild ()
    {
        BulidTarget ("A" , "A");
        BulidTarget ("B" , "B");
        BulidTarget ("C" , "C");
        BulidTarget ("D" , "D");
    }

    //这里封装了一个简单的通用方法。
    static void BulidTarget (string name , string target)
    {
        string app_name = name;

        string target_dir = null;

        string target_name = app_name + ".exe";

        BuildTargetGroup targetGroup = BuildTargetGroup.Standalone;
        BuildTarget buildTarget = BuildTarget.StandaloneWindows;

        string applicationPath = Application.dataPath.Replace ("/Assets" , "");

        target_dir = applicationPath + "/Output/"+ target;
        target_name = app_name + ".exe";
        targetGroup = BuildTargetGroup.Standalone;

        //每次build删除之前的残留
        if (Directory.Exists (target_dir))
        {
            if (File.Exists (target_name))
            {
                File.Delete (target_name);
            }
        }
        else
        {
            Directory.CreateDirectory (target_dir);
        }
        
        switch (name)
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