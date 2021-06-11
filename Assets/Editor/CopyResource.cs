using ExcelDataReader;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;


/* 一张xlsx里面有多张表格，因为实际游戏中使用的是json文件，所以生成的时候我只用一张xlsx
  * 一张sheet就是一个数据集合 
  * 根据sheet表字段生成对应的数据集合json
  * 可以创建实体类
  * 表格第一行为列名加类型 id#type
  * 表格第二行为备注
  * 数组用,分割
  */
public class CopyResource : EditorWindow
{
    private Vector2 scrollPos;

    private static string srcDir = "C:/Users/nbd/Desktop/1/Sounds";//C:/Users/nbd/Desktop/1/Sounds

    private static string tgtDir = "D:/Work/SVN/BMW/BMW_VR/Assets/Resources/Sounds/Result/EN";//D:/Work/SVN/BMW/BMW_VR/Assets/

    [MenuItem ("Window/拷贝资源")]
    static void OpenWindow ()
    {
        CopyResource window = (CopyResource)EditorWindow.GetWindow (typeof (CopyResource));
        window.Show ();
        
        srcDir = Path.Combine (Application.dataPath , "Test/TestAction");

        Debug.LogError (Application.dataPath.LastIndexOf ("/"));
        tgtDir = Path.Combine (Application.dataPath.Substring (0, Application.dataPath.LastIndexOf("/")) , "Test/TestAction");
        Debug.LogError (tgtDir);
    }

    private void OnGUI ()
    {
        scrollPos = EditorGUILayout.BeginScrollView (scrollPos);

        EditorGUILayout.BeginVertical ();
        GUILayout.Label ("文件路径: ");
        GUILayout.Space (5);
        srcDir = GUILayout.TextField (srcDir);
        EditorGUILayout.EndVertical ();

        GUILayout.Space (5);

        EditorGUILayout.BeginVertical ();
        GUILayout.Label ("目标路径: ");
        GUILayout.Space (5);
        tgtDir = GUILayout.TextField (tgtDir);
        EditorGUILayout.EndVertical ();

        GUILayout.Space (5);

        if (GUILayout.Button ("选择源路径"))
        {
            SelectSrcDir ();
        }

        GUILayout.Space (5);

        if (GUILayout.Button ("选择目标路径"))
        {
            SelectTgtDir ();
        }

        GUILayout.Space (5);

        if (GUILayout.Button ("拷贝,替换"))
        {
            //Copy (true);
        }

        GUILayout.Space (5);

        if (GUILayout.Button ("拷贝,不替换替换"))
        {
            //Copy (false);
        }

        GUILayout.Space (5);

        //if (GUILayout.Button ("拷贝音频资源"))
        //{
        //    CopyAudio ();
        //}

        if (GUILayout.Button ("替换音频资源"))
        {
            //ReplaceAudio ();
        }

        GUILayout.Space (5);

        //if (GUILayout.Button ("拷贝图片"))
        //{
        //    CopyImage ();
        //}

        //GUILayout.Space (5);

        if (GUILayout.Button ("替换图片名"))
        {
            //ReplaceImageName ();
        }

        if (GUILayout.Button ("替换XXX"))
        {
            ReplaceXX ();
        }

        EditorGUILayout.EndScrollView ();
    }

    private void SelectSrcDir ()
    {
        //string path = EditorUtility.OpenFilePanel ("Overwrite with png" , Application.dataPath , "xlsx");
        string path = EditorUtility.OpenFolderPanel ("Select Folder" , Application.dataPath , "");
        if (path.Length != 0)
        {
            srcDir = path;

            Debug.LogError ("Select Source Dir   " + srcDir);
        }
    }

    private void SelectTgtDir ()
    {
        //string path = EditorUtility.OpenFilePanel ("Overwrite with png" , Application.dataPath , "xlsx");
        string path = EditorUtility.OpenFolderPanel ("Select Folder" , Application.dataPath , "");
        if (path.Length != 0)
        {
            tgtDir = path;

            Debug.LogError ("Select Target Dir   " + tgtDir);
        }
    }

    private void Copy (bool overwrite = false)
    {
        CopyDirContentIntoDestDirectory (srcDir , tgtDir , overwrite);
    }

    private void CopyAudio ()
    {
        CopyDirAudioIntoDestDirectory (srcDir , tgtDir);

        AssetDatabase.Refresh ();
    }

    private void ReplaceAudio ()
    {
        ReplaceAudioIntoDestDirectory (srcDir , tgtDir);

        AssetDatabase.Refresh ();
    }

    private void CopyImage(){

        foreach (var s in Directory.GetFiles (srcDir))
        {
            if (Path.GetFileName (s).EndsWith (".meta"))
            {
                continue;
            }

            string path = Path.GetFileName (s).Replace ("CN_" , "EN_");

            File.Copy (s , Path.Combine (srcDir , path ));

            Debug.LogError ($"Copy File {s} Success !");
        }
        AssetDatabase.Refresh ();
    }


    void ReplaceImageName ()
    {
        //ToolConf[] _tools = Resources.FindObjectsOfTypeAll<ToolConf> ();

       // Debug.LogError (_tools.Length);

        //foreach (ToolConf item in _tools)
        //{
        //    if (item.thumbImage != null)
        //    {
        //        item.thumbImageName = item.thumbImage.name;
        //        Debug.LogError ("Replace Tool== " + item.thumbImage.name);
        //        EditorUtility.SetDirty (item);
        //    }
        //}

        //AssetDatabase.Refresh ();
    }

    void ReplaceXX ()
    {
        string _src = Path.Combine (Application.dataPath , "Resources/Sounds");
        string _e = Path.Combine (Application.dataPath.Substring (0 , Application.dataPath.LastIndexOf ("/")), "Sounds");
        string _s = Path.Combine (Application.streamingAssetsPath , "Sounds");

        CopyAudioToE (_src , _e);
        CopyAudioToS (_src , _s);
        CopyAudioToD ();
    }

    void CopyAudioToE (string _srcdir , string _dstdir)
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

            File.Copy (s , Path.Combine (_dstdir , Path.GetFileName (s)));

            Debug.LogError ($"Copy File {Path.GetFileName (s)} Success !");
        }

        foreach (var s in Directory.GetDirectories (_srcdir))
        {
            Debug.LogError ($"Copy Directory {s} Success !");
            CopyAudioToE (s , Path.Combine (_dstdir , Path.GetFileName (s)));
        }
    }
    void CopyAudioToS (string _srcdir , string _dstdir)
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

            File.Copy (s , Path.Combine (_dstdir , Path.GetFileName (s)));

            Debug.LogError ($"Copy File {Path.GetFileName (s)} Success !");
        }

        foreach (var s in Directory.GetDirectories (_srcdir))
        {
            Debug.LogError ($"Copy Directory {s} Success !");
            CopyAudioToS (s , Path.Combine (_dstdir , Path.GetFileName (s)));
        }
    }

    void CopyAudioToD ()
    {
    }

    private void CopyDirAudioIntoDestDirectory (string srcdir , string dstdir)
    {
        if (!Directory.Exists (dstdir))
        {
            Directory.CreateDirectory (dstdir);
        }

        foreach (var s in Directory.GetFiles (srcdir))
        {
            if (Path.GetFileName (s).EndsWith(".meta"))
            {
                continue;
            }

            string path = Path.GetFileName (s).Replace ("CN_", "EN_");

            File.Copy (s , Path.Combine (dstdir , path ));

            Debug.LogError ($"Copy File {path} Success !");           
        }

        foreach (var s in Directory.GetDirectories (srcdir))
        {
            Debug.LogError ($"Copy Directory {s} Success !");
            CopyDirContentIntoDestDirectory (s , Path.Combine (dstdir , Path.GetFileName (s)));
        }
    }

    private void ReplaceAudioIntoDestDirectory (string srcdir , string dstdir)
    {
        if (!Directory.Exists (dstdir))
        {
            Directory.CreateDirectory (dstdir);
        }

        List<string> audioNameList = new List<string> ();
        foreach (var s in Directory.GetFiles (dstdir))
        {
            if (Path.GetFileName (s).EndsWith (".meta"))
            {
                continue;
            }
            audioNameList.Add (Path.GetFileName (s));
        }
        
        List<string> _srcAudioNameList = new List<string>(Directory.GetFiles (srcdir));

        foreach (var s in Directory.GetFiles (srcdir))
        {
            if (Path.GetFileName (s).EndsWith (".meta"))
            {
                continue;
            }
            _srcAudioNameList.Add (Path.GetFileName (s));
        }

        int _count = 0;
        foreach (string _replaceName in audioNameList)
        {
            if (_srcAudioNameList.Contains (_replaceName))
            {
                //Debug.LogError ("SrcPath=="+ Path.Combine (srcdir , _replaceName) + "==tgtpath=" + Path.Combine (dstdir , _replaceName));
                File.Copy (Path.Combine(srcdir, _replaceName), Path.Combine (dstdir , _replaceName), true);
                //Debug.LogError ($"===Replace {_replaceName} Success ");
                _count += 1;
            }
            else
            {
                Debug.LogError ($"Do Not Have {_replaceName}");
            }
        }

        Debug.LogError ($"==Need Replace {audioNameList.Count} Real Replace {_count}");
    }

    public void CopyDirContentIntoDestDirectory (string srcdir , string dstdir , bool overwrite = true)
    {
        if (!Directory.Exists (dstdir))
        {
            Directory.CreateDirectory (dstdir);
        }

        foreach (var s in Directory.GetFiles (srcdir))
        {            

            if (File.Exists (Path.Combine (dstdir , Path.GetFileName (s))) && !overwrite)
            {
                Debug.LogError ($"Exist File {s} Copy Fail !");
            }
            else
            {
                Debug.LogError ($"Copy File {s} Success !");
                File.Copy (s , Path.Combine (dstdir , Path.GetFileName (s)) , overwrite);
            }
        }

        foreach (var s in Directory.GetDirectories (srcdir))
        {
            Debug.LogError ($"Copy Directory {s} Success !");
            CopyDirContentIntoDestDirectory (s , Path.Combine (dstdir , Path.GetFileName (s)) , overwrite);
        }
    }    
}