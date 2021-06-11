//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using System.Text.RegularExpressions;
//using UnityEngine;

//public class SavePrincess : MonoBehaviour
//{
//    string input = "ASDLESRTLSR";
//    string pattern = @"[^LRS]";

//    public MyTween tween;

//    // Start is called before the first frame update
//    void Start()
//    {
//        tween.TestTT ();

//        if (Regex.IsMatch (input , pattern)) {
//            foreach (Match item in Regex.Matches(input , pattern) )
//            {
//                Debug.LogError (item.Value);
//            }
//        } 
//        //for (int i = 0; i < 5; i++)
//        //{
//        //    string[] cmd = getRandomCmd (i);
//        //    Debug.LogError (string.Concat("测试输入" , i + 1 , "==count=="+cmd.Length));
//        //    foreach (string s in cmd)
//        //    {
//        //        Debug.LogError (s);
//        //    }
//        //    Debug.LogError (string.Concat ("输出" , checkCmd (cmd)));
//        //}
//    }

//    float tick = 0;
//    int cd = 1;

//    void Update ()
//    {
//        tick += Time.deltaTime;
//        if (tick >= cd)
//        {
//            tick -= cd;
//            transform.position.x +=1;
//        }
//    }

//    //public static void main (String[] args)
//    //{
//    //    //boolean test = true; //用户手动输入数据改成false即可
//    //    //String[] cmd;
//    //    //if (!test)
//    //    //{
//    //    //    System.out.println ("输入");
//    //    //    cmd = getUserInput ();
//    //    //    System.out.printf ("输出\n%s\n" , checkCmd (cmd));
//    //    //}
//    //    //else
//    //    //{ //自动测试
//    //    //    for (int i = 0; i < 5; i++)
//    //    //    {
//    //    //        cmd = getRandomCmd (i);
//    //    //        System.out.printf ("测试%d\n输入\n%d\n" , i + 1 , cmd.length);
//    //    //        for (String s : cmd)
//    //    //        {
//    //    //            System.out.println (s);
//    //    //        }
//    //    //        System.out.printf ("输出\n%s\n\n" , checkCmd (cmd));
//    //    //    }
//    //    //}
//    //}

//    //public static String[] getUserInput ()
//    //{
//    //    Scanner sc = new Scanner (System.in);
//    //    int n = 0;
//    //    while (true)
//    //    {
//    //        try
//    //        {
//    //            //System.out.println("请输入1-50之间的数字：");
//    //            n = Integer.valueOf (sc.nextLine ());
//    //            if (n > 0 && n < 51)
//    //                break;
//    //        }
//    //        catch (Exception e)
//    //        {
//    //            n = 1; //default
//    //            break;
//    //        }
//    //        //System.out.println("输入错误，请重输。");
//    //    }
//    //    String[] cmd = new String[n];
//    //    for (int i = 0; i < n; i++)
//    //    {
//    //        while (true)
//    //        {
//    //            try
//    //            {
//    //                //System.out.printf("请输入第%d行指令（指令必须有[LRS]中字母构成其总长度不超过50）：");
//    //                cmd[i] = sc.nextLine ();
//    //                if (cmd[i].matches ("[LRS]{1,50}"))
//    //                    break;
//    //            }
//    //            catch (Exception e)
//    //            {
//    //                cmd[i] = "S"; //default
//    //                break;
//    //            }
//    //            //System.out.println("输入错误，请重输。");
//    //        }
//    //    }
//    //    return cmd;
//    //}

//    public string checkCmd (string[] cmd)
//    {
//        int angle = 0, x0 = 0, y0 = 0, x = x0, y = y0; //方向（角度），距离
//        foreach (string s in cmd)
//        {
//            foreach (char c in s.ToCharArray() )
//            {
//                if (c == 'L')
//                {
//                    angle -= 90; //以左转为-90度
//                }
//                else if (c == 'R')
//                {
//                    angle += 90; //右转为+90度
//                }
//                else if (c == 'S')
//                { //以北为0度
//                    int m = angle % 360;
//                    if (m == 0)
//                        y++; //往北走
//                    else if (m == -90 || m == 270)
//                        x--; //往西走
//                    else if (m == 180 || m == -180)
//                        y--; //往南走
//                    else if (m == -270 || m == 90)
//                        x++; //往东走
//                }
//            }
//        }
//        if (angle % 360 == 0 && ( x != x0 || y != y0 ))
//        { //如果方向不变位置改变
//            Debug.LogError (angle + "===" + x + "===" + y );
//            return "yes";
//        }
//        else
//        {
//            Debug.LogError (angle + "===" + x + "===" + y);
//            return "no";
//        }
//    }

//    public string[] getRandomCmd (int uc)
//    { //测试数据
//        if (uc == 0)
//        {
//            return new string[] { "LLLLS" };
//        }
//        else if (uc == 1)
//        {
//            return new string[] { "SSSS" , "R" };
//        }
//        char[] chs = { 'L' , 'R' , 'S' };
//        if (uc > 49)
//            uc = 49;
//        int n = Random.Range (0, uc + 1) + 1;
//        string[] cmd = new string[n];
//        for (int i = 0; i < n; i++)
//        {
//            int len = Random.Range (0 , uc + 1) + 1;
//            StringBuilder buf = new StringBuilder ();
//            for (int j = 0; j < len; j++)
//            {
//                buf.Append (chs[Random.Range(0, 3)]);
//            }
//            cmd[i] = buf.ToString ();
//        }
//        return cmd;
//    }
//}
