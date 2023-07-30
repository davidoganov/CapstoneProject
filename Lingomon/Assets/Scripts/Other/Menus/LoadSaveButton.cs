using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CI.QuickSave;
using TMPro;

// used to load the selected save in the dropdown menu of saves in the LoadSave scene
// takes selected save and loads the scene **SCENE TRANSITION TO BE UPDATED TO NONEXPERIMENTAL SCENE
public class LoadSaveButton : MonoBehaviour
{
    // init all the UI components necessary and the necessary reference instances
    public TMP_Dropdown saves;
    public Button loadSaveButton;
    public SceneManagement sceneManager;

    private void Start()
    {
        saves.onValueChanged.AddListener(delegate { UpdateLoadSaveButtonInteractability(); });
        UpdateLoadSaveButtonInteractability();
    }

    // load the selected save
    public void LoadingSave()
    {
        // check if there are any saved games and if a game is selected
        if (saves.options.Count > 1 && saves.value > 0) 
        {
            // get the selected game from the dropdown
            string selectedGame = saves.options[saves.value].text;

            // extract the UserID from the selectedGame (UserID is the first part of the string before " - ")
            string[] gameInfo = selectedGame.Split(new string[] { " - " }, System.StringSplitOptions.None);
            string userID = gameInfo[0];

            // load corresponding user's game data
            LoadUserData(userID);

            // load the experiment scene or any other scene where the user continues from their saved state
            sceneManager.TransitionToExperimentScene(); // <-- Replace with the appropriate scene transition.
        }
    }

    private void UpdateLoadSaveButtonInteractability()
    {
        loadSaveButton.interactable = saves.options.Count > 1 && saves.value > 0;
    }

    // load the logged in users data
    private void LoadUserData(string userID)
    {
        // load the game data using the QuickSaveReader
        // use unique key for each user
        QuickSaveReader.Create("GameData_" + userID) 
                       .Read<string>("UserID", (userIDValue) => GameManager.Instance.userID = userIDValue)
                       .Read<string>("Password", (password) => GameManager.Instance.password = password)
                       .Read<string>("Nickname", (nickname) => GameManager.Instance.nickname = nickname)
                       .Read<string>("ClassID", (classID) => GameManager.Instance.classID = classID)
                       .Read<double>("SpellingPercentage", (percentage) => GameManager.Instance.spellingPercentage = percentage)
                       .Read<double>("GrammarPercentage", (percentage) => GameManager.Instance.GrammarPercentage = percentage)
                       .Read<double>("DictionPercentage", (percentage) => GameManager.Instance.DictionPercentage = percentage)
                       .Read<double>("ConjugationPercentage", (percentage) => GameManager.Instance.ConjugationPercentage = percentage);
    }
}
