using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//给定一个字符串，请你找出其中不含有重复字符的 最长子串的长度。

//示例 1:

//输入: "abcabcbb"
//输出: 3 
//解释: 因为无重复字符的最长子串是 "abc"，所以其长度为 3。
//示例 2:

//输入: "bbbbb"
//输出: 1
//解释: 因为无重复字符的最长子串是 "b"，所以其长度为 1。
//示例 3:

//输入: "pwwkew"
//输出: 3
//解释: 因为无重复字符的最长子串是 "wke"，所以其长度为 3。
//     请注意，你的答案必须是 子串 的长度，"pwke" 是一个子序列，不是子串。

public class LengthOfLongestSubstring : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string str1 = "abcabcbb";
        string str2 = "bbbbb";
        string str3 = "pwwkew";
        string str4 = "dvdf";

        Debug.LogError (str1 + "  " + Solution (str1) + "\n");
        Debug.LogError (str2 + "  " + Solution (str2) + "\n");
        Debug.LogError (str3 + "  " + Solution (str3) + "\n");
        Debug.LogError (str4 + "  " + Solution (str3) + "\n");

        Debug.LogError (str1 + "  " + Solution2 (str1) + "\n");
        Debug.LogError (str2 + "  " + Solution2 (str2) + "\n");
        Debug.LogError (str3 + "  " + Solution2 (str3) + "\n");
        Debug.LogError (str4 + "  " + Solution2 (str3) + "\n");
    }

    // Update is called once per frame
    int Solution(string s)
    {
        int max = 0;
        char[] chars = s.ToCharArray ();
        List<char> temp = new List<char> ();
        for (int i = 0; i < chars.Length; i++)
        {            
            for (int j = i; j < chars.Length; j++)
            {
                if (temp.Contains (chars[j]))
                {
                    temp.Clear ();
                    break;
                }
                else
                {
                    temp.Add (chars[j]);
                }
                max = Mathf.Max (max , temp.Count);
            }           
        }
        return max;
    }

    int Solution2 (string s)
    {
        int max = 0;
        char[] chars = s.ToCharArray ();
        Queue<char> queue = new Queue<char> ();
        for (int i = 0; i < chars.Length; i++)
        {
            while (queue.Contains (chars[i]))
            {
                queue.Dequeue ();
                
            }
            queue.Enqueue (chars[i]);
            max = Math.Max (max, queue.Count);
        }
        return max;
    }
}
