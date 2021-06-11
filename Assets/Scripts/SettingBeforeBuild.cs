#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class SettingBeforeBuild : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    public int callbackOrder => 0;

    string relativePath = @"BuildTime.json";

    public void OnPreprocessBuild (BuildReport report)
    {
        Debug.LogError (Application.persistentDataPath);
        Debug.LogError (Application.temporaryCachePath);
        Debug.LogError (Application.streamingAssetsPath);
        SaveTextToPath (Path.Combine(Application.streamingAssetsPath , relativePath) , DateTime.Now.ToString());

        Debug.Log ("打包前");
    }

    public void OnPostprocessBuild (BuildReport report)
    {
        Debug.Log ("打包后");
    }

    public void SaveTextToPath (string fileName , string text)
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
#endif
