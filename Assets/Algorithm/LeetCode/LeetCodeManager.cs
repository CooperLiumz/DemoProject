using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeetCodeManager : MonoBehaviour
{
    public void DoSolution ( )
    {
        LeetCode[] _codes = transform.GetComponentsInChildren<LeetCode> ( );
        foreach ( LeetCode item in _codes )
        {
            item.DoSolution ( );
        }
    
    }
}
