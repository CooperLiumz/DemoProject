using UnityEngine;
[ExecuteInEditMode]
public class BezierPipe : MonoBehaviour
{
	[SerializeField]
	public float cornerScale = 1f;
	[Range ( 3 , 256 )]
	[SerializeField]
	public int cornerStep = 10;
	[Range ( 2 , 64 )]
	[SerializeField]
	public int circleStep = 10;
	[SerializeField]
	public float r = 0.1f;
	[SerializeField]
	public Transform point1;
	[SerializeField]
	public Transform point2;

	[System.NonSerialized]
	private float cornerScale_old;
	[System.NonSerialized]
	private int cornerStep_old;
	[System.NonSerialized]
	private int circleStep_old;
	[System.NonSerialized]
	private float r_old;
	[System.NonSerialized]
	private Transform point1_old;
	[System.NonSerialized]
	private Transform point2_old;

	public bool update = false;

	[System.NonSerialized]
	private Mesh mesh;
	[System.NonSerialized]
	Vector3[] verts;
	[System.NonSerialized]
	int[] triangles;
	[System.NonSerialized]
	private MeshCollider mc;

	[System.NonSerialized]
	private Vector3[] P_old;
	[System.NonSerialized]
	private Vector3[] P;
	[System.NonSerialized]
	private Vector3[] Pts;

	void CalcuP ( )
	{
		P[ 0 ] = point1.position;
		P[ 3 ] = point2.position;
		float scale = cornerScale;
		float length = 3 * Mathf.Max ( ( P[ 0 ] - P[ 3 ] ).magnitude / 4 , r );
		if ( scale > length )
		{
			scale = length;
		}
		P[ 1 ] = point1.position + point1.rotation * Vector3.forward * scale;
		P[ 2 ] = point2.position - point2.rotation * Vector3.forward * scale;
	}
	void CalcuPts ( )
	{
		float steps = Pts.Length + 2;
		for ( int i = 0 ; i < Pts.Length ; ++i )
		{
			float t = ( i + 1 ) / steps;
			float t2 = t * t;
			float t3 = t * t2;
			float rt = 1 - t;
			float rt2 = rt * rt;
			float rt3 = rt * rt2;
			Pts[ i ] =
				P[ 0 ] * rt3 +
				3 * P[ 1 ] * t * rt2 +
				3 * P[ 2 ] * t2 * rt +
				P[ 3 ] * t3;
		}
	}
	bool CheckAndBackupMain ( )
	{
		bool changed = false;
		if ( cornerScale_old != cornerScale )
		{
			cornerScale_old = cornerScale;
			changed = true;
		}
		if ( cornerStep_old != cornerStep )
		{
			cornerStep_old = cornerStep;
			changed = true;
		}
		if ( circleStep_old != circleStep )
		{
			circleStep_old = circleStep;
			changed = true;
		}
		if ( r_old != r )
		{
			r_old = r;
			changed = true;
		}
		if ( point1_old != point1 )
		{
			point1_old = point1;
			changed = true;
		}
		if ( point2_old != point2 )
		{
			point2_old = point2;
			changed = true;
		}
		if ( changed )
		{
			ResetPs ( );
		}
		return changed;
	}
	void BackupP ( )
	{
		for ( int i = 0 ; i < P.Length ; ++i )
		{
			P_old[ i ] = P[ i ];
		}
	}
	public void ResetPs ( )
	{
		P_old = new Vector3[ 4 ];
		P = new Vector3[ 4 ];
		Pts = new Vector3[ cornerStep ];
		verts = new Vector3[ circleStep * ( cornerStep + 2 ) ];
		triangles = new int[ circleStep * ( cornerStep + 1 ) * 6 ];
		CalcuP ( );
		CalcuPts ( );
		BackupP ( );
	}
	void Start ( )
	{
		mesh = new Mesh ( );
		mesh.name = "Pipe";
		MeshFilter mf = GetComponent<MeshFilter> ( );
		if ( mf != null )
		{
			mf.sharedMesh = mesh;
		}
		mc = GetComponent<MeshCollider> ( );
	}
	void LateUpdate ( )
	{
		if ( !update )
		{
			return;
		}

		if ( point1 == null || point2 == null )
		{
			return;
		}

		bool changed = CheckAndBackupMain ( );
		CalcuP ( );
		CalcuPts ( );
		for ( int i = 0 ; i < P.Length ; ++i )
		{
			Vector3 voff = P[ i ] - P_old[ i ];
			if ( voff.sqrMagnitude <= 0 )
			{
				continue;
			}
			changed = true;
		}
		if ( !changed )
		{
			return;
		}
		BuildMesh ( );
	}
	void GetCirclePoint ( Vector3 pos , Vector3 dir , int i )
	{
		int pos_start = i * circleStep;
		Quaternion dir_q = Quaternion.LookRotation ( dir , Vector3.up );
		for ( int a = 0 ; a < circleStep ; a++ )
		{
			float p = 2 * Mathf.PI * a / circleStep;
			Vector3 cp = new Vector3 ( r * Mathf.Cos ( p ) , r * Mathf.Sin ( p ) , 0 );
			cp = dir_q * cp + pos;
			cp = transform.worldToLocalMatrix.MultiplyPoint ( cp );
			verts[ pos_start + a ] = cp;
		}
	}
	void SetTriangles ( )
	{
		int i;
		int write_start = 0;
		int max_pos = ( cornerStep + 1 ) * circleStep;
		int[,] ids = new int[ 2 , circleStep + 1 ];
		for ( int row_start = 0 ; row_start < max_pos ; row_start += circleStep )
		{
			ids[ 0 , 0 ] = row_start;
			ids[ 0 , circleStep ] = row_start;
			float dis_min = ( verts[ row_start ] - verts[ row_start + circleStep ] ).sqrMagnitude;
			int i_min = 0;
			for ( i = 1 ; i < circleStep ; ++i )
			{
				ids[ 0 , i ] = row_start + i;
				float dis_cur = ( verts[ row_start ] - verts[ row_start + circleStep + i ] ).sqrMagnitude;
				if ( dis_cur < dis_min )
				{
					dis_min = dis_cur;
					i_min = i;
				}
			}
			ids[ 1 , 0 ] = row_start + circleStep + i_min;
			ids[ 1 , circleStep ] = ids[ 1 , 0 ];
			for ( i = 1 ; i < circleStep ; ++i )
			{
				ids[ 1 , i ] = row_start + circleStep + ( ( i_min + i ) % circleStep );
			}
			for ( i = 0 ; i < circleStep ; ++i )
			{
				triangles[ write_start + 0 ] = ids[ 0 , i ];
				triangles[ write_start + 1 ] = ids[ 0 , i + 1 ];
				triangles[ write_start + 2 ] = ids[ 1 , i ];
				triangles[ write_start + 3 ] = ids[ 1 , i ];
				triangles[ write_start + 4 ] = ids[ 0 , i + 1 ];
				triangles[ write_start + 5 ] = ids[ 1 , i + 1 ];
				write_start += 6;
			}
		}
		mesh.triangles = triangles;
	}
	public void BuildMesh ( )
	{
		if ( mesh == null )
		{
			return;
		}
		GetCirclePoint ( point1.position , Pts[ 0 ] - point1.position , 0 );
		for ( int i = 0 ; i < Pts.Length - 1 ; ++i )
		{
			GetCirclePoint ( Pts[ i ] , Pts[ i + 1 ] - Pts[ i ] , i + 1 );
		}
		GetCirclePoint ( Pts[ Pts.Length - 1 ] , point2.position - Pts[ Pts.Length - 1 ] , Pts.Length );
		GetCirclePoint ( point2.position , point2.position - Pts[ Pts.Length - 1 ] , Pts.Length + 1 );
		mesh.Clear ( );
		mesh.vertices = verts;
		SetTriangles ( );
		mesh.RecalculateNormals ( );
		BackupP ( );
		if ( mc != null )
		{
			mc.sharedMesh = mesh;
		}
	}
}
