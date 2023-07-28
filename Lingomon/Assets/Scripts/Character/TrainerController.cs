using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerController : MonoBehaviour, Interactable
{
    [SerializeField] string name;
    [SerializeField] Sprite sprite;
    [SerializeField] Dialog dialog;
    Character character;

    void Awake()
    { 
        character = GetComponent<Character>();
    }

    public void Interact(Transform initiator)
    {
        character.LookTowards(initiator.position);
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, character.Animator, onFinished:() =>
        {
            GameController.Instance.StartTrainerBattle(this);
        }));
    }

    public IEnumerator TriggerTrainerBattle()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, character.Animator, () =>
        {
            GameController.Instance.StartTrainerBattle(this);
        }));
    }

    public string Name
    {
        get => name;
    }

    public Sprite Sprite
    {
        get => sprite;
    }
}
