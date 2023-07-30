using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

// Coordinates user saves by storing information locally, when saved, a JSON file is created in the default storage on the provided platform
// Also coordinates the loading of the saved user data. Accesses the locally saved data file from the default directory
public class SaveManager : MonoBehaviour
{
    // Saves the game data using the corresponding save key for the user
    public void SaveGame()
    {
        // use a unique key for each user
        string saveKey = "GameData_" + GameManager.Instance.userID; 

        // check whether local saving has been enabled, this is temporary 
        if (GameManager.Instance.IsLocalSaveEnabled())
        {
            // save the game data using the QuickSaveWriter
            // write all the game data to the corresponding data field
            QuickSaveWriter.Create(saveKey)
                           .Write("UserID", GameManager.Instance.userID)
                           .Write("Password", GameManager.Instance.password)
                           .Write("Nickname", GameManager.Instance.nickname)
                           .Write("ClassID", GameManager.Instance.classID)
                           .Write("SpellingPercentage", GameManager.Instance.spellingPercentage)
                           .Write("GrammarPercentage", GameManager.Instance.GrammarPercentage)
                           .Write("DictionPercentage", GameManager.Instance.DictionPercentage)
                           .Write("ConjugationPercentage", GameManager.Instance.ConjugationPercentage)
                           .Commit(); // commit the save
        }
    }

    // Load the game data using the corresponding save key for the user
    public void LoadGame()
    {
        // use a unique key for each user
        string saveKey = "GameData_" + GameManager.Instance.userID;

        // check whether local saving has been enabled, this is temporary 
        if (GameManager.Instance.IsLocalSaveEnabled())
        {
            // load the game data using the QuickSaveReader with the unique key
            // use lambda expressions to assing loaded data to corresponding data field
            QuickSaveReader.Create(saveKey)
                           .Read<string>("UserID", (userID) => GameManager.Instance.userID = userID)
                           .Read<string>("Password", (password) => GameManager.Instance.password = password)
                           .Read<string>("Nickname", (nickname) => GameManager.Instance.nickname = nickname)
                           .Read<string>("ClassID", (classID) => GameManager.Instance.classID = classID)
                           .Read<double>("SpellingPercentage", (percentage) => GameManager.Instance.spellingPercentage = percentage)
                           .Read<double>("GrammarPercentage", (percentage) => GameManager.Instance.GrammarPercentage = percentage)
                           .Read<double>("DictionPercentage", (percentage) => GameManager.Instance.DictionPercentage = percentage)
                           .Read<double>("ConjugationPercentage", (percentage) => GameManager.Instance.ConjugationPercentage = percentage);
        }
    }
}
