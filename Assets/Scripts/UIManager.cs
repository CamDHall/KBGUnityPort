﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;

    public Color backgroundColor;
    public GameObject registerForm, quizForm, scrollView;
    public Text errorMessage;
    Dictionary<Image, Color> colorOptions = new Dictionary<Image, Color>();

    private void Awake()
    {
        Instance = this;

        if (SceneManager.GetActiveScene().name == "Registeration")
        {
            GameObject colors = GameObject.FindGameObjectWithTag("Color");

            Image[] colorOptionBtns = colors.GetComponentsInChildren<Image>();

            foreach (Image img in colorOptionBtns)
            {
                colorOptions[img] = img.color;
            }
        }
    }

    private void Start()
    {
        if (errorMessage != null)
        {
            errorMessage.gameObject.SetActive(false);
            if (quizForm != null)
                quizForm.gameObject.SetActive(false);
        }
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
            ErrorOff();
            quizForm.SetActive(true);
        }
    }

    public void QuizSubmit()
    {
        if(Utils.FormValid())
        {
            UserManager.Instance.QuizSubmit();
            SceneManager.LoadScene("AvatarScene");
        } else
        {
            DisplayError("Some fields are missing or invalid.");
        }
    }

    public void Value()
    {
        UIManager.Instance.ErrorOff();
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        string pn = clicked.transform.parent.name;
        string val = clicked.name;

        CollapseSection cs;

        if (clicked.transform.parent.parent.name == "QuizForm")
        {
            cs = clicked.transform.parent.GetComponent<CollapseSection>();
        }
        else
        {
            cs = clicked.transform.parent.parent.GetComponent<CollapseSection>();
        }
        
        Image btn = clicked.GetComponent<Image>();

        if (SceneManager.GetActiveScene().name != "login")
        {
            // For default buttons
            if (pn != "Color")
            {
                // Colors
                List<Image> siblings = siblings = clicked.transform.parent.GetComponentsInChildren<Image>().ToList();

                foreach (Image sibling in siblings)
                {
                    if (sibling != btn)
                    {
                        sibling.color = btn.color;
                    }
                }
            }
            else
            {
                // Set all buttons to original color besides current button
                foreach (Image option in colorOptions.Keys)
                {
                    if (option != btn)
                    {
                        option.color = colorOptions[option];
                    }
                }
            } // For color options

            Color newColor = new Color(btn.color.r * 0.7f, btn.color.g * 0.7f, btn.color.b * 0.7f);
            btn.color = newColor;
        }
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
            cs.genderSelected = true;
        } else if(pn == "Age")
        {
           cs.ageSelected = true;
            UserManager.Instance.ageGroup = val;
        }
        
        if(cs != null)
            cs.Collapse();
    }

    public void DisplayError(string message)
    {
        errorMessage.transform.parent.gameObject.SetActive(true);
        errorMessage.gameObject.SetActive(true);

        errorMessage.text = message;
    }

    public void ErrorOff()
    {
        Debug.Log("PARENT: " + errorMessage.transform.parent.gameObject.name);
        errorMessage.transform.parent.gameObject.SetActive(false);
        errorMessage.gameObject.SetActive(false);
    }

    public void LogOutBtn()
    {
        UserManager.Instance.LogOut();
        SceneManager.LoadScene("login");
    }

    public void GoToScene()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        string _name = clicked.name;
        SceneManager.LoadScene(_name);
    }

    public void BuyUpgrade()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        GameObject cParent = clicked.transform.parent.gameObject;
        Button btn = clicked.GetComponent<Button>();

        if (!btn.isActiveAndEnabled) return;
        btn.enabled = false;

        RectTransform[] children = cParent.GetComponentsInChildren<RectTransform>();
        int price = int.Parse(children[5].GetComponent<Text>().text);

        int newValue = int.Parse(UserManager.Instance._data["coins"].ToString()) - price;
        UserManager.Instance._data["coins"] = price;

        StoreManager.Instance.Bought(btn, newValue, clicked.name);
    }
}
