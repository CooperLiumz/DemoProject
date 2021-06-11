using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.Networking;
using System.IO;

public class InitJson : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //LocalDatVO _local = new LocalDatVO ();

        //LotVO _lotVO = new LotVO ();
        //_local.Lots= new LotVO[] { _lotVO };

        //ProductionInfoVO _piVO = new ProductionInfoVO ();
        //_local.Products = new ProductionInfoVO[] { _piVO };

        //TechInfoVO _tiVO = new TechInfoVO ();
        //_local.Techs= new TechInfoVO[] { _tiVO };

        //string _json = JsonFx.Json.JsonWriter.Serialize( _local );        

        //Debug.Log(_json);

        //StartCoroutine ( LoadStreamAssetWeb ());

        //System.Uri uri = new System.Uri ( Path.Combine ( Application.streamingAssetsPath, "data.json" ) );

        //Debug.Log ( "uri ==>" + uri );
    }

    IEnumerator LoadStreamAsset ()
    {
        yield return null;

        string _path = string.Concat ( Application.streamingAssetsPath, "/sailun.json" );        

        WWW www = new WWW ( _path );
        yield return www;
        if (string.IsNullOrEmpty ( www.error ))
        {
            //LocalDatVO _vo = JsonMapper.ToObject<LocalDatVO> ( www.text);

            //Debug.Log (_vo.Lots[0].LotName);

            Debug.Log ( "Copy Stream Asset Success !" );
        }
        else
        {
            Debug.Log ( "Copy Stream Asset Fail !" );
        }
    }

    IEnumerator LoadStreamAssetWeb ()
    {
        string _path = string.Concat ( Application.streamingAssetsPath, "/sailun.json" );

        Debug.Log ( "path ==>" + _path );

        UnityWebRequest uwr = UnityWebRequest.Get ( _path );

        yield return uwr.SendWebRequest ();

        if (uwr.isHttpError || uwr.isNetworkError)
        {
            Debug.Log ( "Copy Stream Asset Fail !" + uwr.error);
        }
        else
        {
            //LocalDatVO _vo = JsonMapper.ToObject<LocalDatVO> ( uwr.downloadHandler.text );

            //Debug.Log ( _vo.Lots[0].LotName );

            Debug.Log ( "Copy Stream Asset Success !" );
        }
    }

    //IEnumerator TestUnityWebRequest ()
    //{
    //    string url = string.Concat ( Application.streamingAssetsPath, "/sailun.json" );//http 
    //    UnityWebRequest uwr = UnityWebRequest.Get ( url );
    //    yield return uwr.SendWebRequest ();
    //    if (uwr.isHttpError || uwr.isNetworkError)
    //    {
    //        Debug.Log ( "Fail !" + uwr.error );
    //    }
    //    else
    //    {
    //        Debug.Log (uwr.downloadHandler.text);
    //        Debug.Log ( "Success !" );
    //    }
    //}
}
