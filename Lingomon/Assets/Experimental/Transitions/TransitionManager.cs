using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Image battleSr;
    [SerializeField] float transitionSpeed;

    [SerializeField] Texture2D horizontal;
    [SerializeField] Texture2D vertical;
    [SerializeField] Texture2D battle;
    float transitionProg;

    public static TransitionManager Instance { get; private set; }
    private void Awake() { Instance = this; }

    public IEnumerator enteringHouse()
    {
        sr.material.SetTexture("_TransitionTexture", horizontal);
        transitionProg = 0f;
        sr.material.SetFloat("_CutOff", transitionProg);
        while (sr.material.GetFloat("_CutOff") < 1.1f)
        {
            transitionProg += .01f;
            sr.material.SetFloat("_CutOff", transitionProg);
            yield return new WaitForSeconds(.05f / transitionSpeed);
        }
    }

    public IEnumerator playerSpawning()
    {;
        sr.material.SetTexture("_TransitionTexture", horizontal);
        transitionProg = 1.1f;
        sr.material.SetFloat("_CutOff", transitionProg);
        while (sr.material.GetFloat("_CutOff") > 0f)
        {
            transitionProg -= .01f;
            sr.material.SetFloat("_CutOff", transitionProg);
            yield return new WaitForSeconds(.05f / transitionSpeed);
        }
    }

    public IEnumerator randomEncounter()
    {
        sr.material.SetInt("_Fade", 1);
        transitionProg = 0f;

        sr.material.SetFloat("_CutOff", transitionProg);
        while (sr.material.GetFloat("_CutOff") < 1.1f)
        {
            transitionProg += .04f;
            sr.material.SetFloat("_CutOff", transitionProg);
            yield return new WaitForSeconds(.01f / transitionSpeed);
        }
        while (sr.material.GetFloat("_CutOff") > 0f)
        {
            transitionProg -= .04f;
            sr.material.SetFloat("_CutOff", transitionProg);
            yield return new WaitForSeconds(.01f / transitionSpeed);
        }


        sr.material.SetFloat("_CutOff", transitionProg);
        while (sr.material.GetFloat("_CutOff") < 1.1f)
        {
            transitionProg += .04f;
            sr.material.SetFloat("_CutOff", transitionProg);
            yield return new WaitForSeconds(.01f / transitionSpeed);
        }
        while (sr.material.GetFloat("_CutOff") > 0f)
        {
            transitionProg -= .04f;
            sr.material.SetFloat("_CutOff", transitionProg);
            yield return new WaitForSeconds(.01f / transitionSpeed);
        }
        sr.material.SetInt("_Fade", 0);
    }

    public IEnumerator lingomonBattle1()
    {
        sr.material.SetTexture("_TransitionTexture", battle);
        transitionProg = 0f;
        sr.material.SetFloat("_CutOff", transitionProg);
        while (sr.material.GetFloat("_CutOff") < 1.1f)
        {
            transitionProg += .01f;
            sr.material.SetFloat("_CutOff", transitionProg);
            yield return new WaitForSeconds(.05f / transitionSpeed);
        }
        transitionProg = 0f;
        sr.material.SetFloat("_CutOff", transitionProg);
    }

    public IEnumerator lingomonBattle2()
    {
        transitionProg = 1.1f;
        battleSr.material.SetTexture("_TransitionTexture", vertical);
        battleSr.material.SetFloat("_CutOff", transitionProg);
        while (battleSr.material.GetFloat("_CutOff") > 0f)
        {
            transitionProg -= .01f;
            battleSr.material.SetFloat("_CutOff", transitionProg);
            yield return new WaitForSeconds(.05f / transitionSpeed);
        }

    }

}
