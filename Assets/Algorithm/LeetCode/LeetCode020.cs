using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//给定一个只包括 '('，')'，'{'，'}'，'['，']' 的字符串 s ，判断字符串是否有效。

//有效字符串需满足：

//左括号必须用相同类型的右括号闭合。
//左括号必须以正确的顺序闭合。
//每个右括号都有一个对应的相同类型的左括号。
 

//示例 1：

//输入：s = "()"
//输出：true
//示例 2：

//输入：s = "()[]{}"
//输出：true
//示例 3：

//输入：s = "(]"
//输出：false 
//提示：

//1 <= s.length <= 104
//s 仅由括号 '()[]{}' 组成


public class LeetCode020 : LeetCode
{
    public override void DoSolution ( )
    {
        Debug.LogError ( IsValid ( "()[]{}" ) );
    }

    public bool IsValid ( string s )
    {
        if ( string.IsNullOrEmpty(s) || s.Length < 2)
        {
            return false;
        }

        Stack<char> stack = new Stack<char> ( );
        for ( int i = 0 ; i < s.Length; i++ )
        {
            if ( stack.Count > 0 )
            {
                if ( isLeft ( s[ i ] ) )
                {
                    stack.Push ( s[ i ] );
                }
                else
                {

                    if ( isMatch ( s[ i ], stack.Peek ( ) ) )
                    {
                        stack.Pop ( );
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            else
            {
                stack.Push ( s[ i ] );
            }            
        }

        if ( stack.Count > 0 )
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool isLeft ( char _s )
    {
        if ( _s.Equals('(')
            || _s.Equals ( '[' ) 
            || _s.Equals ( '{' ) )
        {
            return true;
        }
        return false;
    }

    bool isMatch ( char _target, char _src )
    {
        switch ( _src )
        {
            case '(':
                return _target.Equals ( ')' );
            case '[':
                return _target.Equals ( ']' );
            case '{':
                return _target.Equals ( '}' );
        }
        return false;
    }

    //首先，我们应该处理一些临界情况。所有负数都不可能是回文，例如：-123 不是回文，因为 - 不等于 3。
    //所以我们可以对所有负数返回 false。除了 0 以外，所有个位是 0 的数字不可能是回文，因为最高位不等于 0。
    //所以我们可以对所有大于 0 且个位是 0 的数字返回 false。

    //现在，让我们来考虑如何反转后半部分的数字。

    //对于数字 1221，如果执行 1221 % 10，我们将得到最后一位数字 1，
    //要得到倒数第二位数字，我们可以先通过除以 10 把最后一位数字从 1221 中移除，1221 / 10 = 122，
    //再求出上一步结果除以 10 的余数，122 % 10 = 2，就可以得到倒数第二位数字。如果我们把最后一位数字乘以 10，
    //再加上倒数第二位数字，1 * 10 + 2 = 12，就得到了我们想要的反转后的数字。
    //如果继续这个过程，我们将得到更多位数的反转数字。

    //现在的问题是，我们如何知道反转数字的位数已经达到原始数字位数的一半？

    //由于整个过程我们不断将原始数字除以 10，然后给反转后的数字乘上 10，
    //所以，当原始数字小于或等于反转后的数字时，就意味着我们已经处理了一半位数的数字了。


    public bool IsPalindrome2 ( int x )
    {
        // 特殊情况：
        // 如上所述，当 x < 0 时，x 不是回文数。
        // 同样地，如果数字的最后一位是 0，为了使该数字为回文，
        // 则其第一位数字也应该是 0
        // 只有 0 满足这一属性
        if ( x < 0 || ( x % 10 == 0 && x != 0 ) )
        {
            return false;
        }

        int revertedNumber = 0;
        while ( x > revertedNumber )
        {
            revertedNumber = revertedNumber * 10 + x % 10;
            x /= 10;
        }

        // 当数字长度为奇数时，我们可以通过 revertedNumber/10 去除处于中位的数字。
        // 例如，当输入为 12321 时，在 while 循环的末尾我们可以得到 x = 12，revertedNumber = 123，
        // 由于处于中位的数字不影响回文（它总是与自己相等），所以我们可以简单地将其去除。
        return x == revertedNumber || x == revertedNumber / 10;
    }

}
