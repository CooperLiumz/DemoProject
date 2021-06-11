using UnityEngine;
using System.Collections.Generic;

public class NetWorkManager : MonoBehaviour {

    //正在下载队列 key url
    public Dictionary<string, NetWorkRequest> loadDict = new Dictionary<string, NetWorkRequest>();
    
    //加载标志位
    private bool isLoading;

    private static NetWorkManager mInstance;
    private static readonly object mStaticSyncRoot = new object();

    private NetWorkManager() { }

    public static NetWorkManager Instance
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
                        mInstance = singleton.AddComponent<NetWorkManager>();
                    }
                }
            }
            return mInstance;
        }
    }

    //请求
    public void Send(NetPacket netPacket, NetWorkRequest.ReponseCompleteDelegate completeCallBack = null,
        NetWorkRequest.ReponseErrorDelegate errorCallBack = null) {
        isLoading = true;
        NetWorkRequest loadRequest = new NetWorkRequest(netPacket, completeCallBack, errorCallBack);
        
        if (loadDict.ContainsKey(netPacket.Param)) {
            loadDict[netPacket.Param].reponseCompleteCallBack += completeCallBack;
            loadDict[netPacket.Param].reponseErrorCallBack += errorCallBack;
        }
        else {
            loadDict.Add(netPacket.Param, loadRequest);
        }
    }

    /// <summary>  
    ///  任务下载完成回调  
    /// </summary>  
    public void CompleteDelegateHandle(NetWorkRequest request)
    {
        if (request.reponseCompleteCallBack != null)
        {
            int count = request.reponseCompleteCallBack.GetInvocationList().GetLength(0);

            for (int i = 0; i < count; i++)
            {               
                NetWorkRequest.ReponseCompleteDelegate responseCallBack = (NetWorkRequest.ReponseCompleteDelegate)request.reponseCompleteCallBack.GetInvocationList()[i];

                responseCallBack.Invoke(request.netPacket, request.CallBackMsg);
            }
        }
    }


    /// <summary>  
    ///  任务下载错误回调  
    /// </summary>  
    public void ErrorDelegateHandle(NetWorkRequest request)
    {
        if (request.reponseErrorCallBack != null)
        {
            request.reponseErrorCallBack ( request.netPacket, request.CallBackMsg );

            request.webRequest.Dispose ();
        }
    }

    //自己计算两帧之间 delta 时间，不适用系统的delta时间
    float m_lastTime;
    float m_UIDelta;
    float m_checktick;
    float m_cd = 0.1f;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Application.backgroundLoadingPriority = ThreadPriority.Low;
    }

    void Start() {
        m_lastTime = Time.realtimeSinceStartup;
        m_checktick = 0;
    }
    // Update is called once per frame
    void Update()
    {
        m_UIDelta = Time.realtimeSinceStartup - m_lastTime;
        m_lastTime = Time.realtimeSinceStartup;
        
        m_checktick += m_UIDelta;
        if (m_checktick > m_cd) {
           
            m_checktick -= m_cd;

            CheckQueue();
        }
    }

    /// <summary>  
    /// 根据优先级，从等待队列里面移除一个任务到下载队列里  
    /// </summary>  
    public void MoveRequestFromWaitDictToLoadDict()
    {
        isLoading = loadDict.Count > 0;        
    }
    
    /// <summary>
    /// 检查下载任务
    /// 移除完成任务
    /// </summary>
    private void CheckQueue() {
        if (!isLoading) return;

        foreach (KeyValuePair<string, NetWorkRequest> pair in loadDict)
        {
            NetWorkRequest request = pair.Value;
                      
            request.loadTotalFrames++;

            #region UnityWebRequest
            if (request.webRequest.isHttpError || request.webRequest.isNetworkError)
            {
                request.alreadyDown = true;
                loadDict.Remove ( request.netPacket.Param );
                if (request.isTimeOut)
                {
                    request.error = string.Concat ( "Load time out: service==>" + request.netPacket.service + "  <=error=> " + request.webRequest.error );
                }
                else
                {
                    request.error = string.Concat ( "Load error: service==>" + request.netPacket.service  + "  <=error=> " + request.webRequest .error);
                }

                Debug.LogWarning ( request.error );

                ErrorDelegateHandle ( request );

                break;
            }
            else
            {
                if (!request.alreadyDown)
                {
                    // if done  
                    if (request.webRequest != null && request.webRequest.isDone)
                    {
                        CompleteDelegateHandle ( request );

                        request.alreadyDown = true;
                        loadDict.Remove ( request.netPacket.Param );
                        MoveRequestFromWaitDictToLoadDict ();

                        request.webRequest.Dispose ();
                        break;
                    }
                }
            }


            #endregion           
        }
    }   
}
