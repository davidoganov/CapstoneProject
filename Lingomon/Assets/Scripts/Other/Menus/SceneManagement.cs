using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagement : MonoBehaviour
{
    public string AccCreationScene = "Acc-Creation-Menu";
    public string LoginMenuScene = "Login-Menu";
    public string LoadGameScene = "Load-Game-Menu";
    public string ExpScene = "Experiment";

    public void TransitionToCreateAccScene()
    {
        Debug.Log("Loading account creation scene...");
        SceneManager.LoadScene(AccCreationScene);
    }

    public void TransitionToLoginMenuScene()
    {
        Debug.Log("Loading login menu scene...");
        SceneManager.LoadScene(LoginMenuScene);
    }

    public void TransitionToLoadGameScene()
    {
        Debug.Log("Loading load game scene...");
        SceneManager.LoadScene(LoadGameScene);
    }

    public void TransitionToExperimentScene()
    {
        Debug.Log("Loading experiment scene...");
        SceneManager.LoadScene(ExpScene);
    }
}

