//using System.Collections;
//using System.Runtime.InteropServices;
//using UnityEngine;

///// <summary>
///// 调用Android Ios 方法
///// </summary>
//public class SDKTools : MonoBehaviour
//{

//    #if UNITY_EDITOR

//    public static void Call_QuitUnity()
//    {
//        Debug.Log("Call_QuitUnity");
//    }

//    public static void Call_ForwardAddressPage()
//    {
//        Debug.Log("Call_ForwardAddressPage");
//    }

//    public static bool Call_IsShowContact()
//    {
//        Debug.Log("Call_IsShowContact");
//        return true;
//    }

//    public static int Call_GetLanguage()
//    {
//        Debug.Log("Call_GetLanguage");
//        #if CN
//        return 1;
//        #else
//        return 2;
//        #endif
//    }

//    public static string Call_GetParams()
//    {
//        Debug.Log("Call_GetParams");
//        return null;
//    }

//    public static void Call_MultiInteract(string json)
//    {
//        Debug.Log("Call_MultiInteract");
//    }

//    public static void Call_UploadPhoto(string path)
//    {
//        Debug.Log("Call_UploadPhoto");
//    }

//    public static void Call_DownLoadMoudle()
//    {
//        UIGlobal.ui_sence.DownLoadMartinDataSuccess();
//        Debug.Log("Call_DownLoadMoudle");
//    }

//    public static void Call_LoadEngine()
//    {
//        Debug.Log("Call_LoadEngine");
//    }

//    public static bool Call_IsNeedDownload()
//    {
//        Debug.Log("Call_IsNeedDownload");
//        return false;
//    }

//    #elif !UITY_EDITOR && (UNITY_IOS || UNITY_IPHONE)

//    #if IOS_TEST
//    //退出unity
//    //[DllImport("__Internal")]
//    //private static extern void quitUnity();
//    public static void Call_QuitUnity()
//    {
//        Debug.Log("Call_QuitUnity");
//        //quitUnity();
//    }

//    //打开通讯录
//    //[DllImport("__Internal")]
//    //private static extern void forwardAddressPage();
//    public static void Call_ForwardAddressPage()
//    {
//        Debug.Log("Call_ForwardAddressPage");
//        //forwardAddressPage();
//    }

//    //是否显示kepler通讯录
//    //[DllImport("__Internal")]
//    //private static extern bool isShowContact();
//    public static bool Call_IsShowContact() {
//        Debug.Log("Call_IsShowContact");
//        //return isShowContact();
//        return false;
//    }

//    //获取语言类型
//    //[DllImport("__Internal")]
//    //private static extern int getLanguage();
//    public static int Call_GetLanguage()
//    {
//        Debug.Log("Call_GetLanguage");
//        //return getLanguage();
//        return 1;
//    }

//    //获取角色信息
//    //[DllImport("__Internal")]
//    //private static extern string getParams();
//    public static string Call_GetParams()
//    {
//        Debug.Log("Call_GetParams");
//        //return getParams();
//        return null;
//    }

//    //保存截图
//    //[DllImport("__Internal")]
//    //private static extern void saveScreenShot(string path);
//    public static void Call_SaveScreenShot(string path)
//    {
//        //saveScreenShot(path);
//    }

//    //发送多屏互动消息
//    //[DllImport("__Internal")]
//    //private static extern void sendMultiInteractMSG(string json);
//    public static void Call_MultiInteract(string json) {
//        Debug.Log("Call_MultiInteract");
//        //sendMultiInteractMSG(json);
//    }

//    //保存拍照
//    //[DllImport("__Internal")]
//    //private static extern void uploadPhoto(string path);
//    public static void Call_UploadPhoto(string path)
//    {
//        Debug.Log("Call_UploadPhoto");
//        //uploadPhoto(path);
//    }
    
//    //开始下载Martin数据
//    //[DllImport("__Internal")]
//    //private static extern void downLoadMoudle();
//    public static void Call_DownLoadMoudle()
//    {
//        //downLoadMoudle();
//        Debug.Log("Call_DownLoadMoudle");
//    }
    
//    //加载Martin引擎
//    //[DllImport("__Internal")]
//    //private static extern void loadEngine();
//    public static void Call_LoadEngine()
//    {
//        //loadEngine();
//        Debug.Log("Call_LoadEngine");
//    }

//    //判断是否需要加载Martin 数据
//    //[DllImport("__Internal")]
//    //private static extern bool isNeedDownload();
//    public static bool Call_IsNeedDownload()
//    {
//        Debug.Log("Call_IsNeedDownload");
//        //return isNeedDownload();
//        return false;
//    }   
//    #else
//    //退出unity
//    [DllImport("__Internal")]
//    private static extern void quitUnity();
//    public static void Call_QuitUnity()
//    {
//        Debug.Log("Call_QuitUnity");
//        quitUnity();
//    }

//    //打开通讯录
//    [DllImport("__Internal")]
//    private static extern void forwardAddressPage();
//    public static void Call_ForwardAddressPage()
//    {
//        Debug.Log("Call_ForwardAddressPage");
//        forwardAddressPage();
//    }

//    //是否显示kepler通讯录
//    [DllImport("__Internal")]
//    private static extern bool isShowContact();
//    public static bool Call_IsShowContact()
//    {
//        Debug.Log("Call_IsShowContact");
//        return isShowContact();
//    }

//    //获取语言类型
//    [DllImport("__Internal")]
//    private static extern int getLanguage();
//    public static int Call_GetLanguage()
//    {
//        Debug.Log("Call_GetLanguage");
//        return getLanguage();
//    }

//    //获取角色信息
//    [DllImport("__Internal")]
//    private static extern string getParams();
//    public static string Call_GetParams()
//    {
//        Debug.Log("Call_GetParams");
//        return getParams();
//    }

//    //保存截图
//    [DllImport("__Internal")]
//    private static extern void saveScreenShot(string path);
//    public static void Call_SaveScreenShot(string path)
//    {
//        saveScreenShot(path);
//    }

//    //发送多屏互动消息
//    [DllImport("__Internal")]
//    private static extern void sendMultiInteractMSG(string json);
//    public static void Call_MultiInteract(string json)
//    {
//        Debug.Log("Call_MultiInteract");
//        sendMultiInteractMSG(json);
//    }

//    //保存拍照
//    [DllImport("__Internal")]
//    private static extern void uploadPhoto(string path);
//    public static void Call_UploadPhoto(string path)
//    {
//        Debug.Log("Call_UploadPhoto");
//        uploadPhoto(path);
//    }

//    //开始下载Martin数据
//    [DllImport("__Internal")]
//    private static extern void downLoadMoudle();
//    public static void Call_DownLoadMoudle()
//    {
//        downLoadMoudle();
//        Debug.Log("Call_DownLoadMoudle");
//    }

//    //加载Martin引擎
//    [DllImport("__Internal")]
//    private static extern void loadEngine();
//    public static void Call_LoadEngine()
//    {
//        loadEngine();
//        Debug.Log("Call_LoadEngine");
//    }

//    //判断是否需要加载Martin 数据
//    [DllImport("__Internal")]
//    private static extern bool isNeedDownload();
//    public static bool Call_IsNeedDownload()
//    {
//        Debug.Log("Call_IsNeedDownload");
//        return isNeedDownload();
//    }
//    #endif

//    #elif !UITY_EDITOR && UNITY_ANDROID

//    private static AndroidJavaObject javaObject = null;
//    private static AndroidJavaObject GetJavaObject()
//    {
//        if (javaObject == null)
//        {
//            AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//            javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
//        }
//        return javaObject;        
//    }
        
//    public static void Call_QuitUnity()
//    {
//        Debug.Log("Call_QuitUnity");
//        GetJavaObject().Call("quitUnity");
//    }

//    public static void Call_ForwardAddressPage()
//    {
//        Debug.Log("Call_ForwardAddressPage");
//        GetJavaObject().Call("forwardAddressPage");
//    }

//    public static bool Call_IsShowContact()
//    {
//        Debug.Log("Call_IsShowContact");
//        return GetJavaObject().Call<bool>("isShowContact");
//    }

//    public static int Call_GetLanguage()
//    {
//        Debug.Log("Call_GetLanguage");
//        return GetJavaObject().Call<int>("getLanguage");
//    }

//    public static string Call_GetParams()
//    {
//        Debug.Log("Call_GetParams");
//        return GetJavaObject().Call<string>("getParams");
//    }

//    public static void Call_MultiInteract(string json) {
//        Debug.Log("Call_MultiInteract");
//        GetJavaObject().Call("sendMultiInteractMSG", json);
//    }

//    public static void Call_UploadPhoto(string path)
//    {
//        Debug.Log("Call_UploadPhoto");
//        GetJavaObject().Call("uploadPhoto", path);
//    }

//    public static void Call_DownLoadMoudle()
//    {
//        Debug.Log("Call_DownLoadMoudle");
//        GetJavaObject().Call("downLoadMoudle");
//    }   
    
//    public static void Call_LoadEngine()
//    {
//        Debug.Log("Call_LoadEngine");
//        GetJavaObject().Call("loadEngine");
//    } 
    
//    public static bool Call_IsNeedDownload()
//    {
//        Debug.Log("Call_IsNeedDownload");
//        return GetJavaObject().Call<bool>("isNeedDownload");
//    }      
//    #endif

//}
