using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Cutscene))]
public class CutsceneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var cutscene = target as Cutscene;

        if (GUILayout.Button("Add Dialogue Action"))
            cutscene.AddAction(new DialogueAction());
        else if (GUILayout.Button("Add Move Actor Action"))
            cutscene.AddAction(new MoveAction());
        else if (GUILayout.Button("Add Turn Actor Action"))
            cutscene.AddAction(new TurnActorAction());
        else if (GUILayout.Button("Add Coordinate Action"))
            cutscene.AddAction(new CoordinateAction());

        base.OnInspectorGUI();
    }
}
