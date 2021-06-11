
//using UnityEngine;
//using System.Collections.Generic;
//using System;
//using System.Linq;

//public class DownLoadManager : MonoBehaviour {

//    //最大同时下载数量
//    public int MAX_LOAD_REQUEST = 2;

//    //正在下载队列 key url
//    public Dictionary<string, DownLoadRequest> loadDict = new Dictionary<string, DownLoadRequest>();
//    //等待下载队列 key url
//    public Dictionary<string, DownLoadRequest> waitDict = new Dictionary<string, DownLoadRequest>();
//    //下载完成队列 key url
//    public Dictionary<string, DownLoadParam> completeDict = new Dictionary<string, DownLoadParam>();


//    //优先级队列
//    private List<string> priorityList = new List<string>();

//    //加载标志位
//    private bool isLoading;

//    private static DownLoadManager mInstance;
//    private static readonly object mStaticSyncRoot = new object();
//    private DownLoadManager() {
//        //Application.backgroundLoadingPriority = ThreadPriority.Low;
//    }   

//    public static DownLoadManager Instance
//    {
//        get
//        {
//            if (mInstance == null)
//            {
//                lock (mStaticSyncRoot)
//                {
//                    if (mInstance == null)
//                    {
//                        GameObject singleton = GameObject.Find("_Singleton");
//                        if (singleton == null)
//                        {
//                            singleton = new GameObject("_Singleton");
//                        }
//                        mInstance = singleton.AddComponent<DownLoadManager>();
//                    }
//                }
//            }
//            return mInstance;
//        }
//    }    

//    //下载
//    public void Load(string url, DownLoadRequest.DownCompleteDelegate completeFunc, 
//        object customParam = null, EU_LoadFileType fileType = EU_LoadFileType.NONE, 
//        int priority = 2, 
//        DownLoadRequest.DownErrorDelegate errorFunc = null, 
//        DownLoadRequest.DownProcessDelegate processFunc = null)
//    {
//        url = url.Trim();
//        if (string.IsNullOrEmpty(url)) return;

//        #region 已完成列表
//        if (completeDict.ContainsKey(url))
//        {
//            //需要处理，根据url和MD5一起判断
//            if (CheckMD5(completeDict[url], customParam))
//            {
//                // 已下载资源，MD5相同，直接调用回调函数  
//                if (customParam != null)
//                {
//                    completeDict[url].param = customParam;
//                }
//                try
//                {
//                    completeFunc.Invoke(completeDict[url]);
//                }
//                catch (Exception exception)
//                {
//                    Debug.LogWarning("exception:" + exception.Message);
//                }
//                Debug.Log("已下载资源，直接调用回调函数  " + url);
//            }
//            else {
//                //已下载资源，但MD5不同
//                //移除已经完成的，重新下载新资源

//                //删除已下载的资源
//                completeDict[url].isDelete = true;

//                try
//                {
//                    completeFunc.Invoke(completeDict[url]);
//                }
//                catch (Exception exception)
//                {
//                    Debug.LogWarning(" 删除已下载资源 exception:" + exception.Message);
//                }

//                completeDict.Remove(url);

//                //重新加载
//                Load(url, completeFunc, customParam, fileType, priority, errorFunc, processFunc);

//                Debug.Log("已下载资源，但MD5不同，需要重新下载 " + url);
//            }          
//        }
//        #endregion

//        #region 正在进行列表
//        else if (loadDict.ContainsKey(url))
//        {
//            if (CheckMD5(loadDict[url], customParam))
//            {
//                // 已经提交相同请求，但是没有下载完成  
//                loadDict[url].completeEvent += completeFunc;
//                loadDict[url].processEvent += processFunc;
//                loadDict[url].errorEvent += errorFunc;
//                loadDict[url].customParams.Add(customParam);
//                loadDict[url].isObsolete = false;                

//                Debug.Log("已经提交相同请求，但是没有下载完成， 不需要重新下载" + url);
//            }
//            else {
//                //弃用下载重新下载
//                int count = loadDict[url].errorEvent.GetInvocationList().GetLength(0);

//                LocalDownLoadUnitVO _obsoleteUnit = loadDict[url].customParams[0] as LocalDownLoadUnitVO;
//                LocalDownLoadUnitVO _curUnit = customParam as LocalDownLoadUnitVO;
//                _obsoleteUnit.md5 = _curUnit.md5;

//                loadDict[url].wwwObject.Dispose();
//                loadDict[url].isObsolete = true;
//                loadDict.Remove(url);

//                //fix same url different MD5
//                for (int i = 0; i < count; i++) {
//                    //重新加载
//                    Load(url, completeFunc, _obsoleteUnit, fileType, priority, errorFunc, processFunc);
//                }
//                Load(url, completeFunc, _curUnit, fileType, priority, errorFunc, processFunc);

//                Debug.Log("已经提交相同请求，但是没有下载完成， 需要重新下载" + url);
//            }            
//        }
//        #endregion

//        #region 等待列表
//        else if (waitDict.ContainsKey(url))
//        {
//            if (CheckMD5(waitDict[url], customParam))
//            { 
//                Debug.Log("已经提交相同请求，但是还没轮到加载，不需要替换 " + url);
//            }
//            else {
//                int _pCount = waitDict[url].customParams.Count;
//                for (int i = 0; i < _pCount; i++) {
//                    waitDict[url].customParams[i] = customParam;
//                }
//                Debug.Log("已经提交相同请求，但是还没轮到加载，替换信息 " + url);
//            }

//            // 已经提交相同请求，但是还没轮到加载  
//            waitDict[url].completeEvent += completeFunc;
//            waitDict[url].processEvent += processFunc;
//            waitDict[url].errorEvent += errorFunc;
//            waitDict[url].customParams.Add(customParam);
//        }
//        #endregion
//        #region 新下载
//        else
//        {
//            // 未加载过的  
//            if (loadDict.Count < MAX_LOAD_REQUEST)
//            {
//                isLoading = true;
//                DownLoadRequest loadRequest = new DownLoadRequest(url, customParam, fileType, 
//                    completeFunc, errorFunc, processFunc);

//                if (customParam != null && customParam.GetType().ToString() == "System.Collections.Generic.List`1[System.Object]")
//                {
//                    loadRequest.customParams = (List<object>)customParam;
//                }

//                loadDict.Add(url, loadRequest);
                
//                Debug.Log("未加载过的 加入下载队列   " + url);
//            }
//            else
//            {
//                // 已达到最大加载数目，加入等待队列  
//                DownLoadRequest loadRequest = new DownLoadRequest();
//                loadRequest.requestURL = url;
//                loadRequest.completeEvent = completeFunc;
//                loadRequest.errorEvent = errorFunc;
//                loadRequest.processEvent = processFunc;
//                loadRequest.customParams.Add(customParam);
//                loadRequest.fileType = fileType;
//                loadRequest.priotiry = priority;
//                waitDict.Add(url, loadRequest);
//                priorityList.Add(url);
//                priorityList = priorityList.OrderBy(s => waitDict[s].priotiry).ToList();

//                Debug.Log("已达到最大加载数目，加入等待队列 " + url);
//            }
//        }
//        #endregion
//    }


//    /// <summary>  
//    ///  下载错误回调  
//    /// </summary>  
//    public void ErrorDelegateHandle(DownLoadRequest request)
//    {
//        if (request.errorEvent != null)
//        {
//            int count = request.errorEvent.GetInvocationList().GetLength(0);
//            for (int i = 0; i < count; i++)
//            {
//                DownLoadRequest.DownErrorDelegate errorFunc = (DownLoadRequest.DownErrorDelegate)request.errorEvent.GetInvocationList()[i];
               
//                try
//                {
//                    errorFunc.Invoke(request);
//                }
//                catch (Exception e)
//                {
//                    Debug.LogWarning("exception:" + e.Message);
//                }
//            }
//        }
//    }
    
//    /// <summary>  
//    ///  下载进度回调  
//    /// </summary>  
//    public void ProcessDelegateHandle(DownLoadRequest request)
//    {
//        //一直在调会引起卡顿
//        if (request.processEvent != null)
//        {
//            int count = request.processEvent.GetInvocationList().GetLength(0);
//            for (int i = 0; i < count; i++)
//            {
//                DownLoadRequest.DownProcessDelegate processFunc = (DownLoadRequest.DownProcessDelegate)request.processEvent.GetInvocationList()[i];

//                //Debug.Log("process  " + request.requestURL + "  " + request.wwwObject.progress);  

//                try
//                {
//                    //request.wwwObject.bytesDownloaded 如果文件比较大的时候会明显卡顿
//                    //processFunc.Invoke(request.requestURL, request.wwwObject.progress, request.wwwObject.bytesDownloaded);

//                    processFunc(request.requestURL, request.wwwObject.progress);
//                }
//                catch (Exception e)
//                {
//                    Debug.LogWarning("exception:" + e.Message);
//                }
//            }
//        }
//    }

//    /// <summary>  
//    ///  任务下载完成回调  
//    /// </summary>  
//    public void CompleteDelegateHandle(DownLoadRequest request, DownLoadParam param)
//    {
//        if (request.completeEvent != null)
//        {
//            int count = request.completeEvent.GetInvocationList().GetLength(0);

//            for (int i = 0; i < count; i++)
//            {
//                if (i < request.customParams.Count)
//                {
//                    param.param = request.customParams[i];
//                }
//                DownLoadRequest.DownCompleteDelegate completeFunc = (DownLoadRequest.DownCompleteDelegate)request.completeEvent.GetInvocationList()[i];
                               
//                try
//                {
//                    completeFunc.Invoke(param);
//                }
//                catch (Exception exception)
//                {
//                    Debug.LogWarning("exception:" + exception.Message);
//                }
//            }
//        }
//    }
    
//    //自己计算两帧之间 delta 时间，不适用系统的delta时间
//    float m_lastTime;
//    float m_UIDelta;
//    float m_checktick;
//    float m_cd = 0.5f;

//    void Awake()
//    {
//        DontDestroyOnLoad(transform.gameObject);
//        Application.backgroundLoadingPriority = ThreadPriority.Low;
//    }

//    void Start() {
//        m_lastTime = Time.realtimeSinceStartup;
//        m_checktick = 0;
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        m_UIDelta = Time.realtimeSinceStartup - m_lastTime;
//        m_lastTime = Time.realtimeSinceStartup;
        
//        m_checktick += m_UIDelta;
//        if (m_checktick > m_cd) {
           
//            m_checktick -= m_cd;

//            CheckQueue();
//        }
//    }

//    /// <summary>  
//    /// 根据优先级，从等待队列里面移除一个任务到下载队列里  
//    /// </summary>  
//    public void MoveRequestFromWaitDictToLoadDict()
//    {
//        isLoading = loadDict.Count > 0;
//        if (priorityList.Count > 0)
//        {
//            if (waitDict.ContainsKey(priorityList[0]))
//            {
//                DownLoadRequest request = waitDict[priorityList[0]];
//                waitDict.Remove(priorityList[0]);
//                priorityList.RemoveAt(0);

//                Load(request.requestURL, request.completeEvent, 
//                    request.customParams, request.fileType, 
//                    request.priotiry, request.errorEvent, request.processEvent);
//            }
//        }
//    }
    
//    /// <summary>
//    /// 检查下载任务
//    /// 移除完成任务
//    /// </summary>
//    private void CheckQueue() {
//        if (!isLoading) return;

//        //Debug.Log("check start " + Time.realtimeSinceStartup);

//        foreach (KeyValuePair<string, DownLoadRequest> pair in loadDict)
//        {
//            DownLoadRequest request = pair.Value;

//            if (request.isObsolete)
//            {
//                request.wwwObject.Dispose();
//                Debug.Log("弃用");
//            }
//            else {
//                request.loadTotalFrames++;
//                // deal error  
//                if ((request.wwwObject != null && request.wwwObject.error != null) || request.isTimeOut)
//                {
//                    if (request.requestURL.Contains(".apk") || request.requestURL.Contains(".ipa"))
//                    {
//                        return;
//                    }
//                    request.alreadyDown = true;
//                    loadDict.Remove(request.requestURL);
//                    if (request.isTimeOut)
//                    {
//                        request.error = string.Concat("Load time out: " + request.requestURL);   
//                    }
//                    else
//                    {
//                        request.error = string.Concat("Load error: " + request.requestURL);
//                    }

//                    Debug.Log("Error ===> " + request.wwwObject.error + " <== url ==> " + request.requestURL);

//                    ErrorDelegateHandle(request);


//                    request.wwwObject.Dispose();

//                    //报错后停止一切下载
//                    //MoveRequestFromWaitDictToLoadDict();
//                    ErrorToStop();
//                    break;
//                }

//                //   
//                if (!request.alreadyDown)
//                {

//                    //因为www在取大文件的时候比较卡顿
//                    //取消Process                   
//                    //ProcessDelegateHandle(request);
                    
//                    // if done  
//                    if (request.wwwObject != null && request.wwwObject.isDone)
//                    {
//                        DownLoadParam param = ParseLoadParamFromLoadRequest(request);
//                        if (request.fileType != EU_LoadFileType.BINARY)
//                        {
//                            completeDict.Add(request.requestURL, param);
//                        }
//                        CompleteDelegateHandle(request, param);

//                        request.alreadyDown = true;
//                        loadDict.Remove(request.requestURL);
//                        MoveRequestFromWaitDictToLoadDict();

//                        request.wwwObject.Dispose();
//                        break;
//                    }
//                }
//            }
//        }
//        //Debug.Log("check end " + Time.realtimeSinceStartup);
//    }

//    /// <summary>  
//    ///  解析下载内容  
//    ///  目前用到文本，图片，压缩包
//    ///  用于保存
//    /// </summary>  
//    public DownLoadParam ParseLoadParamFromLoadRequest(DownLoadRequest request)
//    {
//        DownLoadParam param = new DownLoadParam();
//        param.url = request.requestURL;
//        param.fileType = request.fileType;

//        switch (request.fileType)
//        {
//            case EU_LoadFileType.IMAGE:
//            case EU_LoadFileType.MARKER_IMAGE:
//            case EU_LoadFileType.THUMB_IMAGE:
//                try
//                {
//                    param.texture2D = request.wwwObject.texture;
//                    param.texture2D.Compress(false);    // compress 有何影响 
//                    param.bytes = request.wwwObject.bytes;
//                }
//                catch (Exception exception)
//                {
//                    Debug.LogWarning("read texture2d error:" + request.requestURL + "\n" + exception.Message);
//                }
//                break;
//            case EU_LoadFileType.TXT:
//                try
//                {
//                    param.text = request.wwwObject.text;
//                    param.bytes = request.wwwObject.bytes;
//                }
//                catch (Exception exception)
//                {
//                    Debug.LogWarning("read text error:" + request.requestURL + "\n" + exception.Message);
//                }
//                break;
//            default:
//                try
//                {
//                    param.bytes = request.wwwObject.bytes;
//                }
//                catch (Exception exception)
//                {
//                    Debug.LogWarning("read  binary error:" + request.requestURL + "\n" + exception.Message);
//                }
//                break;
//        }
//        return param;
//    }

//    /// <summary>
//    /// 错误停止下载
//    /// </summary>
//    private void ErrorToStop() {
//        waitDict.Clear();
//        foreach (KeyValuePair<string, DownLoadRequest> kvp in loadDict) {
//            kvp.Value.wwwObject.Dispose();
//        }
//        loadDict.Clear();
//    }

//    //对比两个文件的MD5
//    public bool CheckMD5(DownLoadParam unitVO, object customParam) {

//        if (unitVO.param != null && customParam != null)
//        {
//            LocalDownLoadUnitVO _unitVO = unitVO.param as LocalDownLoadUnitVO;
//            LocalDownLoadUnitVO _customVO = customParam as LocalDownLoadUnitVO;

//            if (_unitVO != null && _customVO != null)
//            {
//                return _unitVO.md5.Equals(_customVO.md5);
//            }
//            else {
//                return false;
//            }
//        }
//        else {
//            return false;
//        }              
//    }

//    //对比两个文件的MD5
//    public bool CheckMD5(DownLoadRequest dlRequest, object customParam)
//    {
//        if (dlRequest.customParams.Count > 0 && customParam != null)
//        {
//            LocalDownLoadUnitVO _unitVO = dlRequest.customParams[0] as LocalDownLoadUnitVO;
//            LocalDownLoadUnitVO _customVO = customParam as LocalDownLoadUnitVO;
//            if (_unitVO != null && _customVO != null)
//            {
//                return _unitVO.md5.Equals(_customVO.md5);
//            }
//            else
//            {
//                return false;
//            }
//        }
//        else {
//            return false;
//        }
//    }      
//}
