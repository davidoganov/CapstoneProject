using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

// makes unity web requests to api that communicates with database to save and load data. 
public class DatabaseController : MonoBehaviour
{
    private const string baseURL = "https://your-api-url.com/"; // replace with our api url

    // save game data to the database
    public void SaveGameData(string userID, string password, string classID, double spellingPercentage, double grammarPercentage, double dictionPercentage, double conjugationPercentage)
    {
        StartCoroutine(SaveGameDataToDatabase(userID, password, classID, spellingPercentage, grammarPercentage, dictionPercentage, conjugationPercentage));
    }

    // coroutine to send POST request
    private IEnumerator SaveGameDataToDatabase(string userID, string password, string classID, double spellingPercentage, double grammarPercentage, double dictionPercentage, double conjugationPercentage)
    {
        // API endpoint for saving game data for specific user
        string url = baseURL + "EndUser/" + userID; // replace the enduser with the api endpoint for it

        // create a WWWForm to collect data for the POST request
        WWWForm form = new WWWForm();
        form.AddField("UserID", userID);
        form.AddField("Password", password);
        form.AddField("ClassID", classID);
        form.AddField("SpellingPercentage", spellingPercentage.ToString());
        form.AddField("GrammarPercentage", grammarPercentage.ToString());
        form.AddField("DictionPercentage", dictionPercentage.ToString());
        form.AddField("ConjugationPercentage", conjugationPercentage.ToString());

        // send the POST request
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Failed to save game data: " + www.error);
            }
            else
            {
                Debug.Log("Game data saved successfully for user: " + userID);
            }
        }
    }


    // load game data from the database
    public void LoadGameData(string userID, string password)
    {
        StartCoroutine(LoadGameDataFromDatabase(userID, password));
    }

    // coroutine to send a GET request 
    private IEnumerator LoadGameDataFromDatabase(string userID, string password)
    {
        // replace the endusers with the api endpoint for it
        string url = baseURL + "EndUsers?UserID=" + userID + "&Password=" + password; 

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Failed to get end user: " + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                // parse the JSON response
                // deserialize the JSON response into a data class
                UserData userData = JsonUtility.FromJson<UserData>(jsonResponse);

                // update the users game data
                UpdateGameData(userData);
            }
        }
    }

    // get all already created users from the database
    public IEnumerator GetAllUsers(Action<List<UserManager.UserData>> callback)
    {
        yield return StartCoroutine(GetAllUsersFromDB(callback));
    }

    // coroutine to perform GET request to retrieve all users 
    private IEnumerator GetAllUsersFromDB(Action<List<UserManager.UserData>> callback)
    {
        // replace the endusers with the api endpoint for it
        string url = baseURL + "EndUser";

        // perform Unity web request
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            // wait for the request to send
            yield return www.SendWebRequest();

            // error handle 
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Failed to retrieve users from the database:" + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                // parse the JSON response
                // deserialize the JSON response into a list of UserData objects
                List<UserManager.UserData> dbUsers = JsonUtility.FromJson<List<UserManager.UserData>>(jsonResponse);

                // return the list of users to UserManager
                callback?.Invoke(dbUsers);
            }
        }
    }

    // data class to match JSON response
    [System.Serializable]
    private class UserData
    {
        public string userID;
        public string password;
        public string classID;
        public double spellingPercentage;
        public double grammarPercentage;
        public double dictionPercentage;
        public double conjugationPercentage;
    }

    // update the game data with the retrieved data from the database
    private void UpdateGameData(UserData userData)
    {
        GameManager.Instance.userID = userData.userID;
        GameManager.Instance.password = userData.password;
        GameManager.Instance.classID = userData.classID;
        GameManager.Instance.spellingPercentage = userData.spellingPercentage;
        GameManager.Instance.GrammarPercentage = userData.grammarPercentage;
        GameManager.Instance.DictionPercentage = userData.dictionPercentage;
        GameManager.Instance.ConjugationPercentage = userData.conjugationPercentage;
    }

}
