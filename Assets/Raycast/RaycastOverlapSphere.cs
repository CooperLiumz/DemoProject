using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Physics.OverlapSphere(Vector3 position, float radius)
//Physics.OverlapSphere (Vector3 position , float radius , int layerMask)

////position  3D相交球的球心
////radius    3D相交球的球半径
////layerMask 在某个Layer层上进行碰撞体检索，例如当前选中Player层，则只会返回周围半径内               
////          Layer标示为Player的GameObject的碰撞体集合
// 返回的数组里没有先后顺序
//Collider[];

public class RaycastOverlapSphere : MonoBehaviour
{
    //代码
    public Transform OverlapSphereCube;
    public float SearchRadius;
    //假设 SearchRadius表示的相交球的检测半径值，大到足够覆盖到Cube4

    void Start ()
    {
        SearchNearUnits ();

        SearchNearUnitsLayers ();
    }

    public void SearchNearUnits ()
    {
        Collider[] colliders = Physics.OverlapSphere (OverlapSphereCube.position , SearchRadius);

        if (colliders.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < colliders.Length; i++)
            Debug.LogError ("SearchNearUnits==" + colliders[i].gameObject.name +"==distance=="  + Vector3.Distance(OverlapSphereCube.position, colliders[i].transform.position) );
    }

    public void SearchNearUnitsLayers ()
    {
        Collider[] colliders = Physics.OverlapSphere (OverlapSphereCube.position , SearchRadius, 1 << LayerMask.NameToLayer ("Enemy"));

        if (colliders.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < colliders.Length; i++)
            Debug.LogError ("SearchNearUnitsLayers==" + colliders[i].gameObject.name + "==distance==" + Vector3.Distance (OverlapSphereCube.position , colliders[i].transform.position));
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        Vector3 position = transform.localPosition;
        position.y += 0.01f;
        Gizmos.DrawWireSphere (position , SearchRadius);
    }
}
