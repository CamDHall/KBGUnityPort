﻿using System.Collections;
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
            Debug.Log("UPGRADE: " + upgrade.name);
            if(UserManager.Instance._data.Contains(upgrade.name.ToLower()))
            {
                Debug.Log("IF: " + upgrade.name);
                upgrade.SetActive(true);
            }
        }
    }
}
