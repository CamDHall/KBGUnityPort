using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;

    public Color backgroundColor;
    public GameObject registerForm, quizForm, scrollView;
    public Text errorMessage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        errorMessage.gameObject.SetActive(false);
    }

    public void RegisterScene()
    {
        SceneManager.LoadScene("Registeration");
    }

    public void LogInScene()
    {
        SceneManager.LoadScene("login");
    }

    public void Register()
    {
        if(UserManager.Instance._name == null || UserManager.Instance._username == null || UserManager.Instance.email == null ||
            UserManager.Instance.password == null || UserManager.Instance.cPassword == false)
        {
            UIManager.Instance.DisplayError("Please fill out all information.");
        } else
        {
            scrollView.GetComponentInChildren<Image>().color = backgroundColor;

            UserManager.Instance.RegisterUser();
            registerForm.SetActive(false);
            quizForm.SetActive(true);
        }
    }

    public void QuizSubmit()
    {
        if(Utils.FormValid())
        {
            UserManager.Instance.QuizSubmit();
        } else
        {
            DisplayError("Some fields are missing or invalid.");
        }
    }

    public void Value()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        string pn = clicked.transform.parent.name;
        string val = clicked.name;

        if (pn == "Color")
        {
            UserManager.Instance.favoriteColor = val;
        } else if(pn == "Subject")
        {
            UserManager.Instance.subject = val;
        } else if(pn =="Hobby")
        {
            UserManager.Instance.hobby = val;
        } else if(pn == "Likes")
        {
            UserManager.Instance.likes = val;
        } else if(pn == "Quality")
        {
            UserManager.Instance.quality = val;
        } else if(pn == "Gender")
        {
            UserManager.Instance.gender = val;
        } else if(pn == "Age")
        {
            UserManager.Instance.ageGroup = val;
        }
    }

    public void DisplayError(string message)
    {
        errorMessage.gameObject.SetActive(true);

        errorMessage.text = message;
    }

    public void ErrorOff()
    {
        errorMessage.gameObject.SetActive(false);
    }
}
