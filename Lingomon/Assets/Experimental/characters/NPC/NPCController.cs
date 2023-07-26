using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    public Dialog dialog;
    [SerializeField] bool hasDialog;
    Character character;

    void Awake()
    { 
        character = GetComponent<Character>();
    }

    void Update() { 
        character.HandleUpdate();
    }

    public void Interact(Transform initiator) {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, character.Animator));
        character.LookTowards(initiator.position);
    }
}

public enum NPCState {  Idle, Walking, Dialog}
