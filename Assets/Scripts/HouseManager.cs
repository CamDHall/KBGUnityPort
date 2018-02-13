using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour {

    public GameObject doorContainer, roofContainer, wallContainer;
    public GameObject boy, girl;
    public List<GameObject> upgrades;

    private void OnLevelWasLoaded(int level)
    {
        SetupHouse();
    }

    void SetupHouse()
    {
        doorContainer.GetComponent<SpriteRenderer>().sprite = (Resources.Load("House/" + UserManager.Instance._data["door"].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
        wallContainer.GetComponent<SpriteRenderer>().sprite = (Resources.Load("House/" + UserManager.Instance._data["wall"].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
        roofContainer.GetComponent<SpriteRenderer>().sprite = (Resources.Load("House/" + UserManager.Instance._data["roof"].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;

        // Upgrades
        foreach(GameObject upgrade in upgrades)
        {
            if(UserManager.Instance._data.Contains(upgrade.name.ToLower()))
            {
                upgrade.SetActive(true);
            }
        }

        if (UserManager.Instance.gender == "Boy") Avatar.Instance.SetupAvatar("House", boy.transform);
        else Avatar.Instance.SetupAvatar("House", girl.transform);        
    }
}
