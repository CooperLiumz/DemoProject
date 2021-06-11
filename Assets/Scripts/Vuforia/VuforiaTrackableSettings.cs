//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.IO;
//using Vuforia;

///// <summary>  
///// 追踪功能设置类  
///// </summary>  
//public class VuforiaTrackableSettings : MonoBehaviour
//{
//    public const string Name = "VuforiaTrackableSettings";
//    static VuforiaTrackableSettings () { }
//    protected VuforiaTrackableSettings () { }
//    protected static volatile VuforiaTrackableSettings mInstance = null;
//    protected static readonly object mStaticSyncRoot = new object ();
//    public static VuforiaTrackableSettings Instance
//    {
//        get
//        {
//            if (mInstance == null)
//            {
//                lock (mStaticSyncRoot)
//                {
//                    if (mInstance == null)
//                    {
//                        GameObject singleton = GameObject.Find ( "_Singleton" );
//                        if (singleton == null)
//                        {
//                            singleton = new GameObject ( "_Singleton" );
//                        }
//                        mInstance = singleton.AddComponent<VuforiaTrackableSettings> ();
//                    }
//                }
//            }
//            return mInstance;
//        }
//    }

//    /// <summary>  
//    /// 已经激活的识别数据集  
//    /// </summary>  
//    protected Dictionary<string, string> mCurrentActiveDataSets = new Dictionary<string, string> ();
//    /// <summary>  
//    /// 所有数据集对应的路径  
//    /// </summary>  
//    protected Dictionary<string, string> mCurrentAllDataSets = new Dictionary<string, string> ();
//    /// <summary>  
//    /// 是否使用本地内置识别数据集 
//    /// </summary>  
//    public bool bLocal = false;
//    /// <summary>  
//    /// 扩展追踪是否开启  
//    /// </summary>  
//    protected bool mExtTrackingEnabled = false;

//    /// <summary>  
//    /// 获得扩展追踪开启状态  
//    /// </summary>  
//    /// <returns></returns>  
//    public bool IsExtendedTrackingEnabled ()
//    {
//        return mExtTrackingEnabled;
//    }

//    /// <summary>  
//    /// 开启、关闭扩展追踪模式  
//    /// </summary>  
//    /// <param name="extTrackingEnabled">是否开启</param>  
//    public virtual void SwitchExtendedTracking ( bool extTrackingEnabled )
//    {
//        ////返回可以访问当前所有跟踪的状态管理器实例。  
//        //StateManager stateManager = TrackerManager.Instance.GetStateManager ();

//        //// 我们遍历所有trackablebehaviours启动或停止扩展跟踪他们所代表的目标。  
//        //bool success = true;
//        //foreach (var tb in stateManager.GetTrackableBehaviours ())
//        //{
//        //    if (tb is ImageTargetBehaviour)
//        //    {
//        //        ImageTargetBehaviour itb = tb as ImageTargetBehaviour;
//        //        if (extTrackingEnabled)
//        //        {
//        //            if (!itb.ImageTarget.StartExtendedTracking ())
//        //            {
//        //                success = false;
//        //                Debug.LogError ( "扩展跟踪开始失败，对应目标： " + itb.TrackableName );
//        //            }
//        //        }
//        //        else
//        //        {
//        //            itb.ImageTarget.StopExtendedTracking ();
//        //        }
//        //    }
//        //    else if (tb is MultiTargetBehaviour)
//        //    {
//        //        MultiTargetBehaviour mtb = tb as MultiTargetBehaviour;
//        //        if (extTrackingEnabled)
//        //        {
//        //            if (!mtb.MultiTarget.StartExtendedTracking ())
//        //            {
//        //                success = false;
//        //                Debug.LogError ( "Failed to start Extended Tracking on Target " + mtb.TrackableName );
//        //            }
//        //        }
//        //        else
//        //        {
//        //            mtb.MultiTarget.StopExtendedTracking ();
//        //        }
//        //    }
//        //    else if (tb is CylinderTargetBehaviour)
//        //    {
//        //        CylinderTargetBehaviour ctb = tb as CylinderTargetBehaviour;
//        //        if (extTrackingEnabled)
//        //        {
//        //            if (!ctb.CylinderTarget.StartExtendedTracking ())
//        //            {
//        //                success = false;
//        //                Debug.LogError ( "Failed to start Extended Tracking on Target " + ctb.TrackableName );
//        //            }
//        //        }
//        //        else
//        //        {
//        //            ctb.CylinderTarget.StopExtendedTracking ();
//        //        }
//        //    }
//        //    else if (tb is ObjectTargetBehaviour)
//        //    {
//        //        ObjectTargetBehaviour otb = tb as ObjectTargetBehaviour;
//        //        if (extTrackingEnabled)
//        //        {
//        //            if (!otb.ObjectTarget.StartExtendedTracking ())
//        //            {
//        //                success = false;
//        //                Debug.LogError ( "Failed to start Extended Tracking on Target " + otb.TrackableName );
//        //            }
//        //        }
//        //        else
//        //        {
//        //            otb.ObjectTarget.StopExtendedTracking ();
//        //        }
//        //    }
//        //    else if (tb is VuMarkBehaviour)
//        //    {
//        //        VuMarkBehaviour vmb = tb as VuMarkBehaviour;
//        //        if (extTrackingEnabled)
//        //        {
//        //            if (!vmb.VuMarkTemplate.StartExtendedTracking ())
//        //            {
//        //                success = false;
//        //                Debug.LogError ( "Failed to start Extended Tracking on Target " + vmb.TrackableName );
//        //            }
//        //        }
//        //        else
//        //        {
//        //            vmb.VuMarkTemplate.StopExtendedTracking ();
//        //        }
//        //    }
//        //}
//        //mExtTrackingEnabled = success && extTrackingEnabled;
//    }
//    /// <summary>  
//    /// 获取当前激活识别数据集名称列表  
//    /// </summary>  
//    /// <returns></returns>  
//    public List<string> GetActiveDatasetNameList ()
//    {
//        ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
//        List<DataSet> activeDataSets = tracker.GetActiveDataSets ().ToList ();
//        List<string> tempList = new List<string> ();
//        for (int i = 0; i < activeDataSets.Count; i++)
//        {
//            string datasetPath = activeDataSets.ElementAt ( i ).Path;
//            Debug.Log ( "datasetPath:" + datasetPath );
//            string datasetName = datasetPath.Substring ( datasetPath.LastIndexOf ( "/" ) + 1 );
//            Debug.Log ( "datasetName:" + datasetName );
//            tempList.Add ( datasetName.TrimEnd ( ".xml".ToCharArray () ) );
//        }
//        return tempList;
//    }
//    /// <summary>  
//    /// 激活指定的识别数据集  
//    /// </summary>  
//    /// <param name="datasetName">数据集名称或绝对路径</param>  
//    public virtual void ActivateDataSet ( string datasetName )
//    {
//        if (mCurrentActiveDataSets.ContainsKey ( datasetName ))
//        {
//            Debug.Log ( string.Format ( "要激活的识别库:{0}已经激活", datasetName ) );
//            return;
//        }

//        // objecttracker跟踪包含在数据集，提供了用于创建和方法的激活数据。  
//        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
//        IEnumerable<DataSet> datasets = objectTracker.GetDataSets ();

//        IEnumerable<DataSet> activeDataSets = objectTracker.GetActiveDataSets ();
//        List<DataSet> activeDataSetsToBeRemoved = activeDataSets.ToList ();

//        // 1. 循环遍历所有的活动数据集并禁用它们  
//        foreach (DataSet ads in activeDataSetsToBeRemoved)
//        {
//            objectTracker.DeactivateDataSet ( ads );

//            string _datasetPath = ads.Path;
//            string _datasetName = _datasetPath.Substring ( _datasetPath.LastIndexOf ( "/" ) + 1 );
//            _datasetName = _datasetName.TrimEnd ( ".xml".ToCharArray () );

//            mCurrentActiveDataSets.Remove ( _datasetName );
//            Debug.Log ( "禁用  " + _datasetName );
//        }

//        // 在ObjectTracker运行时，不应该对数据集进行交换  
//        // 2. 所以首先要关闭tracker  
//        objectTracker.Stop ();

//        // 3. 然后，查找新数据集，如果存在，激活它  
//        foreach (DataSet ds in datasets)
//        {
//            if (ds.Path.Contains ( datasetName ))
//            {
//                objectTracker.ActivateDataSet ( ds );
//                if (mCurrentActiveDataSets.ContainsKey ( datasetName ))
//                {
//                    mCurrentActiveDataSets.Remove ( datasetName );
//                }
//                mCurrentActiveDataSets.Add ( datasetName, ds.Path );

//                Debug.Log ( string.Format ( "{0}已经激活", datasetName ) );
//            }
//        }

//        CameraDevice.Instance.SetFocusMode ( CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO );

//        CameraDevice.Instance.SetFocusMode ( CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO );

//        // 4. 最后重启traker.  
//        objectTracker.Start ();
//    }

//    /// <summary>  
//    /// 关闭指定识别数据集  
//    /// </summary>  
//    /// <param name="datasetName">数据集名称或绝对路径</param>  
//    public virtual void DeactivateDateset ( string datasetName )
//    {
//        if (!mCurrentActiveDataSets.ContainsKey ( datasetName ))
//        {
//            Debug.Log ( string.Format ( "要关闭的识别数据集:{0}不存在", datasetName ) );
//            return;
//        }

//        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
//        IEnumerable<DataSet> datasets = objectTracker.GetDataSets ();

//        IEnumerable<DataSet> activeDataSets = objectTracker.GetActiveDataSets ();

//        List<DataSet> activeDataSetsToBeRemoved = activeDataSets.ToList ();

//        List<DataSet> dataSetsToBeActive = new List<DataSet> ();

//        foreach (DataSet ads in activeDataSetsToBeRemoved)
//        {
//            if (!ads.Path.Contains ( datasetName ))
//            {
//                dataSetsToBeActive.Add ( ads );
//            }
//            objectTracker.DeactivateDataSet ( ads );
//        }

//        objectTracker.Stop ();

//        foreach (DataSet ds in dataSetsToBeActive)
//        {
//            objectTracker.ActivateDataSet ( ds );
//        }
//        mCurrentActiveDataSets.Remove ( datasetName );

//        objectTracker.Start ();
//    }
//    /// <summary>  
//    /// 载入识别数据集  
//    /// </summary>  
//    /// <param name="DataSetName">数据集名称</param>  
//    /// <param name="Local">是否是本地识别数据集</param>  
//    public virtual void LoadDataSet ( string path, string DataSetName, bool Local = false )
//    {
//        if (mCurrentAllDataSets.ContainsKey ( DataSetName ))
//        {
//            Debug.Log ( "已加载识别数据集" );
//            return;
//        }

//        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
//        objectTracker.Stop ();
//        bool bLoadDataSet = false;
//        DataSet mDataSet = null;
//        if (VuforiaRuntimeUtilities.IsVuforiaEnabled ())
//        {
//            objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
//            mDataSet = objectTracker.CreateDataSet ();
//            if (!Local)
//            {
//                bLoadDataSet = mDataSet.Load ( path + DataSetName + ".xml", VuforiaUnity.StorageType.STORAGE_ABSOLUTE );//绝对路径 一般用来加载网络下载的识别库（dat和xml文件）    
//            }
//            else
//            {
//                bLoadDataSet = mDataSet.Load ( DataSetName );//本地预制的识别数据集    
//            }
//        }

//        if (bLoadDataSet)
//        {
//            mCurrentAllDataSets.Add ( DataSetName, mDataSet.Path );
//        }
//        else
//        {
//            Debug.Log ( "加载识别数据集失败" );
//        }

//        CameraDevice.Instance.SetFocusMode ( CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO );

//        CameraDevice.Instance.SetFocusMode ( CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO );

//        objectTracker.Start ();//开启识别器 （可以理解为摄像头）    
//    }
//    /// <summary>  
//    /// 卸载所有识别数据集（识别库）  
//    /// </summary>  
//    public virtual void UnLoadAllDataSet ()
//    {
//        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
//        bool isVuforiaEnabled = VuforiaRuntimeUtilities.IsVuforiaEnabled ();

//        if (isVuforiaEnabled)
//        {
//            //1. 关闭跟踪器  
//            objectTracker.Stop ();
//            //获取对应数据集合集（和激活数据集写法略有不同）  
//            IEnumerable<DataSet> dataSets = objectTracker.GetActiveDataSets ();
//            IEnumerator<DataSet> dEnumerator = dataSets.GetEnumerator ();
//            List<DataSet> listDataSet = new List<DataSet> ();

//            while (dEnumerator.MoveNext ())
//            {
//                listDataSet.Add ( dEnumerator.Current );
//            }
//            //关闭每一个数据集  
//            for (int i = 0; i < listDataSet.Count; i++)
//            {
//                Debug.Log ( "关闭对应数据集：" + listDataSet[i].Path );
//                objectTracker.DeactivateDataSet ( listDataSet[i] );
//            }

//            for (int i = 0; i < listDataSet.Count; i++)
//            {
//                Debug.Log ( "关闭对应数据集：" + listDataSet[i].Path );
//                objectTracker.DestroyDataSet ( listDataSet[i], false );
//            }

//            //管理所有可跟踪行为的状态  
//            StateManager stateManager = TrackerManager.Instance.GetStateManager ();
//            //ImageTargetBehaviour[] ImageTargetBehaviours = GameObject.FindObjectsOfType<ImageTargetBehaviour>();  
//            IEnumerable<TrackableBehaviour> IETrackableBehaviours = stateManager.GetTrackableBehaviours ();
//            //销毁对应创建的ImageTarget跟踪具体行为（就是在imagetarget上设置的参数）和销毁对应的GameObject  
//            foreach (var tb in IETrackableBehaviours)
//            {
//                stateManager.DestroyTrackableBehavioursForTrackable ( tb.Trackable, true );
//            }
//            objectTracker.DestroyAllDataSets ( false );
//            Debug.Log ( "销毁识别数据成功" );

//            mCurrentActiveDataSets.Clear ();
//            mCurrentAllDataSets.Clear ();
//        }
//        else
//        {
//            Debug.Log ( "销毁数据失败" );
//        }
//    }
//    /// <summary>  
//    /// 更新ImageTarget上的信息  
//    /// </summary>  
//    public virtual ImageTargetBehaviour[] UpdateImageTarget ()
//    {
//        ImageTargetBehaviour[] m_ImageTargetBehaviours = GameObject.FindObjectsOfType<ImageTargetBehaviour> ();
//        for (int i = 0; i < m_ImageTargetBehaviours.Length; i++)
//        {
//            ImageTargetBehaviour imageTargetBehaviour = m_ImageTargetBehaviours[i];
//            imageTargetBehaviour.name = m_ImageTargetBehaviours[i].ImageTarget.Name + "Target";
//            imageTargetBehaviour.transform.localScale = Vector3.one;
//            //imageTargetBehaviour.gameObject.AddComponent<DefaultTrackableEventHandler>();
//            //imageTargetBehaviour.gameObject.AddComponent<TurnOffBehaviour>();
//        }
//        return m_ImageTargetBehaviours;
//    }
//    /// <summary>  
//    /// 更新ModelTarget上的信息  
//    /// </summary>  
//    public virtual ModelTargetBehaviour[] UpdateModelTarget ()
//    {
//        ModelTargetBehaviour[] m_ModelTargetBehaviours = GameObject.FindObjectsOfType<ModelTargetBehaviour> ();
//        for (int i = 0; i < m_ModelTargetBehaviours.Length; i++)
//        {
//            ModelTargetBehaviour modelTargetBehaviour = m_ModelTargetBehaviours[i];
//            modelTargetBehaviour.name = m_ModelTargetBehaviours[i].ModelTarget.Name + "Target";
//            modelTargetBehaviour.transform.localScale = Vector3.one;
//        }
//        return m_ModelTargetBehaviours;
//    }

//    /// <summary>  
//    /// 设置同时识别Image的个数  
//    /// </summary>  
//    /// <param name="TrackedCount"></param>  
//    public virtual void SetTrackedImageCount ( int TrackedCount )
//    {
//        VuforiaUnity.SetHint ( VuforiaUnity.VuforiaHint.HINT_MAX_SIMULTANEOUS_IMAGE_TARGETS, TrackedCount );
//    }
//    /// <summary>  
//    /// 设置同时识别3D物体的个数  
//    /// </summary>  
//    /// <param name="TrackedCount"></param>  
//    public virtual void SetTrackedObjectCount ( int TrackedCount )
//    {
//        VuforiaUnity.SetHint ( VuforiaUnity.VuforiaHint.HINT_MAX_SIMULTANEOUS_OBJECT_TARGETS, TrackedCount );
//    }
//}