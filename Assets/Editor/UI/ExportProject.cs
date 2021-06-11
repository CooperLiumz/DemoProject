//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEditor;
//using UnityEngine;

//public class ExportProject : Editor {
//    [MenuItem("Window/ExportAndroid")]
//    static public void ExportAndroid() {
//        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
//        {
//            EditorUserBuildSettings.activeBuildTargetChanged = delegate () {
//                Debug.Log("Build Target Changed Success Cur Active Target ==> " + EditorUserBuildSettings.activeBuildTarget);
//                BuildUnityTarget();
//            };
//            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
//        }
//        else {
//            BuildUnityTarget();
//        }
//    }

//    [MenuItem("Window/ExportIOS")]
//    static public void ExportIOS()
//    {
//        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
//        {
//            EditorUserBuildSettings.activeBuildTargetChanged = delegate () {
//                Debug.Log("Build Target Changed Success Cur Active Target ==> " + EditorUserBuildSettings.activeBuildTarget);
//                BuildUnityTarget();
//            };
//            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);
//        }
//        else
//        {
//            BuildUnityTarget();
//        }
//    }

//    static void BuildUnityTarget()
//    {
//        string _targetDir = Application.dataPath;

//        string[] _scenes = FindEnabledEditorScenes();

//        BuildTargetGroup _targetGroup = BuildTargetGroup.Android;
//        BuildTarget _target = BuildTarget.Android;
//        BuildOptions _buildOption = BuildOptions.AcceptExternalModificationsToPlayer;

//        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
//        {
//            _targetGroup = BuildTargetGroup.Android;
//            _target = BuildTarget.Android;

//            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.ADT;

//            _targetDir = @"D:\Work\2017\TestExport\Android";
//        }
//        else if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
//        {
//            _targetGroup = BuildTargetGroup.iOS;
//            _target = BuildTarget.iOS;
//            _targetDir = @"D:\Work\2017\TestExport\IOS";
//        }

//        //PlayerSettings.SetScriptingDefineSymbolsForGroup(_targetGroup, "HOMEPAGE;DEV");

//        PlayerSettings.SetGraphicsAPIs(_target, new UnityEngine.Rendering.GraphicsDeviceType[] { UnityEngine.Rendering.GraphicsDeviceType.OpenGLES2});
//        PlayerSettings.applicationIdentifier = "com.lenovo.test";
//        PlayerSettings.bundleVersion = "1.0";

//        //每次build删除之前的残留
//        if (Directory.Exists(_targetDir))
//        {
//            Directory.Delete(_targetDir);
//        }
//        Directory.CreateDirectory(_targetDir);

//        string res = BuildPipeline.BuildPlayer(_scenes, _targetDir, _target, _buildOption);

//        if (res.Length > 0)
//        {
//            throw new Exception("BuildPlayer failure: " + res);
//        }
//        else {
//            Debug.Log("Build Player Success To => " + _targetDir);
//        }
//    }

//    static private string[] FindEnabledEditorScenes()
//    {
//        List<string> EditorScenes = new List<string>();
//        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
//        {
//            if (!scene.enabled) continue;
//            EditorScenes.Add(scene.path);
//        }
//        return EditorScenes.ToArray();
//    }
//}
