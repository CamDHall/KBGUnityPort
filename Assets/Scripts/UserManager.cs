﻿using System.Collections;
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

    public InputField userNameField, passwordField;
    public string gender, ageGroup, _name, _username, email, password; // Registeration
    public string favoriteColor, subject, hobby, likes, quality; // Quiz

    public bool cPassword = false;

    private void Awake()
    {
        Instance = this;
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://kids-break-ground.firebaseio.com");
        db = Firebase.Database.FirebaseDatabase.DefaultInstance;
        // Get the root reference location of the database.

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    public void SignInAttempt()
    {
        UIManager.Instance.errorMessage.gameObject.SetActive(false);
        string username = userNameField.text;
        string password = passwordField.text;

        if(username == "" || username.Length < 3)
        {
            UIManager.Instance.DisplayError("Please provide a valid username.");
            return;
        } else if(password == "" || password.Length < 6)
        {
            UIManager.Instance.DisplayError("Please provide a valid password");
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
                UIManager.Instance.DisplayError("Invalid Password. Try Again or Press Forgot Password.");
                return;
            }

            user = task.Result;
            
        });
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

    public void Username(string uname)
    {
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

                    if (dictUser["username"].ToString() == uname)
                    {
                        UIManager.Instance.DisplayError("Username already exist.");
                        return;
                    }
                }
            }
        });

        _username = uname;
    }

    public void RegisterUser()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            user = task.Result;
            db.GetReference("users").Child(user.UserId).Child("username").SetValueAsync(_username);
            db.GetReference("users").Child(user.UserId).Child("name").SetValueAsync(_name);
            db.GetReference("users").Child(user.UserId).Child("email").SetValueAsync(email);
            db.GetReference("users").Child(user.UserId).Child("password").SetValueAsync(password);
            db.GetReference("users").Child(user.UserId).Child("gender").SetValueAsync(gender);
            db.GetReference("users").Child(user.UserId).Child("age").SetValueAsync(ageGroup);
        });
    }

    public void QuizSubmit()
    {
        db.GetReference("users").Child(user.UserId).Child("color").SetValueAsync(favoriteColor);
        db.GetReference("users").Child(user.UserId).Child("subject").SetValueAsync(subject);
        db.GetReference("users").Child(user.UserId).Child("hobby").SetValueAsync(hobby);
        db.GetReference("users").Child(user.UserId).Child("likes").SetValueAsync(likes);
        db.GetReference("users").Child(user.UserId).Child("quality").SetValueAsync(quality);
    }
}