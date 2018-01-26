using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Color backgroundColor;
    public GameObject registerForm, quizForm, scrollView;

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
        scrollView.GetComponentInChildren<Image>().color = backgroundColor;
        registerForm.SetActive(false);
        quizForm.SetActive(true);
    }
}
