using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 文件的最长绝对路径
// 这里将 dir 作为根目录中的唯一目录。
// dir 包含两个子目录 subdir1 和 subdir2 。
// subdir1 包含文件 file1.ext 和子目录 subsubdir1；
// subdir2 包含子目录 subsubdir2，该子目录下包含文件 file2.ext 。

// 在文本格式中，如下所示(⟶表示制表符)：
// dir
// ⟶ subdir1
// ⟶ ⟶ file1.ext
// ⟶ ⟶ subsubdir1
// ⟶ subdir2
// ⟶ ⟶ subsubdir2
// ⟶ ⟶ ⟶ file2.ext

// 如果是代码表示，上面的文件系统可以写为
// "dir\n\tsubdir1\n\t\tfile1.ext\n\t\tsubsubdir1\n\tsubdir2\n\t\tsubsubdir2\n\t\t\tfile2.ext" 。
// '\n' 和 '\t' 分别是换行符和制表符。

// 文件系统中的每个文件和文件夹都有一个唯一的 绝对路径 ，即必须打开才能到达文件/目录所在位置的目录顺序，所有路径用 '/' 连接。上面例子中，指向 file2.ext 的 绝对路径 是 "dir/subdir2/subsubdir2/file2.ext" 。每个目录名由字母、数字和/或空格组成，每个文件名遵循 name.extension 的格式，其中 name 和 extension由字母、数字和/或空格组成。

// 给定一个以上述格式表示文件系统的字符串 input ，返回文件系统中 指向 文件 的 最长绝对路径 的长度 。 如果系统中没有文件，返回 0。

// 1 <= input.length <= 104
// input 可能包含小写或大写的英文字母，一个换行符 '\n'，一个制表符 '\t'，一个点 '.'，一个空格 ' '，和数字。

public class LeetCode388 : LeetCode
{
    private string input = "dir\n\tsubdir1\n\t\tfile1.ext\n\t\tsubsubdir1\n\tsubdir2\n\t\tsubsubdir2\n\t\t\tfile2.ext";

    public override void DoSolution ( )
    {
        base.DoSolution ( );

        Debug.LogError(LengthLongestPath ( input ));
    }

    int LengthLongestPath ( string _input)
    {
        Stack<int> _stack = new Stack<int> ( );

        int _result = 0;   
        int _index = 0;
        int _inputLength = _input.Length;

        while ( _index < _inputLength )
        {
            int _depth = 1;

            // 计算深度
            while ( _index < _inputLength && _input[ _index ] == '\t' )
            {
                _depth++;
               _index++;
            }

            int _lgh = 0;

            bool _isFile = false;
            // 计算 \n 之间字符串长度
            while ( _index < _inputLength && _input[ _index] != '\n')
            {
                if ( _input[ _index ] == '.' )
                {
                    _isFile = true;
                }
                _lgh ++;
                _index++;
            }

            _index++;

            // 如果已经遍历的深度比当前深度大
            // 就出栈到相同深度
            while ( _stack.Count >= _depth)
            {
                _stack.Pop ( );
            }

            // 计算当前深度的长度
            if ( _stack.Count > 0)
            {
                _lgh += _stack.Peek ( ) + 1;
            }

            if ( _isFile )
            {
                // 如果当前深度是文件，就与之前的比较大小
                _result = Math.Max ( _result, _lgh );
            }
            else
            {
                // 将当前深度入栈
                _stack.Push ( _lgh );
            }
        }

        return _result;
    }
}
