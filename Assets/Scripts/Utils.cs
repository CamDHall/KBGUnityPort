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
}
