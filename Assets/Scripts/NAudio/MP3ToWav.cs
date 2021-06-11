using NAudio.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MP3ToWAV : MonoBehaviour
{
    WaitForEndOfFrame wf = new WaitForEndOfFrame ();

    int fileCount = 0;
    List<string> fileList = new List<string> ();

    string tgtDir;
    string tgtPath;

    public void StartCopyAudio ()
    {
        tgtDir = Application.persistentDataPath;


        CollectFile (Path.Combine (Application.streamingAssetsPath , "Sounds") ,
            Path.Combine (Application.persistentDataPath , "Sounds"));
        
        fileCount = fileList.Count;

        StartCoroutine (CopyAudio ());
    }

    IEnumerator CopyAudio()
    {
        yield return null;
        while (fileCount > 0)
        {
            yield return new WaitForEndOfFrame ();
            fileCount -= 1;

            int start = fileList[fileCount].IndexOf ("Sounds");
            int end = fileList[fileCount].LastIndexOf ("\\");

            tgtPath = Path.Combine (tgtDir , fileList[fileCount].Substring (start , ( end - start )) , Path.GetFileName (fileList[fileCount]));

            if (fileList[fileCount].EndsWith (".mp3"))
            {
                tgtPath = tgtPath.Replace (".mp3" , ".wav");

                //判断是否已经转换过
                if (File.Exists (tgtPath))
                {
                    File.Delete (tgtPath);
                }
                //Debug.LogError ($"WaveFileWriter Copy File {tgtPath} Success !");
                //将MP3转换成WAV
                using (var audioStrem = new Mp3FileReader (fileList[fileCount]))
                {
                    WaveFileWriter.CreateWaveFile (tgtPath , audioStrem);
                }
            }
            else
            {
                if (File.Exists (tgtPath))
                {
                    File.Delete (tgtPath);                    
                    //Debug.LogError ($"Copy File {tgtPath} Success !");
                }
                File.Copy (fileList[fileCount] , tgtPath);
            }
        }
    }

    private void CollectFile (string srcdir , string dstdir)
    {
        if (!Directory.Exists (dstdir))
        {
            Directory.CreateDirectory (dstdir);
        }

        foreach (var s in Directory.GetFiles (srcdir))
        {
            if (Path.GetFileName (s).EndsWith (".meta"))
            {
                continue;
            }

            fileList.Add (s);           
        }

        foreach (var s in Directory.GetDirectories (srcdir))
        {
            CollectFile (s , Path.Combine (dstdir , Path.GetFileName (s)));
        }
    }
}
