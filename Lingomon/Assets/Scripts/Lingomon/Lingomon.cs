using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Lingomon
{
    [SerializeField] LingomonBase _base;

    public LingomonBase Base { 
        get { 
            return _base;
        }    
    }

    public int HP { get; set; }
    /* public int maxHP { get; set; } */

    public List<Answer> Answers { get; set; }

    public List<Question> Questions { get; set; }

    public void Init()
    {
        HP = MaxHP;

        Answers = new List<Answer>();
        foreach (var answer in Base.PossibleAnswers)
        {
            Answers.Add(new Answer(answer.Base));

            if (Answers.Count >= 4)
                break;
        }

        Questions = new List<Question>();
        foreach (var question in Base.PossibleQuestions)
        {
            Questions.Add(new Question(question.Base));

            if (Questions.Count >= 4)
                break;
        }
        /* maxHP = lBase.MaxHP; */
    }

    public int MaxHP {
        get {
            return _base.MaxHP;
        }
    } 

    public bool TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            return true;
        }

        return false;
    }

    public bool CorrectAnswer(Question question, string answer)
    {
        return answer == question.Base.Answers[0];
    }

    public Question GenerateQuestion()
    {
        foreach (Question question in Questions)
        {
            if (!question.IsActive)
            {
                question.IsActive = true;
                return question;
            }
        }
        Questions.ForEach(question => question.IsActive = false);
        Questions[0].IsActive = true;
        return Questions[0];
    }
}
