using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    
    [SerializeField] bool isPlayerUnit; //determine if it is a player or enemy lingomon, meaning front or back sprite
    [SerializeField] BattleHud hud;

    public BattleHud Hud {
        get { return hud; }
    }

    public Lingomon Lingomon { get; set; }

    Image image;
    Vector3 originalPos;
    Color originalColor;

    public void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public void Setup(Lingomon lingomon)
    {
        Lingomon = lingomon;
        if (isPlayerUnit)
            GetComponent<Image>().sprite = Lingomon.Base.BackSprite;
        else
            GetComponent<Image>().sprite = Lingomon.Base.FrontSprite;

        hud.gameObject.SetActive(true);
        hud.SetData(lingomon);

        PlayEnterAnimation();
    }

    public void Clear ()
    {
        hud.gameObject.SetActive(false);
    }

    public void PlayEnterAnimation()
    {
        image.DOFade(1f, 0.1f);
        if (isPlayerUnit)
            image.transform.localPosition = new Vector3(-500f, originalPos.y);
        else
            image.transform.localPosition = new Vector3(500f, originalPos.y);

        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        if (isPlayerUnit)
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 50, 0.25f));
        else
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x - 50, 0.25f));

        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }

    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.gray, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }

    public void PlayFaintAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 150, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }
}
