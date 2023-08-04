using UnityEngine;

// used for tracking all game data specific to a user, and utilizing it in every game scene to keep database updated.
public class GameManager : MonoBehaviour
{
    // init the gamemanager instance
    private static GameManager instance;


    public static GameManager Instance
    {
        get
        {
            if (instance == null) // instance does not yet exist
            {
                instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(instance.gameObject); // instance persists through scene changes
            } 
            return instance;
        }
    }

    // data-tracking variables
    public string userID;
    public string password;
    public string nickname; // doesn't need to be stored in the database, only local save
    public string classID;
    public double spellingPercentage;
    public double GrammarPercentage;
    public double DictionPercentage;
    public double ConjugationPercentage;

    // store the local & db save states
    private bool isLocalSaveEnabled = false;
    private bool isDBSaveEnabled = false;

    // enable local save
    public void EnableLocalSave()
    {
        isLocalSaveEnabled = true;
        Debug.Log("Local save enabled.");
    }

    // disable local save
    public void DisableLocalSave()
    {
        isLocalSaveEnabled = false;
        Debug.Log("Local save disabled.");
    }

    // check if local save is enabled
    public bool IsLocalSaveEnabled()
    {
        return isLocalSaveEnabled;
    }

    // enable Database save
    public void EnableDBSave()
    {
        isDBSaveEnabled = true;
        Debug.Log("Database save enabled.");
    }

    // disable Database save
    public void DisableDBSave()
    {
        isDBSaveEnabled = false;
        Debug.Log("Database save disabled.");
    }

    // check if Database save is enabled
    public bool IsDBSaveEnabled()
    {
        return isDBSaveEnabled;
    }
}
