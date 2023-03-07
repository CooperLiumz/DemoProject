using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor( typeof(LeetCodeManager) )]
public class LeetCodeManager_Editor : Editor
{
    public override void OnInspectorGUI ( )
    {
        base.DrawDefaultInspector ( );

        LeetCodeManager cur = ( LeetCodeManager ) this.target;
        EditorGUILayout.Separator ( );

        EditorGUILayout.Separator ( );
        if ( GUILayout.Button ( "DoSolution" ) )
        {
            cur.DoSolution ( );
        }
    }
}
