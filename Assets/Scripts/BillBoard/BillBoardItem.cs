using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillBoardItem : MonoBehaviour {

    public CanvasGroup canvasGroup;

    public Text uiText;
    public Transform rearrangeTransform;
    public Transform cacheTransform;

    private float locaPositonY = 60f;
    
    private int count;
    private float delayRange = 0.5f;
    private float duration = 0.5f;

    void Start()
    {
        if (cacheTransform==null)
        {
            cacheTransform = this.transform;
        }
        if (rearrangeTransform==null)
        {
            rearrangeTransform = cacheTransform.GetChild(0);
        }
        canvasGroup.alpha = 0f;
        rearrangeTransform.localPosition = new Vector3(0f,locaPositonY,0f);
        gameObject.SetActive(true);
        //gameObject.SetActive(false);
    }

    //Billboard animation
    public void TweenAnim(bool forward)
    {
        float delay = Random.Range(0f,delayRange);
        if (forward)
        {
            gameObject.SetActive(true);
            canvasGroup.DOFade(1f, duration).SetDelay(delay);
            rearrangeTransform.DOLocalMoveY(0f,duration).SetDelay(delay);
        }
        else
        {
            canvasGroup.DOFade(0f, duration).SetDelay(delay).OnComplete(()=> { gameObject.SetActive(false); });
            rearrangeTransform.DOLocalMoveY(locaPositonY, duration).SetDelay(delay);
        }
    }

    public void SetPosition(Vector3 localpositon)
    {
        localpositon=new Vector3(1920 / Screen.width * localpositon.x, 1080 / Screen.height * localpositon.y, localpositon.z);
        cacheTransform.GetComponent<RectTransform>().anchoredPosition3D = localpositon;
    }

}
