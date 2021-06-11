using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class ChangeMaterials : EditorWindow
{
    [MenuItem("Window/ChangeMaterials")]
    static void Init()
    {
        //  ChangeSprite window = (ChangeSprite)EditorWindow.GetWindow(typeof(ChangeSprite));
        EditorWindow.GetWindow(typeof(ChangeMaterials));
    }

    string prefabName;
    string tgtMaterialName;
    string srcMaterialName;

    void OnGUI()
    {
        GUILayout.Label("换材质工具 ");
        EditorGUILayout.BeginVertical("Box");

        //string _prefabName = EditorGUILayout.IntField("筛选物体名字", prefabName);
        //string _tgtMaterialName = EditorGUILayout.IntField("被替换材质", tgtMaterialName);
        //string _srcMaterialName = EditorGUILayout.IntField("替换材质", srcMaterialName);


        //if (GUILayout.Button("替换"))
        //{
        //    m_spriteAssetLib.Clear();
        //    mspriteList.Clear();
        //    for (int i = 0; i < filtrates.Length; i++)
        //    {
        //        Sprite[] _spriteList = Resources.LoadAll<Sprite>("UI/Pck_UI/" + filtrates[i].name);

        //        for (int j = 0; j < _spriteList.Length; j++)
        //        {
        //            mspriteList.Add(_spriteList[j]);
        //        }

        //    }

        //    Object[] selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);//Selection.objects;

        //    for (int i = 0; i < selection.Length; ++i)
        //    {
        //        string mPath = AssetDatabase.GetAssetPath(selection[i]);

        //        if (mPath.Contains(".prefab"))
        //        {
        //            GameObject oldprefab = selection[i] as GameObject;
        //            GameObject newprefab = PrefabUtility.InstantiatePrefab(oldprefab) as GameObject;
        //            SetGameTransformActivity(newprefab.transform);


        //            {

        //                Image[] uilabellist = newprefab.GetComponentsInChildren<Image>();
        //                if (uilabellist.Length > 0)
        //                {
        //                    bool _addAsset = false;
        //                    for (int n = 0; n < uilabellist.Length; ++n)
        //                    {
        //                        Image uilabel = uilabellist[n];
        //                        if (null != uilabel)
        //                        {
        //                            if (null != uilabel.sprite)
        //                            {
        //                                for (int j = 0; j < mspriteList.Count; j++)
        //                                {
        //                                    //匹配，替换。存下已修改标志位
        //                                    if (uilabel.sprite.name == mspriteList[j].name)
        //                                    {

        //                                        _addAsset = true;
        //                                        uilabel.sprite = mspriteList[i];
        //                                    }
        //                                }


        //                            }

        //                        }
        //                    }

        //                    //如果已修改，则需要替换旧资源保存，将此列表存下来，供过后查看执行替换
        //                    if (_addAsset == true)
        //                    {
        //                        ESCT_SpriteAsset _newSpriteAsset = new ESCT_SpriteAsset();
        //                        _newSpriteAsset.OldAsset = oldprefab;
        //                        _newSpriteAsset.nowAsset = newprefab;
        //                        m_spriteAssetLib.Add(_newSpriteAsset);
        //                    }
        //                    else
        //                    {
        //                        DestroyImmediate(newprefab);
        //                    }
        //                }
        //                else
        //                {
        //                    DestroyImmediate(newprefab);
        //                }
        //            }
        //        }

        //    }

        //    //替换预设，保存修改
        //    for (int i = 0; m_spriteAssetLib != null && i < m_spriteAssetLib.Count; ++i)
        //    {
        //        PrefabUtility.ReplacePrefab(m_spriteAssetLib[i].nowAsset, m_spriteAssetLib[i].OldAsset);
        //    }
        //    AssetDatabase.Refresh();

        //}


        EditorGUILayout.EndVertical();
    }
}
