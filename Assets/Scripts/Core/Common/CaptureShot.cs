using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.UI;

public class CaptureShot : MonoBehaviour
{
    
    void OnShotBtnClicked ()
    {
        //Application.CaptureScreenshot pc端默认存储在Application.dataPath
        //android iphone 存储在 Application.persistentDataPath
        //Application.CaptureScreenshot(string.Concat("D:/" + filename));

        StartCoroutine ( CaptureScreen () );
        //StartCoroutine(CaptureCamera());
    }


    public IEnumerator CaptureScreen ()
    {
        yield return new WaitForEndOfFrame ();

        Texture2D screenShot = new Texture2D ( Screen.width, Screen.height, TextureFormat.RGB24, false );

        screenShot.ReadPixels ( new Rect ( 0, 0, Screen.width, Screen.height ), 0, 0, true );

        byte[] bytes = screenShot.EncodeToPNG ();

        screenShot.Compress ( true );

        screenShot.Apply ();


        System.DateTime now = new System.DateTime ();

        now = System.DateTime.Now;

        string filename = string.Format ( "image{0}{1}{2}{3}.png", now.Day, now.Hour, now.Minute, now.Second );

        string path = null;

        #if UNITY_EDITOR
        path = string.Concat ( DownLoadUtil.CacheRootPath, "ScreenShot/" );
        if (!Directory.Exists ( path ))
        {
            Directory.CreateDirectory ( path );
        }
        path = string.Concat ( path, filename );
        File.WriteAllBytes ( path, bytes );
        #elif !UNITY_EDITOR && UNITY_ANDROID
        path = string.Concat("/sdcard/DCIM/ScreenShot/");
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }

        path = string.Concat(path, filename);
        File.WriteAllBytes(path, bytes);
#elif !UNITY_EDITOR && UNITY_IPHONE
        path = string.Concat(DownLoadUtil.CacheRootPath, filename);
        File.WriteAllBytes(path, bytes);

        yield return new WaitForSeconds(1f);
#endif
       
        GameObject.DestroyImmediate ( screenShot );
    }

    /// <summary>  
    /// 对相机截图。   
    /// </summary>  
    private IEnumerator CaptureCamera ()
    {
        yield return new WaitForEndOfFrame ();
        Rect rect = new Rect ( 0, 0, Screen.width, Screen.height );

        Camera _camera = Camera.main;

        // 创建一个RenderTexture对象  
        RenderTexture rt = new RenderTexture ( (int)rect.width, (int)rect.height, 0 );
        // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
        _camera.targetTexture = rt;
        _camera.Render ();
        //ps: --- 如果这样加上第二个相机，可以实现只截图某几个指定的相机一起看到的图像。  
        //ps: camera2.targetTexture = rt;  
        //ps: camera2.Render();  
        //ps: -------------------------------------------------------------------  

        // 激活这个rt, 并从中中读取像素。  
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D ( (int)rect.width, (int)rect.height, TextureFormat.RGB24, false );
        screenShot.ReadPixels ( rect, 0, 0 );// 注：这个时候，它是从RenderTexture.active中读取像素  
        screenShot.Apply ();

        // 重置相关参数，以使用camera继续在屏幕上显示  
        _camera.targetTexture = null;
        //ps: camera2.targetTexture = null;  
        RenderTexture.active = null; // JC: added to avoid errors  
        GameObject.Destroy ( rt );
        // 最后将这些纹理数据，成一个png图片文件  
        byte[] bytes = screenShot.EncodeToPNG ();

        System.DateTime now = new System.DateTime ();
        now = System.DateTime.Now;
        string filename = string.Format ( "image{0}{1}{2}{3}.png", now.Day, now.Hour, now.Minute, now.Second );

        filename = string.Concat ( "", "_", filename );

        string path = null;

#if UNITY_EDITOR
        path = string.Concat ( DownLoadUtil.CacheRootPath, "ScreenShot/" );
        if (!Directory.Exists ( path ))
        {
            Directory.CreateDirectory ( path );
        }
        path = string.Concat ( path, filename );
        File.WriteAllBytes ( path, bytes );
#elif !UNITY_EDITOR && UNITY_ANDROID
        path = string.Concat("/sdcard/DCIM/ScreenShot/Photo/");
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }

        path = string.Concat(path, filename);
        File.WriteAllBytes(path, bytes);
#elif !UNITY_EDITOR && UNITY_IPHONE
        path = string.Concat(DownLoadUtil.CacheRootPath, filename);
        File.WriteAllBytes(path, bytes);

        yield return new WaitForSeconds(1f);
#endif


        GameObject.DestroyImmediate ( screenShot );
    }


    //=====================================
    IEnumerator DelayCopyEditor ( string _fileName )
    {

        yield return new WaitForEndOfFrame ();

        string _path = string.Concat ( "D:/", _fileName );
        int _count = 0;
        bool _exits = File.Exists ( _path );
        Debug.Log ( _exits );
        while (!_exits)
        {
            _count += 1;
            if (_count > 20)
            {
                _exits = true;
            }
            else
            {
                _exits = File.Exists ( _path );

                Debug.Log ( _exits );
            }
            yield return new WaitForSeconds ( 0.1f );
        }

    }

    IEnumerator DelayCopy ( string _fileName )
    {
        yield return null;

        string _path = string.Concat ( DownLoadUtil.CacheRootPath, _fileName );
        int _count = 0;
        bool _exits = File.Exists ( _path );
        while (!_exits)
        {
            _count += 1;
            if (_count > 20)
            {
                _exits = true;
            }
            else
            {
                _exits = File.Exists ( _path );
            }
            yield return new WaitForSeconds ( 0.1f );
        }

        if (File.Exists ( _path ))
        {
            byte[] _bytes = DownLoadUtil.ReadFileBytes ( _path );
            string _savePath = null;
            _savePath = "/sdcard/DCIM/ScreenShot/";

            if (!Directory.Exists ( _savePath ))
            {
                Directory.CreateDirectory ( _savePath );
            }

            if (_bytes != null && _bytes.Length > 0)
            {
                File.WriteAllBytes ( string.Concat ( _savePath, _fileName ), _bytes );
            }
            else
            {
                Debug.Log ( "Capture Error !" );
            }

        }
        else
        {
            Debug.Log ( "Capture Error !" );
        }
    }
}
