﻿using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

//[InitializeOnLoad]  
public class ChangeFontWindow : EditorWindow
{

    static ChangeFontWindow()
    {
        //toChangeFont = new Font("Arial");  
        //toChangeFontStyle = FontStyle.Normal;  
    }

    [MenuItem("Window/Change Font")]
    private static void ShowWindow()
    {
        ChangeFontWindow cw = EditorWindow.GetWindow<ChangeFontWindow>(true, "Window/Change Font");

    }
    Font toFont;// = new Font("Arial");
    static Font toChangeFont;
    FontStyle toFontStyle;
    static FontStyle toChangeFontStyle;
    private void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Label("目标字体:");
        toFont = (Font)EditorGUILayout.ObjectField(toFont, typeof(Font), true, GUILayout.MinWidth(100f));
        toChangeFont = toFont;
        GUILayout.Space(10);
        GUILayout.Label("类型:");
        toFontStyle = (FontStyle)EditorGUILayout.EnumPopup(toFontStyle, GUILayout.MinWidth(100f));
        toChangeFontStyle = toFontStyle;
        if (GUILayout.Button("修改字体！"))
        {
            Change();

            AssetDatabase.Refresh();
        }
    }
    public static void Change()
    {
        //获取所有UILabel组件  
        if (Selection.objects == null || Selection.objects.Length == 0) return;
        //如果是UGUI讲UILabel换成Text就可以  
        Object[] labels = Selection.GetFiltered(typeof(Text), SelectionMode.Deep);
        foreach (Object item in labels)
        {
            //如果是UGUI讲UILabel换成Text就可以  
            Text label = (Text)item;
            label.font = toChangeFont;
            label.fontStyle = toChangeFontStyle;
            Debug.Log(item.name + ":" + label.text);
            EditorUtility.SetDirty(item);//重要  
        }
    }
}
