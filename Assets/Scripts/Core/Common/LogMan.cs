using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine.Networking;

public class LogMan : MonoBehaviour
{
    static UTF8Encoding _utf8 = new UTF8Encoding(true);

    StringBuilder _stringBuilder = new StringBuilder ();

    public bool openLog = false;

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void OnDestroy()
    {
        if (openLog)
        {
            if (_logFs != null)
            {
                _logFs.Flush();
                _logFs.Close();
                _logFs.Dispose ();
                _logFs = null;
            }            

        }
    }

    void Start()
    {
        InitFileStream ();

        Application.logMessageReceived += HandleLog;
    }

    public static FileStream _logFs = null;

    void InitFileStream()
    {       
        #if UNITY_EDITOR

        if (!Directory.Exists(Application.persistentDataPath + @"/Log"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + @"/Log");
        }
        _logFs = new FileStream(Application.persistentDataPath + @"/Log/logs.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

        #elif !UNITY_EDITOR && UNITY_ANDROID
        string _path = string.Concat(Application.persistentDataPath, "/LOG");
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
            Debug.LogError(" Log Error==> CreateDirectory");
        }
        _logFs = new FileStream(_path + "/logs.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

        Debug.LogError(" FileStream   " + _path + "/logs.txt");

        #elif !UNITY_EDITOR && ( UNITY_IPHONE || UNITY_IOS )

        //if (!Directory.Exists(Application.persistentDataPath + @"/Log"))
        //{
        //    Directory.CreateDirectory(Application.persistentDataPath + @"/Log");
        //}
        //_logFs = new FileStream(Application.persistentDataPath + @"/Log/logs.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

        #else

        if (!Directory.Exists(Application.persistentDataPath + @"/Log"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + @"/Log");
        }
        _logFs = new FileStream(Application.persistentDataPath + @"/Log/logs.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        #endif
    }

    int count = 0;
    public void LogErr(LogType type, string logStr, string stackTrace)
    {
        if (openLog)
        {
            _stringBuilder.Append ("[");
            _stringBuilder.Append (System.DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss"));
            _stringBuilder.Append ("]");

            _stringBuilder.Append ("[");
            _stringBuilder.Append (type);
            _stringBuilder.Append ("]");

            _stringBuilder.Append (logStr);
            _stringBuilder.Append (" <==> ");
            _stringBuilder.Append (stackTrace);

            _stringBuilder.AppendLine();
            _stringBuilder.AppendLine ();

            //string str = "[" + System.DateTime.Now.ToString("HH:mm:ss") + "]" + "[" + type + "]" + logStr + " <==> " + stackTrace + "\r\n\n";
           
            byte[] info = _utf8.GetBytes(_stringBuilder.ToString());
            
            _logFs.Write (info , 0 , info.Length);

            _stringBuilder.Clear ();

            count += 1;

            if (count >= 100)
            {
                _logFs.Flush ();
                count = 0;
            }
        }
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (openLog)
        {
            if (type == LogType.Error || type == LogType.Warning || type == LogType.Exception || type == LogType.Log)
            {            
                LogErr(type, logString, stackTrace);
            }
        }
    }
}
