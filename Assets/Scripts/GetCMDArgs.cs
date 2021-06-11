using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCMDArgs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] strs = Environment.GetCommandLineArgs ();

        Debug.LogError (strs.Length);

        Debug.LogError (strs[0]);

        Debug.LogError (strs[1]);

        foreach (var item in strs)
        {
            Debug.LogError (item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
