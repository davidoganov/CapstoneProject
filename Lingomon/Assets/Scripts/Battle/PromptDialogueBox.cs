using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptDialogueBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Text dialogueText;

    [SerializeField] GameObject promptBox;

    public void SetDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
    }

    public void EnablePromptBox(bool enable)
    {
        promptBox.SetActive(enable);
    }

    public IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";
        foreach (var letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
    }
    
}
