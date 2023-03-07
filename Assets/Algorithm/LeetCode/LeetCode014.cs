using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//14.最长公共前缀
//编写一个函数来查找字符串数组中的最长公共前缀。

//如果不存在公共前缀，返回空字符串 ""。 

//示例 1：

//输入：strs = [ "flower", "flow", "flight" ]
//输出："fl"
//示例 2：

//输入：strs = [ "dog", "racecar", "car" ]
//输出：""
//解释：输入不存在公共前缀。

// 提示：
//1 <= strs.length <= 200
//0 <= strs[ i ].length <= 200
//strs[ i ] 仅由小写英文字母组成

public class LeetCode014 : LeetCode
{
    public override void DoSolution ( )
    {
        string[] strs = new string[] { "flower", "flow", "flight" };

        Debug.LogError ( LongestCommonPrefix ( strs ) );
    }

    string LongestCommonPrefix ( string[] strs)
    {
        if ( strs == null || strs.Length < 1 )
        {
            return null;
        }
        if ( strs.Length == 1 )
        {
            return strs[ 0 ];
        }

        string result = strs[ 0 ];
        for ( int i = 1 ; i < strs.Length ; i++ )
        {
            if ( strs[ i ].Length < result.Length )
            {
                result = result.Substring ( 0, strs[ i ].Length );
            }

            for ( int j = 0 ; j < result.Length ; j++ )
            {
                if ( result[ j ].Equals ( strs[ i ][ j ] ) )
                {

                }
                else
                {
                    result = result.Substring ( 0, j );
                    break;
                }
            }
        }
        return result;
    }
}
