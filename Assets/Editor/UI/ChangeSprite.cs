using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine.UI;

//将图集图片中的精灵替换到UI中
public class ChangeSprite : EditorWindow
{
    [MenuItem("Window/新图集方式替换精灵工具")]
    static void Init()
    {
      //  ChangeSprite window = (ChangeSprite)EditorWindow.GetWindow(typeof(ChangeSprite));
		EditorWindow.GetWindow (typeof(ChangeSprite));
    }
//    string spriteNameTemp = "";
//    string spriteName = "";

    int filtrateNum = 0;
    Object[] filtrates;
    private static List<Sprite> mspriteList = new List<Sprite>();

    struct ESCT_SpriteAsset
    {
        public GameObject OldAsset;
        public GameObject nowAsset;
    }
    List<ESCT_SpriteAsset> m_spriteAssetLib = new List<ESCT_SpriteAsset>();
    void OnGUI()
    {
        GUILayout.Label("新图集方式替换精灵工具 ");
        EditorGUILayout.BeginVertical("Box");

        int _filtrateNum = EditorGUILayout.IntField("筛选字体的类型", filtrateNum);
        if (filtrateNum != _filtrateNum)
        {
            filtrateNum = _filtrateNum;
            filtrates = new Object[filtrateNum];
        }
        for (int i = 0; filtrates != null && i < filtrates.Length; ++i)
        {
            //((filtrates[i] != null) ? filtrates[i].name : "未指定"), 
            filtrates[i] = EditorGUILayout.ObjectField(((filtrates[i] != null) ? filtrates[i].name : "未指定"), filtrates[i], typeof(Object),true);

        }
        if (GUILayout.Button("替换"))
        {
            m_spriteAssetLib.Clear();
            mspriteList.Clear();
            for (int i = 0; i < filtrates.Length; i++)
            {
                Sprite[] _spriteList = Resources.LoadAll<Sprite>("UI/Pck_UI/" + filtrates[i].name);
                
                for (int j=0;j<_spriteList.Length;j++)
                {
                    mspriteList.Add(_spriteList[j]);
                }
                
            }

            Object[] selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);//Selection.objects;

            for (int i = 0; i < selection.Length; ++i)
            {
                string mPath = AssetDatabase.GetAssetPath(selection[i]);

                if (mPath.Contains(".prefab"))
                {
                    GameObject oldprefab = selection[i] as GameObject;
                    GameObject newprefab = PrefabUtility.InstantiatePrefab(oldprefab) as GameObject;
                    SetGameTransformActivity(newprefab.transform);


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
                                        for (int j = 0;j< mspriteList.Count;j++) {
                                            //匹配，替换。存下已修改标志位
                                            if (uilabel.sprite.name == mspriteList[j].name)
                                            {

                                                _addAsset = true;
                                                uilabel.sprite = mspriteList[i];
                                            }
                                        }
                                       

                                    }

                                }
                            }

                            //如果已修改，则需要替换旧资源保存，将此列表存下来，供过后查看执行替换
                            if (_addAsset == true)
                            {
                                ESCT_SpriteAsset _newSpriteAsset = new ESCT_SpriteAsset();
                                _newSpriteAsset.OldAsset = oldprefab;
                                _newSpriteAsset.nowAsset = newprefab;
                                m_spriteAssetLib.Add(_newSpriteAsset);
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

            //替换预设，保存修改
            for (int i = 0; m_spriteAssetLib != null && i < m_spriteAssetLib.Count; ++i)
            {
                PrefabUtility.ReplacePrefab(m_spriteAssetLib[i].nowAsset, m_spriteAssetLib[i].OldAsset);
            }
            AssetDatabase.Refresh();

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
