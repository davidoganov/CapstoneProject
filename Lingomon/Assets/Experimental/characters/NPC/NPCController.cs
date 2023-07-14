using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    public Dialog dialog;

    public void Interact() {
        DialogManager.Instance.ShowDialog(dialog);
        Debug.Log("get ready for trouble!");
    }
}
