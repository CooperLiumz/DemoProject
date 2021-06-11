using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardEngine : MonoBehaviour {

    //The part that needs to display the label
    public Transform[] billBoardTfs;


	// Use this for initialization
	void Start () {
        EventCenter.BillBoardEvent.RaiseSetBillBoardTarget(billBoardTfs);
	}

}
