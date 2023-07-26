using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// the register button has been clicked, switch to the account creation scene
public class LoginRegisterButton : MonoBehaviour
{
    // init scenemanagement reference
    public SceneManagement sceneManager;

    // called when button clicked
    public void LoginRegisterButtonClicked()
    {
        // transition to the account creation scene
        sceneManager.TransitionToCreateAccScene();
    }
}
