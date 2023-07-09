using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] HPBar HPBar;

    public void SetData(Lingomon lingomon)
    {
        nameText.text = lingomon.Base.Name;
        HPBar.setHP((float) lingomon.HP / lingomon.Base.MaxHP);
    }
}
