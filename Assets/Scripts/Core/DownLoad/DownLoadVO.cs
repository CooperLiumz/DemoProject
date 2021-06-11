//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System;

//public class CacheUserFileVO : System.Object {
//    public CacheDownLoadFileVO[] caheDownLoadFileVOs;
//}

//public class CacheDownLoadFileVO : System.Object
//{
//    //用户ID
//    public long userId;

//    //等待下载的文件 断点续传 ，现在没用
//    public ServerDownLoadUnitVO[] waitLoadList;

//    //已下载文件列表
//    public ServerDownLoadUnitVO[] completeUnitVOList;    

//    //下载完成的工作流工程信息
//    public ServerProjectVO[] completeWorkFlowVOList;
//    //下载完成的识别导览工程信息
//    public ServerProjectVO[] completeMarkerTourVOList;
//    //下载完成的Tango工程信息
//    public ServerProjectVO[] completeTangoVOList;
//    //下载完成的Manual工程信息
//    public ServerProjectVO[] completeManualVOList;
//    //下载完成的轮廓识别工程信息
//    public ServerProjectVO[] completeContourVOList;
//    //下载完成的Martin识别工程信息
//    public ServerProjectVO[] completeMartinVOList;
//}

//#region 服务器数据

////项目VO
//public class ServerProjectVO : System.Object {

//	/// <summary>
//    /// 项目id
//    /// </summary>
//    public string id;

//    /// <summary>
//    /// 工程名字
//    /// </summary>
//    public string name;
//    /// <summary>
//    /// 工程描述
//    /// </summary>
//    public string resume;
	
//    /// <summary>
//    /// 工程上次修改时间
//    /// 根据时间判断是否要更新工程
//    /// </summary>
//    public long modifyDate;

//    /// <summary>
//    /// 工程创建时间
//    /// </summary>
//    public long createDate;

//    /// <summary>
//    /// 项目封面图id
//    /// </summary>
//    public string objectId;

//    /// <summary>
//    /// 是否可以跳步
//    /// 0 否 1 是 
//    /// </summary>
//    public int automatic = 0;

//    /// <summary>
//    /// 项目类型
//    /// 只用来区分工业和医疗
//    /// 1 工业
//    /// 2 医疗
//    /// </summary>
//    public int projectType;
	
//	/// <summary>
//    /// 识别类型
//    /// 0 无
//    /// 1 图片识别
//    /// 2 空间识别
//    /// 3 轮廓识别
//    /// 4 Martin
//    /// </summary>
//    public int markType;

//    /// <summary>
//    /// 移动端可编辑
//    /// 1 是
//    /// 2 否
//    /// </summary>
//    public int mobileEdit;

//    /// <summary>
//    /// 播放条件
//    /// 1 语音
//    /// 2 手动
//    /// </summary>
//    public int playConn;//conditions

//    /// <summary>
//    /// 播放顺序
//    /// 1 顺序
//    /// 2 无需
//    /// </summary>
//    public int playOrder;

//    /// <summary>
//    /// 项目发布ID
//    /// </summary>
//    public long publishId;

//    /// <summary>
//    /// 企业id
//    /// </summary>
//    public long companyId;

//    /// <summary>
//    /// 用户id
//    /// </summary>
//    public long userId;

//    /// <summary>
//    /// 创建人名字
//    /// </summary>
//    public string userName;

//    /// <summary>
//    /// 项目状态
//    /// 1 保存
//    /// 2 发布
//    /// </summary>
//    public int status;

//    /// <summary>
//    /// martin工作流的martin模型id
//    /// 轮廓识别的数据集id
//    /// </summary>
//    public string aiModelId;
    
//    //配置信息
//    public ServerStageVO[] stageList;

//    //下载文件
//    public ServerDownLoadListVO downloadList;    
//}

////stage
//public class ServerStageVO : System.Object
//{
//    //场景stage ID
//    public string id;

//    //项目id
//    public string projectId;

//    //页面顺序 
//    public int lineNo;

//    /// <summary>
//    /// 识别图id,Tango模式为null
//    /// 对应DownLoadUnitVO里的id
//    /// </summary>
//    public string markImageId;

//    //步骤信息
//    public ServerPageVO[] stagePageList;
//}

////page
//public class ServerPageVO : System.Object {

//    //页面id 
//    public string id;

//    //stage ID
//    public string stageId;

//    //名字
//    public string name;

//    //页面顺序 
//    public int lineNo;
    
//    /// <summary>
//    /// 切换模式
//    /// 0 没有
//    /// 1 线性
//    /// </summary>
//    public int flipMode;
//    //切换时间
//    public int flipTime;

//    /// <summary>
//    /// 是否为故障树
//    /// 0 否
//    /// 1 是
//    /// </summary>
//    public int isFaultTree;

//    //物体数组
//    public ServerObjectVO[] pageObjectList;

//	//测量信息
//    public ServerMeasureVO[] measureList;
//    //标记点
//    public ServerMarkerPointVO[] markPointList;

//    //故障树信息
//    public ServerFaultTreeVO faultTree;
//}

////ObjectVO
//public class ServerObjectVO : System.Object {

//    //物体ID(服务器生成的)
//    public string id;
	
//	//物体的动态id
//   //编辑段编辑时的UUID
//    public string dynamicId;

//    //对应的父物体的 dynamicId
//    public string parentId;

//    //tag标识是不是不同Page的同一模型
//    public string tagId;

//    //page id
//    public string pageId;

//    //物体名字
//    public string name;
//    //当物体为文字时显示的内容
//    public string text;

//    /// <summary>
//    /// 物体来源
//    /// 1 系统模型库
//    /// 2 用户自定义上传的模型  
//    /// </summary>
//    public int srcType;
//    /// <summary>
//    /// 物体类型
//    /// 1 Model   模型
//    /// 2 Image   图片 
//    /// 3 Audio   音频
//    /// 4 Video   视频
//    /// 5 Text    文字
//    /// </summary>
//    public int objectType;
//    /// <summary>
//    /// 物体对应资源的id
//    /// 对应DownLoadUnitVO里的id
//    /// </summary>
//    public string objectId;

//    //物体初始位置
//    public string position;
//    //物体初始旋转
//    public string rotation;
//    //物体初始缩放
//    public string scale;
//    /// <summary>
//    /// 物体动画循环方式
//    /// Restart = 0,
//    /// Yoyo = 1,    
//    /// Incremental = 2
//    /// </summary>
//    public int loopType = 1;
//    /// <summary>
//    /// loop times 
//    /// -1(无限循环)  0 不循环 1 循环一次 
//    /// </summary>
//    public int loopTimes = -1;

//    /// <summary>
//    /// 要跳转到的
//    /// page lineNo
//    /// </summary>
//    public int jumpPageLineNo = -1;

//    //物体行为
//    public ServerActionVO[] objectActionList;
//}

////measure vo 
//public class ServerMeasureVO : System.Object {
//    public string id;
//    //对应的父物体的id
//    public string parentId;
//    public string position;
//    public string color;
//    /// <summary>
//    /// 1 标记点
//    /// 2 标记区域
//    /// </summary>
//    public int type;
//}

////marker point/area vo
//public class ServerMarkerPointVO : System.Object {
//    public string id;
//    //对应的父物体的id
//    public string parentId;
//    public string position;
//    public string scale;
//    public string color;
//    public float opacity;
//    public float radius;
//    public string text;
//    /// <summary>
//    /// 1 标记点
//    /// 2 标记区域  病灶
//    /// 3 标记区域  可疑
//    /// 4 标记区域  禁碰
//    /// </summary>
//    public int type;
//}

////故障树
//public class ServerFaultTreeVO : System.Object {
//    //故障树描述
//    public string resume;

//    //故障音频
//    public string audioId;

//    //故障树按钮
//    public ServerFaultTreeButtonVO[] buttonList;
//}

////故障树按钮
//public class ServerFaultTreeButtonVO : System.Object {
//    //按钮名字 <= 4
//    public string name;
//    /// <summary>
//    /// 链接到下一个page(可以是正常步骤或故障树)
//    /// 对应page lineNo
//    /// </summary>
//    public int pageLineNo;

//}

////模型动作 
//public class ServerActionVO : System.Object
//{
//    //动作iD
//    public string id;

//    //objectID
//    public string pageObjectId;

//    //动作顺序
//    public int lineNo;

//    //duration
//    public float time = 1;   

//    //positon
//    public string position;
//    //rotation
//    public string rotation;
//    //scale
//    public string scale;
//}

////下载列表
//public class ServerDownLoadListVO : System.Object {
//    //需要下载的文件列表
//    public ServerDownLoadUnitVO[] objectList;

//    //识别图列表
//    public ServerDownLoadUnitVO[] markerImageList;

//}

////下载列表里的元素
//public class ServerDownLoadUnitVO : System.Object
//{
//    /// <summary>
//    /// 文件唯一ID   
//    /// </summary>
//    public string id;
//    /// <summary>
//    /// 上传前压缩包的名字，也是模型的名字 
//    /// 目前目录是
//    /// ZIP
//    ///     FBX
//    ///     Tex
//    /// 中间没有文件夹
//    /// </summary>
//    public string name;
//    /// <summary>
//    /// 文件展示名字
//    /// </summary>
//    public string showName;
//    /// <summary>
//    /// 物体来源
//    /// 1 系统模型库
//    /// 2 用户自定义上传的模型  
//    /// </summary>
//    public int srcType;
//    /// <summary>
//    /// 物体类型
//    /// 1 Model   模型
//    /// 2 Image   图片 
//    /// 3 Audio   音频
//    /// 4 Video   视频
//    /// 5 Text    文字
//    /// MarkerImage 6  
//    /// ImagePck    7   图片压缩包
//    /// ThumbImage  8   缩略图
//    /// </summary>
//    public int objectType;

//    //文件的地址
//    public string url;
//    //文件MD5值
//    public string md5;

//    //缩略图的地址
//    public string thumbUrl;
//    //缩略图的MD5值
//    public string thumbMd5;

//    /// <summary>
//    /// 资源大小 KB
//    /// </summary>
//    public int size;

//    //备注
//    public string remark;
//}

//#endregion

//#region 本地缓存数据

//public class MarkerImageVO : System.Object
//{
//    //识别图信息
//    public byte[] bytes;

//    //识别图配置信息
//    public LocalStageVO stageVO;

//    //工程信息
//    public LocalProjectVO projectVO;

//    //动作索引
//    private int pageIndex;

//    //识别图索引
//    private int stageIdx;

//    //最大识别的动作索引
//    private int maxIndex;

//    private bool alreadyMarked;

//    private EU_PageActionType  pageActiontype = EU_PageActionType.NONE;

//    public int PageIndex
//    {
//        get {
//            return pageIndex;
//        }
//        set {
//            pageIndex = value;
//        }
//    }

//    public int StageIndex
//    {
//        get
//        {
//            return stageIdx;
//        }
//        set
//        {
//            stageIdx = value;
//        }
//    }

//    public int MaxIndex
//    {
//        get
//        {
//            return maxIndex;
//        }
//        set
//        {
//            maxIndex = value > maxIndex ? value : maxIndex;
//        }
//    }

//    public bool AlreadyMarked
//    {
//        get
//        {
//            return alreadyMarked;
//        }
//        set
//        {
//            alreadyMarked = value;
//        }
//    }

//    public bool CanMoveNext {
//        get {
//            if (alreadyMarked) {
//                if (PageIndex > MaxIndex)
//                {
//                    return false;
//                }
//                else {
//                    return true;
//                }
//            } else {
//                return false;
//            }
//        }
//    }

//    public bool IsFaultTreeCurPage
//    {
//        get {
//            return stageVO.stagePageList[pageIndex].IsFaultTree;
//        }
//    }

//    public EU_PageActionType PageActiontype
//    {
//        get {
//            return pageActiontype;
//        }
//        set {
//            pageActiontype = value;
//        }
//    }
//}

//public class TransformVO : System.Object
//{
//    public Vector3 position;
//    public Vector3 rotation;
//    public Vector3 scale;

//    public string strPosition;
//    public string strRotation;
//    public string strScale;

//    public TransformVO(Vector3 _position, Vector3 _rotation, Vector3 _scale)
//    {
//        this.position = _position;
//        this.rotation = _rotation;
//        this.scale = _scale;
//    }

//    public TransformVO(string _strPosition, string _strRotation, string _strScale) {
//        this.strPosition = _strPosition;
//        this.strRotation = _strRotation;
//        this.strScale = _strScale;

//        TransformVO _tfVO = ToolKit.ConvertCoordinate(_strPosition, _strRotation, _strScale);

//        this.position = _tfVO.position;
//        this.rotation = _tfVO.rotation;
//        this.scale = _tfVO.scale;
//    }

//    public TransformVO()
//    {
//    }
	
//	public TransformVO Copy() {
//        TransformVO inTarget = new TransformVO();
//        inTarget.strPosition = this.strPosition;
//        inTarget.strRotation = this.strRotation;
//        inTarget.strScale = this.strScale;
//        inTarget.position = this.position;
//        inTarget.rotation = this.rotation;
//        inTarget.scale = this.scale;
//        return inTarget;
//    }
//}

//public class ParentVO : System.Object
//{
//    public string id;

//}

////本地项目VO
//public class LocalProjectVO : System.Object
//{
//    /// <summary>
//    /// 项目id
//    /// </summary>    
//	public string id;

//	/// <summary>
//    /// 工程名字
//    /// </summary>
//	public string name;
	
//     /// <summary>
//    /// 工程描述
//    /// </summary>    
//	public string resume;
	
//    /// <summary>
//    /// 工程上次修改时间
//    /// 根据时间判断是否要更新工程
//    /// </summary>
//    public long modifyDate;

//    /// <summary>
//    /// 工程创建时间
//    /// </summary>
//    public long createDate;

//    /// <summary>
//    /// 项目封面图id
//    /// </summary>
//    public string objectId;

//    /// <summary>
//    /// 是否可以跳步
//    /// 0 否 1 是 
//    /// </summary>
//    public int automatic = 0;

//    /// <summary>
//    /// 项目类型
//    /// 只用来区分工业和医疗
//    /// 1 工业
//    /// 2 医疗
//    /// </summary>
//    public int projectType;

//    /// <summary>
//    /// 识别类型
//    /// 0 无
//    /// 1 图片识别
//    /// 2 空间识别
//    /// 3 轮廓识别
//    /// 4 Martin
//    /// </summary>
//    public int markType;

//    /// <summary>
//    /// 移动端可编辑
//    /// 1 是
//    /// 2 否
//    /// </summary>
//    public int mobileEdit;

//    /// <summary>
//    /// 播放条件
//    /// 1 语音
//    /// 2 手动
//    /// </summary>
//    public int playConn;//conditions

//    /// <summary>
//    /// 播放顺序
//    /// 1 顺序
//    /// 2 无序
//    /// </summary>
//    public int playOrder;

//    /// <summary>
//    /// 项目发布ID
//    /// </summary>
//    public long publishId;

//    /// <summary>
//    /// 企业id
//    /// </summary>
//    public long companyId;

//    /// <summary>
//    /// 用户id
//    /// </summary>
//    public long userId;

//    /// <summary>
//    /// 创建人名字
//    /// </summary>
//    public string userName;

//    /// <summary>
//    /// 项目状态
//    /// 1 保存
//    /// 2 发布
//    /// </summary>
//    public int status;

//    /// <summary>
//    /// martin工作流的martin模型id
//    /// 轮廓识别的数据集id
//    /// </summary>
//    public string aiModelId;

//    //配置信息
//    public LocalStageVO[] stageList;

//    //需要下载的文件列表
//    public LocalDownLoadUnitVO[] downloadVOList;
    
//    /// <summary>
//    /// 工程状态
//    /// 0 未下载
//    /// 1 已下载
//    /// 2 要更新
//    /// 3 不可用，待删除
//    /// </summary>
//    public int state;

//    public static implicit operator LocalProjectVO(ServerProjectVO inSource)
//    {
//        LocalProjectVO inTarget = new LocalProjectVO();
//        inTarget.id = inSource.id;
//        inTarget.name = inSource.name;
//        inTarget.resume = inSource.resume;
//        inTarget.modifyDate = inSource.modifyDate;
//        inTarget.createDate = inSource.createDate;
//        inTarget.objectId = inSource.objectId;
//        inTarget.automatic = inSource.automatic;
//        inTarget.projectType = inSource.projectType;
//        inTarget.publishId = inSource.publishId;
//        inTarget.companyId = inSource.companyId;
//        inTarget.userId = inSource.userId;
//        inTarget.userName = inSource.userName;
//        inTarget.status = inSource.status;
// 		inTarget.markType = inSource.markType;
//        inTarget.mobileEdit = inSource.mobileEdit;
//        inTarget.playConn = inSource.playConn;
//        inTarget.playOrder = inSource.playOrder;
//        inTarget.aiModelId = inSource.aiModelId;

//        List<LocalStageVO> _stageVOList = new List<LocalStageVO>();
//        int _stageLgh = inSource.stageList.Length;
//        for (int i = 0; i < _stageLgh; i++)
//        {
//            _stageVOList.Add(inSource.stageList[i]);
//        }
//        _stageVOList.Sort(SortStage);

//        inTarget.stageList = _stageVOList.ToArray();

//        List<LocalDownLoadUnitVO> _unitVOList = new List<LocalDownLoadUnitVO>();
//        if (inSource.downloadList != null)
//        {
//            if (inSource.downloadList.markerImageList != null && inSource.downloadList.markerImageList.Length > 0)
//            {
//                int _lgh = inSource.downloadList.markerImageList.Length;
//                for (int i = 0; i < _lgh; i++)
//                {
//                    //识别图
//                    LocalDownLoadUnitVO _unitVO = inSource.downloadList.markerImageList[i];
//                    _unitVO.srcType = 2;
//                    _unitVO.objectType = 31;
//                    _unitVOList.Add(_unitVO);
//                }
//            }

//            if (inSource.downloadList.objectList != null && inSource.downloadList.objectList.Length > 0)
//            {
//                int _lgh = inSource.downloadList.objectList.Length;
//                for (int i = 0; i < _lgh; i++)
//                {
//                    LocalDownLoadUnitVO _unitVO = inSource.downloadList.objectList[i];
//                    _unitVOList.Add(_unitVO);

//                    if (!string.IsNullOrEmpty(_unitVO.thumbUrl))
//                    {
//                        //缩略图
//                        LocalDownLoadUnitVO _thumVO = inSource.downloadList.objectList[i];
//                        _thumVO.id = _thumVO.thumbMd5;
//                        //_thumVO.id = _thumVO.thumbUrl;
//                        _thumVO.srcType = 2;
//                        _thumVO.objectType = 33;
//                        _thumVO.url = _thumVO.thumbUrl;
//                        _thumVO.md5 = _thumVO.thumbMd5;
//                        _thumVO.size = 0;

//                        _unitVOList.Add(_thumVO);
//                    }
//                }
//            }
//        }

//        inTarget.downloadVOList = _unitVOList.ToArray();        

//        return inTarget;
//    }

//    public static implicit operator ServerProjectVO(LocalProjectVO inSource)
//    {
//        ServerProjectVO inTarget = new ServerProjectVO();
//        inTarget.id = inSource.id;
//        inTarget.name = inSource.name;
//        inTarget.resume = inSource.resume;
//        inTarget.modifyDate = inSource.modifyDate;
//        inTarget.createDate = inSource.createDate;
//        inTarget.objectId = inSource.objectId;
//        inTarget.automatic = inSource.automatic;
//        inTarget.projectType = inSource.projectType;
//        inTarget.publishId = inSource.publishId;
//        inTarget.companyId = inSource.companyId;
//        inTarget.userId = inSource.userId;
//        inTarget.userName = inSource.userName;
//        inTarget.status = inSource.status;
//		inTarget.markType = inSource.markType;
//        inTarget.mobileEdit = inSource.mobileEdit;
//        inTarget.playConn = inSource.playConn;
//        inTarget.playOrder = inSource.playOrder;
//        inTarget.aiModelId = inSource.aiModelId;

//        inTarget.stageList = new ServerStageVO[inSource.stageList.Length];
//        int _stageLgh = inSource.stageList.Length;
//        for (int i = 0; i < _stageLgh; i++)
//        {
//            inTarget.stageList[i] = inSource.stageList[i];
//        }

//        List<ServerDownLoadUnitVO> _imgList = new List<ServerDownLoadUnitVO>();
//        List<ServerDownLoadUnitVO> _objList = new List<ServerDownLoadUnitVO>();
//        if (inSource.downloadVOList != null && inSource.downloadVOList.Length > 0)
//        {
//            int _lgh = inSource.downloadVOList.Length;
//            for (int i = 0; i < _lgh; i++)
//            {
//                if (inSource.downloadVOList[i].isMarkerImage)
//                {
//                    _imgList.Add(inSource.downloadVOList[i]);
//                }
//                else
//                {
//                    if (!inSource.downloadVOList[i].isThumbImage)
//                    {
//                        _objList.Add(inSource.downloadVOList[i]);
//                    }
//                }
//            }
//        }

//        inTarget.downloadList = new ServerDownLoadListVO();
//        inTarget.downloadList.markerImageList = _imgList.ToArray();
//        inTarget.downloadList.objectList = _objList.ToArray();

//        return inTarget;
//    }
	
//	 //待优化，转成json，再转回来
//    //转回来之后需要设置state
//    public LocalProjectVO Copy() {
//        LocalProjectVO inTarget = new LocalProjectVO();
//        inTarget.id = this.id;
//        inTarget.name = this.name;
//        inTarget.resume = this.resume;
//        inTarget.modifyDate = this.modifyDate;
//        inTarget.createDate = this.createDate;
//        inTarget.objectId = this.objectId;
//        inTarget.automatic = this.automatic;
//        inTarget.projectType = this.projectType;
//        inTarget.publishId = this.publishId;
//        inTarget.companyId = this.companyId;
//        inTarget.userId = this.userId;
//        inTarget.userName = this.userName;
//        inTarget.status = this.status;
//        inTarget.markType = this.markType;
//        inTarget.mobileEdit = this.mobileEdit;
//        inTarget.playConn = this.playConn;
//        inTarget.playOrder = this.playOrder;
//        inTarget.aiModelId = this.aiModelId;

//        List<LocalStageVO> _stageVOList = new List<LocalStageVO>();
//        int _stageLgh = this.stageList.Length;
//        for (int i = 0; i < _stageLgh; i++)
//        {
//            _stageVOList.Add(this.stageList[i].Copy());
//        }
//        _stageVOList.Sort(SortStage);

//        inTarget.stageList = _stageVOList.ToArray();

//        List<LocalDownLoadUnitVO> _unitVOList = new List<LocalDownLoadUnitVO>();
//        if (this.downloadVOList != null)
//        {
//            foreach (LocalDownLoadUnitVO _unitvo in this.downloadVOList) {
//                _unitVOList.Add(_unitvo.Copy());
//            }
//        }

//        inTarget.downloadVOList = _unitVOList.ToArray();

//        return inTarget;
//    }

//    private static int SortStage(LocalStageVO value1, LocalStageVO value2) {
//        //默认从小到大排序，返回-1
//        return value1.lineNo.CompareTo(value2.lineNo);
//    }

//    public bool isAutomatic()
//    {
//        if (automatic == 0)
//        {
//            return false;
//        }
//        else
//        {
//            return true;
//        }
//    }

//    /// <summary>
//    /// 工程状态
//    /// </summary>
//    public bool isOut {
//        get {
//            return state == 2;
//        }
//    }

//    /// <summary>
//    /// 找到对应的配置信息的索引
//    /// </summary>
//    public int getStageVOIdx(string _stageId)
//    {
//        if (stageList == null)
//        {
//            return -1;
//        }

//        int _length = stageList.Length;
//        for (int i = 0; i < _length; i++)
//        {
//            if (stageList[i].id.Equals(_stageId))
//            {
//                return i;
//            }
//        }

//        return 0;
//    }

//    /// <summary>
//    /// 总步数
//    /// </summary>
//    private int stepCount = -1;
//    public int StepCount {
//        get {
//            if (stepCount >= 0)
//            {
//                return stepCount;
//            }
//            else {
//                InitStepCount();
//                return stepCount;
//            }
//        }
//    }
//    private void InitStepCount() {
//        if (stageList != null && stageList.Length > 0)
//        {
//            //计算所有的识别图的动作数即为识别总步数
//            int _stageLength = stageList.Length;
//            int _length = 0;
//            for (int i = 0; i < _stageLength; i++)
//            {
//                if (stageList[i].stagePageList != null && stageList[i].stagePageList.Length > 0)
//                {
//                    _length += stageList[i].stagePageList.Length;
//                }
//            }
//            stepCount = _length;
//        }
//        else
//        {
//            stepCount = 0;
//        }
//    }

//    /// <summary>
//    /// 总大小
//    /// KB
//    /// </summary>
//    private int projectSize = -1;
//    public string ProjectSize
//    {
//        get
//        {
//            if (projectSize >= 0)
//            {
//                //InitProjectSize();
//                return ToolKit.ConvertKBToM(projectSize);
//            }
//            else
//            {
//                InitProjectSize();
//                return ToolKit.ConvertKBToM(projectSize);
//            }
//        }
//    }
//    private void InitProjectSize()
//    {
//        projectSize = 0;
//        if (downloadVOList != null && downloadVOList.Length > 0)
//        {
//            int _lgh = downloadVOList.Length;
//            for (int i = 0; i < _lgh; i++) {
//                projectSize += downloadVOList[i].size;
//            }
//        }
//    }

//    public LocalDownLoadUnitVO ThumbVO
//    {
//        get {
//            if (string.IsNullOrEmpty(objectId))
//            {
//                return new LocalDownLoadUnitVO();
//            }
//            else {
//                int _lgh = downloadVOList.Length;
//                for (int i = 0; i < _lgh; i++)
//                {
//                    if (downloadVOList[i].id.Equals(objectId)) {
//                        return downloadVOList[i].Copy();
//                    }
//                }
//                return new LocalDownLoadUnitVO();
//            }
//        }
//    }

//    //图片识别有序
//    public bool isWorkFlow {
//        get {
//            return markType == 1 && playOrder == 1;
//        }
//    }

//    //图片识别无序
//    public bool isMarkerTour
//    {
//        get
//        {
//            return markType == 1 && playOrder == 2;
//        }
//    }

//    //空间识别 tango
//    public bool isTango
//    {
//        get
//        {
//            #if UNITY_EDITOR || UNITY_ANDROID
//            return markType == 2;
//            #else
//            return false;
//            #endif
//        }
//    }

//    //空间识别 ARKit
//    public bool isARKit
//    {
//        get
//        {
//            #if UNITY_IPHONE || UNITY_IOS
//            return markType == 2;
//            #else
//            return false;
//            #endif
//        }
//    }

//    //手动教程
//    public bool isManual
//    {
//        get
//        {
//            return markType == 0;
//        }
//    }

//    //空间识别医疗版
//	public bool isMedic
//    {
//        get
//        {
//            return markType == 2 && projectType == 2;
//        }
//    }

//    //轮廓识别
//    public bool isContour
//    {
//        get
//        {
//            return markType == 3;
//        }
//    }

//    //Martin识别
//    public bool isMartin
//    {
//        get
//        {
//            return markType == 4;
//        }
//    }

//    //有序
//    public bool isOrder {
//        get {
//            return playOrder == 1;
//        }
//    }

//    public bool isSameCompany {
//        get {
//            return companyId == UIGlobal.user_data.companyId;
//        }
//    }

//    //是否能在app编辑
//	public bool isEditable {
//        get {
//            return mobileEdit == 1;
//        }
//    }

//    /// <summary>
//    /// 根据pageId查找对应的PageVO
//    /// </summary>
//    public LocalPageVO RetrievePageVOByPageID(string pageVOId)
//    {
//        if (stageList == null || stageList.Length < 1) {
//            return new LocalPageVO();
//        }

//        int _count = stageList.Length;
//        for (int i = 0; i < _count; i++)
//        {
//            LocalPageVO[] _pageVOs = stageList[i].stagePageList;
//            if (_pageVOs != null && _pageVOs.Length > 0)
//            {
//                int _pageCount = _pageVOs.Length;

//                for (int j = 0; j < _pageCount; j++)
//                {
//                    if (_pageVOs[j].id.Equals(pageVOId))
//                    {
//                        return _pageVOs[j];
//                    }
//                }
//            }
//        }
//        return new LocalPageVO();
//    }

//    /// <summary>
//    /// 根据PageVO查找前一个PageVO
//    /// </summary>
//    public LocalPageVO RetrievePrevPageVOByPageID(string _pageID)
//    {
//        if (stageList == null || stageList.Length < 1) {
//            return new LocalPageVO();
//        }

//        List<LocalPageVO> _pageList = new List<LocalPageVO>();

//        int _stageCount = stageList.Length;
//        for (int i = 0; i < _stageCount; i++)
//        {
//            if (stageList[i].stagePageList != null && stageList[i].stagePageList.Length > 0) {
//                _pageList.AddRange(new List<LocalPageVO>(stageList[i].stagePageList));
//            }
//        }

//        int _pageCount = _pageList.Count;

//        for (int i = 0; i < _pageCount; i++)
//        {
//            if (_pageList[i].id.Equals(_pageID)) {
//                if (i > 0)
//                {
//                    return _pageList[i - 1];
//                }
//                else {
//                    return new LocalPageVO();
//                }
//            }
//        }

//        return new LocalPageVO();
//    }

//    /// <summary>
//    /// 根据PageVO查找后一个PageVO
//    /// </summary>
//    public LocalPageVO RetrieveNextPageVOByPageID(string _pageID)
//    {
//        if (stageList == null || stageList.Length < 1)
//        {
//            return new LocalPageVO();
//        }

//        List<LocalPageVO> _pageList = new List<LocalPageVO>();

//        int _stageCount = stageList.Length;
//        for (int i = 0; i < _stageCount; i++)
//        {
//            if (stageList[i].stagePageList != null && stageList[i].stagePageList.Length > 0)
//            {
//                _pageList.AddRange(new List<LocalPageVO>(stageList[i].stagePageList));
//            }
//        }

//        int _pageCount = _pageList.Count;

//        for (int i = 0; i < _pageCount; i++)
//        {
//            if (_pageList[i].id.Equals(_pageID))
//            {
//                if (i < _pageCount - 1)
//                {
//                    return _pageList[i + 1];
//                }
//                else
//                {
//                    return new LocalPageVO();
//                }
//            }
//        }

//        return new LocalPageVO();
//    }

//    /// <summary>
//    /// </summary>
//    public List<LocalDownLoadUnitVO> RetrieveMarkerImageList() {
//        List<LocalDownLoadUnitVO> _result = new List<LocalDownLoadUnitVO>();
//        if (downloadVOList != null && downloadVOList.Length > 0) {
//            int _length = downloadVOList.Length;
//            for (int i = 0; i < _length; i++)
//            {
//                if (downloadVOList[i].isMarkerImage)
//                {
//                    _result.Add(downloadVOList[i]);
//                }
//            }
//        }
//        return _result;
//    }

//	//=== 

//    //待修改
//    public void CheckState(LocalProjectVO _serverProjectVO) {
//        if (_serverProjectVO == null )
//        {
//            return;
//        }

//        if (_serverProjectVO.modifyDate.Equals(modifyDate))
//        {
//            state = 1;

//			#region 更新数据

//            //工程的创建人名字,返回数据时读取的外部数据
//            //创建人名字修改时，服务器工程状态不会改变
//            this.userName = _serverProjectVO.userName;

//            this.id = _serverProjectVO.id;
//            this.name = _serverProjectVO.name;
//            this.resume = _serverProjectVO.resume;
//            this.modifyDate = _serverProjectVO.modifyDate;
//            this.createDate = _serverProjectVO.createDate;
//            this.objectId = _serverProjectVO.objectId;
//            this.automatic = _serverProjectVO.automatic;
//            this.projectType = _serverProjectVO.projectType;
//            this.publishId = _serverProjectVO.publishId;
//            this.companyId = _serverProjectVO.companyId;
//            this.userId = _serverProjectVO.userId;
//            this.userName = _serverProjectVO.userName;
//            this.status = _serverProjectVO.status;
//            this.markType = _serverProjectVO.markType;
//            this.mobileEdit = _serverProjectVO.mobileEdit;
//            this.playConn = _serverProjectVO.playConn;
//            this.playOrder = _serverProjectVO.playOrder;
//			#endregion
//        }
//        else {
//            state = 2;
//        }
//    }
//}

////stage
//public class LocalStageVO : System.Object
//{
//    //场景stage ID
//    public string id;

//    //项目id
//    public string projectId;
    
//    /// <summary>
//    /// 场景顺序
//    /// 0-n
//    /// </summary>
//    public int lineNo;

//    /// <summary>
//    /// Manual 为null
//    /// ImageIdentify  (void ar)识别图id (vuforia 数据集里识别图id)
//    /// Slam 为空
//    /// Contour 数据集id
//    /// Martin 数据集id
//    /// 对应DownLoadUnitVO里的id
//    /// </summary>
//    public string markImageId;
    
//    //步骤信息
//    public LocalPageVO[] stagePageList;


//    public static implicit operator LocalStageVO(ServerStageVO inSource)
//    {
//        LocalStageVO inTarget = new LocalStageVO();
//        inTarget.id = inSource.id;
//        inTarget.projectId = inSource.projectId;
//        inTarget.lineNo = inSource.lineNo;
//        inTarget.markImageId = inSource.markImageId;      

//        List<LocalPageVO> _pageList = new List<LocalPageVO>();
//        int _stageLgh = inSource.stagePageList.Length;
//        for (int i = 0; i < _stageLgh; i++) {
//            _pageList.Add(inSource.stagePageList[i]);
//        }
//        _pageList.Sort(SortPage);

//        inTarget.stagePageList = _pageList.ToArray();

//        return inTarget;
//    }

//    public static implicit operator ServerStageVO(LocalStageVO inSource)
//    {
//        ServerStageVO inTarget = new ServerStageVO();
//        inTarget.id = inSource.id;
//        inTarget.projectId = inSource.projectId;
//        inTarget.lineNo = inSource.lineNo;
//        inTarget.markImageId = inSource.markImageId;

//        inTarget.stagePageList = new ServerPageVO[inSource.stagePageList.Length];
//        int _stageLgh = inSource.stagePageList.Length;
//        for (int i = 0; i < _stageLgh; i++)
//        {
//            inTarget.stagePageList[i] = inSource.stagePageList[i];
//        }

//        return inTarget;
//    }

//	public LocalStageVO Copy() {
//        LocalStageVO inTarget = new LocalStageVO();
//        inTarget.id = this.id;
//        inTarget.projectId = this.projectId;
//        inTarget.lineNo = this.lineNo;
//        inTarget.markImageId = this.markImageId;

//        List<LocalPageVO> _pageList = new List<LocalPageVO>();
//        int _stageLgh = this.stagePageList.Length;
//        for (int i = 0; i < _stageLgh; i++)
//        {
//            _pageList.Add(this.stagePageList[i].Copy());
//        }
//        _pageList.Sort(SortPage);

//        inTarget.stagePageList = _pageList.ToArray();

//        return inTarget;
//    }

//    private static int SortPage(LocalPageVO value1, LocalPageVO value2)
//    {
//        //默认从小到大排序，返回-1
//        return value1.lineNo.CompareTo(value2.lineNo);
//    }

//    public int RetrievePageIndex(string pageId) {
//        if (stagePageList != null && stagePageList.Length > 0)
//        {
//            int _count = stagePageList.Length;
//            for (int i = 0; i < _count; i++)
//            {
//                if (stagePageList[i].id.Equals(pageId))
//                {
//                    return i;
//                }
//            }
//            return 0;
//        }
//        else {
//            return 0;
//        }
//    }

//    public LocalPageVO RetrievePageVOByIdx(int idx)
//    {
//        if (stagePageList != null && stagePageList.Length > 0)
//        {
//            if (idx < 0)
//            {
//                return stagePageList[stagePageList.Length - 1];
//            }
//            else
//            {
//                return stagePageList[idx];
//            }
//        }
//        return new LocalPageVO();
//    }

//}

////page
//public class LocalPageVO : System.Object
//{
//    //页面id 
//    public string id;

//    //stage ID
//    public string stageId;

//    //名字
//    public string name;
    
//    /// <summary>
//    /// 页面顺序 
//    /// 0-n
//    /// 顺序为全局顺序
//    /// </summary>
//    public int lineNo;

//    /// <summary>
//    /// 切换模式
//    /// 0 没有
//    /// 1 线性
//    /// </summary>
//    public int flipMode;
//    //切换时间
//    public int flipTime;

//    /// <summary>
//    /// 是否为故障树
//    /// 0 否
//    /// 1 是
//    /// </summary>
//    public int isFaultTree;

//    //物体数组
//    public LocalObjectVO[] pageObjectList;

//	//测量信息
//    public LocalMeasureVO[] measureList;
//    //标记点
//    public LocalMarkerPointVO[] markPointList;

//    //故障树信息
//    public LocalFaultTreeVO faultTree;

//    public static implicit operator LocalPageVO(ServerPageVO inSource)
//    {
//        LocalPageVO inTarget = new LocalPageVO();
//        inTarget.id = inSource.id;
//        inTarget.stageId = inSource.stageId;
//        inTarget.lineNo = inSource.lineNo;
//        inTarget.flipMode = inSource.flipMode;
//        inTarget.flipTime = inSource.flipTime;
//        inTarget.name = inSource.name;
//        inTarget.isFaultTree = inSource.isFaultTree;


//        if (inSource.pageObjectList != null)
//        {
//            inTarget.pageObjectList = new LocalObjectVO[inSource.pageObjectList.Length];
//            int _stageLgh = inSource.pageObjectList.Length;
//            for (int i = 0; i < _stageLgh; i++)
//            {
//                inTarget.pageObjectList[i] = inSource.pageObjectList[i];
//            }
//        }
//        else {
//            List<LocalObjectVO> _objList = new List<LocalObjectVO>();
//            inTarget.pageObjectList = _objList.ToArray();
//        }

//        if (inSource.measureList != null)
//        {
//            inTarget.measureList = new LocalMeasureVO[inSource.measureList.Length];
//            int _measureLgh = inSource.measureList.Length;
//            for (int i = 0; i < _measureLgh; i++)
//            {
//                inTarget.measureList[i] = inSource.measureList[i];
//            }
//        }
//        else
//        {
//            List<LocalMeasureVO> _objList = new List<LocalMeasureVO>();
//            inTarget.measureList = _objList.ToArray();
//        }

//        if (inSource.markPointList != null) {
//            inTarget.markPointList = new LocalMarkerPointVO[inSource.markPointList.Length];
//            int _markerPointLgh = inSource.markPointList.Length;
//            for (int i = 0; i < _markerPointLgh; i++)
//            {
//                inTarget.markPointList[i] = inSource.markPointList[i];
//            }
//        }
//        else
//        {
//            List<LocalMarkerPointVO> _objList = new List<LocalMarkerPointVO>();
//            inTarget.markPointList = _objList.ToArray();
//        }

//        if (inSource.faultTree != null)
//        {
//            inTarget.faultTree = inSource.faultTree;
//        }
//        else {
//            inTarget.faultTree = new LocalFaultTreeVO();
//        }

//        return inTarget;
//    }

//    public static implicit operator ServerPageVO(LocalPageVO inSource)
//    {
//        ServerPageVO inTarget = new ServerPageVO();
//        inTarget.id = inSource.id;
//        inTarget.stageId = inSource.stageId;
//        inTarget.lineNo = inSource.lineNo;
//        inTarget.flipMode = inSource.flipMode;
//        inTarget.flipTime = inSource.flipTime;
//        inTarget.name = inSource.name;
//        inTarget.isFaultTree = inSource.isFaultTree;

//        if (inSource.pageObjectList != null) {
//            inTarget.pageObjectList = new ServerObjectVO[inSource.pageObjectList.Length];
//            int _stageLgh = inSource.pageObjectList.Length;
//            for (int i = 0; i < _stageLgh; i++)
//            {
//                inTarget.pageObjectList[i] = inSource.pageObjectList[i];
//            }
//        }

//        if (inSource.measureList != null) {
//            inTarget.measureList = new ServerMeasureVO[inSource.measureList.Length];
//            int _measureLgh = inSource.measureList.Length;
//            for (int i = 0; i < _measureLgh; i++)
//            {
//                inTarget.measureList[i] = inSource.measureList[i];
//            }
//        }

//        if (inSource.markPointList != null) {
//            inTarget.markPointList = new ServerMarkerPointVO[inSource.markPointList.Length];
//            int _markerPointLgh = inSource.markPointList.Length;
//            for (int i = 0; i < _markerPointLgh; i++)
//            {
//                inTarget.markPointList[i] = inSource.markPointList[i];
//            }
//        }

//        if (inSource.faultTree != null)
//        {
//            inTarget.faultTree = inSource.faultTree;
//        }

//        return inTarget;
//    }
	
//	public LocalPageVO Copy() {
//        LocalPageVO inTarget = new LocalPageVO();
//        inTarget.id = this.id;
//        inTarget.stageId = this.stageId;
//        inTarget.lineNo = this.lineNo;
//        inTarget.flipMode = this.flipMode;
//        inTarget.flipTime = this.flipTime;
//        inTarget.name = this.name;
//        inTarget.isFaultTree = this.isFaultTree;

//        if (this.pageObjectList != null)
//        {
//            inTarget.pageObjectList = new LocalObjectVO[this.pageObjectList.Length];
//            int _stageLgh = this.pageObjectList.Length;
//            for (int i = 0; i < _stageLgh; i++)
//            {
//                inTarget.pageObjectList[i] = this.pageObjectList[i].Copy();
//            }
//        }
//        else
//        {
//            List<LocalObjectVO> _objList = new List<LocalObjectVO>();
//            inTarget.pageObjectList = _objList.ToArray();
//        }

//        if (this.measureList != null)
//        {
//            inTarget.measureList = new LocalMeasureVO[this.measureList.Length];
//            int _measureLgh = this.measureList.Length;
//            for (int i = 0; i < _measureLgh; i++)
//            {
//                inTarget.measureList[i] = this.measureList[i].Copy();
//            }
//        }
//        else
//        {
//            List<LocalMeasureVO> _objList = new List<LocalMeasureVO>();
//            inTarget.measureList = _objList.ToArray();
//        }

//        if (this.markPointList != null)
//        {
//            inTarget.markPointList = new LocalMarkerPointVO[this.markPointList.Length];
//            int _markerPointLgh = this.markPointList.Length;
//            for (int i = 0; i < _markerPointLgh; i++)
//            {
//                inTarget.markPointList[i] = this.markPointList[i].Copy();
//            }
//        }
//        else
//        {
//            List<LocalMarkerPointVO> _objList = new List<LocalMarkerPointVO>();
//            inTarget.markPointList = _objList.ToArray();
//        }

//        if (this.faultTree != null)
//        {
//            inTarget.faultTree = this.faultTree.Copy();
//        }
//        else {
//            inTarget.faultTree = new LocalFaultTreeVO();
//        }

//        return inTarget;
//    }

//    public bool IsFaultTree {
//        get {

//            return isFaultTree == 1;
//        }
//    }
//}

////ObjectVO
//public class LocalObjectVO : System.Object
//{
//    //物体ID
//    public string id;

//	//物体的动态id
//    //编辑段编辑时的UUID
//    public string dynamicId;//dynamicId

//    //对应的父物体的 dynamicId
//    public string parentId;
	
//    //tag标识是不是不同Page的同一模型
//    public string tagId;

//    //page id
//    public string pageId;

//    //物体名字
//    public string name;
//    //当物体为文字时显示的内容
//    public string text;

//    //colorstr
//    public string colorStr;
//    //颜色
//    public Vector4 color;

//    /// <summary>
//    /// 物体来源
//    /// 1 系统模型库
//    /// 2 用户自定义上传的模型  
//    /// </summary>
//    public int srcType;
//    /// <summary>
//    /// 物体类型
//    /// 1   Model         模型
//    /// 2   Image         图片 
//    /// 3   Audio         音频
//    /// 4   Video         视频
//    /// 5   Text          文字
//    /// 6   MedicModel    病例模型
//    /// 7   MedicEquip    医疗器械
//    /// 8   MedicVideo    医疗视频
//    /// 9   ScreenShot    Web截图
//    /// 10  Contour       轮廓识别
//    /// </summary>
//    public int objectType;

//    /// <summary>
//    /// 物体对应资源的id
//    /// 对应DownLoadUnitVO里的id
//    /// </summary>
//    public string objectId;
    
//    public TransformVO transformVO;

//    /// <summary>
//    /// 物体动画循环方式
//    /// Restart = 0,
//    /// Yoyo = 1,    
//    /// Incremental = 2
//    /// </summary>
//    public int loopType = 1;
//    /// <summary>
//    /// loop times 
//    /// -1(无限循环)  0 不循环 1 循环一次 
//    /// </summary>
//    public int loopTimes = -1;

//    /// <summary>
//    /// 要跳转到的
//    /// page lineNo
//    /// </summary>
//    public int jumpPageLineNo = -1; 

//    //物体行为
//    public LocalActionVO[] objectActionList;

//    public static implicit operator LocalObjectVO(ServerObjectVO inSource)
//    {
//        LocalObjectVO inTarget = new LocalObjectVO();
//        inTarget.id = inSource.id;
//		inTarget.dynamicId = inSource.dynamicId;
//        inTarget.parentId = inSource.parentId;
//        inTarget.tagId = inSource.tagId;
//        inTarget.pageId = inSource.pageId;
//        inTarget.name = inSource.name;
//        inTarget.text = inSource.text;
//        inTarget.srcType = inSource.srcType;
//        inTarget.objectType = inSource.objectType;
//        inTarget.objectId = inSource.objectId;

//        //文字的时候转化颜色值
//        if (inTarget.objectType == 5) {
//            inTarget.color = ToolKit.StringToColor(inTarget.text);

//            string _text = inTarget.text;
//            int _index = _text.IndexOf("<color>");
//            if (_index > -1)
//            {
//                inTarget.colorStr = _text.Substring(_index);
//                inTarget.text = _text.Remove(_index);
//            }
//        }

//        inTarget.transformVO = new TransformVO(inSource.position, inSource.rotation, inSource.scale);        
      
//        inTarget.loopType = inSource.loopType;
//        inTarget.loopTimes = inSource.loopTimes;

//        inTarget.jumpPageLineNo = inSource.jumpPageLineNo;

//        List<LocalActionVO> _actionVOList = new List<LocalActionVO>();
//        int _stageLgh = inSource.objectActionList.Length;
//        for (int i = 0; i < _stageLgh; i++) {
//            _actionVOList.Add(inSource.objectActionList[i]);
//        }
//        _actionVOList.Sort(SortAction);

//        inTarget.objectActionList = _actionVOList.ToArray();
//        return inTarget;
//    }

//    public static implicit operator ServerObjectVO(LocalObjectVO inSource)
//    {
//        ServerObjectVO inTarget = new ServerObjectVO();
//        inTarget.id = inSource.id;
//		inTarget.dynamicId = inSource.dynamicId;
//        inTarget.parentId = inSource.parentId;
//        inTarget.tagId = inSource.tagId;
//        inTarget.pageId = inSource.pageId;
//        inTarget.name = inSource.name;
//        inTarget.text = inSource.text;
//        inTarget.srcType = inSource.srcType;
//        inTarget.objectType = inSource.objectType;
//        inTarget.objectId = inSource.objectId;

//        //文字的时候转化颜色值
//        if (inTarget.objectType == 5)
//        {
//            inTarget.text = string.Concat(inSource.text, inSource.colorStr);
//        }

//        inTarget.position = inSource.transformVO.strPosition;
//        inTarget.rotation = inSource.transformVO.strRotation;
//        inTarget.scale = inSource.transformVO.strScale;

//        inTarget.loopType = inSource.loopType;
//        inTarget.loopTimes = inSource.loopTimes;

//        inTarget.jumpPageLineNo = inSource.jumpPageLineNo;

//        inTarget.objectActionList = new ServerActionVO[inSource.objectActionList.Length];
//        int _stageLgh = inSource.objectActionList.Length;
//        for (int i = 0; i < _stageLgh; i++)
//        {
//            inTarget.objectActionList[i] = inSource.objectActionList[i];
//        }
//        return inTarget;
//    }

// 	public LocalObjectVO Copy() {
//        LocalObjectVO inTarget = new LocalObjectVO();
//        inTarget.id = this.id;
//        inTarget.dynamicId = this.dynamicId;
//        inTarget.parentId = this.parentId;
//        inTarget.tagId = this.tagId;
//        inTarget.pageId = this.pageId;
//        inTarget.name = this.name;
//        inTarget.text = this.text;
//        inTarget.srcType = this.srcType;
//        inTarget.objectType = this.objectType;
//        inTarget.objectId = this.objectId;

//        inTarget.color = this.color;
//        inTarget.colorStr = this.colorStr;
//        inTarget.text = this.text;

//        inTarget.transformVO = this.transformVO.Copy();

//        inTarget.loopType = this.loopType;
//        inTarget.loopTimes = this.loopTimes;

//        List<LocalActionVO> _actionVOList = new List<LocalActionVO>();
//        int _stageLgh = this.objectActionList.Length;
//        for (int i = 0; i < _stageLgh; i++)
//        {
//            _actionVOList.Add(this.objectActionList[i].Copy());
//        }
//        _actionVOList.Sort(SortAction);

//        inTarget.objectActionList = _actionVOList.ToArray();

//        return inTarget;
//    }
	
//    private static int SortAction(LocalActionVO value1, LocalActionVO value2)
//    {
//        //默认从小到大排序，返回-1
//        return value1.lineNo.CompareTo(value2.lineNo);
//    }
    
//    //系统模型
//    public bool isLocal {
//       get {
//            return srcType == 1;
//       }
//    }

//    //用户自定义上传模型
//    public bool isCustom
//    {
//        get
//        {
//            return srcType == 2;
//        }
//    }

//    //模型
//    public bool isModel
//    {
//        get
//        {
//	 		//临时修改
//            return objectType == 1 || isMedicModel || isMedicEquip;
//        }
//    }
//    //图片
//    public bool isImage
//    {
//        get
//        {
//            return objectType == 2;
//        }
//    }
//    //音频
//    public bool isAudio
//    {
//        get
//        {
//            return objectType == 3;
//        }
//    }
//    //视频
//    public bool isVideo
//    {
//        get
//        {
//            return objectType == 4;
//        }
//    }    
//    //文字
//    public bool isText
//    {
//        get
//        {
//            return objectType == 5;
//        }
//    }
	
//	//临时修改 type 
//    //病例模型 6 
//    public bool isMedicModel
//    {
//        get
//        {
//            return objectType == 6;
//        }
//    }
//    //医疗器材 7 
//    public bool isMedicEquip
//    {
//        get
//        {
//            return objectType == 7;
//        }
//    }    
//}

////measure vo 
//public class LocalMeasureVO : System.Object
//{
//    public string id;
//    //对应的父物体的id
//    public string parentId;
//    //多个点position
//    public string position;
//    public int type;

//    //
//    public List<Vector3> postionList;

//    public Vector4 color;
//    //颜色
//    public string colorStr;

//    public static implicit operator LocalMeasureVO(ServerMeasureVO inSource)
//    {
//        LocalMeasureVO inTarget = new LocalMeasureVO();
//        inTarget.id = inSource.id;
//        inTarget.parentId = inSource.parentId;
//        inTarget.position = inSource.position;
//        inTarget.type = inSource.type;
               
//        inTarget.colorStr = inSource.color;
//        inTarget.color = ToolKit.HexToColor(inSource.color);

//        //标记点用的是局部坐标，暂时转换有问题，
//        //临时做处理 x轴置反
//        inTarget.postionList = ToolKit.StringParseToVector3List(inSource.position);
//        int _count = inTarget.postionList.Count;
//        for (int i = 0; i < _count; i++ )
//        {
//            Vector3 _pos = inTarget.postionList[i];
//            _pos = new Vector3(-_pos.x, _pos.y, _pos.z);
//            inTarget.postionList[i] = _pos;
//        }

//        return inTarget;
//    }

//    public static implicit operator ServerMeasureVO(LocalMeasureVO inSource)
//    {
//        ServerMeasureVO inTarget = new ServerMeasureVO();
//        inTarget.id = inSource.id;
//        inTarget.parentId = inSource.parentId;
//        inTarget.position = inSource.position;
//        inTarget.color = inSource.colorStr;
//        inTarget.type = inSource.type;
//        return inTarget;
//    }

//    public LocalMeasureVO Copy() {
//        LocalMeasureVO inTarget = new LocalMeasureVO();
//        inTarget.id = this.id;
//        inTarget.parentId = this.parentId;
//        inTarget.position = this.position;
//        inTarget.type = this.type;

//        inTarget.colorStr = this.colorStr;
//        inTarget.color = this.color;

//        List<Vector3> _posList = new List<Vector3>();
//        if (this.postionList != null) {
//            foreach (Vector3 _pos in this.postionList) {
//                _posList.Add(_pos);
//            }
//        }
//        inTarget.postionList = _posList;
//        return inTarget;
//    }
//}

////marker point/area vo
//public class LocalMarkerPointVO : System.Object
//{
//    public string id;
//    //对应的父物体的id
//    public string parentId;
//    public string position;
//    public string scale;
//    public float opacity;
//    public float radius;
//    public string text;
//    /// <summary>
//    /// 1 标记点
//    /// 2 标记区域  病灶
//    /// 3 标记区域  可疑
//    /// 4 标记区域  禁碰
//    /// </summary>
//    public int type;

//    public Vector4 color;
//    public string colorStr;

//    //
//    public TransformVO transformVO;

//    //文字颜色
//    public Vector4 textColor;
//    public string textColorStr;

//    public static implicit operator LocalMarkerPointVO(ServerMarkerPointVO inSource)
//    {
//        LocalMarkerPointVO inTarget = new LocalMarkerPointVO();
//        inTarget.id = inSource.id;
//        inTarget.parentId = inSource.parentId;
//        inTarget.position = inSource.position;
//        inTarget.scale = inSource.scale;
//        inTarget.opacity = inSource.opacity;
//        inTarget.radius = inSource.radius;
//        inTarget.text = inSource.text;
//        inTarget.type = inSource.type;

//        inTarget.color = ToolKit.HexToColor(inSource.color);
//        inTarget.colorStr = inSource.color;

//        //标记点和标记区域用的是局部坐标，暂时转换有问题，
//        //临时做处理 x轴置反
//        //可能还需要添加旋转
//        Vector3 _pos = ToolKit.StringParseToVector3(inSource.position);
//        _pos = new Vector3(-_pos.x, _pos.y, _pos.z);
//        Vector3 _scale = ToolKit.StringParseToVector3(inSource.scale);
//        inTarget.transformVO = new TransformVO(_pos, Vector3.zero, _scale);

//        //Debug.Log("bef  " + inTarget.id + " " + inTarget.transformVO.position);
//        //inTarget.transformVO = new TransformVO(inSource.position, "0,0,0", inSource.scale);
//        //Debug.Log("after  "+ inTarget.id  +" "+ new TransformVO(inSource.position, "0,0,0", inSource.scale).position);
        

//        inTarget.textColor = ToolKit.StringToColor(inSource.text);

//        string _text = inTarget.text;
//        int _index = _text.IndexOf("<color>");
//        if (_index > -1)
//        {
//            inTarget.textColorStr = _text.Substring(_index);
//            inTarget.text = _text.Remove(_index);
//        }

//        return inTarget;
//    }

//    public static implicit operator ServerMarkerPointVO(LocalMarkerPointVO inSource)
//    {
//        ServerMarkerPointVO inTarget = new ServerMarkerPointVO();
//        inTarget.id = inSource.id;
//        inTarget.parentId = inSource.parentId;
//        inTarget.position = inSource.position;
//        inTarget.scale = inSource.scale;
//        inTarget.opacity = inSource.opacity;
//        inTarget.radius = inSource.radius;
//        inTarget.text = inSource.text;
//        inTarget.type = inSource.type;

//        inTarget.color = inSource.colorStr;

//        inTarget.text = inSource.text;

//        return inTarget;
//    }

//    public LocalMarkerPointVO Copy() {
//        LocalMarkerPointVO inTarget = new LocalMarkerPointVO();
//        inTarget.id = this.id;
//        inTarget.parentId = this.parentId;
//        inTarget.position = this.position;
//        inTarget.scale = this.scale;
//        inTarget.opacity = this.opacity;
//        inTarget.radius = this.radius;
//        inTarget.text = this.text;
//        inTarget.type = this.type;

//        inTarget.color = this.color;
//        inTarget.colorStr = this.colorStr;

//        inTarget.transformVO = this.transformVO.Copy();

//        inTarget.textColor = this.textColor;
//        inTarget.textColorStr = this.textColorStr;
//        inTarget.text = this.text;
//        return inTarget;
//    }
//}

////故障树
//public class LocalFaultTreeVO : System.Object
//{
//    //故障树描述
//    public string resume;

//    //audio id
//    public string audioId;

//    //故障树按钮
//    public LocalFaultTreeButtonVO[] buttonList;

//    public static implicit operator LocalFaultTreeVO(ServerFaultTreeVO inSource) {
//        LocalFaultTreeVO inTarget = new LocalFaultTreeVO();
//        inTarget.resume = inSource.resume;
//        inTarget.audioId = inSource.audioId;

//        if (inSource.buttonList != null)
//        {
//            inTarget.buttonList = new LocalFaultTreeButtonVO[inSource.buttonList.Length];
//            int _btnLgh = inSource.buttonList.Length;
//            for (int i = 0; i < _btnLgh; i++)
//            {
//                inTarget.buttonList[i] = inSource.buttonList[i];
//            }
//        }
//        else
//        {
//            List<LocalFaultTreeButtonVO> _btnList = new List<LocalFaultTreeButtonVO>();
//            inTarget.buttonList = _btnList.ToArray();
//        }

//        return inTarget;
//    }

//    public static implicit operator ServerFaultTreeVO(LocalFaultTreeVO inSource){
//        ServerFaultTreeVO inTarget = new ServerFaultTreeVO();
//        inTarget.resume = inSource.resume;
//        inTarget.audioId = inSource.audioId;

//        if (inSource.buttonList != null)
//        {
//            inTarget.buttonList = new ServerFaultTreeButtonVO[inSource.buttonList.Length];
//            int _btnLgh = inSource.buttonList.Length;
//            for (int i = 0; i < _btnLgh; i++)
//            {
//                inTarget.buttonList[i] = inSource.buttonList[i];
//            }
//        }
//        return inTarget;
//    }

//    public LocalFaultTreeVO Copy() {
//        LocalFaultTreeVO inTarget = new LocalFaultTreeVO();
//        inTarget.resume = this.resume;
//        inTarget.audioId = this.audioId;

//        if (this.buttonList != null)
//        {
//            inTarget.buttonList = new LocalFaultTreeButtonVO[this.buttonList.Length];
//            int _btnLgh = this.buttonList.Length;
//            for (int i = 0; i < _btnLgh; i++)
//            {
//                inTarget.buttonList[i] = this.buttonList[i].Copy();
//            }
//        }
//        else
//        {
//            List<LocalFaultTreeButtonVO> _btnList = new List<LocalFaultTreeButtonVO>();
//            inTarget.buttonList = _btnList.ToArray();
//        }

//        return new LocalFaultTreeVO();
//    }
//}

////故障树按钮
//public class LocalFaultTreeButtonVO : System.Object
//{
//    //按钮名字 <= 4
//    public string name;
//    /// <summary>
//    /// 链接到下一个page(可以是正常步骤或故障树)
//    /// 对应page lineNo
//    /// </summary>
//    public int pageLineNo = -1;

//    public static implicit operator LocalFaultTreeButtonVO(ServerFaultTreeButtonVO inSource) {
//        LocalFaultTreeButtonVO inTarget = new LocalFaultTreeButtonVO();
//        inTarget.name = inSource.name;
//        inTarget.pageLineNo = inSource.pageLineNo;

//        return inTarget;
//    }

//    public static implicit operator ServerFaultTreeButtonVO(LocalFaultTreeButtonVO inSource)
//    {
//        ServerFaultTreeButtonVO inTarget = new ServerFaultTreeButtonVO();
//        inTarget.name = inSource.name;
//        inTarget.pageLineNo = inSource.pageLineNo;

//        return inTarget;
//    }

//    public LocalFaultTreeButtonVO Copy() {
//        LocalFaultTreeButtonVO inTarget = new LocalFaultTreeButtonVO();
//        inTarget.name = this.name;
//        inTarget.pageLineNo = this.pageLineNo;
//        return inTarget;
//    }
//}

////模型动作 
//public class LocalActionVO : System.Object
//{
//    //动作iD
//    public string id;

//    //objectID
//    public string pageObjectId;

//    //动作顺序
//    public int lineNo;

//    //duration
//    public float time = 1;    

//    public TransformVO transformVO;

//    public static implicit operator LocalActionVO(ServerActionVO inSource)
//    {
//        LocalActionVO inTarget = new LocalActionVO();
//        inTarget.id = inSource.id;
//        inTarget.pageObjectId = inSource.pageObjectId;
//        inTarget.lineNo = inSource.lineNo;
//        inTarget.time = inSource.time;

//        inTarget.transformVO = new TransformVO(inSource.position, inSource.rotation, inSource.scale);

//        return inTarget;
//    }

//    public static implicit operator ServerActionVO(LocalActionVO inSource)
//    {
//        ServerActionVO inTarget = new ServerActionVO();
//        inTarget.id = inSource.id;
//        inTarget.pageObjectId = inSource.pageObjectId;
//        inTarget.lineNo = inSource.lineNo;
//        inTarget.time = inSource.time;

//        inTarget.position = inSource.transformVO.strPosition;
//        inTarget.rotation = inSource.transformVO.strRotation;
//        inTarget.scale = inSource.transformVO.strScale;

//        return inTarget;
//    }
//	public LocalActionVO Copy() {
//        LocalActionVO inTarget = new LocalActionVO();
//        inTarget.id = this.id;
//        inTarget.pageObjectId = this.pageObjectId;
//        inTarget.lineNo = this.lineNo;
//        inTarget.time = this.time;

//        inTarget.transformVO = this.transformVO.Copy();
//        return inTarget;
//    } 
//}

////下载列表里的元素
//public class LocalDownLoadUnitVO : System.Object
//{
//    /// <summary>
//    /// 文件唯一ID   
//    /// </summary>
//    public string id;
//    /// <summary>
//    /// 文件上传前的真实名字
//    /// zip里文件的名字
//    /// </summary>
//    public string name;
//    /// <summary>
//    /// 文件展示名字
//    /// </summary>
//    public string showName;
//    /// <summary>
//    /// 物体来源
//    /// 1 系统模型库
//    /// 2 用户自定义上传的模型  
//    /// </summary>
//    public int srcType;
//    /// <summary>
//    /// 物体类型
//    /// 1   Model       模型
//    /// 2   Image       图片 
//    /// 3   Audio       音频
//    /// 4   Video       视频
//	/// 5   PDF 
//    /// 6   MedicModel  病例模型
//    /// 7   MedicEquip  医疗器材
//    /// 8   MedicVideo  医疗视频
//    /// 9   ScreenShot  Web截图
//    /// 10  Contour     轮廓识别
//    /// 
//    /// 
//    /// 
//    /// 30 Text    文字
//    /// 31 MarkerImage   识别图
//    /// 32 ImagePck      图片压缩包
//    /// 33 ThumbImage    缩略图
//    /// 34 MartinModel   Martin数据，Android下载

//    /// </summary>
//    public int objectType;

//    //文件的地址
//    public string url;
//    //文件MD5值
//    public string md5;

//    //缩略图的地址
//    public string thumbUrl;
//    //缩略图的MD5值
//    public string thumbMd5;

//    /// <summary>
//    /// 资源大小 KB
//    /// </summary>
//    public int size;

// 	/// <summary>
//    /// 备注
//    /// objectType为6时（医疗模型）
//    /// remark代表医疗模型的类型
//    /// Skull 头骨
//    /// Spine 脊柱
//    /// Artery 血管
//    /// Femur 腿骨
//    /// </summary>
//    public string remark;

//    /// <summary>
//    /// 物体状态
//    /// 0 未下载
//    /// 1 已下载
//    /// 2 要更新
//    /// 3 待删除，不可用
//    /// </summary>
//    public int state;

//    public static implicit operator LocalDownLoadUnitVO(ServerDownLoadUnitVO inSource) {
//        LocalDownLoadUnitVO inTarget = new LocalDownLoadUnitVO();
//        inTarget.id = inSource.id;
//        inTarget.name = ToolKit.FormatExtension(inSource.name);
//        inTarget.showName = inSource.showName;
//        inTarget.srcType = inSource.srcType;
//        inTarget.objectType = inSource.objectType;
//        inTarget.url = DownLoadUtil.FormatURL(inSource.url);
//        inTarget.md5 = inSource.md5;
//        inTarget.thumbUrl = DownLoadUtil.FormatURL(inSource.thumbUrl);
//        inTarget.thumbMd5 = inSource.thumbMd5;
//        inTarget.size = inSource.size;
//        inTarget.remark = inSource.remark;

//        inTarget.saveName = DownLoadUtil.GetFileNameWithoutExtensionByURL(inTarget.url);
//        inTarget.saveNameWithEx = DownLoadUtil.GetFileNameByURL(inTarget.url);

//        return inTarget;
//    }

//    public static implicit operator ServerDownLoadUnitVO(LocalDownLoadUnitVO inSource) {
//        ServerDownLoadUnitVO inTarget = new ServerDownLoadUnitVO();
//        inTarget.id = inSource.id;
//        inTarget.name = inSource.name;
//        inTarget.showName = inSource.showName;
//        inTarget.srcType = inSource.srcType;
//        inTarget.objectType = inSource.objectType;
//        inTarget.url = inSource.url;
//        inTarget.md5 = inSource.md5;
//        inTarget.thumbUrl = inSource.thumbUrl;
//        inTarget.thumbMd5 = inSource.thumbMd5;
//        inTarget.size = inSource.size;
//        inTarget.remark = inSource.remark;
//        return inTarget;
//    }

//    //下载类型
//    public EU_LoadFileType LoadFileType {
//        get {
//            EU_LoadFileType _loadType = EU_LoadFileType.NONE;
//            if (isMarkerImage) {
//                _loadType = EU_LoadFileType.MARKER_IMAGE;
//            } else if (isImage) {
//                _loadType = EU_LoadFileType.IMAGE;
//            } else if (isThumbImage) {
//                _loadType = EU_LoadFileType.THUMB_IMAGE;
//            } else {
//                _loadType = EU_LoadFileType.NONE;
//            }
//            return _loadType;
//        }
//    }

//    //下载优先级
//    public int LoadFileTypePriority {
//        get {
//            int _priority = 0;
//            if (isMarkerImage) {
//                _priority = 1;
//            } else if (isImage) {
//                _priority = 2;
//            } else if (isThumbImage) {
//                _priority = 3;
//            } else if (isImagePck) {
//                _priority = 4;
//            } else if (isModel) {
//                _priority = 5;
//            } else if (isAudio) {
//                _priority = 6;
//            } else if (isVideo || isMedicVideo) {
//                _priority = 7;
//            } else if (isContourData) {
//                _priority = 8;
//            }
//            return _priority;
//        }
//    }

//    //模型
//    public bool isModel
//    {
//		get
//         {   // 修改 
//            // 普通模型
//            // 病历模型
//            // 医疗器械
//            return objectType == 1 || objectType == 6 || objectType == 7;
//        }
//    }
//    //图片
//    public bool isImage
//    {
//        get
//        {
//            return objectType == 2;
//        }
//    }
//    //音频
//    public bool isAudio
//    {
//        get
//        {
//            return objectType == 3;
//        }
//    }
//    //视频
//    public bool isVideo
//    {
//        get
//        {
//            return objectType == 4;
//        }
//    }
// 	//PDF 5
//    public bool isPDF
//    {
//        get
//        {
//            return objectType == 5;
//        }
//    }
//    //病例模型 6 
//    public bool isMedicModel
//    {
//        get
//        {
//            return objectType == 6;
//        }
//    }
//    //医疗器材 7 
//    public bool isMedicEquip
//    {
//        get
//        {
//            return objectType == 7;
//        }
//    }
//    //医疗影像 8
//    public bool isMedicVideo
//    {
//        get
//        {
//            return objectType == 8;
//        }
//    }
//    //轮廓数据 10
//    public bool isContourData
//    {
//        get
//        {
//            return objectType == 10;
//        }
//    }
    
//    //识别图
//    public bool isMarkerImage
//    {
//        get
//        {
//            return objectType == 31;
//        }
//    }
//    //文字
//    public bool isText
//    {
//        get
//        {
//            return objectType == 30;
//        }
//    }
//    //图片压缩包
//    public bool isImagePck
//    {
//        get
//        {
//            return objectType == 32;
//        }
//    }
//    //缩略图
//    public bool isThumbImage
//    {
//        get
//        {
//            return objectType == 33;
//        }
//    }

//    //Martin数据集
//    public bool isMartinMoudle {
//        get {
//            return objectType == 34;
//        }
//    }
    
//    /// <summary>
//    /// 如果是模型则为解压后文件夹名字
//    /// 如果不是模型则是下载后文件的名字， 不带后缀
//    /// </summary>
//    private string saveName;
//    public string SaveName
//    {
//        get {
//            return saveName;
//        }
//    }

//    //文件保存名字,有后缀
//    private string saveNameWithEx;
//    public string SaveNameWithEx
//    {
//        get
//        {
//            return saveNameWithEx;
//        }
//    }

//    public LocalDownLoadUnitVO ThumbVO {
//        get {
//            if (string.IsNullOrEmpty(thumbUrl))
//            {
//                return new LocalDownLoadUnitVO();
//            }
//            else { 
//                LocalDownLoadUnitVO _result = new LocalDownLoadUnitVO();
//                _result.id = this.thumbUrl;
//                _result.name = "ThumbImage";
//                _result.srcType = 2;
//                _result.objectType = 33;
//                _result.url = DownLoadUtil.FormatURL(this.thumbUrl);//?x-oss-process=image/resize,w_100,h_100,limit_0
//                _result.md5 = this.thumbMd5;
//                return _result;
//            }
//        }
//    }

//    public LocalDownLoadUnitVO Copy() {
//        LocalDownLoadUnitVO inTarget = new LocalDownLoadUnitVO();
//        inTarget.id = this.id;
//        inTarget.name = this.name;
//        inTarget.showName = this.showName;
//        inTarget.srcType = this.srcType;
//        inTarget.objectType = this.objectType;
//        inTarget.url = this.url;
//        inTarget.md5 = this.md5;
//        inTarget.thumbUrl = this.thumbUrl;
//        inTarget.thumbMd5 = this.thumbMd5;
//        inTarget.size = this.size;
//        inTarget.remark = this.remark;

//        inTarget.saveName = this.saveName;
//        inTarget.saveNameWithEx = this.saveNameWithEx;

//        return inTarget;
//    }
//}

//#endregion

//#region 用户数据

//public class LocalUserData : System.Object{
//    //用户id
//    public long uid;
//    //公司id
//    public long companyId;
//    //token
//    public string token;
//    //服务器地址
//    public string serverUrl;
//    /// <summary>
//    /// 语言
//    /// 1 中文
//    /// 2 英文
//    /// 3 其他
//    /// </summary>
//    public int language;
//    /// <summary>
//    /// 按键音状态
//    /// 0 关闭
//    /// 1 开启
//    /// </summary>
//    public int musicFlag;
//    /// <summary>
//    /// 提示音声音类型
//    /// 0 男
//    /// 1 女
//    /// 2 关闭
//    /// </summary>
//    public int soundSex;
//}

////多屏互动消息
//public class LocalMultiInteractVO : System.Object {

//    /// <summary>
//    /// 发起共享的用户id
//    /// </summary>
//    public long userId;
    
//    /// <summary>
//    /// 只有空间识别可以多屏互动
//    /// 识别类型
//    /// 0 无
//    /// 1 图片识别
//    /// 2 空间识别
//    /// 3 轮廓识别
//    /// 4 Martin
//    /// </summary>
//    public int markType = 2;

//    /// <summary>
//    /// 要共享的工程id
//    /// </summary>
//    public string projectId;

//    /// <summary>
//    /// 消息类型
//    /// 0 空
//    /// 1 开始 
//    /// 2 共享
//    /// 3 结束
//    /// </summary>
//    public int msgType;

//    /// <summary>
//    /// 要共享的page lineNo
//    /// </summary>
//    public int lineNo;


//}

////场景信息
//public class LocalSceneDataVO : System.Object{
//    public string sceneType;
//    public string projectId;
//}
//#endregion

