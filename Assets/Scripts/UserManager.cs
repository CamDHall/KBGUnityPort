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
    public IDictionary _data = new Dictionary<string, string>();
    FirebaseDatabase db;
    DataSnapshot snapshot;

    public InputField userNameField, passwordField;
    public string gender, ageGroup, _name, _username, email, password; // Registeration
    public string favoriteColor, subject, hobby, likes, quality; // Quiz

    public bool cPassword = false;
    bool loading = false;

    GameObject canvas;
    GameObject img;
    public GameObject avatarObj;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://kids-break-ground.firebaseio.com");
        db = Firebase.Database.FirebaseDatabase.DefaultInstance;
        // Get the root reference location of the database.

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    private void Update()
    {
        if (img != null)
        {
            if (!loading && img.activeSelf)
            {
                img.SetActive(false);
            }

            if (loading && !img.activeSelf)
            {
                img.SetActive(true);
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        loading = false;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public void SignInAttempt()
    {

        UIManager.Instance.errorMessage.gameObject.SetActive(false);
        string username = userNameField.text;
        string password = passwordField.text;

        if(username == "" || username.Length < 3)
        {
            loading = false;

            UIManager.Instance.DisplayError("Please provide a valid username.");
            return;
        } else if(password == "" || password.Length < 6)
        {
            UIManager.Instance.DisplayError("Please provide a valid password");
            password = null;
            return;
        }

        if (!loading)
        {
            img = Instantiate(Resources.Load("LoadingImage") as GameObject);
            img.transform.SetParent(canvas.transform, false);
            loading = true;
        }

        db.GetReference("users").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.Log("SIGN IN ERROR");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                bool found = false;

                foreach (DataSnapshot user in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary)user.Value;

                    if(dictUser["username"].ToString() == username)
                    {
                        found = true;
                        string email = dictUser["email"].ToString();
                        LogIn(email, password);
                    }
                }
                if (!found)
                {
                    UIManager.Instance.DisplayError("Username not found.");
                    loading = false;
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
                loading = false;
                UIManager.Instance.DisplayError("Invalid Password. Try Again or Press Forgot Password.");
                return;
            }

            user = task.Result;
            GetUserData();
            
            SceneManager.LoadScene("AvatarScene");
        });
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

                    if (dictUser["username"].ToString() == uname && SceneManager.GetActiveScene().ToString() == "Registeration")
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
                UIManager.Instance.DisplayError("Sorry there was an unexpected error. Please try again later.");
                return;
            }
            if (task.IsFaulted)
            {
                UIManager.Instance.DisplayError("Username/Password not found.");
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

        db.GetReference("users").Child(user.UserId).Child("skinColor").SetValueAsync("RGBA(1.00, 0.803, 0.580, 1.00)");
        db.GetReference("users").Child(user.UserId).Child("hairColor").SetValueAsync("RGBA(0.00, 0.00, 0.00, 1.00)");
        db.GetReference("users").Child(user.UserId).Child("colorHatPrime").SetValueAsync("RGBA(0.933, 0.293, 0.341, 1.00)");
        db.GetReference("users").Child(user.UserId).Child("colorClothPrime").SetValueAsync("RGBA(0.933, 0.293, 0.341, 1.00)");

        db.GetReference("users").Child(user.UserId).Child("door").SetValueAsync("door");
        db.GetReference("users").Child(user.UserId).Child("roof").SetValueAsync("roof1");
        db.GetReference("users").Child(user.UserId).Child("wall").SetValueAsync("wallstyle1");

        db.GetReference("users").Child(user.UserId).Child("coins").SetValueAsync(0);

        //
        // Local
        //

        _data["skinColor"] = "RGBA(1.00, 0.803, 0.580, 1.00)";
        _data["hairColor"] = "RGBA(0.00, 0.00, 0.00, 1.00)";
        _data["colorHatPrime"] = "RGBA(0.933, 0.293, 0.341)";
        _data["colorClothPrime"] = "RGBA(0.933, 0.293, 0.341)";
        _data["gender"] = gender;

        // House
        _data["door"] = "door";
        _data["roof"] = "roof1";
        _data["wall"] = "wallstyle1";

        // Coin and qualities
        _data["coins"] = "0";

    }

    public void LogOut()
    {
        foreach(string key in _data.Keys)
        {
            db.GetReference("users").Child(user.UserId).Child(key).SetValueAsync(_data[key]);
        }
        auth.SignOut();
    }

    public void UpdateUserData(string key, string val)
    {
        _data[key] = val;
        db.GetReference("users").Child(user.UserId).Child(key).SetValueAsync(val);
    }

    public void UpdateUserData(string key, int val)
    {
        if (key == "coins")
        {
            int newValue = int.Parse(_data["coins"].ToString()) + val;
            _data[key] = newValue;
            db.GetReference("users").Child(user.UserId).Child(key).SetValueAsync(val);
        }
    }

    public void GetUserData()
    {
        db.GetReference("users").Child(user.UserId).GetValueAsync().ContinueWith(t =>
        {
            if(t.IsCompleted)
            {
                snapshot = t.Result;
            }
        });

        _data = (IDictionary)snapshot.Value;
        gender = _data["gender"].ToString();
    }

    public void AddItem(string item)
    {
        db.GetReference("users").Child(user.UserId).Child(item).SetValueAsync(item);
    }
}
