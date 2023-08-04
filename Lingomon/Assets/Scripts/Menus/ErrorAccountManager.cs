using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// handle error message displays for errors during account interferences **may be used elsewhere
public class ErrorAccountManager : MonoBehaviour
{
    public static ErrorAccountManager instance { get; private set; } // create a static property 
    public TMP_Text errorMessageText; // text object container
    public float displayDuration = 3f; // display the message for 3 seconds

    // called when init 
    private void Awake()
    {
        // check if instance already exists
        if (instance == null) // doesn't exist 
        {
            // set the current instance
            instance = this; 

            // make sure it can be on multiple scenes 
            DontDestroyOnLoad(gameObject); 

            // hide msg
            HideErrorMessage();
        }
        else // instance already exists -- prevent duplicate
        {
            Destroy(gameObject);
        }
    }

    // hide the error message
    private void HideErrorMessage()
    {
        // toggle the activity of the gameobject to hide it
        gameObject.SetActive(false);
    }

    // display the error message
    public void DisplayErrorMessage(string message)
    {
        // set the message text
        errorMessageText.text = message;

        // toggle the activity of the gameobject to display it
        gameObject.SetActive(true);

        // call the hide error message function after the specified display duration
        Invoke(nameof(HideErrorMessage), displayDuration);
    }

}
