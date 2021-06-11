using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

//某居住楼为了举办大型庆祝活动，购买了一批灯控设备，但是这批设备比较奇特，它有如下功能
//一套设备能控制一个层楼中所有房间的灯的开关
//如果把一个房间里的灯熄灭，这个房间后面房间的灯也会熄灭
//如果把一个房间里的灯点亮，这个房间后面房间的灯也会点亮
//假如 用0表示灯是熄灭状态，用1表示灯是点亮状态
//某楼层的房间号是R1, R2, R3, R4, R5 它们的初始状态是[0 , 0 , 0 , 0 , 0] 说明所有房间的都是熄灭的。
//若我们用设备把第二个房间R2的灯点亮, 那么 房间R3, R4, R5的灯也会点亮 此时它们的状态变为[0 , 1 , 1 , 1 , 1]
//  现在，我们要使用这个灯控系统用居民楼显示文字和图案。
//假设初始状态所有房间的灯都是熄灭的。
//现在给出某层楼里各个房间的灯的最后状态。请问最少操作多少次这个灯控设备，就能使这个楼层的灯变成最后的状态？
//输入
//一行，一个由“0”和“1”组成的字符串S，表示各个房间灯的最后状态
//输入约束
//字符串S中仅包含字符0或1，且字符个数（房间个数）范围是[1 , 50]
//输出
//一行，一个整数，表示为了达到这个效果最少需要的操作次数
//例子
//输入
//0011
//输出
//1
//解释：
//输入表示房间的最后状态是
//R1 关
//R2 关
//R3 开
//R4 开
//在所有房间的灯都熄灭的情况下，只需要操作设备把R3房间的灯点亮 就能达到这种效果。所以结果是1，输出1。
namespace ConsoleApp2
{
    class Light
    {
        static void Main (string[] args)
        {    
            // 匹配
            string pattern = @"[^01]";

            string input = Console.ReadLine ();

            if (Regex.IsMatch (input , pattern))
            {
                //Console.WriteLine ("输入错误， 请重新输入！");
            }
            else
            {
                CountOperation (input.ToArray ());
            }
           

            Console.ReadKey ();
        }

        public static void CountOperation (char[] charArry)
        {
            int _lgh = charArry.Length;
            int _count = 0;
            for (int i = 1; i < _lgh; i++)
            {
                if (charArry[i] == charArry[i - 1])
                {

                }
                else
                {
                    _count += 1;
                }
            }
            if (charArry[0] == '1')
            {
                _count += 1;
            }

            Console.WriteLine (_count);
        }
    }
}
