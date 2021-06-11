using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GestureRotateControl : GestureControl {

    public int rotateDelta = 80;

    protected override void InputCheck()
    {
        base.InputCheck();
        #region 单点触发旋转
        if (Input.touchCount == 1) {
            //移动
            if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                status = 1;
                try {
                    StartCoroutine(CustomOnMouseDown());
                } catch (Exception e) {
                    Debug.Log(e.ToString());
                }
            }
        }
        #endregion

        #region   键盘A、D、W、S模拟旋转（真实模型旋转）  
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(Vector3.up, rotateDelta * Time.deltaTime, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(Vector3.up, -rotateDelta * Time.deltaTime, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Rotate(Vector3.left, rotateDelta * Time.deltaTime, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Rotate(Vector3.left, -rotateDelta * Time.deltaTime, Space.World);
        }
        #endregion

    }

    IEnumerator CustomOnMouseDown() {
        yield return null;
        //检测到一直触摸时，会一直运行
        while (Input.GetMouseButton(0)) {
            //判断是否点击在UI上
#if UNITY_ANDROID || UNITY_IPHONE
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            if(EventSystem.current.IsPointerOverGameObject())
#endif
            {
                Debug.Log("当前点击在UI上");
            }
            else
            {
                float XX = Input.GetAxis("Mouse X");
                float YY = Input.GetAxis("Mouse Y");

                Debug.Log("XX ==> " + XX);
                Debug.Log("YY ==> " + YY);

                #region 
                //判断左右滑动距离与上下滑动距离的大小
                if (Mathf.Abs(XX) >= Mathf.Abs(YY))
                {
                    //向左
                    if (XX < 0)
                    {
                        transform.Rotate(Vector3.up, rotateDelta * Time.deltaTime, Space.World);
                    }
                    //向右
                    if (XX > 0)
                    {
                        transform.Rotate(-Vector3.up, rotateDelta * Time.deltaTime, Space.World);
                    }
                }
                else {
                    //向下
                    if (YY < 0) {
                        transform.Rotate(Vector3.left, rotateDelta * Time.deltaTime, Space.World);
                    }
                    //向上
                    if (YY > 0) {
                        transform.Rotate(-Vector3.left, rotateDelta * Time.deltaTime, Space.World);
                    }
                }
                #endregion
                yield return new WaitForFixedUpdate();            
            }
        }
    }
}
