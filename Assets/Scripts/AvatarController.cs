using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AvatarController : MonoBehaviour {
	
    public ScrollRect vw;
    public RectTransform skin, hair, hat, cloth;
    string gender;
	void Awake () {
        GameObject temp;

        // For testing just this scene
        if (UserManager.Instance == null)
        {
            temp = Resources.Load("Boy") as GameObject;
            gender = "Boy";            
        }
        else
        {
            gender = UserManager.Instance._data["gender"].ToString();
            if (gender == "Boy")
            {
                temp = Resources.Load("Boy") as GameObject;
            }
            else
            {
                temp = Resources.Load("Girl") as GameObject;
            }
        }

        Avatar.Instance.SetupAvatar(gender, temp);
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

    public void SetColor()
    {
        Button clicked = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        string pName = clicked.transform.name;
        Color _color = clicked.colors.normalColor;

        if(pName == "Skin")
        {
            Avatar.Instance.skinColor = _color;
        } else if(pName == "Hair")
        {
            Avatar.Instance.hairColor = _color;
        } else if(pName == "Hat")
        {
            Avatar.Instance.colorHatPrime = _color;
        } else if(pName == "Cloth")
        {
            Avatar.Instance.colorClothPrime = _color;
        }

        Avatar.Instance.UpdateAvatar(pName, _color);
        UserManager.Instance.UpdateUserData(pName, _color.ToString());
    }
}
