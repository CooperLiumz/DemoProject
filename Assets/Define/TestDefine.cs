using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// https://www.jianshu.com/p/9833ce1e2ec6
// https://www.cnblogs.com/CodeGize/p/7853121.html

public class TestDefine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Test1 ();
#if Test2
        Test2 ();
#endif
        Test3 ();
    }

#if Test1
    void Test1 ()
    {
        UnityEngine.Debug.Log ("Test1 Define");
    }
#endif

    void Test1 ()
    {

        UnityEngine.Debug.Log ("Test1 Normal");
    }

    void Test2 ()
    {
        UnityEngine.Debug.Log ("Test2");
    }

    [Conditional("Test3")]
    void Test3 ()
    {
        UnityEngine.Debug.Log ("Test3");
    }

}
