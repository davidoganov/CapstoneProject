using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour, IPlayerTriggerable
{
    [SerializeReference]
    [SerializeField] List<CutsceneAction> actions;
    [SerializeField] bool repeatable;
    [SerializeField] int minPhase;
    [SerializeField] int maxPhase;

    public bool TriggerRepeatedly => repeatable;

    public IEnumerator Play() {
        GameController.Instance.StartCutsceneState();
        foreach (var action in actions) {
            if (action.WaitForCompletion)
                yield return action.Play();
            else 
                StartCoroutine(action.Play());
        }
        GameController.Instance.StartFreeRoamState();
    }

    public void AddAction(CutsceneAction action)
    {
        action.Name = action.GetType().ToString();
        actions.Add(action);
    }

    public void OnPlayerTriggered(PlayerController player)
    {
        int currPhase = GamePhase.Instance.phase;
        if (currPhase >= minPhase && currPhase <= maxPhase)
        {
            player.character.IsMoving = false;
            StartCoroutine(Play());
        }
    }
}
