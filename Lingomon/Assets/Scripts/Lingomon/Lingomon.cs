using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lingomon : MonoBehaviour
{
    public LingomonBase Base { get; set; }

    public int HP { get; set; }
    public int maxHP { get; set; }
    
    public Lingomon(LingomonBase lBase)
    {
        Base = lBase;
        HP = lBase.MaxHP;
        maxHP = lBase.MaxHP;
    }

    public int MaxHP {
        get {
            return maxHP;
        }
    }
}
