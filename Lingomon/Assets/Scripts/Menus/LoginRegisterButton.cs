using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// the user does not have an account and wishes to create an account;
// the register button has been clicked, switch to the account creation scene.
public class LoginRegisterButton : MonoBehaviour
{
    // init scenemanagement reference
    public SceneManagement sceneManager;

    // init audiomanager reference;
    public string sound = "Click";

    // called when button clicked
    public void LoginRegisterButtonClicked()
    {
        // play button click sound
        AudioManager.instance.Play(sound);

        // transition to the account creation scene
        sceneManager.TransitionToCreateAccScene();
    }
}
