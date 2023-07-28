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

    // Add your data-tracking variables here
    // For example:
    //public int score;
    //public bool isGameStarted;

    // Other methods and functionality can be added here
}