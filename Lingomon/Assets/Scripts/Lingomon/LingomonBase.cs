using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lingomon", menuName = "Lingomon/Create new lingomon")]
public class LingomonBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] LingomonType type;

    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] List<PossibleQuestion> possibleQuestions;

    public string Name {
        get { return name; }
    }

    public string Description {
        get { return description; }
    }

    public Sprite FrontSprite {
        get { return frontSprite; }
    }
    
    public Sprite BackSprite {
        get { return backSprite; }
    }

    public LingomonType Type {
        get { return type; }
    }

    public int MaxHP {
        get { return maxHP; }
    }

    public List<PossibleQuestion> PossibleQuestions
    {
        get { return possibleQuestions; }
    }
}

[System.Serializable]
public class PossibleQuestion
{
    [SerializeField] QuestionBase questionBase;

    public QuestionBase Base
    {
        get { return questionBase; }
    }
}

public enum LingomonType
{
    None,
    Grammar,
    Conjugation
}