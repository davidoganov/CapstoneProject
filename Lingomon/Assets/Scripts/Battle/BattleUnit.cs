using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] LingomonBase _base;
    [SerializeField] bool isPlayerUnit; //determine if it is a player or enemy lingomon, meaning front or back sprite

    public Lingomon Lingomon { get; set; }

    public void Setup()
    {
        Lingomon = new Lingomon(_base);
        if (isPlayerUnit)
            GetComponent<Image>().sprite = Lingomon.Base.BackSprite;
        else
            GetComponent<Image>().sprite = Lingomon.Base.FrontSprite;
    }
}
