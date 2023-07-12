using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] HPBar HPBar;

    Lingomon _lingomon;

    public void SetData(Lingomon lingomon)
    {
        _lingomon = lingomon;
        nameText.text = lingomon.Base.Name;
        HPBar.SetHP((float) lingomon.HP / lingomon.Base.MaxHP);
    }

    public IEnumerator UpdateHP()
    {
        yield return HPBar.SetHPSmooth((float) _lingomon.HP / _lingomon.Base.MaxHP);
    }
}
