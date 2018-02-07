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
        //Sprite door = (Resources.Load(UserManager.Instance._data["door"].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
        //Sprite wall = (Resources.Load(UserManager.Instance._data["wall"].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
        //Sprite roof = (Resources.Load(UserManager.Instance._data["roof"].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;

        doorContainer.GetComponent<SpriteRenderer>().sprite = (Resources.Load("House/" + UserManager.Instance._data["door"].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
        wallContainer.GetComponent<SpriteRenderer>().sprite = (Resources.Load("House/" + UserManager.Instance._data["wall"].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
        roofContainer.GetComponent<SpriteRenderer>().sprite = (Resources.Load("House/" + UserManager.Instance._data["roof"].ToString()) as GameObject).GetComponent<SpriteRenderer>().sprite;
    }
}
