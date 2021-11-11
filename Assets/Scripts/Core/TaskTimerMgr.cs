using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void TaskTimerHandler();

public delegate void TaskTimerArgsHandler(System.Object[] args);

public class TaskTimer
{
    public TaskTimerHandler Handler;           //无参的委托
    public TaskTimerArgsHandler ArgsHandler;   //带参数的委托
    public float Frequency;               //时间间隔
    public int Repeats;                   //重复次数
    public System.Object[] Args;

    public float LastTickTime;

    public event Action OnComplete;        //计时器完成一次工作
    public event Action OnDestroy;        //计时器被销毁

    public TaskTimer() { }

    /// <summary>
    /// 创建一个时间事件对象
    /// </summary>
    /// <param name="Handler">回调函数</param>
    /// <param name="ArgsHandler">带参数的回调函数</param>
    /// <param name="frequency">时间内执行</param>
    /// <param name="repeats">重复次数</param>
    /// <param name="Args">参数  可以任意的传不定数量，类型的参数</param>
    public TaskTimer(TaskTimerHandler Handler, TaskTimerArgsHandler ArgsHandler, float frequency, int repeats, System.Object[] Args)
    {
        this.Handler = Handler;
        this.ArgsHandler = ArgsHandler;
        this.Frequency = frequency;
        this.Repeats = repeats == 0 ? 1 : repeats;
        this.Args = Args;
        this.LastTickTime = Time.time;
    }

    public void Notify()
    {
        if (Handler != null)
            Handler();
        if (ArgsHandler != null)
            ArgsHandler(Args);
        if (OnComplete != null)
        {
            OnComplete();
        }
    }

    /// <summary>
    /// 清理计时器，初始化参数  同时清理事件
    /// </summary>
    public void CleanUp()
    {
        Handler = null;
        ArgsHandler = null;
        Repeats = 1;
        Frequency = 0;
        if (OnDestroy != null)
        {
            OnDestroy();
        }
        OnDestroy = null;
        OnComplete = null;
    }
}

/// <summary>
/// 计时器
/// 添加一个计时事件
/// 删除一个计时事件
/// 更新计时事件
/// </summary>
public class TaskTimerMgr : Singleton<TaskTimerMgr>
{
    private List<TaskTimer> _Timers;//时间管理器
    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);
        if (_Timers == null)
        {
            _Timers = new List<TaskTimer>();
        }
        Application.runInBackground = true;
    }
    /// <summary>
    /// 创建一个简单的计时器
    /// </summary>
    /// <param name="callBack">回调函数</param>
    /// <param name="time">计时器时间</param>
    /// <param name="repeats">回调次数  小于0代表循环 大于0代表repeats次</param>
    public TaskTimer CreateTimer(TaskTimerHandler callBack, float time, int repeats = 1)
    {
        return Create(callBack, null, time, repeats);
    }

    public TaskTimer CreateTimerWithArgs(TaskTimerArgsHandler callBack, float time, int repeats, params System.Object[] args)
    {
        return Create(null, callBack, time, repeats, args);
    }

    private TaskTimer Create(TaskTimerHandler callBack, TaskTimerArgsHandler callBackArgs, float time, int repeats, params System.Object[] args)
    {
        TaskTimer timer = new TaskTimer(callBack, callBackArgs, time, repeats, args);
        _Timers.Add(timer);
        return timer;
    }

    public TaskTimer DestroyTimer(TaskTimer timer)
    {
        if (timer != null)
        {
            _Timers.Remove(timer);
            timer.CleanUp();
            timer = null;
        }
        return timer;
    }
    public void ClearAll()
    {
        if (_Timers != null)
        {
            for (int i = 0; i < _Timers.Count; i++)
            {
                _Timers[i].CleanUp();
            }
            _Timers.Clear();
        }
    }
    /// <summary>
    /// 固定更新检查更新的频率
    /// </summary>
    void Update()
    {
        if (_Timers!=null&&_Timers.Count != 0)
        {
            for (int i = _Timers.Count - 1; i >= 0; i--)
            {
                if (i > _Timers.Count - 1) {
                    return;
                }
                TaskTimer timer = _Timers[i];
                float curTime = Time.time;
                if (timer.Frequency + timer.LastTickTime > curTime)
                {
                    continue;
                }
                timer.LastTickTime = curTime;
                if (timer.Repeats-- == 0)
                {//计时完成，可以删除了
                    DestroyTimer(timer);
                }
                else
                {//触发计时
                    timer.Notify();
                }
            }
        }
    }

    protected override void OnDestroy ()
    {
        base.OnDestroy ();
        ClearAll ();
    }
}
