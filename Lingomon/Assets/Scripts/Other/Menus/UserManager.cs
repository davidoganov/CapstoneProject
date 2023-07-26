using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    // Create a data structure to store all of the entered user information
    private List<UserData> users = new List<UserData>();

    // create a class to represent each user
    public class UserData
    {
        public string userID;
        public string password;
        public string role;
        public string classID;
    }

    // add user
    public void AddUser(string userID, string password, string role, string classID)
    {
        // Check if the user already exists based on userID and password
        if (!DoesUserExist(userID, password))
        {
            // init all the variables
            UserData user = new UserData
            {
                userID = userID,
                password = password,
                role = role,
                classID = classID
            };

            // add the user to the list of users
            users.Add(user);
        }
    }

    // get a user
    public UserData GetUserByID(string userID)
    {
        return users.Find(user => user.userID == userID);
    }

    // Check if the user exists based on userID and password
    public bool DoesUserExist(string userID, string password)
    {
        return users.Exists(user => user.userID == userID && user.password == password);
    }
}