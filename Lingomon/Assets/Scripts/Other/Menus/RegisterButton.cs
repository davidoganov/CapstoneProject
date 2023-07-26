using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// validate all input fields required to registration of a new user properly
public class RegisterButton : MonoBehaviour
{
    // init all the input fields, dropdown, textvalidator, passwordmatchindicator reference, roleUpdate reference, usermanager reference, scenemanager reference
    public TMP_InputField userID;
    public TMP_InputField nickname;
    public TMP_InputField password;
    public TMP_InputField confPass;
    public TMP_Dropdown roles;
    public TMP_InputField classID;
    public TMP_Text inputValidator;
    public PasswordMatchIndicator passMatchIndicator;
    public RoleUpdate roleUpdate;
    public UserManager userManager;
    public SceneManagement sceneManager;
    public Button registerButton;

    // Start is called before the first frame update
    void Start()
    {
        // init event listeners to all inputs when the value is changed 
        userID.onValueChanged.AddListener(delegate { UserInputDetected(); });
        nickname.onValueChanged.AddListener(delegate { UserInputDetected(); });
        password.onValueChanged.AddListener(delegate { UserInputDetected(); });
        confPass.onValueChanged.AddListener(delegate { UserInputDetected(); });
        roles.onValueChanged.AddListener(delegate { UserInputDetected(); });
        classID.onValueChanged.AddListener(delegate { UserInputDetected(); });

        Debug.Log("Listeners added...");

        // validate input fields
        ValidateInputs();
    }

    private void UserInputDetected()
    {
        Debug.Log("User input has been detected...");
        // validate every time that input has been detected
        ValidateInputs();
    }

    private void ValidateInputs()
    {
        Debug.Log("Validating inputs...");

        // init validator checks
        inputValidator.text = "Registration is just a click away!";
        inputValidator.color = Color.green; // text color
        bool valid = true;

        Debug.Log("VALID = " + valid);

        // check all the REQUIRED inputs for registration
        if (string.IsNullOrWhiteSpace(userID.text) || string.IsNullOrWhiteSpace(nickname.text) ||
            string.IsNullOrWhiteSpace(password.text) || string.IsNullOrWhiteSpace(confPass.text))
        {
            Debug.Log("VALID = " + valid);
            Debug.Log("REQUIRED inputs for registration did NOT PASS validation...");

            inputValidator.text = "All fields are required."; // update the validator text
            inputValidator.color = Color.red; // text color
            valid = false; // update validity

            Debug.Log("VALID = " + valid);
        }

        Debug.Log("REQUIRED inputs for registration DID PASS validation...");
        Debug.Log("VALID = " + valid);

        // check role
        if (!roleUpdate.IsClassIDValid())
        {
            Debug.Log("VALID = " + valid);
            Debug.Log("class id did NOT PASS validation...");

            inputValidator.text = "Class ID is required."; // message
            inputValidator.color = Color.red; // text color
            valid = false; // update validity

            Debug.Log("VALID = " + valid);
        }

        Debug.Log("class id DID PASS validation...");
        Debug.Log("VALID = " + valid);

        // check password 
        if (!passMatchIndicator.ForeignPassCheck())
        {
            Debug.Log("VALID = " + valid);
            Debug.Log("password did NOT PASS validation...");

            inputValidator.text = "Passwords do not match."; // message
            inputValidator.color = Color.red; // text color
            valid = false; // update validity

            Debug.Log("VALID = " + valid);
        }

        Debug.Log("password DID PASS validation...");
        Debug.Log("VALID = " + valid);



        // toggle register button enable based on validation 
        registerButton.interactable = valid;
        Debug.Log("Button interactability set...");
        Debug.Log("VALID = " + valid);
    }

    // when register button is clicked function below:
    public void RegisterButtonHasBeenClicked()
    {
        // call usermanager add user and then make new script for scene management and call it here 

        Debug.Log("Register Button has been clicked...");
        userManager.AddUser(userID.text, password.text, roles.options[roles.value].text, classID.text);
        Debug.Log("User has been added...");
        sceneManager.TransitionToLoadGameScene();
        Debug.Log("Scene transitioning...");
    }
}
