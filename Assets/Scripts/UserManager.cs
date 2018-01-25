using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserManager : MonoBehaviour {
    public static UserManager Instance;
    Firebase.Auth.FirebaseAuth auth;
    FirebaseUser user;
    FirebaseDatabase db;
    public Text errorMessage;

    public InputField userNameField, passwordField;

    private void Awake()
    {
        Instance = this;
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://kids-break-ground.firebaseio.com");
        db = Firebase.Database.FirebaseDatabase.DefaultInstance;
        // Get the root reference location of the database.

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    private void Start()
    {
        errorMessage.gameObject.SetActive(false);
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    public void SignInAttempt()
    {
        errorMessage.gameObject.SetActive(false);
        string username = userNameField.text;
        string password = passwordField.text;

        if(username == "" || username.Length < 3)
        {
            DisplayError("Please provide a valid username.");
            return;
        } else if(password == "" || password.Length < 6)
        {
            DisplayError("Please provide a valid password");
            return;
        }

        db.GetReference("users").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.Log(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot user in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary)user.Value;

                    if(dictUser["username"].ToString() == username)
                    {
                        string email = dictUser["email"].ToString();
                        LogIn(email, password);
                    }
                }
            }
        });
    }

    void LogIn(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                DisplayError("Invalid Password. Try Again or Press Forgot Password.");
                return;
            }

            user = task.Result;
            
        });
    }

    void DisplayError(string message)
    {
        errorMessage.gameObject.SetActive(true);

        errorMessage.text = message;
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        /*if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                DebugLog("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                DebugLog("Signed in " + user.UserId);
                displayName = user.DisplayName ?? "";
                emailAddress = user.Email ?? "";
                photoUrl = user.PhotoUrl ?? "";
            }
        }*/
    }
}
