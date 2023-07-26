using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ensure the password has been entered correctly in both inputfields and prompt the respective message
public class PasswordMatchIndicator : MonoBehaviour
{
    // init the two password input fields and the text ui element and bool tracker
    public TMP_Text passMatchIndicator;
    public TMP_InputField passwordInput;
    public TMP_InputField confPasswordInput;
    public bool passwordsMatch;

    // Start is called before the first frame update
    private void Start()
    {
        // on value changed event listeners for both inputs
        passwordInput.onValueChanged.AddListener(PassValChanged);
        confPasswordInput.onValueChanged.AddListener(ConfValChanged);
    }

    // the value in the password inputfield has been changed
    private void PassValChanged(string newPass)
    {
        checkPass(); // check password
    }

    // the value in the confirmpassword inputfield has been changed
    private void ConfValChanged(string newConfPass)
    {
        checkPass(); // check confirming password
    }

    // check both input password against eachother and update indicator text
    private void checkPass()
    {
        // Compare the password and confirm password fields
        string password = passwordInput.text;
        string confirmPassword = confPasswordInput.text;

        // match
        if (password == confirmPassword)
        {
            passMatchIndicator.text = "Passwords Match"; // message
            passMatchIndicator.color = Color.green; // text color
        }
        else    // do not match
        {
            passMatchIndicator.text = "Passwords Do Not Match"; // message
            passMatchIndicator.color = Color.red; // text color
        }
    }

    // Function to be called from other scripts to check if passwords match
    public bool ForeignPassCheck()
    {
        string password = passwordInput.text;
        string confPassword = confPasswordInput.text;

        return password == confPassword;
    }
}
