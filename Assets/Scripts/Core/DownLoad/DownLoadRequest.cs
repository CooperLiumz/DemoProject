//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class DownLoadParam : System.Object {

//    // 加载的文件类型  
//    public EU_LoadFileType fileType;
//    //加载的路径
//    public string url;
//    //自定义参数
//    public object param = null;
//    // 图片  
//    public Texture2D texture2D;
//    // 文本  
//    public string text = "";

//    // unity3d格式文件，目前针对场景打包的unity3d格式文件  
//    public AssetBundle assetBundle = null;
//    // json文件  
//    public string jsonData;   
//    // 音频文件  
//    public AudioClip audioClip;

//    // 二进制数据 
//    public byte[] bytes;

//    //是否删除
//    public bool isDelete = false;
//}

//public enum EU_LoadFileType
//{
//   NONE,
//   TXT,
//   IMAGE,
//   THUMB_IMAGE,
//   MARKER_IMAGE,

//   UNITY3D,
//   BINARY,
//   JSON,
//   FBX,
//   AUDIO,
//   FONT,
//}

//public enum EU_DownLoadPriority 
//{
//    Normal = 0
//}

////请求对象
//public class DownLoadRequest : System.Object {
//    //定义委托
//    public delegate void DownCompleteDelegate(DownLoadParam param);
//    public delegate void DownErrorDelegate(DownLoadRequest request);
//    public delegate void DownProcessDelegate(string url, float processValue, int fileTotalSize = 0);

//    //定义事件
//    public DownCompleteDelegate completeEvent;
//    public DownErrorDelegate errorEvent;
//    public DownProcessDelegate processEvent;

//    //超时限制
//    public const int TIME_OUT_FRAMES = 300;
//    //加载的总帧数
//    private int _loadTotalFrame = 0;
//    //是否超时
//    public bool isTimeOut = false;
//    //是否完成
//    public bool alreadyDown = false;

//    //是否抛弃
//    public bool isObsolete = false;

//    //下载地址
//    public string requestURL;

//    //www
//    public WWW wwwObject = null;

//    //error
//    public string error;

//    //文件类型
//    public EU_LoadFileType fileType;

//    //自定义参数列表
//    public List<object> customParams = new List<object>();

//    //下载优先级
//    public int priotiry = (int)EU_DownLoadPriority.Normal;


//    public DownLoadRequest() {
//    }

//    public DownLoadRequest(string url, object customParam = null, EU_LoadFileType type = EU_LoadFileType.NONE, 
//        DownCompleteDelegate completeEv = null, 
//        DownErrorDelegate errorEv = null, 
//        DownProcessDelegate processEv = null)
//    {
//        requestURL = url;
//        fileType = type;

//        completeEvent = completeEv;
//        if (completeEv != null) {
//            customParams.Add(customParam);
//        }
//        if (errorEv != null) {
//            errorEvent = errorEv;
//        }
//        if (processEv != null) {
//            processEvent = processEv;
//        }

//        wwwObject = new WWW(requestURL);
//        wwwObject.threadPriority = ThreadPriority.Low;
//    }  
    
//    public int loadTotalFrames {
//        set {
//            _loadTotalFrame = value;
//            isTimeOut = (_loadTotalFrame > DownLoadRequest.TIME_OUT_FRAMES) ? true : false;
//        }
//        get {
//            return _loadTotalFrame;
//        }
//    }  
//}
