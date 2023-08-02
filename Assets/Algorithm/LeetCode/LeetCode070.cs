using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
/// 70. 爬楼梯
假设你正在爬楼梯。需要 n 阶你才能到达楼顶。

每次你可以爬 1 或 2 个台阶。你有多少种不同的方法可以爬到楼顶呢？ 

示例 1：

输入：n = 2
输出：2
解释：有两种方法可以爬到楼顶。
1. 1 阶 + 1 阶
2. 2 阶
示例 2：

输入：n = 3
输出：3
解释：有三种方法可以爬到楼顶。
1. 1 阶 + 1 阶 + 1 阶
2. 1 阶 + 2 阶
3. 2 阶 + 1 阶

*/




public class LeetCode070 : LeetCode
{
    public override void DoSolution ( )
    {
        Debug.LogError ( Time.realtimeSinceStartup);
        //Debug.LogError( ClimbStairs ( 50 ) );
        Debug.LogError ( Time.realtimeSinceStartup );

        Debug.LogError ( Time.realtimeSinceStartup );
        Debug.LogError ( ClimbStairs2 ( 20 ) );
        Debug.LogError ( Time.realtimeSinceStartup );
    }

    public int ClimbStairsDP ( int n )
    {
        int[] dp = new int[ n + 1 ];
        dp[ 0 ] = 1;
        dp[ 1 ] = 1;
        for ( int i = 2 ; i <= n ; i++ )
        {
            dp[ i ] = dp[ i - 1 ] + dp[ i - 2 ];
        }
        return dp[ n ];
    }


    // 递归法容易超时
    // 重复计算
    public int ClimbStairs ( int n )
    {
        if ( n == 1 || n == 2 )
        {
            return n;
        }

        return ClimbStairs( n - 1) + ClimbStairs ( n - 2 );
    }

    public int ClimbStairs2 ( int n )
    {
        if ( n < 3 )
        {
            return n;
        }
        int n_1 = 1;
        int n_2 = 2;
        int temp = 0;

        for ( int i = 3 ; i < n ; i++ )
        {
            temp = n_2;
            n_2 = n_1 + n_2;
            n_1 = temp;
        }

        return n_2;
    }

}
