using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum Parts { Skin, Hair, Cloth, Hat};
public class AvatarController : MonoBehaviour {

    GameObject avatar;
    Color hairColor, mainColor, secondaryColor;
	
    Parts activePart = Parts.Skin;
    public ScrollRect vw;
    public RectTransform skin, hair, hat, cloth;

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

    public void ChangeScrollContent()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        string cName = clicked.name;

        RectTransform temp = vw.content;
        temp.gameObject.SetActive(false);

        if (cName == "SkinTone")
        {
            vw.content = skin;
            skin.gameObject.SetActive(true);
        } else if(cName == "Hair")
        {
            vw.content = hair;
            hair.gameObject.SetActive(true);
        } else if(cName == "Hat")
        {
            vw.content = hat;
            hat.gameObject.SetActive(true);
        } else if(cName == "Shirt")
        {
            vw.content = cloth;
            cloth.gameObject.SetActive(true);
        }
    }
}
