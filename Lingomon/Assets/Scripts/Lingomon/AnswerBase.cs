using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Answer", menuName = "Lingomon/Create new answer")]
public class AnswerBase : ScriptableObject
{
    [SerializeField] string name;

    public string Name {
        get { return name; }
    }
}
