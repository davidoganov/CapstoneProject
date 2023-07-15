using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Lingomon/Create new question")]
public class QuestionBase : ScriptableObject
{
    [SerializeField] string prompt;
    [SerializeField] List<string> answers;

    public string Prompt
    {
        get { return prompt; }
    }

    public List<string> Answers
    {
        get { return answers; }
    }
}
