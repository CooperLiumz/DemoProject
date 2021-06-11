using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public enum EU_Ease
{
    Linear,
    EaseIn,
    EaseOut
}

public class CountingNumber : MonoBehaviour
{
    public EU_Ease ease = EU_Ease.Linear;

    Text mNumText;

    int beginNum;
    int changeNum;

    int goldNum;
    int targetNum;
    float duration;
    bool changing;
    float countTime;

    // Start is called before the first frame update
    void Start ()
    {
        mNumText = gameObject.GetComponent<Text> ();

        ChangeTo (500, 20);
    }

    // Update is called once per frame
    void Update ()
    {
        if (changing)
        {
            countTime += Time.deltaTime;
            
            goldNum = ChangeNum ();
            
            if (countTime >= duration)
            {
                goldNum = targetNum;
                changing = false;
            }

            mNumText.text = goldNum.ToString ();
        }
    }

    public void ChangeTo (int _target, float _duration)
    {
        if (!int.TryParse (mNumText.text , out goldNum))
        {
            goldNum = 0;
        }

        beginNum = goldNum;
        changeNum = _target - beginNum;

        duration = _duration;
        targetNum = _target;
        changing = true;
    }

    int ChangeNum ()
    {
        if (ease == EU_Ease.Linear)
        {
            return Mathf.RoundToInt (ChangeLinear (countTime , beginNum , changeNum , duration));
        }
        else if (ease == EU_Ease.EaseIn)
        {
            return Mathf.RoundToInt (ChangeEaseIn (countTime , beginNum , changeNum , duration));
        }
        else if (ease == EU_Ease.EaseOut)
        {
            return Mathf.RoundToInt (ChangeEaseOut (countTime , beginNum , changeNum , duration));
        }
        else
        {
            return Mathf.RoundToInt (ChangeLinear (countTime , beginNum , changeNum , duration));
        }
    }


    float ChangeLinear (float _time , float _begin , float _change , float _duration)
    {
        return _change * _time / _duration + _begin;
    }

    float ChangeEaseIn(float _time , float _begin , float _change , float _duration)
    {
        return -_change * Mathf.Cos(_time /_duration * ( Mathf.PI/2)) + _change + _begin ;
    }

    float ChangeEaseOut (float _time , float _begin , float _change , float _duration)
    {
        return _change * Mathf.Sin (_time / _duration * ( Mathf.PI / 2 )) + _begin;
    }
}
