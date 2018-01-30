using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour {

    GameObject avatar;

	// Use this for initialization
	void Start () {
        GameObject temp;
        // For testing just this scene
        if (UserManager.Instance == null)
        {
            temp = Resources.Load("Boy") as GameObject;
            
        }
        else
        {

            if (UserManager.Instance._data["gender"].ToString() == "Boy")
            {
                temp = Resources.Load("Boy") as GameObject;
            }
            else
            {
                temp = Resources.Load("Girl") as GameObject;
            }
        }

        avatar = Instantiate(temp, transform);
        avatar.transform.localPosition = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
