//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;

//public class MarkerGO : System.Object {
//    public bool state = false;
//}

//public class MarkerModel : MarkerGO
//{    
//    public LocalObjectVO objectVO;
//    public LocalDownLoadUnitVO downloadUnitVO;
//    public GameObject modelGO;

//    public MarkerModel(){
//    }

//    public MarkerModel(LocalObjectVO _objectVO)
//    {
//        this.objectVO = _objectVO;
//        this.downloadUnitVO = mDownLoadProxy.RetrieveDownLoadUnitVO(_objectVO.objectId);
//    }

//    public MarkerModel(LocalDownLoadUnitVO _downloadUnitVO)
//    {
//        this.downloadUnitVO = _downloadUnitVO;
//    }

//    private DownLoadProxy mDownLoadProxy_;
//    private DownLoadProxy mDownLoadProxy
//    {
//        get
//        {
//            if (null == mDownLoadProxy_)
//            {
//                mDownLoadProxy_ = Facade.Instance.RetrieveProxy(DownLoadProxy.NAME) as DownLoadProxy;
//            }
//            return mDownLoadProxy_;
//        }
//    }
//}

////待优化 优化多态继承
//public class Marker3DText : MarkerGO
//{
//    public Text3DAction textAction;
//    public LocalObjectVO text3DVO;
//}

//public class Marker3DImg : MarkerGO
//{
//    public Image3DAction imgAction;
//    public LocalObjectVO img3DVO;
//}

//public class MarkerVideo : MarkerGO
//{
//    public VideoAction videoAction;
//    public LocalObjectVO videoVO;
//}

//public class MarkerAudio : MarkerGO
//{
//    public AudioAction audioAction;
//    public LocalObjectVO audioVO;
//}

//public class MarkerPoint : MarkerGO
//{
//    public MarkerPointAction markerPointAction;
//    public LocalMarkerPointVO markerPointVO;
//}

//public class MarkerArea : MarkerGO
//{
//    public MarkerAreaAction markerAreaAction;
//    public LocalMarkerPointVO markerAreaVO;
//}

//public class MarkerMeasure : MarkerGO
//{
//    public MarkerMeasureAction markerMeasureAction;
//    public LocalMeasureVO markerMeasureVO;
//}


//public class PoolManager : MonoBehaviour {

//    private static PoolManager mInstance;
//    private static readonly object mStaticSyncRoot = new object();

//    private Transform mCustomModelRoot;
//    private Transform mLocalModelRoot;
//    private Transform mTextRoot;
//    private Transform mImageRoot;
//    private Transform mVideoRoot;
//    private Transform mAudioRoot;

//	private Transform mMarkerPointRoot;
//    private Transform mMarkerAreaRoot;
//    private Transform mMarkerMeasureRoot;
	
//    private Transform mHintTextRoot;

//    //自定义模型List
//    private List<MarkerModel> mCustomModelList = new List<MarkerModel>();
//    //本地模型List
//    private List<MarkerModel> mLocalModelList = new List<MarkerModel>();
//    //3d文字List
//    private List<Marker3DText> mText3DLists = new List<Marker3DText>();
//    //3d图片List
//    private List<Marker3DImg> mImage3DLists = new List<Marker3DImg>();

//     //视频物体列表
//    private List<MarkerVideo> mVideoLists = new List<MarkerVideo>();

//    //音频物体列表
//    private List<MarkerAudio> mAudioLists = new List<MarkerAudio>();

//	//标记点列表
//    private List<MarkerPoint> mMarkerPointLists = new List<MarkerPoint>();
//    //标记区域列表
//    private List<MarkerArea> mMarkerAreaLists = new List<MarkerArea>();
//    //标记测量列表
//    private List<MarkerMeasure> mMarkerMeasureLists = new List<MarkerMeasure>();
	
//    //消息列表 <type, List<Part_HintUnit>>
//    private Dictionary<string, List<Part_HintUnit>> mHintTextMap = new Dictionary<string, List<Part_HintUnit>>();

//    //材质列表<objectId, Material>
//    private Dictionary<string, Material> mMaterialMap = new Dictionary<string, Material>();
//    private Material material;

//    //缓存缩略图 <path, Sprite>
//    private Dictionary<string, Sprite> mThumbSpriteDict = new Dictionary<string, Sprite>();

//    private Sprite mDefaultThumbSprite;
//    private Sprite mDefaultModelThumbSprite;


//    public static PoolManager Instance
//    {
//        get
//        {
//            if (mInstance == null)
//            {
//                lock (mStaticSyncRoot)
//                {
//                    if (mInstance == null)
//                    {
//                        GameObject mNHeroPool = GameObject.Find("HEROPOOL");

//                        if (mNHeroPool == null)
//                        {
//                            mNHeroPool = new GameObject("HEROPOOL");                            
//                        }
//                        mInstance = mNHeroPool.AddComponent<PoolManager>();

//                        mInstance.mCustomModelRoot = new GameObject("CustomModelRoot").transform;
//                        mInstance.mCustomModelRoot.SetParent(mNHeroPool.transform);

//                        mInstance.mLocalModelRoot = new GameObject("LocalModelRoot").transform;
//                        mInstance.mLocalModelRoot.SetParent(mNHeroPool.transform);

//                        mInstance.mTextRoot = new GameObject("TextRoot").transform;
//                        mInstance.mTextRoot.SetParent(mNHeroPool.transform);

//                        mInstance.mImageRoot = new GameObject("ImageRoot").transform;
//                        mInstance.mImageRoot.SetParent(mNHeroPool.transform);

//                        mInstance.mVideoRoot = new GameObject("VideoRoot").transform;
//                        mInstance.mVideoRoot.SetParent(mNHeroPool.transform);

//                        mInstance.mAudioRoot = new GameObject("AudioRoot").transform;
//                        mInstance.mAudioRoot.SetParent(mNHeroPool.transform);

//						mInstance.mMarkerPointRoot = new GameObject("MarkerPointRoot").transform;
//                        mInstance.mMarkerPointRoot.SetParent(mNHeroPool.transform);

//                        mInstance.mMarkerAreaRoot = new GameObject("MarkerAreaRoot").transform;
//                        mInstance.mMarkerAreaRoot.SetParent(mNHeroPool.transform);

//                        mInstance.mMarkerMeasureRoot = new GameObject("MarkerMeasureRoot").transform;
//                        mInstance.mMarkerMeasureRoot.SetParent(mNHeroPool.transform);

//                        mInstance.mHintTextRoot = new GameObject("HintTextRoot").transform;
//                        mInstance.mHintTextRoot.SetParent(mNHeroPool.transform);

//                        mInstance.material = Resources.Load<Material>("Materials/quad_image");

//                        mInstance.mDefaultThumbSprite = AssetManager.Instance.LoadSprite(AssetManager.AssetSprite("bg_common_default_thumb"));

//                        mInstance.mDefaultModelThumbSprite = AssetManager.Instance.LoadSprite(AssetManager.AssetSprite("bg_common_default_model_thumb"));
//                    }
//                }
//            }
//            return mInstance;
//        }
//    }

//    //自定义模型列表长度
//    private int _customModelCount;
//    private int CustomModelCount {
//        get {
//            return _customModelCount;
//        }
//        set {
//            _customModelCount = value;
//        }
//    }

//    //本地模型列表长度
//    private int _localModelCount;
//    private int LocalModelCount
//    {
//        get
//        {
//            return _localModelCount;
//        }
//        set
//        {
//            _localModelCount = value;
//        }
//    }

//    #region 缩略图

//    //默认本地工程缩略图
//    public Sprite RetrieveDefaultThumbImg()
//    {
//        return mDefaultThumbSprite;
//    }

//    //默认本地模型缩略图
//    public Sprite RetrieveDefaultModelThumbImg()
//    {
//        return mDefaultModelThumbSprite;
//    }

//    //获取本地缩略图
//    public Sprite RetrieveThumbImg(string imgName)
//    {
//        string path = string.Concat(DownLoadUtil.ThumbImagePath, imgName);
//        return ReadImage(path);
//    }

//    private Sprite ReadImage(string path)
//    {
//        Debug.Log("Load Thumb Image Name ==> " + path);

//        if (mThumbSpriteDict.ContainsKey(path))
//        {
//            return mThumbSpriteDict[path];
//        }
//        else
//        {
//            //创建Sprite
//            Sprite sprite = DownLoadUtil.ReadSprite(path);

//            if (sprite == null)
//            {
//            }
//            else {
//                sprite.name = path;

//                if (mThumbSpriteDict.ContainsKey(path))
//                {
//                    mThumbSpriteDict.Remove(path);
//                }
//                mThumbSpriteDict.Add(path, sprite);
//                Resources.UnloadUnusedAssets();
//            }           
//            return sprite;
//        }
//    }

//    #endregion 

//    #region 本地模型

//    /// <summary>
//    /// 获取本地模型
//    /// 存在就取出|复制
//    /// </summary>
//    public void RetrieveLocalModel(ref MarkerModel markerModel)
//    {
//        for (int i = 0; i < LocalModelCount; i++)
//        {
//            if (CkeckModelExist(mLocalModelList[i], markerModel))
//            {
//                if (mLocalModelList[i].state)
//                {
//                    GameObject go = Instantiate(mLocalModelList[i].modelGO) as GameObject;
//                    go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//                    markerModel.modelGO = go;
//                    go.SetActive(false);

//                    markerModel.state = true;
//                    break;
//                }
//                else
//                {
//                    mLocalModelList[i].state = true;
//                    mLocalModelList[i].objectVO = markerModel.objectVO;
//                    markerModel = mLocalModelList[i];
//                    break;
//                }
//            }
//        }

//        if (markerModel.modelGO == null)
//        {
//            //本地模型默认是prefab
//            string _url = mDownLoadProxy.RetrieveObjectUrl(markerModel.objectVO.objectId);
//            string _path = AssetManager.AssetModelPath(DownLoadUtil.GetFileNameWithoutExtensionByURL(_url));

//            GameObject go = AssetManager.Instance.InstantiateFromResources(_path);
//            markerModel.modelGO = go;
//            go.SetActive(false);
//            markerModel.state = true;

//            CacheLocalModel(ref markerModel);
//        }
//    }
    
//    /// <summary>
//    /// 缓存自定义模型
//    /// </summary>
//    private void CacheLocalModel(ref MarkerModel markerModel)
//    {         
//        ToolKit.ChangeAllChildShader(markerModel.modelGO);
//        ToolKit.AddMeshCollider(markerModel.modelGO);

//        markerModel.modelGO.transform.SetParent(mLocalModelRoot);
//        markerModel.state = false;
//        mLocalModelList.Add(markerModel);
//        LocalModelCount = mLocalModelList.Count;
//    }

//    /// <summary>
//    /// 回收自定义模型
//    /// 存在就删除
//    /// </summary>
//    public void ReturnLocalModel(MarkerModel markerModel)
//    {
//        if (mLocalModelList.Contains(markerModel))
//        {
//			ResetMeshRenderer(markerModel.modelGO);
			
//            markerModel.state = false;
//            markerModel.modelGO.SetActive(false);
//            markerModel.modelGO.transform.SetParent(mLocalModelRoot);
//        }
//        else
//        {
//            GameObject.DestroyImmediate(markerModel.modelGO);
//            markerModel.modelGO = null;
//        }

//         while (LocalModelCount > 15)
//        {
//            markerModel = mLocalModelList[0];
//            GameObject.DestroyImmediate(markerModel.modelGO);
//            markerModel.modelGO = null;
//            mLocalModelList.RemoveAt(0);
//            LocalModelCount = mLocalModelList.Count;
//        }
//    }

//    #endregion

//    #region 服务器模型
    
//    /// <summary>
//    /// 获取自定义模型
//    /// 存在就取出|复制
//    /// </summary>
//    public void RetrieveCustomModel(ref MarkerModel markerModel) {
//        for (int i = 0; i < CustomModelCount; i++) {
//            if (CkeckModelExist(mCustomModelList[i], markerModel)) {
//                if (mCustomModelList[i].state) {
//                    GameObject go = Instantiate(mCustomModelList[i].modelGO) as GameObject;
//                    go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//                    markerModel.modelGO = go;
//                    go.SetActive(false);
//                    markerModel.state = true;
//                    break;
//                }
//                else {
//                    mCustomModelList[i].state = true;
//                    mCustomModelList[i].objectVO = markerModel.objectVO;
//                    markerModel = mCustomModelList[i];
//                    break;
//                }
//            }
//        }
//    }

//    /// <summary>
//    /// 缓存自定义模型
//    /// </summary>
//    public void CacheCustomModel(ref MarkerModel markerModel) {       
// 		LocalDownLoadUnitVO _unitVO = mDownLoadProxy.RetrieveDownLoadUnitVO(markerModel.objectVO.objectId);
//        if (_unitVO.isMedicModel)
//        {
//            ToolKit.ChangeAllChildShader(markerModel.modelGO, "Legacy Shaders/Diffuse");
//        }
//        else {
//            ToolKit.ChangeAllChildShader(markerModel.modelGO);
//        }
		
//        ToolKit.AddMeshCollider(markerModel.modelGO);

//        markerModel.modelGO.transform.SetParent(mCustomModelRoot);
//        markerModel.state = false;
//        mCustomModelList.Add(markerModel);
//        CustomModelCount = mCustomModelList.Count;
//    }

//    /// <summary>
//    /// 回收自定义模型
//    /// 存在就删除
//    /// </summary>
//    public void ReturnCustomModel(MarkerModel markerModel) {
//        if (mCustomModelList.Contains(markerModel))
//        {
//            ResetMeshRenderer(markerModel.modelGO);
			
//            markerModel.state = false;
//            markerModel.modelGO.SetActive(false);
//            markerModel.modelGO.transform.SetParent(mCustomModelRoot);
//        }
//        else
//        {
//            GameObject.DestroyImmediate(markerModel.modelGO);
//            markerModel.modelGO = null;
//        }

//         while (CustomModelCount > 15)
//        {
//            markerModel = mCustomModelList[0];
//            GameObject.DestroyImmediate(markerModel.modelGO);
//            markerModel.modelGO = null;
//            mCustomModelList.RemoveAt(0);
//            CustomModelCount = mCustomModelList.Count;
//        }
//    }

//    /// <summary>
//    /// 检查模型是否已经存在
//    /// </summary>
//    private bool CkeckModelExist(MarkerModel modelVO1, MarkerModel modelVO2)
//    {
//        bool _idEquals = false;

//        if (modelVO1.objectVO != null && !string.IsNullOrEmpty(modelVO1.objectVO.objectId) 
//            && modelVO2.objectVO != null && !string.IsNullOrEmpty(modelVO2.objectVO.objectId)) {
//            _idEquals = modelVO1.objectVO.objectId.Equals(modelVO2.objectVO.objectId);
//        }

//        if (modelVO1.downloadUnitVO != null && !string.IsNullOrEmpty(modelVO1.downloadUnitVO.id)
//           && modelVO2.downloadUnitVO != null && !string.IsNullOrEmpty(modelVO2.downloadUnitVO.id))
//        {
//            _idEquals = modelVO1.downloadUnitVO.id.Equals(modelVO2.downloadUnitVO.id);
//        }

//        return _idEquals;
//    }

//    #endregion

//    #region 文字

//    /// <summary>
//    /// 获得3d文字 缓存十个
//    /// </summary>
//    public void Retrieve3DText(ref Marker3DText marker3DText) {        
//        int count = mText3DLists.Count;
//        bool found = false;
//        for (int i = 0; i < count; i++) {
//            if (!mText3DLists[i].state)
//            {
//                found = true;
//                mText3DLists[i].state = true;
//                mText3DLists[i].text3DVO = marker3DText.text3DVO;
//                marker3DText = mText3DLists[i];
//                break;
//            }
//        }

//        if (!found) {
//            if (count > 0)
//            {
//                GameObject go = Instantiate(mText3DLists[0].textAction.gameObject) as GameObject;
//                go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//                go.SetActive(false);
//                marker3DText.textAction = go.GetComponent<Text3DAction>();
//                marker3DText.state = true;
//                mText3DLists.Add(marker3DText);
//            }
//            else {
//                GameObject go = AssetManager.Instance.InstantiateFromResources(AssetManager.Asset3DTextMarkerPath);
//                go.SetActive(false);
//                marker3DText.textAction = go.GetComponent<Text3DAction>();
//                marker3DText.state = true;
//                mText3DLists.Add(marker3DText);
//            }
//        }        
//    }

//    /// <summary>
//    /// 回收3d文字
//    /// </summary>
//    public void Return3DText(Marker3DText marker3DText)
//    {
//        int _textCount = mText3DLists.Count;

//        if (_textCount > 9) 
//		{
//            if (mText3DLists.Contains(marker3DText))
//            {
//                GameObject.DestroyImmediate(marker3DText.textAction.gameObject);
//                mText3DLists.Remove(marker3DText);
//                _textCount = mText3DLists.Count;
//            }
//            else
//            {
//                GameObject.DestroyImmediate(marker3DText.textAction.gameObject);
//            }
//        }

//        marker3DText.state = false;
//        if (mText3DLists.Contains(marker3DText))
//        {
//			ResetMeshRenderer(marker3DText.textAction.gameObject);
//            marker3DText.textAction.transform.SetParent(mTextRoot);
//            marker3DText.textAction.gameObject.SetActive(false);
//        }
//        else 
//		{
//            GameObject.DestroyImmediate(marker3DText.textAction.gameObject);
//        }
//    }

//    #endregion

//    #region 图片

//    /// <summary>
//    /// 获得3d图片 缓存十个
//    /// </summary>
//    public void Retrieve3DImage(ref Marker3DImg marker3DImg)
//    {
//        int count = mImage3DLists.Count;
//        bool found = false;

//        Marker3DImg _img = null;

//        for (int i = 0; i < count; i++)
//        {
//            if (CheckImagExist(mImage3DLists[i].img3DVO, marker3DImg.img3DVO))
//            {
//                found = true;
//                _img = mImage3DLists[i];
//                break;
//            }
//        }

//        if (found)
//        {
//            if (_img.state)
//            {
//                GameObject go = Instantiate(mImage3DLists[0].imgAction.gameObject) as GameObject;
//                go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//                go.SetActive(false);
//                marker3DImg.imgAction = go.GetComponent<Image3DAction>();
//                marker3DImg.state = true;
//                mImage3DLists.Add(marker3DImg);
//            }
//            else
//            {
//                _img.state = true;
//                _img.img3DVO = marker3DImg.img3DVO;
//                marker3DImg = _img;
//            }
//        }
//        else
//        {
//            if (count > 0)
//            {
//                GameObject go = Instantiate(mImage3DLists[0].imgAction.gameObject) as GameObject;
//                go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//                go.SetActive(false);
//                marker3DImg.imgAction = go.GetComponent<Image3DAction>();
//                marker3DImg.state = true;
//                mImage3DLists.Add(marker3DImg);
//            }
//            else
//            {
//                GameObject go = AssetManager.Instance.InstantiateFromResources(AssetManager.Asset3DImgMarkerPath);
//                go.SetActive(false);
//                marker3DImg.imgAction = go.GetComponent<Image3DAction>();
//                marker3DImg.state = true;
//                mImage3DLists.Add(marker3DImg);
//            }
//        }
//    }

//    /// <summary>
//    /// 回收3d图片
//    /// </summary>
//    public void Return3DImage(Marker3DImg marker3DImg)
//    {
//        int _imgCount = mImage3DLists.Count;

//        if (_imgCount > 9)
//        {
//            if (mImage3DLists.Contains(marker3DImg))
//            {
//                Debug.Log("DestroyImmediate 3");
//                GameObject.DestroyImmediate(marker3DImg.imgAction.gameObject);
//                mImage3DLists.Remove(marker3DImg);
//            }
//            else
//            {
//                Debug.Log("DestroyImmediate 2");
//                GameObject.DestroyImmediate(marker3DImg.imgAction.gameObject);
//            }            
//        }
//        else 
//		{
//            marker3DImg.state = false;
//            if (mImage3DLists.Contains(marker3DImg))
//            {
//				ResetMeshRenderer(marker3DImg.imgAction.gameObject);
//                marker3DImg.imgAction.transform.SetParent(mImageRoot);
//                marker3DImg.imgAction.gameObject.SetActive(false);
//            }
//            else
//            {
//                Debug.Log("DestroyImmediate 1");
//                GameObject.DestroyImmediate(marker3DImg.imgAction.gameObject);
//            }
//        }
//    }
       
//    /// <summary>
//    /// 判断图片是否存在
//    /// </summary>
//    public bool CheckImagExist(LocalObjectVO imgVO1, LocalObjectVO imgVO2) {
//        bool idEquals = false;
//        if (!string.IsNullOrEmpty(imgVO1.objectId) && !string.IsNullOrEmpty(imgVO2.objectId)
//            && imgVO1.objectId.Equals(imgVO2.objectId))
//        {
//            idEquals = true;
//        }
       
//        return idEquals;
//    }

//    #endregion

//    #region 视频
    
//    /// <summary>
//    /// 获取视频物体
//    /// </summary>
//    public void RetrieveVideoAction(ref MarkerVideo markerVideo) {

//        //不能复制，复制之后视频会翻转
//        GameObject go = AssetManager.Instance.InstantiateFromResources(AssetManager.AssetVideoMarkerPath);
//        go.SetActive(false);
//        markerVideo.videoAction = go.GetComponent<VideoAction>();
//        markerVideo.state = true;
//        ToolKit.AddAABBBoxCollider(markerVideo.videoAction.gameObject);

//        // 解决视频暂停和跳步的冲突
//        // 将视频暂停区域，转移到视频左下角
//        Part_Video _video = markerVideo.videoAction.gameObject.GetComponentInChildren<Part_Video>();
//        _video.gameObject.AddComponent<MeshCollider>();
//        Vector3 _position = _video.transform.localPosition;
//        _position.z = 0.027f;
//        _video.transform.localPosition = _position;

//        mVideoLists.Add(markerVideo);
//    }
	
    
//    /// <summary>
//    /// 回收视频物体
//    /// </summary>
//    public void ReturnVideoAction(MarkerVideo markerVideo)
//    {
//		int _count = mVideoLists.Count;

//        if (_count > 9)
//        {
//            if (mVideoLists.Contains(markerVideo))
//            {
//                GameObject.DestroyImmediate(markerVideo.videoAction.gameObject);
//                mVideoLists.Remove(markerVideo);
//                _count = mVideoLists.Count;
//            }
//            else
//            {
//                GameObject.DestroyImmediate(markerVideo.videoAction.gameObject);
//            }
//            return;
//        }

//        if (mVideoLists.Contains(markerVideo))
//        {
//            markerVideo.state = false;
//            ResetMeshRenderer(markerVideo.videoAction.gameObject);
//            markerVideo.videoAction.transform.SetParent(mVideoRoot);
//            markerVideo.videoAction.gameObject.SetActive(false);
//        }
//        else
//        {
//            GameObject.DestroyImmediate(markerVideo.videoAction.gameObject);
//        }
//    }

//    #endregion

//    #region 音频

//    /// <summary>
//    /// 获取音频物体
//    /// </summary>
//    public void RetrieveAudioAction(ref MarkerAudio markerAudio)
//    {
//    	int count = mAudioLists.Count;
//        bool found = false;
//        for (int i = 0; i < count; i++)
//        {
//            if (!mAudioLists[i].state)
//            {
//                found = true;
//                mAudioLists[i].state = true;
//                mAudioLists[i].audioVO = markerAudio.audioVO;
//                markerAudio = mAudioLists[i];
//                break;
//            }
//        }

//        if (!found)
//        {
//            if (count > 0)
//            {
//                GameObject go = Instantiate(mAudioLists[0].audioAction.gameObject) as GameObject;
//                go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//                go.SetActive(false);
//                markerAudio.audioAction = go.GetComponent<AudioAction>();
//                markerAudio.state = true;
//                mAudioLists.Add(markerAudio);
//            }
//            else
//            {
//                GameObject go = AssetManager.Instance.InstantiateFromResources(AssetManager.AssetAudioMarkerPath);
//                go.SetActive(false);
//                markerAudio.audioAction = go.GetComponent<AudioAction>();
//                markerAudio.state = true;
//                mAudioLists.Add(markerAudio);
//            }
//        }
//    }

//    /// <summary>
//    /// 回收音频物体
//    /// </summary>
//    public void ReturnAudioAction(MarkerAudio markerAudio)
//    {
//		int _count = mAudioLists.Count;

//        if (_count > 9)
//        {
//            if (mAudioLists.Contains(markerAudio))
//            {
//                GameObject.DestroyImmediate(markerAudio.audioAction.gameObject);
//                mAudioLists.Remove(markerAudio);
//                _count = mAudioLists.Count;
//            }
//            else
//            {
//                GameObject.DestroyImmediate(markerAudio.audioAction.gameObject);
//            }
//            return;
//        }

//        if (mAudioLists.Contains(markerAudio))
//        {
//            markerAudio.state = false;
//            markerAudio.audioAction.transform.SetParent(mAudioRoot);
//            markerAudio.audioAction.gameObject.SetActive(false);
//        }
//        else
//        {
//            GameObject.DestroyImmediate(markerAudio.audioAction.gameObject);
//        }
//    }

//    #endregion

// 	#region 标记点

//    /// <summary>
//    /// 获取标记点
//    /// </summary>
//    public void RetrieveMarkerPointAction(ref MarkerPoint markerPoint)
//    {
//        int count = mMarkerPointLists.Count;
//        bool found = false;
//        for (int i = 0; i < count; i++)
//        {
//            if (!mMarkerPointLists[i].state)
//            {
//                found = true;
//                mMarkerPointLists[i].state = true;
//                mMarkerPointLists[i].markerPointVO = markerPoint.markerPointVO;
//                markerPoint = mMarkerPointLists[i];
//                break;
//            }
//        }

//        if (!found)
//        {
//            if (count > 0)
//            {
//                GameObject go = Instantiate(mMarkerPointLists[0].markerPointAction.gameObject) as GameObject;
//                go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//                go.SetActive(false);
//                markerPoint.markerPointAction = go.GetComponent<MarkerPointAction>();
//                markerPoint.state = true;
//                mMarkerPointLists.Add(markerPoint);
//            }
//            else
//            {
//                GameObject go = AssetManager.Instance.InstantiateFromResources(AssetManager.AssetMarkerPointPath);
//                go.SetActive(false);
//                markerPoint.markerPointAction = go.GetComponent<MarkerPointAction>();
//                markerPoint.state = true;
//                mMarkerPointLists.Add(markerPoint);
//            }
//        }
//    }

//    /// <summary>
//    /// 回收标记点
//    /// </summary>
//    public void ReturnMarkerPointAction(MarkerPoint markerPoint)
//    {
//        int _count = mMarkerPointLists.Count;

//        if (_count > 9)
//        {
//            if (mMarkerPointLists.Contains(markerPoint))
//            {
//                GameObject.DestroyImmediate(markerPoint.markerPointAction.gameObject);
//                mMarkerPointLists.Remove(markerPoint);
//                _count = mMarkerPointLists.Count;
//            }
//            else
//            {
//                GameObject.DestroyImmediate(markerPoint.markerPointAction.gameObject);
//            }
//            return;
//        }

//        if (mMarkerPointLists.Contains(markerPoint))
//        {
//            markerPoint.state = false;
//            markerPoint.markerPointAction.transform.SetParent(mMarkerPointRoot);
//            markerPoint.markerPointAction.gameObject.SetActive(false);
//        }
//        else
//        {
//            GameObject.DestroyImmediate(markerPoint.markerPointAction.gameObject);
//        }
//    }

//    #endregion

//    #region 标记区域

//    /// <summary>
//    /// 获取标记区域
//    /// </summary>
//    public void RetrieveMarkerAreaAction(ref MarkerArea markerArea)
//    {
//        int count = mMarkerAreaLists.Count;
//        bool found = false;
//        for (int i = 0; i < count; i++)
//        {
//            if (!mMarkerAreaLists[i].state)
//            {
//                found = true;
//                mMarkerAreaLists[i].state = true;
//                mMarkerAreaLists[i].markerAreaVO = markerArea.markerAreaVO;
//                markerArea = mMarkerAreaLists[i];
//                break;
//            }
//        }

//        if (!found)
//        {
//            if (count > 0)
//            {
//                GameObject go = Instantiate(mMarkerAreaLists[0].markerAreaAction.gameObject) as GameObject;
//                go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//                go.SetActive(false);
//                markerArea.markerAreaAction = go.GetComponent<MarkerAreaAction>();
//                markerArea.state = true;
//                mMarkerAreaLists.Add(markerArea);
//            }
//            else
//            {
//                GameObject go = AssetManager.Instance.InstantiateFromResources(AssetManager.AssetMarkerAreaPath);
//                go.SetActive(false);
//                markerArea.markerAreaAction = go.GetComponent<MarkerAreaAction>();
//                markerArea.state = true;
//                mMarkerAreaLists.Add(markerArea);
//            }
//        }
//    }

//    /// <summary>
//    /// 回收标记区域
//    /// </summary>
//    public void ReturnMarkerAreaAction(MarkerArea markerArea)
//    {
//        int _count = mMarkerAreaLists.Count;

//        if (_count > 9)
//        {
//            if (mMarkerAreaLists.Contains(markerArea))
//            {
//                GameObject.DestroyImmediate(markerArea.markerAreaAction.gameObject);
//                mMarkerAreaLists.Remove(markerArea);
//                _count = mMarkerAreaLists.Count;
//            }
//            else
//            {
//                GameObject.DestroyImmediate(markerArea.markerAreaAction.gameObject);
//            }
//            return;
//        }

//        if (mMarkerAreaLists.Contains(markerArea))
//        {
//            markerArea.state = false;
//            markerArea.markerAreaAction.transform.SetParent(mMarkerAreaRoot);
//            markerArea.markerAreaAction.gameObject.SetActive(false);
//        }
//        else
//        {
//            GameObject.DestroyImmediate(markerArea.markerAreaAction.gameObject);
//        }
//    }

//    #endregion

//    #region 标记测量
    
//    /// <summary>
//    /// 获取测量
//    /// </summary>
//    public void RetrieveMarkerMeasureAction(ref MarkerMeasure markerMeasure)
//    {
//        int count = mMarkerMeasureLists.Count;
//        bool found = false;
//        for (int i = 0; i < count; i++)
//        {
//            if (!mMarkerMeasureLists[i].state)
//            {
//                found = true;
//                mMarkerMeasureLists[i].state = true;
//                mMarkerMeasureLists[i].markerMeasureVO = markerMeasure.markerMeasureVO;
//                markerMeasure = mMarkerMeasureLists[i];
//                break;
//            }
//        }

//        if (!found)
//        {
//            if (count > 0)
//            {
//                GameObject go = Instantiate(mMarkerMeasureLists[0].markerMeasureAction.gameObject) as GameObject;
//                go.name = go.name.Substring(0, go.name.IndexOf("(Clone)"));
//                go.SetActive(false);
//                markerMeasure.markerMeasureAction = go.GetComponent<MarkerMeasureAction>();
//                markerMeasure.state = true;
//                mMarkerMeasureLists.Add(markerMeasure);
//            }
//            else
//            {
//                GameObject go = AssetManager.Instance.InstantiateFromResources(AssetManager.AssetMarkerMeasurePath);
//                go.SetActive(false);
//                markerMeasure.markerMeasureAction = go.GetComponent<MarkerMeasureAction>();
//                markerMeasure.state = true;
//                mMarkerMeasureLists.Add(markerMeasure);
//            }
//        }
//    }

//    /// <summary>
//    /// 回收测量
//    /// </summary>
//    public void ReturnMarkerMeasureAction(MarkerMeasure markerMeasure)
//    {
//        int _count = mMarkerMeasureLists.Count;

//        if (_count > 9)
//        {
//            if (mMarkerMeasureLists.Contains(markerMeasure))
//            {
//                GameObject.DestroyImmediate(markerMeasure.markerMeasureAction.gameObject);
//                mMarkerMeasureLists.Remove(markerMeasure);
//                _count = mMarkerMeasureLists.Count;
//            }
//            else
//            {
//                GameObject.DestroyImmediate(markerMeasure.markerMeasureAction.gameObject);
//            }
//            return;
//        }

//        if (mMarkerMeasureLists.Contains(markerMeasure))
//        {
//            markerMeasure.state = false;
//            markerMeasure.markerMeasureAction.transform.SetParent(mMarkerMeasureRoot);
//            markerMeasure.markerMeasureAction.gameObject.SetActive(false);
//        }
//        else
//        {
//            GameObject.DestroyImmediate(markerMeasure.markerMeasureAction.gameObject);
//        }
//    }

//    #endregion
	
//    #region 消息

//    public Part_HintUnit RetrieveHintText(AUEventUIShowHintInfo _hintInfoEvent) {
//        string _type = _hintInfoEvent.type.ToString();
//        if (mHintTextMap.ContainsKey(_type))
//        {
//            if (mHintTextMap[_type].Count > 0)
//            {                
//                foreach (Part_HintUnit _hint in mHintTextMap[_type]) {
//                    if (!string.IsNullOrEmpty(_hint.mHintText.text) &&
//                        _hint.mHintText.text.Equals(_hintInfoEvent.hint)) {
//                        Part_HintUnit _hintUnit = mHintTextMap[_type][0];
//                        return _hintUnit;
//                    }
//                }
//                foreach (Part_HintUnit _hint in mHintTextMap[_type])
//                {
//                    if (_hint.mCanvasGroup.alpha == 0)
//                    {
//                        Part_HintUnit _hintUnit = _hint;
//                        return _hintUnit;
//                    }
//                }
//            }
//            else
//            {
//                return null;
//            }
//        }
//        return null;
//    }

//     public void CacheHintText(string type, Part_HintUnit hintUnit) {
//        if (mHintTextMap.ContainsKey(type))
//        {
//            mHintTextMap[type].Add(hintUnit);
//        }
//        else
//        {
//            List<Part_HintUnit> _list = new List<Part_HintUnit>();
//            _list.Add(hintUnit);
//            mHintTextMap.Add(type, _list);
//        }
//    }
//    public void ReturnHintText(Part_HintUnit hintUnit) {        
//        hintUnit.transform.SetParent(mHintTextRoot);
//    }

//    #endregion

//    #region 材质

//    /// <summary>
//    /// 获取材质
//    /// </summary>
//    public Material RetrieveMaterial(string objectId) {
//        if (mMaterialMap.ContainsKey(objectId))
//        {
//            return mMaterialMap[objectId];
//        }
//        else
//        {
//            Material mt = Instantiate<Material>(material) as Material;
//            mMaterialMap.Add(objectId, mt);
//            return mt;
//        }
//    }

//    #endregion

//    /// <summary>
//    /// 清理模型池
//    /// </summary>
//    public void ClearPool()
//    {
//        ClearCustomPool();

//        Resources.UnloadUnusedAssets();
//    }

//    /// <summary>
//    /// 清理自定义模型池
//    /// </summary>
//    private void ClearCustomPool()
//    {
//        int _customModelCount = mCustomModelList.Count;
//        for (int i = 0; i < _customModelCount; i++)
//        {
//            if (mCustomModelList[i].modelGO != null)
//            {
//                DestroyImmediate(mCustomModelList[i].modelGO);
//            }
//        }
//        mCustomModelList.Clear();
//        CustomModelCount = 0;
//    }
    
// 	private void ResetMeshRenderer(GameObject go) {

//        if (go == null) {
//            return;
//        }

//        Renderer[] rendererComponents = go.GetComponentsInChildren<Renderer>(true);
//        Collider[] colliderComponents = go.GetComponentsInChildren<Collider>(true);
//        MeshFilter[] meshFilterComponents = go.GetComponentsInChildren<MeshFilter>(true);

//        // Enable rendering:
//        foreach (Renderer component in rendererComponents)
//        {
//            component.enabled = true;
//        }

//        // Enable colliders:
//        foreach (Collider component in colliderComponents)
//        {
//            component.enabled = true;
//        }

//        // Enable colliders:
//        foreach (MeshFilter component in meshFilterComponents)
//        {
//            if (component.mesh.name.Contains("Lenovo_NBD_Test"))
//            {
//                Renderer _render = component.gameObject.GetComponent<Renderer>();
//                _render.enabled = false;
//            }
//        }
//    }
	
//    private DownLoadProxy mDownLoadProxy_ = null;
//    private DownLoadProxy mDownLoadProxy
//    {
//        get
//        {
//            if (mDownLoadProxy_ == null)
//            {
//                mDownLoadProxy_ = Facade.Instance.RetrieveProxy(DownLoadProxy.NAME) as DownLoadProxy;
//            }
//            return mDownLoadProxy_;
//        }
//    }
//}
