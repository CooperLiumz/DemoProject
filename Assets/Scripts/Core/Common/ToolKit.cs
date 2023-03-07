using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ToolKit : System.Object
{
    static public DateTime StampToDateTime (long timeStamp)
    {
        DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime (new DateTime (1970 , 1 , 1));
        long lTime = long.Parse (timeStamp + "0000000");

        TimeSpan toNow = new TimeSpan (lTime);

        return dateTimeStart.Add (toNow);
    }

    #region 字符串转换为Vector

    /// <summary>
    /// 将字符串转成Vector2
    /// </summary>
    static public Vector2 StringParseToVector2 (string iptStr)
    {
        string[] strList = iptStr.Split (',');
        if (strList.Length == 2)
        {
            Vector2 vct2 = Vector2.zero;
            vct2.x = float.Parse (strList[0]);
            vct2.y = float.Parse (strList[1]);
            return vct2;
        }
        else
        {
            return Vector2.zero;
        }
    }

    /// <summary>
    /// 将字符串转成Vector3
    /// </summary>
    static public Vector3 StringParseToVector3 (string iptStr)
    {
        string[] strList = iptStr.Split (',');
        if (strList.Length == 3)
        {
            Vector3 vct3 = Vector3.zero;
            vct3.x = float.Parse (strList[0]);
            vct3.y = float.Parse (strList[1]);
            vct3.z = float.Parse (strList[2]);
            return vct3;
        }
        else
        {
            return Vector3.zero;
        }
    }

    static public List<Vector3> StringParseToVector3List (string iptStr)
    {
        List<Vector3> result = new List<Vector3> ();
        string[] strList = iptStr.Split (',');
        int _lgh = strList.Length;
        if (_lgh > 3)
        {
            for (int i = 0; i < _lgh;)
            {
                Vector3 vct3 = Vector3.zero;
                vct3.x = float.Parse (strList[i]);
                vct3.y = float.Parse (strList[i + 1]);
                vct3.z = float.Parse (strList[i + 2]);
                result.Add (vct3);
                i += 3;
            }
        }
        return result;
    }

    /// <summary>
    /// 将Vector3转成字符串
    /// </summary>
    static public string Vector3ParseToString (Vector3 vect3)
    {
        string _format = "{0},{1},{2}";

        return string.Format (_format , vect3.x , vect3.y , vect3.z);
    }

    /// <summary>
    /// 将字符串转成Vector4
    /// </summary>
    static public Vector4 StringParseToVector4 (string iptStr)
    {
        string[] strList = iptStr.Split (',');
        if (strList.Length == 4)
        {
            Vector4 vct4 = Vector4.zero;
            vct4.x = float.Parse (strList[0]);
            vct4.y = float.Parse (strList[1]);
            vct4.z = float.Parse (strList[2]);
            vct4.w = float.Parse (strList[3]);
            return vct4;
        }
        else
        {
            return Vector4.zero;
        }
    }

    /// <summary>
    /// 将字符串转成Color
    /// </summary>
    static public Vector4 StringParseToColor (string iptStr)
    {
        string[] strList = iptStr.Split (',');
        if (strList.Length == 4)
        {
            Vector4 vct4 = Vector4.zero;
            vct4.x = float.Parse (strList[0]) / 255;
            vct4.y = float.Parse (strList[1]) / 255;
            vct4.z = float.Parse (strList[2]) / 255;
            vct4.w = float.Parse (strList[3]) / 255;
            return vct4;
        }
        else
        {
            return Vector4.zero;
        }
    }

    #endregion


    #region 颜色   
    /// <summary>
    /// color 转换hex
    /// </summary>
    /// <returns></returns>
    static public string ColorToHex (Color color)
    {
        int r = Mathf.RoundToInt (color.r * 255.0f);
        int g = Mathf.RoundToInt (color.g * 255.0f);
        int b = Mathf.RoundToInt (color.b * 255.0f);
        int a = Mathf.RoundToInt (color.a * 255.0f);
        string hex = string.Format ("{0:X2}{1:X2}{2:X2}{3:X2}" , r , g , b , a);
        return hex;
    }

    /// <summary>
    /// hex转换到color
    /// hex == #FFFFFFFF
    /// </summary>
    /// <returns></returns>
    static public Color HexToColor (string hex)
    {
        if (hex.StartsWith ("#"))
        {
            hex = hex.Substring (1 , hex.Length - 1);
        }

        byte br = byte.Parse (hex.Substring (0 , 2) , System.Globalization.NumberStyles.HexNumber);
        byte bg = byte.Parse (hex.Substring (2 , 2) , System.Globalization.NumberStyles.HexNumber);
        byte bb = byte.Parse (hex.Substring (4 , 2) , System.Globalization.NumberStyles.HexNumber);
        float r = br / 255f;
        float g = bg / 255f;
        float b = bb / 255f;
        float a = 255 / 255f;

        if (hex.Length >= 8)
        {
            byte cc = byte.Parse (hex.Substring (6 , 2) , System.Globalization.NumberStyles.HexNumber);
            a = cc / 255f;
        }

        return new Color (r , g , b , a);
    }

    #endregion

    #region 列表转字典


    //列表转字典
    static public List<T> DictToList<T> (Dictionary<string , T> dict) where T : class
    {
        List<T> result = new List<T> ();
        foreach (KeyValuePair<string , T> kvp in dict)
        {
            result.Add (kvp.Value);
        }
        return result;
    }

    //列表转字典
    static public List<T> LongDictToList<T> (Dictionary<long , T> dict) where T : class
    {
        List<T> result = new List<T> ();
        foreach (KeyValuePair<long , T> kvp in dict)
        {
            result.Add (kvp.Value);
        }
        return result;
    }

    //列表转字典
    static public List<T> IntDictToList<T> (Dictionary<int , T> dict) where T : class
    {
        List<T> result = new List<T> ();
        foreach (KeyValuePair<int , T> kvp in dict)
        {
            result.Add (kvp.Value);
        }
        return result;
    }

    #endregion


    #region json

    static public string StringDictToNewWorkJson (Dictionary<string , string> paramDict)
    {
        string _result = "{";

        int _count = paramDict.Count;
        int _index = 0;
        foreach (KeyValuePair<string , string> kvp in paramDict)
        {
            _result += string.Format ("\"{0}\":\"{1}\"" , kvp.Key , kvp.Value);
            _index++;
            if (_index < _count)
            {
                _result += ",";
            }
        }

        _result += "}";

        return _result;
    }

    #endregion


    #region 动画

    // 添加动画事件
    public void AddAnimationEvent (AnimationClip _clip)
    {
        _clip.events = new AnimationEvent[0];

        AnimationEvent _event = new AnimationEvent ();
        _event.functionName = "CallBack";
        _event.time = 0;
        _clip.AddEvent (_event);
    }

    // 动画正倒播
    public void PlayAnimationBack (Animation _animation, AnimationState _animationState)
    {
        // 倒播
        _animationState.speed = -1;
        _animationState.time = _animationState.length;
        _animation.Play ();

        // 正播
        _animationState.speed = 1;
        _animationState.time = 0;
        _animation.Play ();

        //_animation["name"].speed = 1;
        //_animation["name"].time = 1;
        //_animation.Play ("name");
    }

    #endregion

    #region 添加监听

    public void AddListentr ()
    {

        //EventTrigger _trigger = gameObject.AddComponent<EventTrigger> ();
        //EventTrigger.Entry _beginDrag = new EventTrigger.Entry ();
        //_beginDrag.eventID = EventTriggerType.BeginDrag;
        //_beginDrag.callback.AddListener (delegate
        //{
        //    this.OnVideoSliderDown ();
        //});
        //_trigger.triggers.Add (_beginDrag);

        //mVideoSlider.onValueChanged.AddListener (delegate
        //{
        //    this.OnVideoSeekSlider ();
        //});

        //mCloseButton.onClick.AddListener (delegate
        //{
        //    this.OnCloseBtnClicked ();
        //});
    }

    #endregion


    // android 刷新相册
    //public void scanFile(String filePath) {
    //	Log.i("Unity", "------------filePath"+filePath);
    //	Intent scanIntent = new Intent(Intent.ACTION_MEDIA_SCANNER_SCAN_FILE);
    //	scanIntent.setData(Uri.fromFile(new File(filePath)));
    //	this.sendBroadcast(scanIntent);
    //}



    static public T FindInParents<T> (Transform trans) where T : Component
    {
        if (trans == null)
            return null;
        #if UNITY_FLASH
		object comp = trans.GetComponent<T>();
        #else
        T comp = trans.GetComponent<T> ();
        #endif
        if (comp == null)
        {
            Transform t = trans.transform.parent;

            while (t != null && comp == null)
            {
                comp = t.gameObject.GetComponent<T> ();
                t = t.parent;
            }
        }
        #if UNITY_FLASH
		return (T)comp;
        #else
        return comp;
        #endif
    }

    static public void SetDirty (UnityEngine.Object obj)
    {
        #if UNITY_EDITOR
        if (obj)
        {
            UnityEditor.EditorUtility.SetDirty (obj);
        }
        #endif
    }


    static public Texture2D TextureToTexture2d ( Texture texture )
    {
        RenderTexture prevRT = RenderTexture.active;

        Texture2D texture2D = new Texture2D ( texture.width, texture.height );
        if ( texture is RenderTexture )
        {
            RenderTexture.active = ( RenderTexture ) texture;
            texture2D.ReadPixels ( new UnityEngine.Rect ( 0f, 0f, texture.width, texture.height ), 0, 0, false );
            texture2D.Apply ( false, false );

        }
        else
        {
            RenderTexture tempRT = RenderTexture.GetTemporary ( texture.width, texture.height, 0, RenderTextureFormat.ARGB32 );
            Graphics.Blit ( texture, tempRT );

            RenderTexture.active = tempRT;
            texture2D.ReadPixels ( new UnityEngine.Rect ( 0f, 0f, texture.width, texture.height ), 0, 0, false );
            texture2D.Apply ( false, false );
            RenderTexture.ReleaseTemporary ( tempRT );
        }

        RenderTexture.active = prevRT;

        Sprite _sp = Sprite.Create ( texture2D, new Rect ( 0, 0, texture2D.width, texture2D.height ), new Vector2 ( 0.5f, 0.5f ) );

        return texture2D;
        //selfImage.sprite = _sp;
    }

}
