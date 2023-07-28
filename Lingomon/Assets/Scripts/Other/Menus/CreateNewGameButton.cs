using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CreateNewGameButton : MonoBehaviour
{
    // init inputs
    public TMP_InputField gameSaveName;
    public Button createNewGameButton;
    public TMP_Text createNewGameIndicator;
    public TMP_Text loadingMessageIndicator;
    public TMP_Dropdown gameDropdown;
    public SceneManagement sceneManager;
    public float loadDelay = 200f;
    public TMP_Dropdown languageDropdown;

    private bool valid = true;

    // Start is called before the first frame update
    void Start()
    {
        gameSaveName.onValueChanged.AddListener(delegate { UserInputDetected(); });
        languageDropdown.onValueChanged.AddListener(delegate { LanguageDropdownValueChanged(); });

        // init button interactability
        UpdateCreateNewButtonInteractability();
    }

    private void UserInputDetected()
    {
        Debug.Log("User input has been detected...");
        // validate every time that input has been detected
        ValidateNewGameName();
    }

    private void LanguageDropdownValueChanged()
    {
        // Validate the language selection when the dropdown value changes
        if (languageDropdown.value == 0) // Assuming index 0 is "Select a language"
        {
            createNewGameIndicator.text = "Please select a language."; // update the validator text
            createNewGameIndicator.color = Color.red; // text color
            valid = false; // update validity
        }
        else
        {
            // If a language is selected, call ValidateNewGameName to update overall validation
            ValidateNewGameName();
        }
    }

    private void ValidateNewGameName()
    {
        if (string.IsNullOrWhiteSpace(gameSaveName.text) || gameSaveName.text == null || languageDropdown.value == 0)
        {
            createNewGameIndicator.text = "Game name cannot be empty or contain only whitespace."; // update the validator text
            createNewGameIndicator.color = Color.red; // text color
            valid = false; // update validity
        }
        else
        {
            createNewGameIndicator.text = "New game ready to be created."; // update the validator text
            createNewGameIndicator.color = Color.green; // text color
            valid = true; // update validity
        }

        // Update button interactability after validating the game name and language selection
        UpdateCreateNewButtonInteractability();
    }

    private bool UpdateCreateNewButtonInteractability()
    {
        // Combine validation of both game name and language selection
        createNewGameButton.interactable = valid && languageDropdown.value != 0;
        return createNewGameButton.interactable;
    }

    // button is clicked
    public void CreateNewGameButtonIsClicked()
    {
        if (valid)
        {
            // format new dropdown item
            string newItem = $"{gameSaveName.text} - {DateTime.Now.ToString("MM/dd/yyyy")} - Initial";

            // add new dropdown option
            gameDropdown.options.Add(new TMP_Dropdown.OptionData(newItem));

            // refresh the dropdown to show new addition
            gameDropdown.RefreshShownValue();

            // clear the input field 
            gameSaveName.text = string.Empty;

            // reupdate button interactability after new item addition
            UpdateCreateNewButtonInteractability();

            // set a loading message and give a little load time
            LoadingMessageIndicator();

            // open the world 
            sceneManager.TransitionToExperimentScene(); // <-- temporary, replace with call to database when complete.
        } 
        else
        {
            createNewGameIndicator.text = "New game is not ready to be created.";
            createNewGameIndicator.color = Color.red;
        }
    }

    private IEnumerator LoadingMessageIndicator()
    {
        Debug.Log("Delay beginning...");
        loadingMessageIndicator.text = "Loading...";
        Debug.Log("Delay #1 beginning...");

        yield return new WaitForSeconds(loadDelay);

        Debug.Log("Delay #1 completed...");

        loadingMessageIndicator.text = "Successful!";

        Debug.Log("Delay #2 beginning...");

        yield return new WaitForSeconds(loadDelay / 2);

        Debug.Log("Delay #2 completed...");

        Debug.Log("Delay annd scene transition completed...");
    }
}

