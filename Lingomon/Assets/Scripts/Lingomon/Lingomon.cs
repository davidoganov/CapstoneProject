using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lingomon
{
    public LingomonBase Base { get; set; }

    public int HP { get; set; }
    /* public int maxHP { get; set; } */

    public List<Answer> Answers { get; set; }

    public Lingomon(LingomonBase lBase)
    {
        Base = lBase;
        HP = lBase.MaxHP;

        Answers = new List<Answer>();
        foreach (var answer in Base.PossibleAnswers)
        {
            Answers.Add(new Answer(answer.Base));

            if (Answers.Count >= 4)
                break;
        }
        /* maxHP = lBase.MaxHP; */
    }

    /* public int MaxHP {
        get {
            return maxHP;
        }
    } */

    public bool TakeDamage(Answer answer, int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            return true;
        }

        return false;
    }
}
