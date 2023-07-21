using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginMenu : MonoBehaviour
{
    public TMP_InputField userId;
    public TMP_InputField password;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Sign_On(userId, password))
            {
                Debug.Log("Sign_On was true");
                LoadLoggedInMenu();
            }
            else
            {
                Debug.Log("Didn't work!");
            }
        }
    }

    /*the following two lines set a default user to test*/
    public bool Sign_On(TMP_InputField userId, TMP_InputField password)
    {
        if (userId == null || password == null) { return false; } // replace with error message panel activity function

        // check against existing end user in database here
        if (userId.text == "student1234" &&  password.text == "stuPass123") { return true; } else { return false; }
    }

    public void LoadLoggedInMenu()
    {
        SceneManager.LoadScene("InitLogin-Menu-Experimental");
        Debug.Log("Scene transitioning...");
    }

}