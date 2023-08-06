using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// manages the functionality of the createnewgame button, updates its interactivity accordingly, initializes new game and adds it to the dropdown, transitioning to new world.
public class CreateNewGameButton : MonoBehaviour
{
    // init inputs and references
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

    // user input detected in the gameSaveName TMP input field
    private void UserInputDetected()
    {
        Debug.Log("User input has been detected...");

        // validate every time that input has been detected
        ValidateNewGameName();
    }

    // user selection changed detected in language dropdown menu
    private void LanguageDropdownValueChanged()
    {
        // validate the language selection when the dropdown value changes
        if (languageDropdown.value == 0) // index 0 is "Select a language"
        {
            // update the validator text
            createNewGameIndicator.text = "Please select a language.";

            // text color
            createNewGameIndicator.color = Color.red;

            // update validity
            valid = false; 
        }
        else // language selection exists, validate game name
        {
            // If a language is selected, call ValidateNewGameName to update overall validation
            ValidateNewGameName();
        }
    }

    // validate the name of the game being created
    private void ValidateNewGameName()
    {
        // make sure the name, language selection options are not null empty or 0
        if (string.IsNullOrWhiteSpace(gameSaveName.text) || gameSaveName.text == null || languageDropdown.value == 0)
        {
            // update the validator text
            createNewGameIndicator.text = "Game name cannot be empty or contain only whitespace.";

            // text color
            createNewGameIndicator.color = Color.red;

            // update validity
            valid = false; 
        }
        else // valid input selection for new game creation
        {
            // update the validator text
            createNewGameIndicator.text = "New game ready to be created.";

            // text color
            createNewGameIndicator.color = Color.green;

            // update validity
            valid = true; 
        }

        // Update button interactability after validating the game name and language selection
        UpdateCreateNewButtonInteractability();
    }

    // update whether the create new game button can be clicked or not
    private bool UpdateCreateNewButtonInteractability()
    {
        // combine validation of both game name and language selection
        createNewGameButton.interactable = valid && languageDropdown.value != 0;

        return createNewGameButton.interactable;
    }

    // loads message to indicate loading process
    private IEnumerator LoadingMessageIndicator()
    {
        Debug.Log("Delay beginning...");

        // adjust text of message indicator
        loadingMessageIndicator.text = "Loading...";
        Debug.Log("Delay #1 beginning...");

        // wait for the delay to finish
        yield return new WaitForSeconds(loadDelay);

        Debug.Log("Delay #1 completed...");

        // adjust text of message indicator
        loadingMessageIndicator.text = "Successful!";

        Debug.Log("Delay #2 beginning...");

        // wait for the delay to finish
        yield return new WaitForSeconds(loadDelay / 2);

        Debug.Log("Delay #2 completed...");

        Debug.Log("Delay annd scene transition completed...");
    }

    // button is clicked
    public void CreateNewGameButtonIsClicked()
    {
        // check input validity needed for new game creation
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

            // transition to the introductory scene of the newly created game
            sceneManager.TransitionToIntroScene(); 
        } 
        else // input invalid for new game creation
        {
            // adjust text of message indicator
            createNewGameIndicator.text = "New game is not ready to be created.";

            // text color
            createNewGameIndicator.color = Color.red;
        }
    }


}

