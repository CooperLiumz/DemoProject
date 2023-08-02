using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSector : MonoBehaviour
{
    public float lookRange = 25;
    public float lookAngle = 90;
    public float lookAccurate = 1;

    public float rotatePerSecond = 90;

    float subAngle;

    // Start is called before the first frame update
    void Start ()
    {
        subAngle = lookAngle / lookAccurate;
    }

    float tick = 0;
    public float cd = 0.5f;

    // Update is called once per frame
    void Update ()
    {
        tick += Time.deltaTime;
        if (tick >= cd)
        {
            tick -= cd;
            //Look ();

            //Look2 ();

            Look3 ();
        }

        //Debug.DrawRay ( transform.position, transform.forward.normalized * 10, Color.green );
        //Debug.DrawLine ( transform.position, new Vector3 ( 10, 10, 10 ), Color.red );
    }

    //放射线检测
    private bool Look ()
    {
        //一条向前的射线
        if (LookAround (transform , Quaternion.identity , Color.green))
            return true;

        //多一个精确度就多两条对称的射线,每条射线夹角是总角度除与精度
        float subAngle = ( lookAngle / 2 ) / lookAccurate;
        for (int i = 0; i < lookAccurate; i++)
        {
            if (LookAround (transform , Quaternion.Euler (0 , -1 * subAngle * ( i + 1 ) , 0) , Color.green)
                || LookAround (transform , Quaternion.Euler (0 , subAngle * ( i + 1 ) , 0) , Color.green))
                return true;
        }

        return false;
    }

    //放射线检测
    private bool Look2 ()
    {
        string ss = ";";
        if (LookAround (transform , Quaternion.Euler (0 , -lookAngle / 2 + Mathf.Repeat (rotatePerSecond * Time.time , lookAngle) , 0) , Color.red))
            return true;
        return false;
    }

    //放射线检测
    private bool Look3 ()
    {
        //每条射线需要检测的角度范围
        for (int i = 0; i < lookAccurate; i++)
        {
            //Debug.LogError (-lookAngle / 2 + i * subAngle + Mathf.Repeat (rotatePerSecond * Time.time , subAngle));
            if (LookAround (transform , Quaternion.Euler (0 , -lookAngle / 2 + i * subAngle + Mathf.Repeat (rotatePerSecond * Time.time , subAngle) , 0) , Color.red))
            {
                return true;
            }
        }
        return false;
    }



    //射出射线检测是否有Player
    public bool LookAround (Transform eye , Quaternion eulerAnger , Color DebugColor)
    {
        Debug.DrawRay (eye.position , eulerAnger * eye.forward.normalized * lookRange , DebugColor);

        RaycastHit hit;
        if (Physics.Raycast (eye.position , eulerAnger * eye.forward , out hit , lookRange) && hit.collider.CompareTag ("Player"))
        {
            //eye.chaseTarget = hit.transform;
            return true;
        }
        return false;
    }
}
