using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class TestWrite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string fileName = "C:/Users/nbd/Desktop/www/tttt.json";
        FileStream stream = new FileStream (fileName , FileMode.OpenOrCreate);
        stream.Seek (0 , SeekOrigin.Begin);

        byte[] info = Encoding.UTF8.GetBytes ("AAA\r\n");
        stream.Write (info , 0 , info.Length);

        info = Encoding.UTF8.GetBytes ("BBB\r\n");
        stream.Write (info , 0 , info.Length);

        info = Encoding.UTF8.GetBytes ("CCC\r\n");
        stream.Write (info , 0 , info.Length);

        stream.Flush ();

        stream.Seek (0 , SeekOrigin.Begin);

        info = Encoding.UTF8.GetBytes ("DDD\r\n");
        stream.Write (info , 0 , info.Length);

        stream.Flush ();

        info = Encoding.UTF8.GetBytes ("EEE\r\n");
        stream.Write (info , 0 , info.Length);

        //stream.Seek (stream.Length , SeekOrigin.Begin);

        //info = Encoding.UTF8.GetBytes ("EEE\r\n");
        //stream.Write (info , 0 , info.Length);

        //stream.Flush ();

        stream.Close ();

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
