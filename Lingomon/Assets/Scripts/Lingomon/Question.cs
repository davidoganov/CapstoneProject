using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{

    public QuestionBase Base { get; set; }

    public bool IsActive { get; set; }

    public Question(QuestionBase _base)
    {
        Base = _base;
        IsActive = false;
    }
}
