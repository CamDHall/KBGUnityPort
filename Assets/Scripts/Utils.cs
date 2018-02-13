using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

	public static bool FormValid()
    {
        if(UserManager.Instance.favoriteColor != "" && UserManager.Instance.subject != "" &&
            UserManager.Instance.likes != "" && UserManager.Instance.quality != "" && UserManager.Instance.hobby != "")
        {
            Debug.Log("COLOR: " + UserManager.Instance.favoriteColor + " QUALITY: " + UserManager.Instance.quality);
            return true;
        } else
        {
            return false;
        }
    }

    public static Color GenerateColor(string str)
    {
        str = str.Replace("RGBA(", "");
        str = str.Replace(")", "");

        string[] numStr = str.Split(',');
        float[] numValues = System.Array.ConvertAll(numStr, float.Parse);

        Color newColor = new Color(numValues[0], numValues[1], numValues[2], numValues[3]);

        return newColor;
    }
}
