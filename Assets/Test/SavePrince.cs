using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrince : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.LogError ("SLSR");
        //Execute (new string[] { "SLSR" });
        //Debug.LogError ("=============");

        Debug.LogError ("SSSS");
        Debug.LogError ("R");
        Execute (new string[] { "SSSS" , "R" });
        Debug.LogError ("=============");

        Debug.LogError ("SRSRSRSR");
        Execute (new string[] { "SRSRSRSR" });
        Debug.LogError ("=============");
    }

    void Execute (string [] _codeMsg)
    {
        string num = Console.ReadLine ();
        int n = 1;
        List<char[]> codeList = new List<char[]> ();       

        foreach (string item in _codeMsg)
        {
            codeList.Add (item.ToCharArray ());
        }        

        // 朝向
        int direction = 0;

        // 位置
        int[] curPos = new int[2] { 0, 0};

        for (int i = 0; i < 4; i++)
        {
            foreach (char[] item in codeList)
            {
                foreach (char _value in item)
                {
                    switch (_value)
                    {
                        case 'L':
                            direction -= 1;
                            if (direction < -3)
                            {
                                direction += 4;
                            }
                            break;
                        case 'R':
                            direction += 1;
                            if (direction > 3)
                            {
                                direction -= 4;
                            }
                            break;
                        case 'S':
                            if (direction == 0)
                            {
                                curPos[0] += 1;
                            }
                            else if (Math.Abs (direction) == 2)
                            {
                                curPos[0] -= 1;
                            }
                            else if (direction == -1 || direction == 3)
                            {
                                curPos[1] -= 1;
                            }
                            else if (direction == 1 || direction == -3)
                            {
                                curPos[1] += 1;
                            }
                            break;
                    }
                    Debug.LogError ("_value  "  + _value + " direction  " + direction + " X " + curPos[0] + " Y " + curPos[1]);
                }                
            }
            if (direction == 0 && curPos[0] == 0 && curPos[1] == 0)
            {
                Debug.LogError ("no");
                return;
            }
        }

        Debug.LogError ("yes");
    }
}
