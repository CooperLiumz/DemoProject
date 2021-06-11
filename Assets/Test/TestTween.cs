using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;

public class TestTween : MonoBehaviour
{
    public Text mText1;
    public Text mText2;
    public Text mText3;

    // Start is called before the first frame update
    void Start ()
    {
        //Ease.Linear
        mText1.text = "100";
        mText2.text = "100";
        mText3.text = "100";
        //StartCoroutine (ChangeValueTo());
    }

    float time1;
    float time2;
    float time3;
    float result1 = 100;
    float result2 = 100;
    float result3 = 100;
    // Update is called once per frame
    void Update ()
    {
        //result = EaseIn (time , result , 500 , 100);
        //time += Time.deltaTime;
        //mText.text = result.ToString ();
        //Debug.LogError (result);

        if (result1 < 500)
        {
            result1 = Linear (time1 , result1 , 500 , 100);
            mText1.text = result1.ToString ();            
            time1 += 0.1f;
            Debug.LogError ("time1  " + time1);
        }


        if (result2 < 500)
        {
            result2 = EaseIn (time2 , result2 , 500 , 100);
            mText2.text = result2.ToString ();            
            time2 += 0.1f;

            Debug.LogError ("time2  " + time2);
        }

        if (result3 < 500)
        {
            result3 = EaseOut (time3 , result3 , 500 , 100);
            mText3.text = result3.ToString ();
            time3 += 0.1f;
            Debug.LogError ("time3  " + time3);
        }
    }

    IEnumerator ChangeValueTo ()
    {
        yield return null;

        //while (result < 500)
        //{
          
        //    yield return new WaitForSeconds(0.1f);
        //    time += 0.1f;
        //}
    }

    float Linear (float t , float b , float c , float d)
    {
        return c * t / d + b;
    }


    float EaseIn(float t, float b, float c , float d)
    {
        return -c * Mathf.Cos(t/d * ( Mathf.PI/2)) + c + b;
        //return c *(t /= d)*t + b;
    }

    float EaseOut (float t , float b , float c , float d)
    {
        return c * Mathf.Sin (t / d * ( Mathf.PI / 2 )) + b;
        //return -c * ( t /= d ) * ( t - 2 ) + b;
    }
}
