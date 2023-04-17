using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard_Test: MonoBehaviour {

    //The part that needs to display the label
    public Transform[] billBoardTfs;

    public BillBoard billBoard;

	// Use this for initialization
	void Start () 
    {
        Invoke ( "InitBillBoard", 1 );

        Invoke ( "ShowBillBoard", 2 );
    }

    private void InitBillBoard ( )
    {
        billBoard.SetBillBoardTarget ( billBoardTfs );
    }
    private void ShowBillBoard ( )
    {
        billBoard.ShowBillBoard ( true );

        Debug.LogError ( "Show ");
    }
}
