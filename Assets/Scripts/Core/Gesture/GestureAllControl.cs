using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GestureAllControl : GestureControl
{
    //旋转因子
    public int rotateDelta = 80;

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

    void Start()
    {
        //获取物体原始大小
        initialScale = this.transform.localScale.x;
    }

    override protected void InputCheck() {
        base.InputCheck();

        Debug.LogError(Input.touchCount);

        //单指移动
        if (Input.touchCount == 1) {
            //按住3D物体不动1秒后随手指一起移动
            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                TouchTime += Time.deltaTime;
                if (TouchTime > 1)
                {
                    status = 0;
                    StartCoroutine(CustomMove());
                }
            } else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                status = 1;

                StartCoroutine(CustomRotate());
            }
        }
        else if (Input.touchCount == 2)
        {
            status = 2;
            StartCoroutine(CustomZoom());
        } else if (Input.touchCount > 2) {
            status = 3;
            Debug.Log(" Input.touchCount > 2  ==> " + Input.touchCount);
        }
    }

    //移动
    IEnumerator CustomMove() {
        yield return null;
        //将物体世界坐标系华为屏幕坐标系，用来明确屏幕坐标系Z轴的位置
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        //由于鼠标坐标系是二维，需要转化成三维的世界坐标系
        Vector3 WorldPositon = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        //三维的情况下才能计算鼠标位置与物体的距离
        Vector3 distance = transform.position - WorldPositon;

        //手指不抬起，一直检测
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

    //旋转
    IEnumerator CustomRotate()
    {
        yield return null;
        //检测到一直触摸时，会一直运行
        while (Input.GetMouseButton(0))
        {
            //判断是否点击在UI上
#if UNITY_ANDROID || UNITY_IPHONE
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            if (EventSystem.current.IsPointerOverGameObject())
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
                else
                {
                    //向下
                    if (YY < 0)
                    {
                        transform.Rotate(Vector3.left, rotateDelta * Time.deltaTime, Space.World);
                    }
                    //向上
                    if (YY > 0)
                    {
                        transform.Rotate(-Vector3.left, rotateDelta * Time.deltaTime, Space.World);
                    }
                }
                #endregion
                yield return new WaitForFixedUpdate();
            }
        }
    }

    //缩放
    IEnumerator CustomZoom()
    {
        yield return null;
        while (Input.GetMouseButton(0))
        {
            //实时记录模型大小
            realScale = this.transform.localScale;
            if ( (Input.GetTouch(0).phase == TouchPhase.Moved 
                || Input.GetTouch(1).phase == TouchPhase.Moved)
                 && status == 2)
            {
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
                else
                {
                    //判断时候超过边界
                    if (realScale.x > initialScale * minScale)
                    {
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

    bool isEnlarge(Vector3 op1, Vector3 op2, Vector3 np1, Vector3 np2)
    {
        float leng1 = Mathf.Sqrt((op1.x - op2.x) * (op1.x - op2.x) + (op1.y - op2.y) * (op1.y - op2.y));
        float leng2 = Mathf.Sqrt((np1.x - np2.x) * (np1.x - np2.x) + (np1.y - np2.y) * (np1.y - np2.y));
        if (leng1 < leng2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
