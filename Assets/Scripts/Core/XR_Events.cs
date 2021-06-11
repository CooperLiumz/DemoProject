using UnityEngine;
using UnityEngine.Events;
using VRDrive.Controller;

//XR_Events.Initializing.Listen (OnInitializing);
//XR_Events.Initializing.Remove (OnInitializing);
//XR_Events.Initializing.Send (true);

//InitializingAction initializingAction = XR_Events.InitializingAction(OnInitializing);

public static class XR_Events
{
    public abstract class Action
    {
        public abstract void Enable (bool enabled);
        public bool enabled
        {
            set
            {
                Enable (value);
            }
        }
    }

    [System.Serializable]
    public class ActionNoArgs : Action
    {
        public ActionNoArgs (Event _event , UnityAction action)
        {
            this._event = _event;
            this.action = action;
        }

        public override void Enable (bool enabled)
        {
            if (enabled)
                _event.Listen (action);
            else
                _event.Remove (action);
        }

        Event _event;
        UnityAction action;
    }

    [System.Serializable]
    public class Action<T> : Action
    {
        public Action (Event<T> _event , UnityAction<T> action)
        {
            this._event = _event;
            this.action = action;
        }

        public override void Enable (bool enabled)
        {
            if (enabled)
                _event.Listen (action);
            else
                _event.Remove (action);
        }

        Event<T> _event;
        UnityAction<T> action;
    }

    [System.Serializable]
    public class Action<T0, T1> : Action
    {
        public Action (Event<T0 , T1> _event , UnityAction<T0 , T1> action)
        {
            this._event = _event;
            this.action = action;
        }

        public override void Enable (bool enabled)
        {
            if (enabled)
                _event.Listen (action);
            else
                _event.Remove (action);
        }

        Event<T0 , T1> _event;
        UnityAction<T0 , T1> action;
    }

    [System.Serializable]
    public class Action<T0, T1, T2> : Action
    {
        public Action (Event<T0 , T1 , T2> _event , UnityAction<T0 , T1 , T2> action)
        {
            this._event = _event;
            this.action = action;
        }

        public override void Enable (bool enabled)
        {
            if (enabled)
                _event.Listen (action);
            else
                _event.Remove (action);
        }

        Event<T0 , T1 , T2> _event;
        UnityAction<T0 , T1 , T2> action;
    }

    public class Event : UnityEvent
    {
        public void Listen (UnityAction action)
        {
            this.AddListener (action);
        }
        public void Remove (UnityAction action)
        {
            this.RemoveListener (action);
        }
        public void Send ()
        {
            this.Invoke ();
        }
    }

    public class Event<T> : UnityEvent<T>
    {
        public void Listen (UnityAction<T> action)
        {
            this.AddListener (action);
        }
        public void Remove (UnityAction<T> action)
        {
            this.RemoveListener (action);
        }
        public void Send (T arg0)
        {
            this.Invoke (arg0);
        }
    }

    public class Event<T0, T1> : UnityEvent<T0 , T1>
    {
        public void Listen (UnityAction<T0 , T1> action)
        {
            this.AddListener (action);
        }
        public void Remove (UnityAction<T0 , T1> action)
        {
            this.RemoveListener (action);
        }
        public void Send (T0 arg0 , T1 arg1)
        {
            this.Invoke (arg0 , arg1);
        }
    }

    public class Event<T0, T1, T2> : UnityEvent<T0 , T1 , T2>
    {
        public void Listen (UnityAction<T0 , T1 , T2> action)
        {
            this.AddListener (action);
        }
        public void Remove (UnityAction<T0 , T1 , T2> action)
        {
            this.RemoveListener (action);
        }
        public void Send (T0 arg0 , T1 arg1 , T2 arg2)
        {
            this.Invoke (arg0 , arg1 , arg2);
        }
    }

    public static Event<bool> Calibrating = new Event<bool> ();
    public static Action CalibratingAction (UnityAction<bool> action)
    {
        return new Action<bool> (Calibrating , action);
    }

    public static Event<int , bool> DeviceConnected = new Event<int , bool> ();
    public static Action DeviceConnectedAction (UnityAction<int , bool> action)
    {
        return new Action<int , bool> (DeviceConnected , action);
    }

    public static Event<Color , float , bool> Fade = new Event<Color , float , bool> ();
    public static Action FadeAction (UnityAction<Color , float , bool> action)
    {
        return new Action<Color , float , bool> (Fade , action);
    }

    public static Event FadeReady = new Event ();
    public static Action FadeReadyAction (UnityAction action)
    {
        return new ActionNoArgs (FadeReady , action);
    }

    public static Event<bool> Initializing = new Event<bool> ();
    public static Action InitializingAction (UnityAction<bool> action)
    {
        return new Action<bool> (Initializing , action);
    }

}