using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// the sign in button has been clicked, check the user input, if its a valid user, continue on, else try again
public class SignInButton : MonoBehaviour
{
    // init all the input components, scenemanagement reference, usermanager reference, error message
    public TMP_InputField userID;
    public TMP_InputField password;
    public SceneManagement sceneManager;
    public UserManager userManager;
    public TMP_Text signInErrorIndicator;
    public Button signInButton;
    bool validSignIn = true;

    public string tempUser = "stu123";
    public string tempPass = "asdf";

    // Start is called before the first frame update
    void Start()
    {
        userID.onValueChanged.AddListener(delegate { UserInputDetected(); });
        password.onValueChanged.AddListener(delegate { UserInputDetected(); });
        Debug.Log("Listeners added...");

        // init button interactability
        UpdateSignInButtonInteractability();
    }

    void Update()
    {
        // continually update based on the role selection
        UpdateSignInButtonInteractability();
    }

    private void UserInputDetected()
    {
        Debug.Log("User input has been detected...");
        // validate every time that input has been detected
        ValidateSignInInfo();
    }

    private void ValidateSignInInfo()
    {
        if (string.IsNullOrWhiteSpace(userID.text) || string.IsNullOrWhiteSpace(password.text))
        {
            signInErrorIndicator.text = "All fields are required."; // update the validator text
            signInErrorIndicator.color = Color.red; // text color
            validSignIn = false; // update validity
        }
        else
        {
            signInErrorIndicator.text = "Valid login attempt."; // update the validator text
            signInErrorIndicator.color = Color.green; // text color
            validSignIn = true; // update validity
        }
    }

    private void UpdateSignInButtonInteractability()
    {
        ValidateSignInInfo();
        signInButton.interactable = validSignIn;
    }

    // button has been clicked
    public void SignInButtonHasBeenClicked()
    {
        if (validSignIn)
        {
            // check if user exists
            if (!userManager.DoesUserExist(userID.text, password.text))
            {
                Debug.Log("User does not exist...");
                // prompt error message to tell user to make a valid account
                signInErrorIndicator.text = "User data not found. Please create an account to continue."; // update the validator text
                signInErrorIndicator.color = Color.red; // text color
            }
            else
            {
                // load signed in scene
                sceneManager.TransitionToLoadGameScene();
                Debug.Log("Transitioning scene...");
            }
        }
        else
        {
            signInErrorIndicator.text = "Invalid sign-in attempt. Please try again.";
            signInErrorIndicator.color = Color.red;
        }
    }

}
