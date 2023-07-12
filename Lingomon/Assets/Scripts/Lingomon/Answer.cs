using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer
{
    //Use queries to get answers from question
    public AnswerBase Base { get; set; }

    public Answer(AnswerBase _base)
    {
        Base = _base;
    }
}
