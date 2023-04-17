using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class MyUnityWebRequest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator DownloadBundle ( string _url )
    {
        yield return null;

        Debug.LogError ( $"{Caching.cacheCount}==={Caching.defaultCache.path}" );
        Debug.LogError ( Caching.IsVersionCached ( Path.Combine ( _url, "prefabs" ), 1 ) );

        //using ( var www = WWW.LoadFromCacheOrDownload ( Path.Combine ( _url, "prefabs" ), 5 ) )
        //{
        //    yield return www;
        //    if ( !string.IsNullOrEmpty ( www.error ) )
        //    {
        //        Debug.Log ( www.error );
        //        yield return null;
        //    }
        //    var ab = www.assetBundle;

        //    AssetBundle prefabAb = www.assetBundle;
        //    GameObject testPrefab = Instantiate ( prefabAb.LoadAsset<GameObject> ( "HotUpdatePrefab.prefab" ) );
        //}
        using ( UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle ( Path.Combine ( _url, "prefabs" ), 1, 0 ) )
        {
            yield return uwr.SendWebRequest ( );

            if ( uwr.result != UnityWebRequest.Result.Success )
            {
                Debug.Log ( uwr.error );
            }
            else
            {
                Debug.LogError ( " suuccess" );
                // Get downloaded asset bundle
                AssetBundle prefabAb = DownloadHandlerAssetBundle.GetContent ( uwr );
                GameObject testPrefab = Instantiate ( prefabAb.LoadAsset<GameObject> ( "HotUpdatePrefab.prefab" ) );
            }
        }

    }
}
