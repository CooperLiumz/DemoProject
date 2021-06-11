using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class NetPacket : System.Object {
    //service
    public string service;

    public Dictionary<string, string> paramDict = new Dictionary<string, string>();

    public string parmaStr;

    public bool isPost;

    public WWWForm wwwForm = new WWWForm();

    public string url = "http://222.173.93.215:10000/";


    public NetPacket() {
    }
    //get
    public NetPacket(string service)
    {
        this.service = service;

        //string _param = "?";
        //_param += "service" + "=" + service + "&";
        //_param += "token" + "=" + token + "&";
        //_param += "params" + "=" + UnityWebRequest.EscapeURL ( ToolKit.StringDictToNewWorkJson ( paramDict ) );

        this.parmaStr = this.service;

        this.isPost = false;
    }

    //post
    public NetPacket(string service, string paramStr)
    {
        this.service = service;
        this.parmaStr = paramStr;

        wwwForm.AddField ( "service", service );
        wwwForm.AddField ( "params", paramStr );

        this.isPost = true;
    }

    public string Param {
        get {
            return parmaStr;
        }
    }
}

public class NetPacketCallBackMsg : System.Object
{
    public string callBackMsg;

    public NetPacketCallBackMsg (string _msg)
    {
        callBackMsg = _msg;
    }

    public NetPacketCallBackMsg ()
    {
    }
}

//请求对象
public class NetWorkRequest : System.Object {
  
    public delegate void ReponseCompleteDelegate(NetPacket netPacket, NetPacketCallBackMsg msg);
    public delegate void ReponseErrorDelegate(NetPacket netPacket, NetPacketCallBackMsg msg);

    public ReponseCompleteDelegate reponseCompleteCallBack;
    public ReponseErrorDelegate reponseErrorCallBack;

    //超时限制
    public const int TIME_OUT_FRAMES = 300;
    //加载的总帧数
    private int _loadTotalFrame = 0;
    //是否超时
    public bool isTimeOut = false;
    //是否完成
    public bool alreadyDown = false;

    //error
    public string error;

    //下载参数
    public NetPacket netPacket;

    public UnityWebRequest webRequest;

    public NetWorkRequest() {
    }

    public NetWorkRequest(NetPacket netpack, ReponseCompleteDelegate completeCallBack = null, ReponseErrorDelegate errorCallBack = null)
    {
        netPacket = netpack;

        if (completeCallBack != null) {
            reponseCompleteCallBack = completeCallBack;
        }

        if (errorCallBack != null)
        {
            reponseErrorCallBack = errorCallBack;
        }
         
        #region UnityWebRequest
        if (netpack.isPost)
        {
            webRequest = UnityWebRequest.Post ( netpack.url, netpack.wwwForm );
        }
        else
        {
            webRequest = UnityWebRequest.Get( netpack.url + netpack.Param );
        }
        webRequest.SendWebRequest ();
        #endregion

        #region Header 
        //byte[] bytePostData = System.Text.Encoding.UTF8.GetBytes (netpack.json);
        //webRequest = UnityWebRequest.Put (netpack.url , bytePostData); //use PUT method to send simple stream of bytes
        //webRequest.method = UnityWebRequest.kHttpVerbPOST; //hack to send POST to server instead of PUT

        //string _requestId = System.Guid.NewGuid ().ToString ("D");
        //string _time = System.DateTime.Now.ToString ("yyyy/MM/dd HH:mm");

        //webRequest.SetRequestHeader ("Content-Type" , "application/json");

        //webRequest.SetRequestHeader ("X-Auth-Meta" , "b5lftxujHRVm4/XipG0eRTi64s77LqgWJo/2YuJ/3luwTxddTzoVHHzSApOSQ1Om");

        //webRequest.SetRequestHeader ("X-FN" , netpack.service);

        //webRequest.SetRequestHeader ("X-Request-Id" , _requestId);

        //webRequest.SetRequestHeader ("X-Request-Id" , _time);

        //webRequest.SendWebRequest ();

        #endregion

    }

    public NetPacketCallBackMsg CallBackMsg {

        #region UnityWebRequest
        get
        {
            if (webRequest.downloadHandler == null)
            {
                return new NetPacketCallBackMsg ();
            }
            else
            {
                //Debug.Log ( "Call Back MSG " + webRequest.downloadHandler.text );
                return new NetPacketCallBackMsg ( webRequest.downloadHandler.text );
            }
        }
        #endregion

    }

    public int loadTotalFrames {
        set {
            _loadTotalFrame = value;
            isTimeOut = (_loadTotalFrame > TIME_OUT_FRAMES) ? true : false;
        }
        get {
            return _loadTotalFrame;
        }
    }  
}


public class Net_PackParamType : System.Object {
    
    static public readonly string PROJECT_ID = "projectId";
}

public class Net_PackServiceType : System.Object {

    static public readonly string CURRENT_LOTS = "getCurrentLots";
    static public readonly string CURRENT_PRODUCTION_INFO = "getCurrentProductionInfo";
    static public readonly string CURRENT_TECH_INFO = "getCurrentTechInfo";
}

