using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AutoGaze : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [SerializeField]
    private float autoClickTime = 0;//是否需要长注视
    private float curDelayTime;//当前执行时间
    private bool inAutoClick = false;//是否正在进行凝视

    // Update is called once per frame
    void Update ()
    {
        autoClick ();
    }

    private void autoClick ()
    {
        if (inAutoClick)
        {
            Debug.LogError ("curDelayTime  " + curDelayTime);
            curDelayTime = curDelayTime - Time.deltaTime;
            curDelayTime = curDelayTime < 0 ? 0 : curDelayTime;
            Debug.LogError (curDelayTime);
            if (curDelayTime <= 0)
            {
                inAutoClick = false;
                curDelayTime = autoClickTime;

                PointerEventData pointerEventData = new PointerEventData (EventSystem.current);
                ExecuteEvents.Execute (gameObject , pointerEventData , ExecuteEvents.pointerClickHandler);
            }
        }
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        Debug.LogError ("OnPointerEnter");
        if (autoClickTime > 0)
        {
            //Debug.LogError ("OnPointerEnter");
            inAutoClick = true;
            curDelayTime = autoClickTime;
        }
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        Debug.LogError ("OnPointerExit");
        if (autoClickTime > 0)
        {
            //Debug.LogError ("OnPointerExit");
            inAutoClick = false;
            curDelayTime = autoClickTime;
        }
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        Debug.LogError ("OnPointerClick");
        //Debug.LogError ("OnPointerClick");
        inAutoClick = false;
        curDelayTime = autoClickTime;
    }
}

