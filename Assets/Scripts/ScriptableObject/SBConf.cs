using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SBConf : ScriptableObject
{

    #if UNITY_EDITOR
    [MenuItem ("Assets/Create/SBConf" , false , 0)]
    static void CreateDynamicConf ()
    {
        UnityEngine.Object obj = Selection.activeObject;
        if (obj)
        {
            string path = AssetDatabase.GetAssetPath (obj);
            ScriptableObject bullet = CreateInstance<SBConf> ();
            if (bullet)
            {
                string confName = TryGetName<SBConf> (path);
                AssetDatabase.CreateAsset (bullet , confName);
            }
            else
            {
                Debug.Log (typeof (SBConf) + " is null");
            }
        }
    }

    #endif

    #if UNITY_EDITOR
    static string TryGetName<T> (string path , string suffix = ".asset")
    {
        int index = 0;
        string confName = "";
        UnityEngine.Object obj = null;
        do
        {
            confName = path + "/" + typeof (T).Name + "_" + index + suffix;
            obj = UnityEditor.AssetDatabase.LoadAssetAtPath (confName , typeof (T));
            index++;
        } while (obj);
        return confName;
    }
    #endif
}



