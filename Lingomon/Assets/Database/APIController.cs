using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        yield return StartCoroutine(UpdateEndUser());

        // MAKE DELETE REQUESTS

        // Delete an existing end user
        yield return StartCoroutine(DeleteEndUser());
    }

    IEnumerator GetEndUsers()
    {
        string url = baseURL + "EndUsers";
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
        form.AddField("Id", "newuser123");
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

    IEnumerator UpdateEndUser()
    {
        string url = baseURL + "EndUsers/user123";
        WWWForm form = new WWWForm();
        form.AddField("ClassId", 2);
        // Add other fields as necessary

        using (UnityWebRequest www = UnityWebRequest.Put(url, form))
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

    IEnumerator DeleteEndUser()
    {
        string url = baseURL + "EndUsers/user123";

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
}

