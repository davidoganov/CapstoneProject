using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// creates a data structure that maintains a list of users, if db saving is enabled, existing users are loaded to the list at load time.
public class UserManager : MonoBehaviour
{
    // init a data structure to store all of the entered user information
    private List<UserData> users = new List<UserData>();

    // reference to databasecontroller
    private DatabaseController dbController;

    private void Start()
    {
        // init the DatabaseController reference
        dbController = GetComponent<DatabaseController>();

        // check to make sure database connection is enabled
        if (GameManager.Instance.IsDBSaveEnabled())
        {
            // Load the users from the database
            GetDBUsers();
        }

    }

    // create a class to represent each user
    public class UserData
    {
        public string userID;
        public string password;
        public string role;
        public string classID;
        public double spellingPercentage;
        public double grammarPercentage;
        public double dictionPercentage;
        public double conjugationPercentage;
    }

    // add user with the default game data
    public void AddUser(string userID, string password, string role, string classID)
    {
        // check if the user already exists based on userID and password
        if (!DoesUserExist(userID, password))
        {
            // init all the variables
            UserData newUser = new UserData
            {
                // user inputs
                userID = userID,
                password = password,
                role = role,
                classID = classID,

                // default 
                spellingPercentage = 0.0,
                grammarPercentage = 0.0,
                dictionPercentage = 0.0,
                conjugationPercentage = 0.0
            };

            // add the user to the list of users
            users.Add(newUser);
        }
    }

    // get a user
    public UserData GetUserByID(string userID)
    {
        return users.Find(user => user.userID == userID);
    }

    // check if the user exists based on userID and password
    public bool DoesUserExist(string userID, string password)
    {
        return users.Exists(user => user.userID == userID && user.password == password);
    }

    // retrieve all the users in the database by calling the coroutine
    private void GetDBUsers()
    {
        // call the coroutine 
        StartCoroutine(GetDBUsersCoroutine());
    }

    // coroutine to retrieve any created users already in the database, and add them to the list of users
    private IEnumerator GetDBUsersCoroutine()
    {
        // get all the users from the database
        dbController.GetAllUsers(dbUsers =>
        {
            // add them to the user list
            users.AddRange(dbUsers);
        });

        yield return null;
    }
}