using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PeonRagdoll))]
public class PeonRagdollEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PeonRagdoll myScript = (PeonRagdoll)target;

        if (GUILayout.Button("Try Map bones"))
        {
            myScript.MatchBones();
        }
    }
}
