using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BubbleSort : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //int[] list = { 3 , 4 , 1 , 5 , 2 };
        int[] list1 = { 6 , 4 , 7 , 5 , 1 , 3 , 2 };
        BubbleSortNormal (list1);
        //int[] list2 = { 6 , 4 , 7 , 5 , 1 , 3 , 2 };
        int[] list2 = { 7 , 5 , 6 , 4 , 2 , 3 , 1 };
        BubbleSortOpt1 (list2);
        //int[] list3 = { 6 , 4 , 7 , 5 , 1 , 3 , 2 };
        int[] list3 = { 7 , 5 , 6 , 4 , 2 , 3 , 1 };
        BubbleSortOpt2 (list3);
    }

    // 冒泡排序的思路就是，让相邻的两数据进行比较，满足条件就进行位置交换，
    // 最后一个数据一定是最大或者最小的，这样下一轮遍历就可以不用遍历最后一个数据，以此类推，遍历次数是数组长度-1
    void BubbleSortNormal (int[] list)
    {
        Debug.LogError (ReturnString (list));
        int sortCount = 0;

        int lgh = list.Length;
        int temp = 0; // 开辟一个临时空间, 存放交换的中间值
        // 要遍历的次数
        for (int i = 0; i < lgh - 1; i++)
        {
            //依次的比较相邻两个数的大小，遍历一次后，把数组中第i小的数放在第i个位置上
            for (int j = 0; j < lgh - 1 - i; j++)
            {
                // 比较相邻的元素，如果前面的数小于后面的数，就交换
                if (list[j] < list[j + 1])
                {
                    temp = list[j + 1];
                    list[j + 1] = list[j];
                    list[j] = temp;
                }
                sortCount += 1;
                Debug.LogErrorFormat ("第 {0} 遍的第{1} 次交换：" , i + 1 , j + 1);
                Debug.LogError (ReturnString (list));
            }   
            Debug.LogError ("\n#########################");
        }
        Debug.LogErrorFormat ("\n====总共遍历{0}次\n" , sortCount);
    }

    void BubbleSortOpt1 (int[] list)
    {
        Debug.LogError (ReturnString (list));

        int sortCount = 0;

        int lgh = list.Length;
        int temp = 0; // 开辟一个临时空间, 存放交换的中间值
        // 要遍历的次数
        for (int i = 0; i < lgh - 1; i++)
        {
            int flag = 1; //设置一个标志位
            //依次的比较相邻两个数的大小，遍历一次后，把数组中第i小的数放在第i个位置上
            for (int j = 0; j < lgh - 1 - i; j++)
            {
                // 比较相邻的元素，如果前面的数小于后面的数，交换
                if (list[j] < list[j + 1])
                {
                    temp = list[j + 1];
                    list[j + 1] = list[j];
                    list[j] = temp;
                    flag = 0;  //发生交换，标志位置0
                }
                sortCount += 1;
                Debug.LogErrorFormat ("第 {0} 遍的第{1} 次交换：" , i + 1 , j + 1);
                Debug.LogError (ReturnString (list));
            }
            
            if (flag == 1)
            {//如果没有交换过元素，则已经有序
                break;
            }
        }
        Debug.LogErrorFormat ("\n====总共遍历{0}次\n" , sortCount);
    }

    void BubbleSortOpt2 (int[] list)
    {
        Debug.LogError (ReturnString (list));
        int sortCount = 0;

        int lgh = list.Length;
        int tempLgh = lgh - 1;
        int temp = 0; // 开辟一个临时空间, 存放交换的中间值
        int tempPostion = 0;  // 记录最后一次交换的位置
        // 要遍历的次数
        for (int i = 0; i < lgh - 1; i++)
        {
            int flag = 1; //设置一个标志位
            //依次的比较相邻两个数的大小，遍历一次后，把数组中第i小的数放在第i个位置上
            for (int j = 0; j < tempLgh; j++)
            {
                // 比较相邻的元素，如果前面的数小于后面的数，交换
                if (list[j] < list[j + 1])
                {
                    temp = list[j + 1];
                    list[j + 1] = list[j];
                    list[j] = temp;
                    flag = 0;  //发生交换，标志位置0
                    tempPostion = j;  //记录交换的位置
                }

                sortCount += 1;
                Debug.LogErrorFormat ("第 {0} 遍的第{1} 次交换：" , i + 1 , j + 1);
                Debug.LogError (ReturnString (list));
            }
            tempLgh = tempPostion; //把最后一次交换的位置给len，来缩减内循环的次数
           
            if (flag == 1)
            {//如果没有交换过元素，则已经有序
                break;
            }
        }
        Debug.LogErrorFormat ("\n====总共遍历{0}次\n" , sortCount);
    }

    string ReturnString (int[] _list)
    {
        StringBuilder _sb = new StringBuilder ();

        foreach (int _v in _list)
        {
            _sb.Append(_v);
            _sb.Append (" ");
        }

        return _sb.ToString ();
    }
}
