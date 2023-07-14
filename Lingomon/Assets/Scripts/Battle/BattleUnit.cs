using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    
    [SerializeField] bool isPlayerUnit; //determine if it is a player or enemy lingomon, meaning front or back sprite
    [SerializeField] BattleHud hud;

    public BattleHud Hud {
        get { return hud; }
    }

    public Lingomon Lingomon { get; set; }

    public void Setup(Lingomon lingomon)
    {
        Lingomon = lingomon;
        if (isPlayerUnit)
            GetComponent<Image>().sprite = Lingomon.Base.BackSprite;
        else
            GetComponent<Image>().sprite = Lingomon.Base.FrontSprite;

        hud.gameObject.SetActive(true);
        hud.SetData(lingomon);
    }

    public void Clear ()
    {
        hud.gameObject.SetActive(false);
    }
}
