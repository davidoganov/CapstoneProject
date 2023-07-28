using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class APIController : MonoBehaviour
{
    private const string baseURL = "lingomon-api.azurewebsites.net"; // to be replaced


    // Start is called before the first frame update
    IEnumerator Start()
    {
        // MAKE GET REQUESTS

        // Retrieve all end users
        yield return StartCoroutine(GetEndUsers());

        // MAKE POST REQUESTS

        // Create a new end user
        yield return StartCoroutine(CreateEndUser());

        // MAKE PUT REQUESTS

        // Update an existing end user
        yield return StartCoroutine(UpdateEndUser("example_user"));

        // MAKE DELETE REQUESTS

        // Delete an existing end user
        yield return StartCoroutine(DeleteEndUser("example_user"));
    }

    IEnumerator GetEndUsers()
    {
        string url = baseURL + "EndUser";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Failed to get end users: " + www.error);
            }
            else
            {
                Debug.Log("Received end users: " + www.downloadHandler.text);
            }
        }
    }

    IEnumerator CreateEndUser()
    {
        string url = baseURL + "EndUsers";
        WWWForm form = new WWWForm();
        form.AddField("Id", "newuser123"); //add check for whether user is student or not [see create acc scene], add fields for overall completion scores
        form.AddField("ClassId", 1);
        // Add other fields as necessary

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Failed to create end user: " + www.error);
            }
            else
            {
                Debug.Log("End user created successfully");
            }
        }
    }

    IEnumerator UpdateEndUser(string user)
    {
        string url = baseURL + "EndUsers/" + user;
        WWWForm form = new WWWForm();
        form.AddField("ClassId", 2); //add check for whether user is student or not [see create acc scene], add fields for overall completion scores
        // Add other fields as necessary

        using (UnityWebRequest www = UnityWebRequest.Put(url, form.data))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Failed to update end user: " + www.error);
            }
            else
            {
                Debug.Log("End user updated successfully");
            }
        }
    }

    IEnumerator DeleteEndUser(string user)
    {
        string url = baseURL + "EndUsers//" + user;

        using (UnityWebRequest www = UnityWebRequest.Delete(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Failed to delete end user: " + www.error);
            }
            else
            {
                Debug.Log("End user deleted successfully");
            }
        }
    }

    IEnumerator GetLanguage()
    {
        string url = baseURL + "Language/"; // <-- add + "dropdownmenuchoice"; dropdown menu being referred too is in the loadgame scene

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Failed to get end users: " + www.error);
            }
            else
            {
                Debug.Log("Received end users: " + www.downloadHandler.text);
            }
        }
    }
}

