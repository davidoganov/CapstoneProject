using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldController : MonoBehaviour
{
    public TMP_Dropdown roleDropdown;
    public TMP_InputField studentInputField;

    // Start is called before the first frame update
    void Start()
    {
        // set up the initial state of the input field
        UpdateInputFieldInteractivity();
    }

    // Update is called once per frame
    void Update()
    {
        // let it continually update based on the selected dropdown menu
        UpdateInputFieldInteractivity();
    }

    void UpdateInputFieldInteractivity()
    {

        // Check if the Dropdown component is not null
        if (roleDropdown == null)
        {
            Debug.LogError("Dropdown component is missing. Please assign the Dropdown in the Inspector.");
            return;
        }

        // get the current dropdown option selected
        string roleSelected = roleDropdown.options[roleDropdown.value].text;

        // if 'student' role selected, allow the input field to be editable
        if (roleSelected == "Student")
        {
            studentInputField.interactable = true;
        }
        else
        {
            studentInputField.interactable = false;
        }
    }
}
