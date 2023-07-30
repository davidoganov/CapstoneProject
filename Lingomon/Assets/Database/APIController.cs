//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.Networking;

//public class APIController : MonoBehaviour
//{
//    private const string baseURL = "lingomon-api.azurewebsites.net"; // to be replaced


//    // Start is called before the first frame update
//    IEnumerator Start()
//    {
//        // MAKE GET REQUESTS

//        // Retrieve all end users
//        yield return StartCoroutine(GetEndUsers());

//        // MAKE POST REQUESTS

//        // Create a new end user
//        yield return StartCoroutine(CreateEndUser());

//        // MAKE PUT REQUESTS

//        // Update an existing end user
//        yield return StartCoroutine(UpdateEndUser("example_user"));

//        // MAKE DELETE REQUESTS

//        // Delete an existing end user
//        yield return StartCoroutine(DeleteEndUser("example_user"));
//    }

//    IEnumerator GetEndUsers()
//    {
//        string url = baseURL + "EndUser";
//        using (UnityWebRequest www = UnityWebRequest.Get(url))
//        {
//            yield return www.SendWebRequest();

//            if (www.result != UnityWebRequest.Result.Success)
//            {
//                Debug.Log("Failed to get end users: " + www.error);
//            }
//            else
//            {
//                Debug.Log("Received end users: " + www.downloadHandler.text);
//            }
//        }
//    }

//    IEnumerator CreateEndUser()
//    {
//        string url = baseURL + "EndUsers";
//        WWWForm form = new WWWForm();
//        form.AddField("UserID", GameManager.Instance.userID);
//        form.AddField("Password", GameManager.Instance.password);
//        form.AddField("ClassID", GameManager.Instance.classID);
//        form.AddField("SpellingPercentage", GameManager.Instance.spellingPercentage);
//        form.AddField("GrammarPercentage", GameManager.Instance.GrammarPercentage);
//        form.AddField("DictionPercentage", GameManager.Instance.DictionPercentage);
//        form.AddField("ConjugationPercentage", GameManager.Instance.ConjugationPercentage);

//        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
//        {
//            yield return www.SendWebRequest();

//            if (www.result != UnityWebRequest.Result.Success)
//            {
//                Debug.Log("Failed to create end user: " + www.error);
//            }
//            else
//            {
//                Debug.Log("End user created successfully");
//            }
//        }
//    }

//    IEnumerator UpdateEndUser(string user)
//    {
//        string url = baseURL + "EndUsers/" + user;

//        // create a dictionary to represent the user data to be updated
//        // all of the values are taken from GameManager.instance variables
//        Dictionary<string, object> userData = new Dictionary<string, object>
//        {
//            { "UserID", GameManager.Instance.userID },
//            { "Password", GameManager.Instance.password },
//            { "ClassID", GameManager.Instance.classID },
//            { "SpellingPercentage", GameManager.Instance.spellingPercentage },
//            { "GrammarPercentage", GameManager.Instance.GrammarPercentage },
//            { "DictionPercentage", GameManager.Instance.DictionPercentage },
//            { "ConjugationPercentage", GameManager.Instance.ConjugationPercentage }
//        };

//        // convert to JSON formatted string for data interchange
//        string jsonData = JsonUtility.ToJson(userData);

//        // update 
//        using (UnityWebRequest www = UnityWebRequest.Put(url, jsonData))
//        {
//            www.SetRequestHeader("Content-Type", "application/json"); // contenttype is set to json 
//            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData)); // this is the uploadhandlerraw that uploads the data to the server

//            yield return www.SendWebRequest(); // wait for it to finish

//            // response handle
//            if (www.result != UnityWebRequest.Result.Success)
//            {
//                Debug.Log("Failed to update end user: " + www.error);
//            }
//            else
//            {
//                Debug.Log("End user updated successfully");
//            }
//        }
//    }

//    IEnumerator DeleteEndUser(string user)
//    {
//        string url = baseURL + "EndUsers//" + user;

//        using (UnityWebRequest www = UnityWebRequest.Delete(url))
//        {
//            yield return www.SendWebRequest();

//            if (www.result != UnityWebRequest.Result.Success)
//            {
//                Debug.Log("Failed to delete end user: " + www.error);
//            }
//            else
//            {
//                Debug.Log("End user deleted successfully");
//            }
//        }
//    }

//    IEnumerator GetLanguage()
//    {
//        string url = baseURL + "Language/"; // <-- add + "dropdownmenuchoice"; dropdown menu being referred too is in the loadgame scene

//        using (UnityWebRequest www = UnityWebRequest.Get(url))
//        {
//            yield return www.SendWebRequest();

//            if (www.result != UnityWebRequest.Result.Success)
//            {
//                Debug.Log("Failed to get end users: " + www.error);
//            }
//            else
//            {
//                Debug.Log("Received end users: " + www.downloadHandler.text);
//            }
//        }
//    }
//}

