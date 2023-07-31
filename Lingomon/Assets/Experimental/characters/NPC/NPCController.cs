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
        if (dialog.Count > 0)
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog[Mathf.Min(dialog.Count - 1, GamePhase.Instance.phase)], character));
            character.LookTowards(initiator.position);
        }
    }
}

public enum NPCState {  Idle, Walking, Dialog}
