using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBillBoard : MonoBehaviour
{
    public CanvasScaler mCanvasScaler;

    void Awake ()
    {
        Debug.Log ( " Screen width  " + Screen.width + "  Screen  hight  " + Screen.height );
        //mCanvasScaler.referenceResolution = new Vector2 ( Screen.width , Screen.height );
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke ( "ShowBillBoard", 1); 
    }

    void ShowBillBoard ()
    {
        EventCenter.BillBoardEvent.RaiseShowBillBoard ( true );
    }
}
