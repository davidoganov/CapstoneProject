using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// check the selection in the role dropdown component, if student is selected, allow interactivity of classID inputfield.
public class RoleUpdate : MonoBehaviour
{
    // init the role dropdown menu, class id input field
    public TMP_Dropdown roles;
    public TMP_InputField classID;

    // start is called before the first frame update
    void Start()
    {
        // init state of class id input field
        UpdateClassIDInteractivity();
    }

    // update is called once per frame
    void Update()
    {
        // continually update based on the role selection
        UpdateClassIDInteractivity();
    }

    public void UpdateClassIDInteractivity()
    {
        // check if the Dropdown component is not null
        if (roles == null)
        {
            Debug.LogError("Component missing..."); // Error check
            return;
        }

        // get current dropdown selection
        string selected = roles.options[roles.value].text;
        Debug.Log("The selected role at the moment is: " + selected);

        // if 'student' role selected, allow the class id input to be editable
        if (selected == "Student")
        {
            classID.interactable = true;
            Debug.Log("classId is now interactable...");
        }
        else
        {
            classID.interactable = false;
            Debug.Log("classId is now NOT interactable...");
        }
    }

    // function to check if the classID is valid based on the selected role
    public bool IsClassIDValid()
    {
        Debug.Log("Calling isclassidvalid from another script...");
        // get current dropdown selection
        string selected = roles.options[roles.value].text;
        Debug.Log("The selected role at the moment is: " + selected);

        // If 'student' role selected, check if the classID is filled
        if (selected == "Student")
        {
            return !string.IsNullOrWhiteSpace(classID.text);
        } 

        // For other roles, classID is not required, so it is considered valid
        return true;
    }
}

