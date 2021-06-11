
using System.Collections;
using UnityEngine;

public class GestureMoveControl : GestureControl {

    override protected void InputCheck()
    {
        base.InputCheck();

        //单指移动
        if(Input.touchCount == 1){
            //按住3D物体不动1秒后随手指一起移动
            if (Input.GetTouch(0).phase == TouchPhase.Stationary) {
                TouchTime += Time.deltaTime;
                if (TouchTime > 1) {
                    status = 0;
                }
            }
        }
        if (status == 0) {
            StartCoroutine(CustomOnMouseDown());
        }
    }

    IEnumerator CustomOnMouseDown() {
        yield return null;
        //将物体世界坐标系华为屏幕坐标系，用来明确屏幕坐标系Z轴的位置
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        //由于鼠标坐标系是二维，需要转化成三维的世界坐标系
        Vector3 WorldPositon = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
     
        //三维的情况下才能计算鼠标位置与物体的距离
        Vector3 distance = transform.position - WorldPositon;

        //
        while (Input.GetMouseButton(0)) {
            //但前鼠标位置
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            
            //将当前鼠标的二维位置转化成三位位置，再加上鼠标的移动距离
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + distance;
            //curposition 就是物体应该的移动向量赋给transform
            transform.position = curPosition;

            yield return new WaitForFixedUpdate();
        }
    }
}
