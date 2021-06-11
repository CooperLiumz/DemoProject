using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;
using System.Runtime.InteropServices;

public static class ZipUtil
{

    /// <summary>
    /// 将sourcePath 目录压缩到 outputFilePath 目录下
    /// </summary>
    /// <param name="sourcePath"></param>
    /// <param name="outputFilePath"></param>
    /// <param name="zipLevel"></param>
    public static void CompressDirectory ( string sourcePath, string outputFilePath, int zipLevel = 5 )
    {
        new FileStream ( outputFilePath, FileMode.OpenOrCreate ).CompressDirectory ( sourcePath, zipLevel );
    }

    /// <summary>
    /// 将 zipfile 解压缩到 targetpath下
    /// </summary>
    /// <param name="targetPath"></param>
    /// <param name="zipFilePath"></param>
    public static void DecompressToDirectory ( string targetPath, string zipFilePath )
    {
        if (File.Exists ( zipFilePath ))
        {
            File.OpenRead ( zipFilePath ).DecompressToDirectory ( targetPath );
        }
    }

    private static void CompressDirectory ( this Stream target, string sourcepath, int ziplevel )
    {
        sourcepath = Path.GetFullPath ( sourcepath );
        int startIndex = string.IsNullOrEmpty ( sourcepath ) ? Path.GetPathRoot ( sourcepath ).Length : sourcepath.Length;
        List<string> list = new List<string> ();
        list.AddRange ( from d in Directory.GetDirectories ( sourcepath, "*", SearchOption.AllDirectories ) select d + @"\" );
        list.AddRange ( Directory.GetFiles ( sourcepath, "*", SearchOption.AllDirectories ) );
        using (ZipOutputStream stream = new ZipOutputStream ( target ))
        {
            stream.SetLevel ( ziplevel );
            foreach (string str in list)
            {
                string input = str.Substring ( startIndex );
                string name = input.StartsWith ( @"\" ) ? input.ReplaceFirst ( @"\", "", 0 ) : input;
                name = name.Replace ( @"\", "/" );
                stream.PutNextEntry ( new ZipEntry ( name ) );
                if (!str.EndsWith ( @"\" ))
                {
                    //每次读2M
                    byte[] buff = new byte[0x800];
                    using (FileStream stream2 = File.OpenRead ( str ))
                    {
                        int num2;
                        while (( num2 = stream2.Read ( buff, 0, buff.Length ) ) > 0)
                        {
                            stream.Write ( buff, 0, num2 );
                        }
                        stream2.Flush ();
                        stream2.Close ();
                    }
                }
            }
            stream.Finish ();
        }
    }

    private static void DecompressToDirectory ( this Stream source, string targetPath )
    {
        targetPath = Path.GetFullPath ( targetPath );
        using (ZipInputStream stream = new ZipInputStream ( source ))
        {
            ZipEntry entry;
            while (( entry = stream.GetNextEntry () ) != null)
            {
                string name = entry.Name;
                if (entry.IsDirectory && entry.Name.StartsWith ( @"\" ))
                {
                    name = entry.Name.ReplaceFirst ( @"\", "", 0 );
                }
                string path = Path.Combine ( targetPath, name );
                string directoryName = Path.GetDirectoryName ( path );
                if (!( string.IsNullOrEmpty ( directoryName ) || Directory.Exists ( directoryName ) ))
                {
                    Directory.CreateDirectory ( directoryName );
                }
                if (!entry.IsDirectory)
                {
                    byte[] buff = new byte[0x800];
                    using (FileStream stream2 = File.Create ( path ))
                    {
                        int num;
                        //一次读2M
                        while (( num = stream.Read ( buff, 0, buff.Length ) ) > 0)
                        {
                            stream2.Write ( buff, 0, num );
                        }
                        stream2.Flush ();
                        stream2.Close ();
                    }
                }
            }
            stream.Close ();
        }
    }


    /// <summary>
    /// 扩展方法必须写在 静态的非泛型类内
    /// </summary>
    public static string ReplaceFirst ( this string input, string oldValue, string newValue, int startAt = 0 )
    {
        int index = input.IndexOf ( oldValue, startAt );
        if (index < 0)
            return input;
        return ( input.Substring ( 0, index ) + newValue + input.Substring ( index + oldValue.Length ) );
    }
}
