using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

//最近阿宅迷上了一款二次元游戏叫<<解救公主>>。
//其中有一关的规则是这样的
//公主被困在梦境里，梦境里的空间无限大，公主靠自己是走不出来的。 
//系统会随机很多条行动指令，玩家必须帮公主选出正确的指令，公主按照玩家选择的指令，重复执行若干次后就能走出困境。 
//指令有三个字符组成 S, R, L
//S: 前进一步
//R: 向右转
//L: 向左转
//如果公主重复执行错误的指令，她就会一直在绕圈子，走不出梦境。 
//所谓“绕圈子”是指：无论公主重复执行多少次指令，她始终都在一个以出发点为圆心，以R为半径的圆里，永远走不出这个圆，更走不出梦境。 
//阿宅已经卡在这一关很久了。他很痛苦不能早日拯救公主脱离苦海，于是向聪明的你求助，让你写段代码判断哪些指令是正确的，哪些指令是错误的。 
//输入： 
//第一行 一个整数n
//之后共有n行，每行为一个指令串
//输入约束： 
//n位于区间[1 , 50]
//从第二行开始，每行指令串长度为1-50，且仅包含字母 S, L, R
//输出： 
//仅有一个单词。 
//指令串错误 打印 no
//指令串正确 打印 yes
//举例1： 
//输入 
//1 
//SLSR
//输出
//yes
//解释：假设公主初始状态向北，公主的行动序列依次为前进，左转，前进，右转，此时公主仍然向北，但位置已经移动了。只要时间足够长，公主会一直向这个方向前进，所以公主没有在绕圈子。她能走出梦境，指令是正确的
//举例2： 
//输入 
//2 
//SSSS
//R
//输出
//no
//解释： 公主一直在沿着一个边长为4步的小正方形绕圈子，指令是错误的
//一是，指令执行完后方向不变（即L和R抵消）//光方向不变不行，还要不能停留在原来的位置，比如LLSSRRSS，方向不变，实在停留在原点
//二是，指令执行完后位置改变（即不停留在原地）//光位置有改变不行，还要方向不变，LZ的例子的SSSSR就是这个问题，不管转L还是转R都是90度转，第4次就会360度转回到原点，如果是LL或RR180度转，那第二次就会回到原点
//所以，综上，我觉得只要同时满足条件1和2，就不会饶圈子（也就是不管怎么重复，都是勇往直前）
//方向不变还是看L和R抵消，而是看转的角度%360是否和原来一样。因为LLLLS没有R抵消也是方向不变（转了360度），所以它也是yes

namespace ConsoleApp2
{
    class Program
    {
        //static void Main (string[] args)
        //{
        //    // 命令行数
        //    int lineCount = 0;

        //    // 输入数据
        //    string input = null;

        //    // 命令列表
        //    List<string> cmdList = new List<string> ();

        //    // 匹配
        //    string pattern = @"[^LRS]";

        //    //显示消息
        //    Console.WriteLine ("开始输入命令串数量");

        //    input = Console.ReadLine ();

        //    if (int.TryParse (input , out lineCount))
        //    {
        //        if (lineCount > 0 && lineCount < 51)
        //        {
        //            Console.WriteLine ("请输入命令串");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine ("输入错误， 请重新输入！");
        //    }

        //    while (cmdList.Count < lineCount)
        //    {
        //        input = Console.ReadLine ();

        //        if (Regex.IsMatch (input , pattern))
        //        {
        //            Console.WriteLine ("输入错误， 请重新输入！");
        //        }
        //        else
        //        {
        //            cmdList.Add (input);
        //        }
        //    }

        //    checkCmd (cmdList);

        //    Console.WriteLine ("输入完毕");

        //    Console.ReadKey ();
        //}

        public static string checkCmd (List<string> _cmdList)
        {
            // 初始角度 北 为 0
            int _angle = 0; 
            // 初始位置为0,0点
            int _beginX = 0;
            int _beginY = 0;
            int _endX = _beginX;
            int _endY = _beginY;

            foreach (string _cmd in _cmdList)
            {
                foreach (char _c in _cmd.ToCharArray ())
                {
                    //以左转为-90度
                    if (_c.Equals('L'))
                    {
                        _angle -= 90; 
                    }
                    //右转为+90度
                    else if (_c.Equals('R'))
                    {
                        _angle += 90; 
                    }
                    else if (_c.Equals('S'))
                    { 
                        int _mod = _angle % 360;
                        if (_mod == 0)
                        {
                            //往北走
                            _endY++; 
                        }
                        else if (_mod == -90 || _mod == 270)
                        {
                            //往西走
                            _endX--; 
                        }
                        else if (_mod == 180 || _mod == -180)
                        {
                            //往南走
                            _endY--; 
                        }
                        else if (_mod == -270 || _mod == 90)
                        {
                            //往东走
                            _endX++; 
                        }
                    }
                }
            }
            //如果方向不变位置改变,则正确
            if (_angle % 360 == 0 && ( _endX != _beginX || _endY != _beginY ))
            { 
                //Console.WriteLine (_angle + "===" + _endX + "===" + _endY);
                Console.WriteLine ("yes");
                return "yes";
            }
            else
            {
                //Console.WriteLine (_angle + "===" + _endX + "===" + _endY);
                Console.WriteLine ("no");
                return "no";
            }
        }


        bool isValid ( char[] commands, int len )
        {
            int x = 0, y = 0;
            int direction = 0; // 0: north, 1: east, 2: south, 3: west

            for ( int i = 0 ; i < len ; i++ )
            {
                char c = commands[ i ];
                if ( c == 'R' )
                {
                    direction = ( direction + 1 ) % 4;
                }
                else if ( c == 'L' )
                {
                    direction = ( direction + 3 ) % 4;
                }
                else if ( c == 'S' )
                {
                    if ( direction == 0 )
                    {
                        y++;
                    }
                    else if ( direction == 1 )
                    {
                        x++;
                    }
                    else if ( direction == 2 )
                    {
                        y--;
                    }
                    else
                    {
                        x--;
                    }
                }
            }

            return x == 0 && y == 0;
        }



        public static string[] getRandomCmd (int uc)
        { //测试数据
            if (uc == 0)
            {
                return new string[] { "LLLLS" };
            }
            else if (uc == 1)
            {
                return new string[] { "SSSS" , "R" };
            }
            char[] chs = { 'L' , 'R' , 'S' };
            if (uc > 49)
            {
                uc = 49;
            }

            Random _radom = new Random ();

            int n = _radom.Next (0 , uc + 1) + 1;
            string[] cmd = new string[n];
            for (int i = 0; i < n; i++)
            {
                int len = _radom.Next (0 , uc + 1) + 1;
                StringBuilder buf = new StringBuilder ();
                for (int j = 0; j < len; j++)
                {
                    buf.Append (chs[_radom.Next (0 , 3)]);
                }
                cmd[i] = buf.ToString ();
            }
            return cmd;
        }
    }
}
