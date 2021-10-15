using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ModeProcess : AssetPostprocessor
{ 
    void OnPreprocessAsset ( )
    {
        Debug.LogError ( "OnPreprocessAsset ===" + assetPath );
    }

    public void OnPreprocessModel ( )
    {
        Debug.Log ( "OnPreprocessModel=" + assetPath );
        //ModelImporter impor = assetImporter as ModelImporter;
        //DoModelSettings ( );
    }

    public void OnPostprocessModel ( GameObject go )
    {
        Debug.Log ( "OnPostprocessModel=" + go.name );
    }

    public void OnPreprocessTexture ( )
    {
        Debug.Log ( "OnPreProcessTexture=" + assetPath );
        TextureImporter impor = assetImporter as TextureImporter;
    }

    public void OnPostprocessTexture ( Texture2D tex )
    {
        Debug.Log ( "OnPostProcessTexture=" + assetPath );
    }

    public void OnPostprocessAudio ( AudioClip clip )
    {
        Debug.Log ( "OnPostprocessAudio=" + assetPath );
    }

    public void OnPreprocessAudio ( )
    {
        AudioImporter audio = assetImporter as AudioImporter;
    }

    //所有的资源的导入，删除，移动，都会调用此方法，注意，这个方法是static的
    public static void OnPostprocessAllAssets ( string[] importedAsset , string[] deletedAssets , string[] movedAssets , string[] movedFromAssetPaths )
    {
        //Debug.Log("OnPostprocessAllAssets");
        foreach ( string str in importedAsset )
        {
            Debug.Log ( "importedAsset = " + str );
        }
        foreach ( string str in deletedAssets )
        {
            Debug.Log ( "deletedAssets = " + str );
        }
        foreach ( string str in movedAssets )
        {
            Debug.Log ( "movedAssets = " + str );
        }
        foreach ( string str in movedFromAssetPaths )
        {
            Debug.Log ( "movedFromAssetPaths = " + str );
        }
    }

    /// <summary>
    /// 模型参数自动设置
    /// </summary>
    void DoModelSettings ( )
    {
        ModelImporter model = assetImporter as ModelImporter;
        model.useFileUnits = false;
        model.useFileScale = false;
        //model.importBlendShapes = false;
        //model.importBlendShapeNormals = ModelImporterNormals.Import;
        //model.importNormals = ModelImporterNormals.Import;

        model.SaveAndReimport ( );

        //string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        //ModelImporter modelimp = AssetImporter.GetAtPath(path) as ModelImporter;
        //modelimp.animationWrapMode = WrapMode.Once;
        ////modelimp.animationType = ModelImporterAnimationType.Human;
        //modelimp.animationCompression = ModelImporterAnimationCompression.KeyframeReductionAndCompression;
        //ModelImporterClipAnimation[] anims = modelimp.clipAnimations;

        //ModelImporter
        //ShaderImporter;
        //VideoClipImporter;
        //ModelImporter;
        //ModelImporterClipAnimation;
        //AudioImporter;
        //ModelImporter;
        //TextureImporter;
        //AssetImporter;
    }
}

