using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureZoomControl : GestureControl {

    //记录上一次手机触摸位置判断用户是在放大还是缩小
    private Vector2 oldPosition1;
    private Vector2 oldPosition2;

    //实时大小
    Vector3 realScale = new Vector3(1, 1, 1);

    //原始大小
    float initialScale = 0;
    //缩放速度
    public float scaleSpeed = 0.1f;
    //缩放比例
    public float maxScale = 2.5f;
    public float minScale = 0.5f;

    void Start() {
        //获取物体原始大小
        initialScale = this.transform.localScale.x;
    }

    override protected void InputCheck()
    {
        base.InputCheck();
        if (Input.touchCount == 2) {
            status = 2;
            StartCoroutine(CustomOnMouseDown());
        }
    }

    IEnumerator CustomOnMouseDown() {
        yield return null;
        while(Input.GetMouseButton(0)){
            //实时记录模型大小
            realScale = this.transform.localScale;
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved) {
                //记录触摸位置
                Vector3 tempPosition1 = Input.GetTouch(0).position;
                Vector3 tempPosition2 = Input.GetTouch(1).position;

                //函数返回真为放大，返回假为缩小
                if (isEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                {
                    //判断是否超过边界
                    if (realScale.x < initialScale * maxScale)
                    {
                        this.transform.localScale += this.transform.localScale * scaleSpeed;
                    }
                }
                else {
                    //判断时候超过边界
                    if (realScale.x > initialScale*minScale) {
                        this.transform.localScale -= this.transform.localScale * scaleSpeed;
                    }
                }

                //备份上一次的触摸位置
                oldPosition1 = tempPosition1;
                oldPosition2 = tempPosition2;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    bool isEnlarge(Vector3 op1, Vector3 op2, Vector3 np1, Vector3 np2) {
        float leng1 = Mathf.Sqrt((op1.x - op2.x)*(op1.x - op2.x) + (op1.y - op2.y)*(op1.y - op2.y));
        float leng2 = Mathf.Sqrt((np1.x - np2.x)*(np1.x - np2.x) + (np1.y - np2.y)*(np1.y - np2.y));
        if (leng1 < leng2)
        {
            return true;
        }
        else {
            return false;
        }
    }
}
