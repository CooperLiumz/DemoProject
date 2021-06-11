///////////////////////////////////////////
// author     : liuminzhi
// create time: 2015/04/08
// modify time: 
// description: Make Sprite Prefab
///////////////////////////////////////////


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;


public class MakeAtlasTools : MonoBehaviour {

	private static string spriteDir = Application.dataPath + "/Resources/Textures";

//  [MenuItem("Window/Resources Tools/MakAtlasAll")]
//    static private void MakeAtlasAll()
//    {
//        if (!Directory.Exists(spriteDir))
//        {
//            Directory.CreateDirectory(spriteDir);
//        }
//
//        DirectoryInfo rootDirInfo = new DirectoryInfo(Application.dataPath + "/Textures");
//        foreach (DirectoryInfo dirInfo in rootDirInfo.GetDirectories())
//        {
//            foreach (FileInfo pngFile in dirInfo.GetFiles("*.png", SearchOption.AllDirectories))
//            {
//                string allPath = pngFile.FullName;
//                string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
//                Sprite sprite = Resources.LoadAssetAtPath<Sprite>(assetPath);
//                GameObject go = new GameObject(sprite.name);
//                go.AddComponent<SpriteRenderer>().sprite = sprite;
//                allPath = spriteDir + "/" + sprite.name + ".prefab";
//                string prefabPath = allPath.Substring(allPath.IndexOf("Assets"));
//                PrefabUtility.CreatePrefab(prefabPath, go);
//                GameObject.DestroyImmediate(go);
//            }
//        }	
//		AssetDatabase.Refresh ();
//    }

  [MenuItem("Window/Resources Tools/MakAtlas")]
  static private void MakeAtlas()
  {      

      if (!Directory.Exists(spriteDir))
      {
          Directory.CreateDirectory(spriteDir);
      }
      
      foreach (string pngFile in Directory.GetFiles(GetSelectedPathOrFallback(), "*.png", SearchOption.AllDirectories))
      {
          string allPath = pngFile;
          string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
          Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
          GameObject go = new GameObject(sprite.name);
          go.AddComponent<SpriteRenderer>().sprite = sprite;
          allPath = spriteDir + "/" + sprite.name + ".prefab";
          string prefabPath = allPath.Substring(allPath.IndexOf("Assets"));
          PrefabUtility.CreatePrefab(prefabPath, go);
          GameObject.DestroyImmediate(go);
      }
//	  AssetDatabase.Refresh ();
  }

  public static string GetSelectedPathOrFallback()
  {
      string path = "Assets";

      foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
      {
          path = AssetDatabase.GetAssetPath(obj);
          if (!string.IsNullOrEmpty(path) && File.Exists(path))
          {
              path = Path.GetDirectoryName(path);
              break;
          }
      }
      return path;
  }



//	[MenuItem("Window/Resources Tools/MakAtlas")]
//	static private void MakeAtlas()
//	{      
//		string spriteDir = Application.dataPath + "/Resources/";
//		
//		string selectPath = GetSelectedPathOrFallback();
//		
//		selectPath = selectPath.Substring(selectPath.IndexOf("Textures"));
//		
//		spriteDir += selectPath;
//		
//		print (spriteDir);
//		
//		if (!Directory.Exists(spriteDir))
//		{
//			Directory.CreateDirectory(spriteDir);
//			AssetDatabase.Refresh ();
//		}else{
//			foreach (string pngFile in Directory.GetFiles(spriteDir, "*.prefab", SearchOption.AllDirectories))
//			{
//				File.Delete(pngFile);
//			}
//			AssetDatabase.Refresh ();
//		}
//		
//		foreach (string pngFile in Directory.GetFiles(selectPath, "*.png", SearchOption.AllDirectories))
//		{
//			string allPath = pngFile;
//			string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
//			Sprite sprite = Resources.LoadAssetAtPath<Sprite>(assetPath);
//			GameObject go = new GameObject(sprite.name);
//			go.AddComponent<SpriteRenderer>().sprite = sprite;
//			allPath = spriteDir + "/" + sprite.name + ".prefab";
//			string prefabPath = allPath.Substring(allPath.IndexOf("Assets"));
//			PrefabUtility.CreatePrefab(prefabPath, go);
//			GameObject.DestroyImmediate(go);
//		}
//		AssetDatabase.Refresh ();
//	}


}
