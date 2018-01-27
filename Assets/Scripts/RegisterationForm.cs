using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterationForm : MonoBehaviour {

    public static RegisterationForm Instance;
    public string gender, ageGroup, _name, username, email, password;

    private void Awake()
    {
        Instance = this;
    }
}
