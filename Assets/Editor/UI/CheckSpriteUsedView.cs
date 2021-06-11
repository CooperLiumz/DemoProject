using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine.UI;


public class CheckSpriteUsedView : EditorWindow
{

    [MenuItem("Window/图片引用检查工具")]

    static void Init()
    {
        // Get existing open window or if none, make a new one:
//        CheckSpriteUsedView window = (CheckSpriteUsedView)EditorWindow.GetWindow(typeof(CheckSpriteUsedView));
		EditorWindow.GetWindow(typeof(CheckSpriteUsedView));
    }
    //
    string spriteNameTemp = "";
    string spriteName = "";
    List<string> spriteList = new List<string>();
 
    void OnGUI()
    {
        GUILayout.Label("图片引用检查工具 ");
        EditorGUILayout.BeginVertical("Box");

        string _spriteName =  EditorGUILayout.TextField("检查的图片的名称", spriteNameTemp);
        if (_spriteName != spriteNameTemp) {
            spriteName = _spriteName;
        }
        
        if (GUILayout.Button("查找"))
        {
            Object[] selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);//Selection.objects;

            for (int i = 0; i < selection.Length; ++i)
            {
                string mPath = AssetDatabase.GetAssetPath(selection[i]);

                if (mPath.Contains(".prefab"))
                {
                    GameObject oldprefab = selection[i] as GameObject;
                    GameObject newprefab = PrefabUtility.InstantiatePrefab(oldprefab) as GameObject;
                    SetGameTransformActivity(newprefab.transform);


                    spriteList.Clear();

                    {

                        Image[] uilabellist = newprefab.GetComponentsInChildren<Image>();
                        if (uilabellist.Length > 0)
                        {
                            bool _addAsset = false;
                            for (int n = 0; n < uilabellist.Length; ++n)
                            {
                                Image uilabel = uilabellist[n];
                                if (null != uilabel)
                                {
                                    if (null != uilabel.sprite)
                                    {
                                        if (uilabel.sprite.name == spriteName)
                                        {

                                            _addAsset = true;
                                            spriteList.Add(uilabel.name);
                                        }
     
                                    }

                                }
                            }
                            if (_addAsset == true)
                            {
                                Debug.LogError("View-----  " +oldprefab.name);
                                for (int s=0;s<spriteList.Count;s++) {
                                    Debug.Log(spriteList[s]);
                                }
                            }
                            else
                            {
                                DestroyImmediate(newprefab);
                            }
                        }
                        else
                        {
                            DestroyImmediate(newprefab);
                        }
                    }
                }

            }
        }
        EditorGUILayout.EndVertical();


    }

    private static void SetGameTransformActivity(Transform t)
    {
        t.gameObject.SetActive(true);
        int childCout = t.childCount;
        for (int i = 0; i < childCout; i++)
        {
            Transform child = t.GetChild(i);
            if (child.childCount > 0)
            {
                SetGameTransformActivity(child);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }

}
