using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(instance.gameObject); // Make sure the instance persists through scene changes
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

    // store the local save state
    private bool isLocalSaveEnabled = false;

    // enable local save
    public void EnableLocalSave()
    {
        isLocalSaveEnabled = true;
    }

    // disable local save
    public void DisableLocalSave()
    {
        isLocalSaveEnabled = false;
    }

    // check if local save is enabled
    public bool IsLocalSaveEnabled()
    {
        return isLocalSaveEnabled;
    }
  
}