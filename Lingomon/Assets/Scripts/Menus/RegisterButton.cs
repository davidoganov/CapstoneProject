using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// validate all input fields required to registration of a new user properly
public class RegisterButton : MonoBehaviour
{
    // init all the input fields, dropdown, textvalidator,
    // passwordmatchindicator reference, roleUpdate reference,
    // usermanager reference, scenemanager reference, errorAccountManager reference
    public TMP_InputField userID;
    public TMP_InputField nickname;
    public TMP_InputField password;
    public TMP_InputField confPass;
    public TMP_Dropdown roles;
    public TMP_InputField classID;
    public TMP_Text inputValidator;
    public PassMatchIndicator passMatchIndicator;
    public RoleUpdate roleUpdate;
    public UserManager userManager;
    public SceneManagement sceneManager;
    public Button registerButton;
    public ErrorAccountManager errorAccountManager;
    public string sound = "Click";

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

        // check userManager null
        if (userManager == null) 
        {
            Debug.LogError("UserManager is not assigned to RegisterButton.");
            return;
        }

        // check errorAccountManager null
        if (ErrorAccountManager.instance == null)
        {
            Debug.LogError("ErrorAccountManager.instance is not assigned.");
            return;
        }

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
        // play button click sound
        AudioManager.instance.Play(sound);

        Debug.Log("Register Button has been clicked...");

        // error handle for user already exists with provided info
        if (userManager.DoesUserExist(userID.text, password.text)) // user exists
        {
            Debug.Log("User already exists. Registration with provided credentials is not possible.");

            // display an error message to the user 
            if (ErrorAccountManager.instance != null)
            {
                ErrorAccountManager.instance.DisplayErrorMessage("Invalid registration data. User already exists.");
            }
        }
        else // user does NOT exist
        {
            // add the user to the usermanager list
            userManager.AddUser(userID.text, password.text, roles.options[roles.value].text, classID.text);
            Debug.Log("User has been added...");

            // check to see if database saving has been enabled
            if (GameManager.Instance.IsDBSaveEnabled()) // database saving enabled
            {
                // init the user region completion percentages
                double spellingPercentage = 0.0;
                double grammarPercentage = 0.0;
                double dictionPercentage = 0.0;
                double conjugationPercentage = 0.0;

                // call database controller to save the newly created user
             //   DatabaseController dbController = GetComponent<DatabaseController>();
               // dbController.SaveGameData(userID.text, password.text, classID.text, spellingPercentage, grammarPercentage,
                //    dictionPercentage, conjugationPercentage);
                Debug.Log("A new user has been added to the database.");
            }
            else // database saving disabled
            {
                Debug.Log("Database saving is disabled. To add to the database, enable the database saving option.");
            }

            // transition to the create/load game menu
            sceneManager.TransitionToLoadGameScene();
            Debug.Log("Scene transitioning to create/load game menu scene...");
        }
    }
}
