using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

// 插入排序=================

/// <summary>
///  插入排序算法是把一个数插入一个已经排序好的数组中。
///  例如 把 22 插入到[1 , 5 , 10 , 17 , 28 , 39 , 42] 中，
///  结果[1 , 5 , 10 , 17 , 22 , 28 , 39 , 42] 。
///  对数组使用插入排序法
///  数组 int[] array = [11 , 39 , 35 , 30 , 7 , 36 , 22 , 13 , 1 , 38 , 26 , 18 , 12 , 5 , 45 , 32 , 6 , 21 , 42 , 23];
///  数组元素是无序，设定一个从大到小或从小到大的方向，第一位就是有序的[11] ，
///  第一次插入： [11, 39, 35, 30, 7, 36, 22, 13, 1, 38, 26, 18, 12, 5, 45, 32, 6, 21, 42, 23]。
///  取第二个数跟第一个进行比较， 两位有序[11 , 39]
///  第二次插入：[11, 39, 35, 30, 7, 36, 22, 13, 1, 38, 26, 18, 12, 5, 45, 32, 6, 21, 42, 23]
///  取第三个数，[11, 39, 35],进行插入
///  [11 , 35 , 39 , 30 , 7 , 36 , 22 , 13 , 1 , 38 , 26 , 18 , 12 , 5 , 45 , 32 , 6 , 21 , 42 , 23]
///  ... ...
///  以后每次取一个数，插入数组。    
/// </summary>
void InsertSort (int[] list)
{
    for (int i = 0; i < list.Length; i++)    //要将第几位数进行插入
    {
        for (int j = i; j > 0; j--)
        {
            if (list[j] > list[j - 1])
                break;  //如果要排序的数大于已排序元素的最大值，就不用比较了。不然就要不断比较找到合适的位置
            else
            {
                int temp = list[j];
                list[j] = list[j - 1];
                list[j - 1] = temp;
            }
        }
    }
}
    
// 选择排序=================

// 选择排序 每次从后面找到最小或最大的数，进行位移排序。
// 数组 int[] array = [11 , 39 , 35 , 30 , 7 , 36 , 22 , 13 , 1 , 38 , 26 , 18 , 12 , 5 , 45 , 32 , 6 , 21 , 42 , 23];
// 第一位 i = 0
// 最小值下标 minIndex = 0，最小值 min = 11
// 从后面查找比 11 小的数，找到第 下标位 8，值为1，
// 进行交换,交换后[1 , 39 , 35 , 30 , 7 , 36 , 22 , 13 , 11 , 38 , 26 , 18 , 12 , 5 , 45 , 32 , 6 , 21 , 42 , 23];
// 第二位 i = 1，
// 最小值下标 minIndex = 1，最小值 min = 39,
// 从后面查找比 39 小且最小的数，找到 下标为 13，值为 5，
// 进行交换，交换后[1 , 5 , 35 , 30 , 7 , 36 , 22 , 13 , 11 , 38 , 26 , 18 , 12 , 39 , 45 , 32 , 6 , 21 , 42 , 23];
void SelectSort (int[] list)
{
    for (int i = 0; i < list.Length; i++)
    {
        int min = list[i];     //设定第i位为最小值
        int minIndex = i;       //最小值下标
        for (int j = i; j < list.Length; j++)  //从第i为开始找出最小的数
        {
            if (list[j] < list[minIndex])     //重新存储最小值和下标
            {
                min = list[j];
                minIndex = j;
            }
        }

        if (list[i] != list[minIndex])        //如果到比第i为更小的数，则发生交换。找不到则不改变
        {
            list[minIndex] = list[i];
            list[i] = min;
        }
    }
}

// 快速排序=================

// 快速排序（二分法）
// 从数列中挑出一个元素，称为 "基准"（pivot），
// 重新排序数列，所有元素比基准值小的摆放在基准前面，所有元素比基准值大的摆放在基准的后面（相同的数可以到任一边）。
// 在这个分割结束之后，该基准就处于数列的中间位置。这个称为分割（partition）操作。
// 递归地（recursive）把小于基准值元素的子数列和大于基准值元素的子数列排序。
void QuickSortRecursive (int[] list , int left , int right)
{
    if (left < right)
    {
        // 分割
        int i = Division (list , left , right);
        // 排左边
        QuickSortRecursive (list , left , i - 1);
        // 排右边
        QuickSortRecursive (list , i + 1 , right);
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
            // 从左到右，寻找第一个大于基准pivot的元素
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
            while (numbers[++i] < middle)
                ;

            while (numbers[--j] > middle)
                ;

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
