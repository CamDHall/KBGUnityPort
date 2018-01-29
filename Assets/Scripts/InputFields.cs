using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;

public class InputFields : MonoBehaviour {

    const string MatchEmailPattern =
    @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
    + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
      + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
    + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

    public void Username(InputField input)
    {
        UIManager.Instance.errorMessage.gameObject.SetActive(false);
        if(input.text.Length >= 3)
        {
            UserManager.Instance.Username(input.text);
        } else
        {
            UIManager.Instance.DisplayError("Username must be at least three characters long.");
        }
    }

    public void Name(InputField input)
    {
        if(input.text.Length < 3)
        {
            UIManager.Instance.DisplayError("Name must be longer than 3 characters.");
        } else
        {
            UserManager.Instance._name = input.text;
        }
    }

    public void Email(InputField input)
    {
        if(Regex.IsMatch(input.text, MatchEmailPattern))
        {
            UserManager.Instance.email = input.text;
        } else
        {
            UIManager.Instance.DisplayError("Invalid email.");
        }
    }

    public void Password(InputField input)
    {
        int num = 0;
        if(input.text.Length < 6)
        {
            UIManager.Instance.DisplayError("Password must be at least 6 characters long.");
        } else if(int.TryParse(input.text, out num))
        {
            UIManager.Instance.DisplayError("Password must contain at least one number.");
        } else
        {
            UserManager.Instance.password = input.text;
        }
    }

    public void CPassword(InputField input)
    {
        if(input.text != UserManager.Instance.password)
        {
            Debug.Log(UserManager.Instance.password);
            Debug.Log(UserManager.Instance.cPassword);
            UIManager.Instance.DisplayError("Passwords do not match.");
        } else
        {
            UserManager.Instance.cPassword = true;
        }
    }
}