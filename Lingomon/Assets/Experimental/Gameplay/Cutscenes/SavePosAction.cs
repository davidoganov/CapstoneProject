using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class SavePosAction : CutsceneAction
{
    [SerializeField] CutsceneActor actor;

    public override IEnumerator Play()
    {
        Character c = actor.GetCharacter();
        LocationManager.Instance.addLocation(c.CharacterName, c.transform.position);
        yield return null;
    }
}
