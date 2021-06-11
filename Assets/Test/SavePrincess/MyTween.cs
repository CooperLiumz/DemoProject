using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTween : MonoBehaviour
{

    Vector2 pos1 = Vector3.zero;
    Vector2 pos2 = Vector3.zero;
    Vector2 pos3 = Vector3.zero;
    Vector2 pos4 = Vector3.zero;

    public GameObject sphere1;
    public GameObject sphere2;
    public GameObject sphere3;
    public GameObject sphere4;

    float beginValue = 0;
    float changeValue = 10;
    float duration = 10;

    bool fresh = true;
    float time = 0;

    // Start is called before the first frame update
    void Start ()
    {

    }

    void Update ()
    {
        if (fresh)
        {
            if (time < duration)
            {
                time += Time.deltaTime;

                //float _y1 = (float)SineIn (Math.Min (time , duration) , beginValue , changeValue , duration);
                ////Debug.LogError (SineIn (Math.Min (time , duration) , beginValue , changeValue , duration));
                //pos1.y = _y1;
                //pos1.x = time;
                //CreateSphere (pos1 , sphere1);

                float _y2 = (float)SineOut (Math.Min (time , duration) , beginValue , changeValue , duration);
                //Debug.LogError (SineOut (Math.Min (time , duration) , beginValue , changeValue , duration));

                pos2.y = _y2;
                pos2.x = time;
                CreateSphere (pos2 , sphere2);

                //float _y3 = (float)QuadIn (Math.Min (time , duration) , beginValue , changeValue , duration);
                ////Debug.LogError (SineOut (Math.Min (time , duration) , beginValue , changeValue , duration));

                //pos3.y = _y3;
                //pos3.x = time;
                //CreateSphere (pos3 , sphere3);

                //float _y4 = (float)QuadOut (Math.Min (time , duration) , beginValue , changeValue , duration);
                ////Debug.LogError (SineOut (Math.Min (time , duration) , beginValue , changeValue , duration));

                //pos4.y = _y4;
                //pos4.x = time;
                //CreateSphere (pos4 , sphere4);
            }
        }
    }

    void CreateSphere (Vector3 _pos , GameObject _sphere)
    {
        GameObject _go = GameObject.Instantiate (_sphere) as GameObject;
        _go.transform.localPosition = _pos;
    }

    public double SineIn (float _time , float _beginValue , float _changeValue , float _duration)
    {
        return -_changeValue * Math.Cos (_time / _duration * ( Math.PI / 2 )) + _changeValue + _beginValue;
    }

    public double SineOut (float _time , float _beginValue , float _changeValue , float _duration)
    {
        return _changeValue * Math.Sin (_time / _duration * ( Math.PI / 2 )) + _beginValue;
    }

    public double QuadIn (float _time , float _beginValue , float _changeValue , float _duration)
    {
        return _changeValue * ( ( _time / _duration ) * ( _time / _duration ) ) + _beginValue;
    }

    public double QuadOut (float _time , float _beginValue , float _changeValue , float _duration)
    {
        return -_changeValue * ( ( _time / _duration ) * ( _time / _duration - 2 ) ) + _beginValue;
    }
}
