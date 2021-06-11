using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour {

    //prefab of BillBoard
    public GameObject itemObject;

    private Transform cacheTransform;
    private Transform[] targetTfs;
    private BillBoardItem[] billBoardItems;
    private int count;
    private bool showBillBoard = false;

    void Awake()
    {
        cacheTransform = this.transform;

        EventCenter.BillBoardEvent.ShowBillBoardEvent += ShowBillBoard;
        EventCenter.BillBoardEvent.SetBillBoardTargetEvent += SetBillBoardTarget;
    }

    //Instantiate the prefab of BillBoard
    private void SetBillBoardTarget(Transform[] targetTfs)
    {
        this.targetTfs = targetTfs;
        count = targetTfs.Length;
        billBoardItems = new BillBoardItem[count];
        GameObject tempGo;
        for (int i = 0; i < count; i++)
        {
            tempGo = Instantiate(itemObject);
            billBoardItems[i] = tempGo.GetComponent<BillBoardItem>();
            billBoardItems[i].uiText.text = targetTfs[i].gameObject.name.Replace("Billboard_", "");
            billBoardItems[i].transform.SetParent(cacheTransform);
            billBoardItems[i].transform.localScale = Vector3.one;
        }
    }


    //show BillBoard
    private void ShowBillBoard(bool show)
    {
        showBillBoard = show;
        for (int i = 0; i < count; i++)
        {
            billBoardItems[i].TweenAnim(show);
        }
    }

    private void SetPosition()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 tempV3 = Global.Instance.mainCamera.WorldToScreenPoint(targetTfs[i].position);

            //tempV3.x *= ScreenResize.Instance.activeWidth;
            //tempV3.y *= ScreenResize.Instance.activeHeight;
            //tempV3.z = 0f;

            billBoardItems[i].SetPosition(tempV3);
        }
    }

    void Update()
    {
        if (showBillBoard)
        {
            SetPosition();
        }
    }

    void OnDestroy()
    {
        EventCenter.BillBoardEvent.ShowBillBoardEvent += ShowBillBoard;
        EventCenter.BillBoardEvent.SetBillBoardTargetEvent += SetBillBoardTarget;
    }
}
