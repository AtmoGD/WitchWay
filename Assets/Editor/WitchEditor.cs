using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Witch))]
public class WitchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Witch witch = (Witch)target;

        if (GUILayout.Button("Turn Right"))
            witch.TurnRight();

        if (GUILayout.Button("Turn Left"))
            witch.TurnLeft();
    }
}
