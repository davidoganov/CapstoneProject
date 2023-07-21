using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongGrass : MonoBehaviour, IPlayerTriggerable
{
    public void OnPlayerTriggered(PlayerController player)
    {
        if (UnityEngine.Random.Range(1, 101) <= 10)
        {
            StartCoroutine(encounterTransition());
        }
    }

    IEnumerator encounterTransition() {
        GameController.Instance.PauseGame(true);
        yield return StartCoroutine(TransitionManager.Instance.randomEncounter());
        yield return StartCoroutine(TransitionManager.Instance.lingomonBattle1());
        StartCoroutine(TransitionManager.Instance.lingomonBattle2());
        GameController.Instance.PauseGame(false);
        GameController.Instance.StartBattle();
    }
}
