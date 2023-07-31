using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoordinateAction : CutsceneAction
{
    [SerializeField] CutsceneActor actor;
    [SerializeField] List<Vector3> coordinates;

    public override IEnumerator Play()
    {
        var character = actor.GetCharacter();
        foreach (var coord in coordinates)
        {
            yield return character.Move(coord - actor.GetCharacter().transform.position, checkCollisions: false);
        }
    }
}
