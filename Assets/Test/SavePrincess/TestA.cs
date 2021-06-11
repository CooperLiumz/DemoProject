using UnityEngine;

public class TestA : MonoBehaviour
{
    int[] result = new int[] { 0 , 1 , 1 , 1 , 1 };// { 1 , 0 , 1 , 1 , 0 , 1 , 1 , 1 , 1 , 0 };
    int lgh = 0;
    int count = 0;

    void Start ()
    {
        CountOperation ();
        Debug.LogError (count);
    }
    void CountOperation ()
    {
        lgh = result.Length;
        for (int i = 1; i < lgh; i++)
        {
            if (result[i] == result[i - 1])
            {

            }
            else
            {
                count += 1;
            }
        }
        if (result[0] != 0)
        {
            count += 1;
        }
    }

}
