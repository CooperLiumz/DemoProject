using UnityEngine;
using System.Collections;
using System.IO;

public class DownLoadUtil : System.Object
{

    static public bool NetReachable
    {
        get
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
    }   

    #region 路径相关

    /// <summary>
    /// 资源下载后存储的根目录
    /// </summary>
    static private string GetLoadedCaheRootPath ()
    {
        #if UNITY_EDITOR

            #if UNITY_EDITOR_WIN

            if (Directory.Exists ( "D:/" ))
            {
                string _path = string.Concat ( "D:/", "LenovoRes/", Application.identifier, "/" );
                if (!Directory.Exists ( _path ))
                {
                    Directory.CreateDirectory ( _path );
                }
                return _path;
            }
            else
            {
                return string.Concat ( Application.persistentDataPath, "/" );
            }

            #elif UNITY_EDITOR_OSX

            string _path = Application.dataPath;
            int _idx = _path.IndexOf("Documents");
            return string.Concat(_path.Substring(0, _idx + 10),"LenovoRes/", Application.identifier,"/");

            #endif

        #elif !UNITY_EDIOR && UNITY_ANDROID
            return string.Concat(Application.persistentDataPath, "/");
        #elif !UNITY_EDIOR && ( UNITY_IOS || UNITY_IPHONE )
            return string.Concat(Application.persistentDataPath, "/");
        #else
            if (Directory.Exists("D:/"))
            {
                string _path = string.Concat("D:/", "LenovoRes/", Application.identifier, "/");
                if (!Directory.Exists(_path)) {
                    Directory.CreateDirectory(_path);
                }
                return _path;
            }
            else
            {
                return string.Concat(Application.persistentDataPath, "/");
            }  
        #endif
    }

    /// <summary>
    /// StreamingAssets
    /// </summary>
    static private string GetStreamingAssetsPath ()
    {
        #if UNITY_EDITOR_WIN
        return string.Concat ( Application.streamingAssetsPath, "/" );
        #elif UNITY_EDITOR_OSX
        return string.Concat("file://", Application.streamingAssetsPath, "/");
        #elif !UNITY_EDIOR && UNITY_ANDROID
        return string.Concat(Application.streamingAssetsPath, "/");
        #elif !UNITY_EDIOR && ( UNITY_IOS || UNITY_IPHONE )
        return string.Concat("file://", Application.streamingAssetsPath, "/");
        #else
        return string.Concat ( Application.streamingAssetsPath, "/" );
        #endif
    }

    static private string streamingAssetsPath;
    static public string StreamingAssetsPath
    {
        get
        {
            if (string.IsNullOrEmpty ( streamingAssetsPath ))
            {
                streamingAssetsPath = GetStreamingAssetsPath ();
            }
            return streamingAssetsPath;
        }
    }

    /// 下载文件存储的根目录
    static private string cacheRootPath = string.Empty;
    static public string CacheRootPath
    {
        get
        {
            if (string.IsNullOrEmpty ( cacheRootPath ))
            {
                cacheRootPath = GetLoadedCaheRootPath ();
            }
            return cacheRootPath;
        }
    }



    //获取指定目录下的文件列表
    static public string[] GetFilesByDirectory ( string path )
    {
        if (Directory.Exists ( path ))
        {
            return Directory.GetFiles ( path );
        }
        else
        {
            return null;
        }
    }

    //路径后的文件名 包含后缀
    static public string GetFileNameByURL ( string url )
    {
        if (string.IsNullOrEmpty ( url ))
        {
            return null;
        }

        int _idx = url.LastIndexOf ( '/' );
        if (_idx + 1 >= url.Length)
        {
            return null;
        }

        return url.Substring ( _idx + 1 );
    }

    /// <summary>
    /// 获取path最后的文件名 不带扩展名
    /// D:/test\123\456\123.txt
    /// http://c.newbd.com/Test/CoralSet_12.jpg
    /// </summary>
    static public string GetFileNameWithoutExtensionByURL ( string path )
    {
        if (string.IsNullOrEmpty ( path ))
        {
            return null;
        }
        else
        {
            int _index = -1;
            if (path.Contains ( "http" ))
            {
                _index = path.LastIndexOf ( '/' );
            }
            else
            {
                _index = path.LastIndexOf ( '/' );
            }

            if (_index >= path.Length || _index < 0)
            {
                return null;
            }
            else
            {
                string _filName = path.Substring ( _index + 1 );
                int _dotIdx = _filName.LastIndexOf ( '.' );
                if (_dotIdx > 0)
                {
                    return _filName.Remove ( _dotIdx );
                }
                else
                {
                    return _filName;
                }
            }
        }
    }

    /// <summary>
    /// 获取文件后缀名
    /// </summary>
    static public string GetFileExtensionByUrl ( string url )
    {
        if (string.IsNullOrEmpty ( url ))
        {
            return null;
        }

        int _idx = url.LastIndexOf ( '.' );
        if (_idx < 0)
        {
            return null;
        }

        return url.Substring ( _idx );
    } 

    #endregion

    #region 读取保存文件相关
    //将文本信息保存到指定路径，替换原有文件
    static public void SaveTextToPath ( string fileName, string text )
    {
        using (FileStream stream = new FileStream ( fileName, FileMode.OpenOrCreate ))
        {
            stream.Seek ( 0, SeekOrigin.Begin );
            //清空文本里数据
            stream.SetLength ( 0 );
            using (StreamWriter writer = new StreamWriter ( stream, System.Text.Encoding.UTF8 ))
            {
                writer.Write ( text );
                writer.Flush ();
                writer.Close ();
            }
            stream.Close ();
        }
    }

    //读取指定txt文件的所有信息
    static public string ReadFileContentByPath ( string path )
    {
        if (File.Exists ( path ))
        {
            StreamReader stream = new StreamReader ( path );
            if (stream != null)
                return stream.ReadToEnd ();
            stream.Close ();
        }
        return null;
    }

    //读取指定文件的所有信息
    static public byte[] ReadFileBytes ( string path )
    {
        if (File.Exists ( path ))
        {
            //创建文件读取流
            FileStream fileStream = new FileStream ( path, FileMode.Open, FileAccess.Read );
            fileStream.Seek ( 0, SeekOrigin.Begin );
            //创建文件长度缓冲区
            byte[] bytes = new byte[fileStream.Length];
            //读取文件
            fileStream.Read ( bytes, 0, (int)fileStream.Length );
            //释放文件读取流
            fileStream.Close ();
            fileStream.Dispose ();
            fileStream = null;

            return bytes;
        }
        return null;
    }

    //读取图片
    static public Texture2D ReadTexture2D ( string path )
    {
        byte[] _bytes = ReadFileBytes ( path );

        Texture2D mTex2D = null;


        if (_bytes == null || _bytes.Length < 1)
        {
        }
        else
        {
            mTex2D = new Texture2D ( 2, 2, TextureFormat.PVRTC_RGBA4, false );
            mTex2D.LoadImage ( _bytes );
        }
        return mTex2D;
    }

    static public Sprite ReadSprite ( string path )
    {
        Texture2D _tex = ReadTexture2D ( path );
        if (_tex == null)
        {
            return null;
        }
        else
        {
            Sprite _sprite = Sprite.Create ( _tex, new Rect ( 0, 0, _tex.width, _tex.height ), new Vector2 ( 0.5f, 0.5f ) );
            _sprite.name = _tex.name;
            return _sprite;
        }
    }

    //保存bytes文件到指定路径，替换原有文件
    static public void SaveBytesToPath ( string rootpath, string fileName, byte[] bytes )
    {
        if (string.IsNullOrEmpty ( rootpath ) || string.IsNullOrEmpty ( fileName ))
        {
            Debug.LogWarning ( "路径错误 ==> rootpath " + rootpath + " fileName ==> " + fileName );
            return;
        }

        string filePath = Path.Combine ( rootpath, fileName );
        if (!Directory.Exists ( rootpath ))
        {
            Directory.CreateDirectory ( rootpath );
        }

        if (File.Exists ( filePath ))
        {
            File.Delete ( filePath );
        }

        FileStream wfs = new FileStream ( filePath, FileMode.Create, FileAccess.ReadWrite );
        wfs.Write ( bytes, 0, bytes.Length );
        wfs.Close ();
    }

    #endregion

    #region 删除    

    //删除指定文件
    static public void DeleteFileByPath ( string filePath )
    {
        if (File.Exists ( filePath ))
        {
            File.Delete ( filePath );
        }
    }

    //删除指定目录
    static public void DeleteFileByDir ( string dir )
    {
        if (Directory.Exists ( dir ))
        {
            Directory.Delete ( dir, true );
        }
    }

    #endregion

    #region 解压相关

    //解压文件到指定路径
    static public void DecompressToPath ( string targetPath, string decompressFilePath, bool isDeletePre = false )
    {

        //if (string.IsNullOrEmpty ( targetPath ) || string.IsNullOrEmpty ( decompressFilePath ))
        //{
        //    Debug.LogWarning ( "路径错误 ==> targetPath " + targetPath + " decompressFilePath ==> " + decompressFilePath );
        //    return;
        //}

        //if (!Directory.Exists ( targetPath ))
        //{
        //    Directory.CreateDirectory ( targetPath );
        //}
        //else
        //{
        //    //删除同名文件夹里的资源
        //    if (isDeletePre)
        //    {
        //        Directory.Delete ( targetPath, true );
        //    }
        //}

        //ZipUtil.DecompressToDirectory ( targetPath, decompressFilePath );

        ////删除压缩包
        //if (File.Exists ( decompressFilePath ))
        //{
        //    File.Delete ( decompressFilePath );
        //}
    }


    /// <summary>
    /// 遍历子文件夹，把所有文件放到根目录
    /// 并删除子文件夹
    /// </summary>
    static public void CopyAllFileToRoot ( string rootPath )
    {
        //获得所有子文件夹
        if (Directory.GetDirectories ( rootPath ).Length > 0)
        {
            //遍历所有文件夹
            foreach (string childPath in Directory.GetDirectories ( rootPath ))
            {
                //获得子文件夹下的所有文件
                string[] childFiles = Directory.GetFiles ( childPath );
                foreach (string file in childFiles)
                {
                    //获得最后一个/的索引 '\\'可能有问题，待测试s
                    int _index = file.LastIndexOf ( '\\' );
                    if (_index >= file.Length || _index < 0)
                    {
                    }
                    else
                    {
                        //string _fileName = file.Substring(_index + 1);
                        CopyFileToPath ( file, rootPath );
                    }
                }
                //递归子文件夹
                CopyAllFileToRoot ( childPath );
            }
        }
    }

    static public void CopyFileToPath ( string srcFileName, string tgtPath )
    {
        //获得最后一个/的索引
        int _index = srcFileName.LastIndexOf ( '/' );
        if (_index >= srcFileName.Length || _index < 0)
        {
        }
        else
        {
            string _fileName = srcFileName.Substring ( _index + 1 );
            File.Copy ( srcFileName, string.Concat ( tgtPath, _fileName ), true );
        }
    }

    static public void DeleteAllFoldInRoot ( string rootPath )
    {
        //获得所有子文件夹
        if (Directory.GetDirectories ( rootPath ).Length > 0)
        {
            //遍历所有文件夹
            foreach (string childPath in Directory.GetDirectories ( rootPath ))
            {
                Directory.Delete ( childPath, true );
            }
        }
    }

    #endregion   
}
