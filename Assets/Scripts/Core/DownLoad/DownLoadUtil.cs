//using UnityEngine;
//using System.Collections;
//using System.IO;

//public class DownLoadUtil : System.Object {

//    //token
//    static public string NetWorkToken
//    {
//        get {
//            string _params = SDKTools.Call_GetParams();
//            if (!string.IsNullOrEmpty(_params)) {
//                LocalUserData _userData = JsonFx.Json.JsonReader.Deserialize<LocalUserData>(_params);
//                UIGlobal.user_data.token = _userData.token;
//            }
//            return UIGlobal.user_data.token;
//        }
//    }

//    //server url 
//    static public string ServerURL
//    {
//        get
//        {
//            return UIGlobal.user_data.serverUrl;
//        }
//    }

//    static public bool NetReachable {
//        get {
//            return Application.internetReachability != NetworkReachability.NotReachable;
//        }
//    }

//    #region 下载常量

//    //保存所有下载完成的文件信息 服务器url, 本地url, MD5, 上一次使用时间
//    static public readonly string DownLoadedCacheFileName = "DownLoadedCacheFile.txt";

//    //保存当前选择场景的文件
//    static public readonly string CacheARSceneName = "CacheARSceneName.txt";
//    //保存场景列表的文件
//    static public readonly string CacheARSceneFileName = "CacheARSceneFileName.txt";

//    //模型资源路径 
//    static private readonly string modelPath = "ModelPath/";
//    //识别图路径
//    static private readonly string markerImagePath = "MarkerImage/";
//    //压缩模型路径
//    static private readonly string compressModelPath = "CompressModelFile/";
//    //压缩图片路径
//    static private readonly string compressImagePath = "CompressImageFile/";
//    //视频路径
//    static private readonly string videoPath = "VideoPath/";
//    //音频路径
//    static private readonly string audioPath = "AudioPath/";
//    //图片路径
//    static private readonly string imagePath = "ImagePath/";
//    //缩略图路径
//    static private readonly string thumbImagePath = "ThumbImagePath/";
//    //轮廓数据路径
//    static private readonly string contourDataPath = "ContourData/";
//    //压缩轮廓数据路径
//    static private readonly string compressContourDataPath = "CompressContourFile/";

//    #endregion

//    #region 路径相关

//    /// <summary>
//    /// 资源下载后存储的根目录
//    /// </summary>
//    static private string GetLoadedCaheRootPath()
//    {
//        #if UNITY_EDITOR

//        #if UNITY_EDITOR_WIN

//        if (Directory.Exists("D:/"))
//        {
//            string _path = string.Concat("D:/", "LenovoRes/", Application.bundleIdentifier, "/");
//            if (!Directory.Exists(_path))
//            {
//                Directory.CreateDirectory(_path);
//            }
//            return _path;
//        }
//        else
//        {
//            return string.Concat(Application.persistentDataPath, "/");
//        }

//        #elif UNITY_EDITOR_OSX

//        string _path = Application.dataPath;
//        int _idx = _path.IndexOf("Documents");
//        return string.Concat(_path.Substring(0, _idx + 10),"LenovoRes/", Application.bundleIdentifier,"/");

//        #endif

//        #elif !UNITY_EDIOR && UNITY_ANDROID
//        return string.Concat(Application.persistentDataPath, "/");
//        #elif !UNITY_EDIOR && (UNITY_IOS || UNITY_IPHONE)
//        return string.Concat(Application.persistentDataPath, "/");
//        #else
//        if (Directory.Exists("D:/"))
//        {
//            string _path = string.Concat("D:/", "LenovoRes/", Application.bundleIdentifier, "/");
//            if (!Directory.Exists(_path)) {
//                Directory.CreateDirectory(_path);
//            }
//            return _path;
//        }
//        else
//        {
//            return string.Concat(Application.persistentDataPath, "/");
//        }  
//        #endif
//    }

//    /// <summary>
//    /// StreamingAssets
//    /// </summary>
//    static private string GetStreamingAssetsPath()
//    {
//#if UNITY_EDITOR
//        return string.Concat("file://", Application.streamingAssetsPath, "/");
//#elif !UNITY_EDIOR && UNITY_ANDROID
//        return string.Concat(Application.streamingAssetsPath, "/");
//#elif !UNITY_EDIOR && (UNITY_IOS || UNITY_IPHONE) 
//        return string.Concat(Application.streamingAssetsPath, "/");
//#else
//         return string.Concat("file://", Application.streamingAssetsPath, "/");
//#endif
//    }

//    static private string streamingAssetsPath;
//    static public string StreamingAssetsPath
//    {
//        get
//        {
//            if (string.IsNullOrEmpty(streamingAssetsPath))
//            {
//                streamingAssetsPath = GetStreamingAssetsPath();
//            }
//            return streamingAssetsPath;
//        }
//    }

//    //StreamingAssets ZIP 目录
//    //内部资源目录
//    public static string InternalAssetsZipDirectory
//    {
//        get
//        {
//            return string.Concat(StreamingAssetsPath, "Zip/");
//        }
//    }
//    //外部资源目录
//    public static string ExternalAssetDirectory
//    {
//        get
//        {
//            return string.Concat(CacheRootPath, "Assetbundles/");
//        }
//    }

//    /// 下载文件存储的根目录
//    static private string cacheRootPath = string.Empty;
//    static public string CacheRootPath
//    {
//        get
//        {
//            if (string.IsNullOrEmpty(cacheRootPath))
//            {
//                cacheRootPath = GetLoadedCaheRootPath();
//            }
//            return cacheRootPath;
//        }
//    }

//    //下载文件信息存储文件路径
//    static public string CacheFilePath
//    {
//        get
//        {
//            return string.Concat(CacheRootPath, DownLoadedCacheFileName);
//        }
//    }
//    // 模型存储路径
//    static public string ModelPath
//    {
//        get
//        {
//            string _path = string.Concat(CacheRootPath, modelPath);
//            if (!Directory.Exists(_path))
//            {
//                Directory.CreateDirectory(_path);
//            }
//            return _path;
//        }
//    }
//    // 模型压缩包存储路径
//    static public string CompressModelPath
//    {
//        get
//        {
//            string _path = string.Concat(CacheRootPath, compressModelPath);
//            if (!Directory.Exists(_path))
//            {
//                Directory.CreateDirectory(_path);
//            }
//            return _path;
//        }
//    }
//    // 识别图存储路径
//    static public string MarkerImagePath
//    {
//        get
//        {
//            string _path = string.Concat(CacheRootPath, markerImagePath);
//            if (!Directory.Exists(_path))
//            {
//                Directory.CreateDirectory(_path);
//            }
//            return _path;
//        }
//    }
//    // 图片压缩包存储路径
//    static public string CompressImagePath
//    {
//        get
//        {
//            string _path = string.Concat(CacheRootPath, compressImagePath);
//            if (!Directory.Exists(_path))
//            {
//                Directory.CreateDirectory(_path);
//            }
//            return _path;
//        }
//    }
//    // 图片路径
//    static public string ImagePath
//    {
//        get
//        {
//            string _path = string.Concat(CacheRootPath, imagePath);
//            if (!Directory.Exists(_path))
//            {
//                Directory.CreateDirectory(_path);
//            }
//            return _path;
//        }
//    }
//    // 缩略图路径
//    static public string ThumbImagePath
//    {
//        get
//        {
//            string _path = string.Concat(CacheRootPath, thumbImagePath);
//            if (!Directory.Exists(_path))
//            {
//                Directory.CreateDirectory(_path);
//            }
//            return _path;
//        }
//    }
//    // 视频存储路径
//    static public string VideoPath
//    {
//        get
//        {
//            string _path = string.Concat(CacheRootPath, videoPath);
//            if (!Directory.Exists(_path))
//            {
//                Directory.CreateDirectory(_path);
//            }
//            return _path;
//        }
//    }
//    // 音频存储路径
//    static public string AudioPath
//    {
//        get
//        {
//            string _path = string.Concat(CacheRootPath, audioPath);
//            if (!Directory.Exists(_path))
//            {
//                Directory.CreateDirectory(_path);
//            }
//            return _path;
//        }
//    }
//    // 轮廓数据压缩包路径
//    static public string CompressContourDataPath
//    {
//        get
//        {
//            string _path = string.Concat(CacheRootPath, compressContourDataPath);
//            if (!Directory.Exists(_path))
//            {
//                Directory.CreateDirectory(_path);
//            }
//            return _path;
//        }
//    }
//    // 轮廓数据路径
//    static public string ContourDataPath
//    {
//        get
//        {
//            string _path = string.Concat(CacheRootPath, contourDataPath);
//            if (!Directory.Exists(_path))
//            {
//                Directory.CreateDirectory(_path);
//            }
//            return _path;
//        }
//    }

//    //获取指定目录下的文件列表
//    static public string[] GetFilesByDirectory(string path) {
//        if (Directory.Exists(path))
//        {
//            return Directory.GetFiles(path);
//        }
//        else {
//            return null;
//        }
//    }

//    //路径后的文件名 包含后缀
//    static public string GetFileNameByURL(string url)
//    {
//        if (string.IsNullOrEmpty(url))
//        {
//            return null;
//        }

//        int _idx = url.LastIndexOf('/');
//        if (_idx + 1 >= url.Length)
//        {
//            return null;
//        }

//        return url.Substring(_idx + 1);
//    }

//    /// <summary>
//    /// 获取path最后的文件名 不带扩展名
//    /// D:/test\123\456\123.txt
//    /// http://c.newbd.com/Test/CoralSet_12.jpg
//    /// </summary>
//    static public string GetFileNameWithoutExtensionByURL(string path)
//    {
//        if (string.IsNullOrEmpty(path))
//        {
//            return null;
//        }
//        else
//        {
//            int _index = -1;
//            if (path.Contains("http"))
//            {
//                _index = path.LastIndexOf('/');
//            }
//            else
//            {
//                _index = path.LastIndexOf('/');
//            }

//            if (_index >= path.Length || _index < 0)
//            {
//                return null;
//            }
//            else
//            {
//                string _filName = path.Substring(_index + 1);
//                int _dotIdx = _filName.LastIndexOf('.');
//                if (_dotIdx > 0)
//                {
//                    return _filName.Remove(_dotIdx);
//                }
//                else {
//                    return _filName;
//                }
//            }
//        }
//    }

//    /// <summary>
//    /// 获取文件后缀名
//    /// </summary>
//    static public string GetFileExtensionByUrl(string url)
//    {
//        if (string.IsNullOrEmpty(url))
//        {
//            return null;
//        }

//        int _idx = url.LastIndexOf('.');
//        if (_idx < 0)
//        {
//            return null;
//        }

//        return url.Substring(_idx);
//    }


//    /// <summary>
//    /// 截去url参数
//    /// </summary>
//    static public string FormatURL(string url){
//        if (string.IsNullOrEmpty(url)) {
//            return url;
//        }
//        int _index = url.LastIndexOf(URL_FORMAT);
//        if (_index > 0)
//        {
//            return url.Substring(0, _index);
//        }
//        else {            
//            return url;
//        }
//    }   


//    #endregion

//    #region 读取保存文件相关
//    //将文本信息保存到指定路径，替换原有文件
//    static public void SaveTextToPath(string fileName, string text)
//    {
//        using (FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate))
//        {
//            stream.Seek(0, SeekOrigin.Begin);
//            //清空文本里数据
//            stream.SetLength(0);
//            using (StreamWriter writer = new StreamWriter(stream, System.Text.Encoding.UTF8))
//            {
//                writer.Write(text);
//                writer.Flush();
//                writer.Close();
//            }
//            stream.Close();
//        }
//    }

//    //读取指定txt文件的所有信息
//    static public string ReadFileContentByPath(string path)
//    {
//        if (File.Exists(path))
//        {
//            StreamReader stream = new StreamReader(path);
//            if (stream != null)
//                return stream.ReadToEnd();
//            stream.Close();
//        }
//        return null;
//    }

//    //读取指定文件的所有信息
//    static public byte[] ReadFileBytes(string path)
//    {
//        if (File.Exists(path))
//        {
//            //创建文件读取流
//            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
//            fileStream.Seek(0, SeekOrigin.Begin);
//            //创建文件长度缓冲区
//            byte[] bytes = new byte[fileStream.Length];
//            //读取文件
//            fileStream.Read(bytes, 0, (int)fileStream.Length);
//            //释放文件读取流
//            fileStream.Close();
//            fileStream.Dispose();
//            fileStream = null;

//            return bytes;
//        }
//        return null;
//    }

//    //读取图片
//    static public Texture2D ReadTexture2D(string path) {
//        byte[] _bytes = ReadFileBytes(path);

//        Texture2D mTex2D = null;


//        if (_bytes == null || _bytes.Length < 1)
//        {
//        }
//        else {
//            mTex2D = new Texture2D(2, 2, TextureFormat.PVRTC_RGBA4, false);
//            mTex2D.LoadImage(_bytes);
//        }
//        return mTex2D;
//    }

//    static public Sprite ReadSprite(string path) {
//        Texture2D _tex = ReadTexture2D(path);
//        if (_tex == null)
//        {
//            return null;
//        }
//        else {
//            Sprite _sprite = Sprite.Create(_tex, new Rect(0, 0, _tex.width, _tex.height), new Vector2(0.5f, 0.5f));
//            _sprite.name = _tex.name;
//            return _sprite;
//        }
//    }

//    //保存bytes文件到指定路径，替换原有文件
//    static public void SaveBytesToPath(string rootpath, string fileName, byte[] bytes)
//    {
//        if (string.IsNullOrEmpty(rootpath) || string.IsNullOrEmpty(fileName))
//        {
//            Debug.LogWarning("路径错误 ==> rootpath " + rootpath + " fileName ==> " + fileName);
//            return;
//        }

//        string filePath = Path.Combine(rootpath, fileName);
//        if (!Directory.Exists(rootpath))
//        {
//            Directory.CreateDirectory(rootpath);
//        }

//        if (File.Exists(filePath))
//        {
//            File.Delete(filePath);
//        }

//        FileStream wfs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
//        wfs.Write(bytes, 0, bytes.Length);
//        wfs.Close();
//    }       

//#endregion

//#region 删除    

//    //删除指定文件
//    static public void DeleteFileByPath(string filePath) {
//        if (File.Exists(filePath)) {
//            File.Delete(filePath);
//        }
//    }

//    //删除指定目录
//    static public void DeleteFileByDir(string dir)
//    {
//        if (Directory.Exists(dir))
//        {
//            Directory.Delete(dir, true);
//        }
//    }

//#endregion

//#region 解压相关

//    //解压文件到指定路径
//    static public void DecompressToPath(string targetPath, string decompressFilePath, bool isDeletePre = false)
//    {

//        if (string.IsNullOrEmpty(targetPath) || string.IsNullOrEmpty(decompressFilePath))
//        {
//            Debug.LogWarning("路径错误 ==> targetPath " + targetPath + " decompressFilePath ==> " + decompressFilePath);
//            return;
//        }

//        if (!Directory.Exists(targetPath))
//        {
//            Directory.CreateDirectory(targetPath);
//        }
//        else
//        {
//            //删除同名文件夹里的资源
//            if (isDeletePre)
//            {
//                Directory.Delete(targetPath, true);
//            }
//        }

//        ZipUtil.DecompressToDirectory(targetPath, decompressFilePath);

//        // 将fbx名字改为小写
//        // ios区分大小写
//        if (Directory.Exists(targetPath))
//        {
//            //获取文件信息
//            DirectoryInfo direction = new DirectoryInfo(targetPath);

//            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);            

//            for (int i = 0; i < files.Length; i++)
//            {
//                string _ex = files[i].Extension.ToLower();
//                if (_ex.Equals(".fbx"))
//                {
//                    //string _name = Path.GetFileNameWithoutExtension(files[i].ToString());
//                    //string _lastName = string.Concat(_name, files[i].Extension.ToLower());
//                    //string _targetPath = string.Concat(targetPath, "/").Replace("//", "/");
//                    //files[i].MoveTo(string.Concat(_targetPath, "temp.fbx"));
//                    //DeleteFileByPath(files[i].ToString());
//                    //byte[] _bytes = ReadFileBytes(string.Concat(_targetPath, "temp.fbx"));
//                    //SaveBytesToPath(targetPath, string.Concat(_name, files[i].Extension.ToLower()), _bytes);
//                    //DeleteFileByPath(string.Concat(_targetPath, "temp.fbx"));

//                    string _name = Path.GetFileName(files[i].ToString());
//                    string _targetPath = string.Concat(targetPath, "/").Replace("//", "/");
//                    files[i].MoveTo(string.Concat(_targetPath, "temp.fbx"));
//                    DeleteFileByPath(files[i].ToString());
//                    byte[] _bytes = ReadFileBytes(string.Concat(_targetPath, "temp.fbx"));
//                    SaveBytesToPath(targetPath, _name.ToLower(), _bytes);
//                    DeleteFileByPath(string.Concat(_targetPath, "temp.fbx"));
//                }
//            }
//        }

//        //删除压缩包
//        if (File.Exists(decompressFilePath))
//        {
//            File.Delete(decompressFilePath);
//        }
//    }


//    /// <summary>
//    /// 遍历子文件夹，把所有文件放到根目录
//    /// 并删除子文件夹
//    /// </summary>
//    static public void CopyAllFileToRoot(string rootPath)
//    {
//        //获得所有子文件夹
//        if (Directory.GetDirectories(rootPath).Length > 0)
//        {
//            //遍历所有文件夹
//            foreach (string childPath in Directory.GetDirectories(rootPath))
//            {
//                //获得子文件夹下的所有文件
//                string[] childFiles = Directory.GetFiles(childPath);
//                foreach (string file in childFiles)
//                {
//                    //获得最后一个/的索引 '\\'可能有问题，待测试s
//                    int _index = file.LastIndexOf('\\');
//                    if (_index >= file.Length || _index < 0)
//                    {
//                    }
//                    else
//                    {
//                        //string _fileName = file.Substring(_index + 1);
//                        CopyFileToPath(file, rootPath);
//                    }
//                }
//                //递归子文件夹
//                CopyAllFileToRoot(childPath);
//            }
//        }
//    }

//    static public void CopyFileToPath(string srcFileName, string tgtPath)
//    {
//        //获得最后一个/的索引
//        int _index = srcFileName.LastIndexOf('/');
//        if (_index >= srcFileName.Length || _index < 0)
//        {
//        }
//        else
//        {
//            string _fileName = srcFileName.Substring(_index + 1);
//            File.Copy(srcFileName, string.Concat(tgtPath, _fileName), true);
//        }
//    }

//    static public void DeleteAllFoldInRoot(string rootPath)
//    {
//        //获得所有子文件夹
//        if (Directory.GetDirectories(rootPath).Length > 0)
//        {
//            //遍历所有文件夹
//            foreach (string childPath in Directory.GetDirectories(rootPath))
//            {
//                Directory.Delete(childPath, true);
//            }
//        }
//    }

//#endregion


//#region 通用常量

//    static private Shader unlitTransparentCutoutShader;
//    static public Shader UnlitTransparentCutoutShader
//    {
//        get
//        {
//            if (unlitTransparentCutoutShader == null)
//            {
//                //unlitTransparentCutoutShader = Shader.Find("Unlit/Texture");
//                unlitTransparentCutoutShader = Shader.Find("Unlit/Transparent Cutout");
//            }
//            return unlitTransparentCutoutShader;
//        }
//    }

//    static private Material frameMeshMaterial;
//    static public Material FrameMeshMaterial
//    {
//        get
//        {
//            if (frameMeshMaterial == null)
//            {
//                frameMeshMaterial = Resources.Load<Material>("Materials/GreenFrame");
//            }
//            return frameMeshMaterial;
//        }
//    }

//    static public readonly string JSON_FORMAT = "\"{0}\":\"{1}\"";

//    static public readonly string THUMB_IMAGE_FORMAT = "?x-oss-process=image/resize,w_280,h_200,limit_0";

//    static public readonly string MARKER_IMAGE_FORMAT = "?x-oss-process=image/resize,w_1024,h_1024,limit_0";

//    static public readonly string URL_FORMAT = "?x-oss-process=image";


//    static public readonly string LANGUAGE_CN = "translate_zh_CN";
//    static public readonly string LANGUAGE_US = "translate_zh_US";

//    static public readonly string NAME_FORMAT = "...";

//    static public readonly string COLOR_MATCH = "<color>(.*)</color>";
   
//    static public readonly string MARTIN_URL = "http://47.95.255.48:3456/tp/v0.1/aimodels/";

//    #endregion
//}
