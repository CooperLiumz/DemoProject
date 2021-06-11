using UnityEngine;
using System.Collections;

using System;

public class AnimationEventTarget : MonoBehaviour
{

    public Action FinishCallback;

    public void AnimationFinishedEvent(AnimationEvent ae)
    {
        if (null != FinishCallback)
        {
            FinishCallback();
        }
    }
}
