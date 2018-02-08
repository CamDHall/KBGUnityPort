using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour {

    public GameObject doorContainer, roofContainer, wallContainer;
    public GameObject boy, girl;
    private void OnLevelWasLoaded(int level)
    {
        SetupHouse();
    }

    void SetupHouse()
    {
        doorContainer.GetComponent<SpriteRenderer>().sprite = (Resources.Load("House/" + UserManager.Instance._data["door"].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
        wallContainer.GetComponent<SpriteRenderer>().sprite = (Resources.Load("House/" + UserManager.Instance._data["wall"].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
        roofContainer.GetComponent<SpriteRenderer>().sprite = (Resources.Load("House/" + UserManager.Instance._data["roof"].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
    }
}
