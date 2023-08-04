using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

// Manages user saves by storing information locally, when saved, a JSON file is created in the default storage on the provided platform
// Also manages the loading of the saved user data. Accesses the locally saved data file from the default directory
public class SaveManager : MonoBehaviour
{
    // reference to the database controller
    private DatabaseController databaseController; 

    private void Start()
    {
        // init reference to DatabaseController
        databaseController = GetComponent<DatabaseController>(); 
    }

    // saves the game data using the corresponding save key for the user
    public void SaveGame()
    {
        // check save options 
        if (GameManager.Instance.IsLocalSaveEnabled() && GameManager.Instance.IsDBSaveEnabled()) // both local and database
        {
            // save the game data using the QuickSaveWriter
            // write all the game data to the corresponding data field
            QuickSaveWriter.Create(GetSaveKey())
                .Write("UserID", GameManager.Instance.userID)
                .Write("Password", GameManager.Instance.password)
                .Write("Nickname", GameManager.Instance.nickname)
                .Write("ClassID", GameManager.Instance.classID)
                .Write("SpellingPercentage", GameManager.Instance.spellingPercentage)
                .Write("GrammarPercentage", GameManager.Instance.GrammarPercentage)
                .Write("DictionPercentage", GameManager.Instance.DictionPercentage)
                .Write("ConjugationPercentage", GameManager.Instance.ConjugationPercentage)
                .Commit(); // commit the save

            // save the data to the database through the DatabaseController
            databaseController.SaveGameData(GameManager.Instance.userID, GameManager.Instance.password, GameManager.Instance.classID, 
                GameManager.Instance.spellingPercentage, GameManager.Instance.GrammarPercentage, GameManager.Instance.DictionPercentage, GameManager.Instance.ConjugationPercentage);
        } 
        else if (GameManager.Instance.IsLocalSaveEnabled() && !GameManager.Instance.IsDBSaveEnabled()) // local, not database
        {
            // save the game data using the QuickSaveWriter
            // write all the game data to the corresponding data field
            QuickSaveWriter.Create(GetSaveKey())
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
        else if (!GameManager.Instance.IsLocalSaveEnabled() && GameManager.Instance.IsDBSaveEnabled()) // not local, only to database
        {
            // save the data to the database through the DatabaseController
            databaseController.SaveGameData(GameManager.Instance.userID, GameManager.Instance.password, GameManager.Instance.classID,
                GameManager.Instance.spellingPercentage, GameManager.Instance.GrammarPercentage, GameManager.Instance.DictionPercentage, GameManager.Instance.ConjugationPercentage);
        }
        else // both local and database saving are disabled
        {
            // error handle for neither local/database save enabled
            Debug.LogWarning("Both local and database saving are disabled. No save option is enabled.");
        }
    }

    // load the game data using the corresponding save key for the user
    public void LoadGame()
    {
        // check whether local saving has been enabled -- determines where to load game data from 
        if (GameManager.Instance.IsLocalSaveEnabled() && GameManager.Instance.IsDBSaveEnabled()) // both local and database
        {
            // load the game data using the QuickSaveReader with the unique key
            // use lambda expressions to assign loaded data to corresponding data field
            QuickSaveReader.Create(GetSaveKey())
                .Read<string>("UserID", (userID) => GameManager.Instance.userID = userID)
                .Read<string>("Password", (password) => GameManager.Instance.password = password)
                .Read<string>("Nickname", (nickname) => GameManager.Instance.nickname = nickname)
                .Read<string>("ClassID", (classID) => GameManager.Instance.classID = classID)
                .Read<double>("SpellingPercentage", (percentage) => GameManager.Instance.spellingPercentage = percentage)
                .Read<double>("GrammarPercentage", (percentage) => GameManager.Instance.GrammarPercentage = percentage)
                .Read<double>("DictionPercentage", (percentage) => GameManager.Instance.DictionPercentage = percentage)
                .Read<double>("ConjugationPercentage", (percentage) => GameManager.Instance.ConjugationPercentage = percentage);

            // load the data from the database through the DatabaseController
            databaseController.LoadGameData(GameManager.Instance.userID, GameManager.Instance.password);
        }
        else if (GameManager.Instance.IsLocalSaveEnabled() && !GameManager.Instance.IsDBSaveEnabled()) // local, not database
        {
            // load the game data using the QuickSaveReader with the unique key
            // use lambda expressions to assign loaded data to corresponding data field
            QuickSaveReader.Create(GetSaveKey())
                .Read<string>("UserID", (userID) => GameManager.Instance.userID = userID)
                .Read<string>("Password", (password) => GameManager.Instance.password = password)
                .Read<string>("Nickname", (nickname) => GameManager.Instance.nickname = nickname)
                .Read<string>("ClassID", (classID) => GameManager.Instance.classID = classID)
                .Read<double>("SpellingPercentage", (percentage) => GameManager.Instance.spellingPercentage = percentage)
                .Read<double>("GrammarPercentage", (percentage) => GameManager.Instance.GrammarPercentage = percentage)
                .Read<double>("DictionPercentage", (percentage) => GameManager.Instance.DictionPercentage = percentage)
                .Read<double>("ConjugationPercentage", (percentage) => GameManager.Instance.ConjugationPercentage = percentage);
        }
        else if (!GameManager.Instance.IsLocalSaveEnabled() && GameManager.Instance.IsDBSaveEnabled()) // not local, only to database
        {
            // load the data from the database through the DatabaseController
            databaseController.LoadGameData(GameManager.Instance.userID, GameManager.Instance.password);
        }
        else // both local and database saving are disabled
        {
            // error handle for neither local/database save enabled
            Debug.LogWarning("Both local and database saving are disabled. No save option is enabled.");
        }
    }

    // returns a string with the key for the given game instance userID
    private string GetSaveKey()
    {
        return "GameData_" + GameManager.Instance.userID; 
    }

}
