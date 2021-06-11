using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 手势操作父类，用于互斥三种手势
/// </summary>
public class GestureControl : MonoBehaviour {

    //记录手势状态：
    //-1    没有任何手势在操作
    //0     移动手势在操作
    //1     旋转手势在操作
    //2     缩放手势在操作
    //3     三个手指
    public static int status = -1;

    //用与记录触碰物体的时间（区分单指移动与旋转）
    public static float TouchTime = 0;

    protected bool isSelected = false;

    /// <summary>
    /// 手指/鼠标按下
    /// </summary>
    void OnMouseDown() {
        isSelected = true;
    }

    /// <summary>
    /// 手指抬起，记录归零
    /// </summary>
    void OnMouseUp() {
        isSelected = false;
        status = -1;
        TouchTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isSelected)
        {
            return;
        }
        else if(status == -1){
            InputCheck();
        }
	}

    /// <summary>
    /// 具体操作逻辑
    /// </summary>
    virtual protected void InputCheck() {
    }
}
