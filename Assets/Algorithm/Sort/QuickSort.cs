using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 快速排序（二分法）
    // 从数列中挑出一个元素，称为 "基准"（pivot），
    // 重新排序数列，所有元素比基准值小的摆放在基准前面，所有元素比基准值大的摆放在基准的后面（相同的数可以到任一边）。
    // 在这个分割结束之后，该基准就处于数列的中间位置。这个称为分割（partition）操作。
    // 递归地（recursive）把小于基准值元素的子数列和大于基准值元素的子数列排序。
    void QuickSortRecursive (int[] list, int left, int right)
    {
        if (left < right)
        {
            // 分割
            int i = Division(list , left, right);
            // 排左边
            QuickSortRecursive (list, left, i-1);
            // 排右边
            QuickSortRecursive (list , i+1 , right);
        }
    }

    int Division (int[] list , int left , int right)
    {
        //将首元素作为基准数
        //将第一个元素位置挖一个坑
        int pivot = list[left];
        int i = left;
        int j = right;

        while (i < j)
        {
            while (i < j)
            {
                // 从右到左，寻找第一个小于基准pivot的元素
                // 将这个元素填到基准值的坑里
                // 当前的j位置就是新的坑
                if (list[j] <= pivot)
                {
                    list[i] = list[j];
                    break;
                }
                else//如果大于，那么就把j向前移
                {
                    j--;
                }
            }
            while (i < j)
            {
                // 从右到左，寻找第一个大于基准pivot的元素
                // 将这个元素填到新挖的坑里
                // 当前的i位置就变成了新的坑
                if (list[i] > pivot)
                {
                    list[j] = list[i];
                    break;
                }
                else//如果小于等于，那么就把i向后移
                {
                    i++;
                }
            }
            //这时已经跳出两个小循环了，说明i==j了
            //然后将基准值填到最后的坑里
            //记录最后的位置
            list[i] = pivot;
        }
        return i;
    }


    // 基准值取中间
    void QuickSortPlus (int[] numbers , int left , int right)
    {
        if (left < right)
        {
            //因为他拿到的是中间索引的值，所以不需要随后一步赋值
            int middle = numbers[( left + right ) / 2];
            int i = left - 1;
            int j = right + 1;
            while (true)
            {
                while (numbers[++i] < middle);

                while (numbers[--j] > middle);

                if (i >= j)
                    break;

                Swap (numbers , i , j);
            }

            QuickSortPlus (numbers , left , i - 1);
            QuickSortPlus (numbers , j + 1 , right);
        }
    }

    //数据交换方法
    void Swap (int[] numbers , int i , int j)
    {
        int number = numbers[i];
        numbers[i] = numbers[j];
        numbers[j] = number;
    }
}
