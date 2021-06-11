using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Security.Cryptography;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using UnityEngine.Networking;

public class AssetManager : MonoBehaviour
{

    private static AssetManager mInstance;
    private static readonly object mStaticSyncRoot = new object();

    private Dictionary<string, UnityEngine.Object> mPrefabPool = new Dictionary<string, UnityEngine.Object>();

    private AssetManager() { }

    public static AssetManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                lock (mStaticSyncRoot)
                {
                    if (mInstance == null)
                    {
                        GameObject singleton = GameObject.Find("_Singleton");
                        if (singleton == null)
                        {
                            singleton = new GameObject("_Singleton");
                        }
                        mInstance = singleton.AddComponent<AssetManager>();
                        Screen.sleepTimeout = SleepTimeout.NeverSleep;
                    }
                }
            }
            return mInstance;
        }
    }    

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    //本地加载Res
    public GameObject InstantiateFromResources(string _path, bool _cache = false)
    {
        UnityEngine.Object obj = null;

        if (mPrefabPool.ContainsKey(_path))
        {
            obj = mPrefabPool[_path];
        }
        else if (_cache)
        {
            CachePrefabFromResources(_path);
            obj = mPrefabPool[_path];
        }
        else
        {
            obj = Resources.Load(_path, typeof(GameObject));
        }

        if (obj == null)
        {
            Debug.LogError(string.Format("{0} isn't exist!", _path));
        }
        else
        {
            GameObject go = Instantiate(obj) as GameObject;
            go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
            return go;
        }
        return null;
    }

    //缓存Res
    private void CachePrefabFromResources(string _path)
    {
        if (!mPrefabPool.ContainsKey(_path))
        {
            UnityEngine.Object obj = Resources.Load(_path, typeof(GameObject));
            if (obj == null)
            {
                Debug.LogError(_path + " not exists !");
                return;
            }
            mPrefabPool[_path] = obj;
        }
    }

    //加载图片
    public Sprite LoadSprite(string _path)
    {
        UnityEngine.Object obj = Resources.Load(_path, typeof(GameObject));

        if (obj == null)
        {
            Debug.LogWarning(string.Format("{0} isn't exist!", _path));
        }
        else
        {
            return (obj as GameObject).GetComponent<SpriteRenderer>().sprite;
        }
        return null;
    }


    public void OnClear()
    {
        mPrefabPool.Clear();        
    }

    public void ClearAll()
    {
        //存的是 UnityEngine Object ，是Asset-Object资源不能直接删
        //如果是Instantiate之后的资源是Cloned-Object可以删
        //foreach (KeyValuePair<string, UnityEngine.Object> prefab in mPrefabPool)
        //{
        //    DestroyObject(prefab.Value);
        //}
        OnClear();
    }

    #region 路径有关
    

    public static string AssetUI(string _filename)
    {
        return string.Concat("UI/", _filename);
    }

    //获取本地路径
    static public string AssetModelPath(string fileName)
    {
        return string.Concat("Model/", fileName);
    }

    //音频
    public static string AssetSound(string _filename)
    {
        return string.Concat("Sounds/", _filename);
    }

    //图片
    public static string AssetSprite(string _filename)
    {
        return string.Concat("Textures/", _filename);
    }

    #endregion

    #region 本地化

    //---------------------------------------------------------Localization-------------------------------------------------------
    //设置语言
    public IEnumerator SetLanguage(string _language)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        string path = string.Concat(Application.dataPath, "/StreamingAssets/Localization/", _language, ".txt");
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        ByteReader reader = new ByteReader(data);
        Localization.Set(_language, reader.ReadDictionary());
#else
        yield return StartCoroutine(CopyAsset(string.Concat(DownLoadUtil.InternalAssetsZipDirectory, "Localization.zip"), string.Concat(DownLoadUtil.ExternalAssetDirectory, "Localization.zip")));
		ReadLocalization(string.Concat(DownLoadUtil.ExternalAssetDirectory, "Localization.zip"), _language);
#endif
        yield return 1;
    }

    //读取本地化文件
    private void ReadLocalization(string _path, string _language)
    {
        string language1 = string.Concat(_language, ".txt");
        using (ZipInputStream zis = new ZipInputStream(ToStreamForZip(_path)))
        {
            ZipEntry ze = zis.GetNextEntry();
            while (ze != null)
            {
                if (ze.Name.Equals(language1))
                {
                    byte[] data = new byte[ze.Size];
                    zis.Read(data, 0, data.Length);
                    ByteReader reader = new ByteReader(data);
                    Localization.Set(_language, reader.ReadDictionary());
                    break;
                }
                ze = zis.GetNextEntry();
            }
        }
    }

    //返回文件流
    private Stream ToStreamForZip(string _path)
    {
        FileStream rfs = new FileStream(_path, FileMode.Open, FileAccess.Read);
        byte[] buff = new byte[rfs.Length];
        rfs.Read(buff, 0, buff.Length);
        rfs.Close();

        byte[] data = Decrypt(buff);

        return new MemoryStream(data);
    }

    //加密
    private byte[] Encrypt(byte[] _src)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        ICryptoTransform ict = des.CreateEncryptor(ASCIIEncoding.ASCII.GetBytes("lenovoce"), ASCIIEncoding.ASCII.GetBytes("tangogog"));
        return ict.TransformFinalBlock(_src, 0, _src.Length);
    }

    //解密
    private byte[] Decrypt(byte[] _src)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        ICryptoTransform ict = des.CreateDecryptor(ASCIIEncoding.ASCII.GetBytes("lenovoce"), ASCIIEncoding.ASCII.GetBytes("tangogog"));
        return ict.TransformFinalBlock(_src, 0, _src.Length);
    }

    //将本地化文件拷贝出来
    private IEnumerator CopyAsset(string _src, string _dest)
    {
        string dir = Path.GetDirectoryName(_dest);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        if (_src.StartsWith("jar:file://") || _src.StartsWith("file://") || _src.StartsWith("http://"))
        {
            UnityWebRequest uwr = UnityWebRequest.Get (_src);
            yield return uwr.SendWebRequest();
            if (uwr.isHttpError || uwr.isNetworkError)
            {
                Debug.LogError ("CopyAsset  Error " + uwr.error);
            }
            else
            {
                FileStream wfs = new FileStream (_dest , FileMode.Create , FileAccess.ReadWrite);
                wfs.Write (uwr.downloadHandler.data , 0 , uwr.downloadHandler.data.Length);
                wfs.Dispose ();
                Debug.LogError ("CopyAsset  Success ");
            }
        }
        else
        {
            File.Copy(_src, _dest, true);
            Debug.LogError ("PC CopyAsset  Success ");
            yield return 1;
        }
    }
    //----------------------------------------------------------------------------------------------------------------

    #endregion
}