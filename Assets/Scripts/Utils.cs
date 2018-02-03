using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

	public static bool FormValid()
    {
        if(UserManager.Instance.favoriteColor != null && UserManager.Instance.subject != null &&
            UserManager.Instance.likes != null && UserManager.Instance.quality != null && UserManager.Instance.hobby != null)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public static Color GenerateColor(string str)
    {
        Debug.Log("D: " + str);
        str = str.Replace("RGBA(", "");
        str = str.Replace(")", "");

        string[] numStr = str.Split(',');
        float[] numValues = System.Array.ConvertAll(numStr, float.Parse);

        Debug.Log(numValues.Length);

        Color newColor = new Color(numValues[0], numValues[1], numValues[2], numValues[3]);

        return newColor;
    }
}
