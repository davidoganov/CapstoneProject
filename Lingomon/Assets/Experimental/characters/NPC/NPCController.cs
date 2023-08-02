using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    public List<Dialog> dialog;
    Character character;


    void Awake()
    { 
        character = GetComponent<Character>();
    }

    void Update() { 
        character.HandleUpdate();
    }

    public void Interact(Transform initiator) {
        int index = Mathf.Min(dialog.Count - 1, GamePhase.Instance.phase);
        if (dialog.Count > 0)
        {
            if (!dialog[index].IsNurse)
                StartCoroutine(DialogManager.Instance.ShowDialog(dialog[index], character));
            else
                StartCoroutine(DialogManager.Instance.ShowDialog(dialog[index], character, onFinished: () =>
                {
                    GameController.Instance.HealPlayerLingomon();
                }));
            character.LookTowards(initiator.position);
        }
    }
}

public enum NPCState {  Idle, Walking, Dialog}
