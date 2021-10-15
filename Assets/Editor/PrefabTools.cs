using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Text;

using UnityEditor;
using UnityEditor.Animations;

/// <summary>
/// 自动生成Prefab，只需要资源按照一定的目录规范就可以
/// </summary>
public static class PrefabTools
{
    [MenuItem ( "KTools/Prefab生成" , false , 100 )]
    public static void GenerateCode ( )
    {
        // YanLingFaShi假定是每个模型的资源根目录
        // 根据选择的目录文件，其中包含了FBX文件，程序生成的controller文件，自动生成prefab，绑定animator
        string targetPath = @"Assets\Resources\Prefabs";

        Object[] objects = Selection.GetFiltered ( typeof ( object ) , SelectionMode.Assets );
        if ( objects.Length == 0 )
        {
            Debug.LogWarning ( "请选择资源目录后再进行操作" );
            return;
        }

        foreach ( var obj in objects )
        {
            string assetPath = AssetDatabase.GetAssetPath ( obj );
            string parentName = Path.GetDirectoryName ( assetPath );
            parentName = Path.GetFileNameWithoutExtension ( parentName );

            string ctrlName = Path.GetFileNameWithoutExtension ( assetPath );
            Debug.LogWarning ( "处理目录：" + assetPath );
            //Debug.LogWarning(parentName);
            //Debug.LogWarning(ctrlName);

            CreatePrefab ( assetPath , targetPath + $"/{parentName}/{ctrlName}.prefab" );
        }

        AssetDatabase.Refresh ( );
        AssetDatabase.SaveAssets ( );
    }

    /// <summary>
    /// 创建模型Prefab，可以对应创建规则内的Materials,绑定贴图等等
    /// </summary>
    /// <param name="path"></param>
    /// <param name="outPath"></param>
    public static bool CreatePrefab ( string path , string outPath )
    {
        //EditorPathUtils.SaveCreateFolder ( Path.GetDirectoryName ( outPath ) );

        // 该筛选器字符串可以包含：名称、资产标签(l)和类型（t类名称）的搜索数据
        // Mesh AnimationClip Material，等等都在里面
        string[] fbxGUIDs = AssetDatabase.FindAssets ( "t:Object" , new string[] { path } );
        //string[] ctrlGUIDs = AssetDatabase.FindAssets("t:AnimatorController", new string[] { path });

        // 提取fbx目录和controller目录
        string fbxPath = null;
        string ctrlPath = null;
        foreach ( var v in fbxGUIDs )
        {
            string path1 = AssetDatabase.GUIDToAssetPath ( v );
            string fileExt = Path.GetExtension ( path1 );
            if ( fileExt.Equals ( ".fbx" , System.StringComparison.CurrentCultureIgnoreCase ) )
            {
                fbxPath = path1;
                //Debug.Log(path1);
            }
            else if ( fileExt.Equals ( ".controller" , System.StringComparison.CurrentCultureIgnoreCase ) )
            {
                ctrlPath = path1;
            }
        }

        if ( string.IsNullOrEmpty ( fbxPath ) )
        {
            Debug.LogWarning ( "该资源不存在对应的fbx文件，跳过生成， path =" + path );
            return false;
        }

        Object asset = AssetDatabase.LoadAssetAtPath<Object> ( fbxPath );

        GameObject gobj = ( GameObject ) GameObject.Instantiate ( asset );
        //gobj.name = asset.name;

        EditorUtility.SetDirty ( gobj );
        // 如果path对应的目录下有动画控制器文件，那么自动添加animator
        if ( !string.IsNullOrEmpty ( ctrlPath ) )
        {
            // 先判断一下，Unity默认有可能会创建这个组件
            Animator anim = gobj.GetComponent<Animator> ( );
            if ( !anim )
            {
                anim = gobj.AddComponent<Animator> ( );
            }
            AnimatorController ctrl = AssetDatabase.LoadAssetAtPath<AnimatorController> ( ctrlPath );
            anim.runtimeAnimatorController = ctrl;
        }
        else
        {
            Animator anim = gobj.GetComponent<Animator> ( );
            if ( null != anim )
            {
                GameObject.DestroyImmediate ( anim );
                Debug.LogWarning ( "该Prefab不应该生成Animator组件，自动删除， path =" + path );
            }
        }

        PrefabUtility.SaveAsPrefabAsset ( gobj , outPath );

        GameObject.DestroyImmediate ( gobj );
        return true;
    }
}