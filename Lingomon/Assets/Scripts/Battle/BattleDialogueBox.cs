using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogueBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;

    [SerializeField] Text dialogueText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;

    [SerializeField] List<Text> actionText;
    [SerializeField] List<Text> moveText;

    public void SetDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
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

    public void EnableDialogueText(bool enabled)
    {
        dialogueText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled)
    {
        moveSelector.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionText.Count; ++i)
        {
            if (i == selectedAction)
                actionText[i].color = highlightedColor;
            else
                actionText[i].color = Color.black;
        }
    }

    public void UpdateMoveSelection(int selectedMove)
    {
        for (int i = 0; i < moveText.Count; ++i)
        {
            if (i == selectedMove)
                moveText[i].color = highlightedColor;
            else
                moveText[i].color = Color.black;
        }
    }

    public void SetAnswerNames(List<Answer> answers)
    {
        List<int> randomIndexes = RandomNumberGenerator.GenerateUniqueRandomNumbers(0, 3);

        for (int i = 0; i < moveText.Count; i++)
        {
            if (i < randomIndexes.Count)
                moveText[i].text = answers[randomIndexes[i]].Base.Name;
            else
                moveText[i].text = "-";
        }
        /* for (int i = 0; i < moveText.Count; ++i)
        {
            if (i < answers.Count)
                moveText[i].text = answers[i].Base.Name;
            else
                moveText[i].text = "-";
        } */
    }

    public List<int> SetAnswerNames(Question question)
    {
        List<string> answers = question.Base.Answers;
        List<int> randomIndexes = RandomNumberGenerator.GenerateUniqueRandomNumbers(0, 3);

        for (int i = 0; i < moveText.Count; i++)
        {
            if (i < randomIndexes.Count)
                moveText[i].text = answers[randomIndexes[i]];
            else
                moveText[i].text = "-";
        }
        return randomIndexes;
        /* for (int i = 0; i < moveText.Count; ++i)
        {
            if (i < answers.Count)
                moveText[i].text = answers[i].Base.Name;
            else
                moveText[i].text = "-";
        } */
    }
}
