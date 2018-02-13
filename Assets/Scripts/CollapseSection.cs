using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollapseSection : MonoBehaviour {

    public bool ageSelected, genderSelected;
    public Button icon;
    RectTransform thisRT;
    GameObject submit;
    public GameObject area;
    public RectTransform[] topLevel;

    private void Start()
    {
        submit = GameObject.FindGameObjectWithTag("Submit");
        thisRT = GetComponent<RectTransform>();
    }

    public void Collapse()
    {
        if ((gameObject.name == "Background" && ageSelected && genderSelected) || gameObject.name != "Background")
        {
            icon.gameObject.SetActive(true);
            gameObject.SetActive(false);
            submit.GetComponent<RectTransform>().anchoredPosition += (Vector2.up * 10);
        }

        if (transform.name != "Quality" && gameObject.name != "Background")
        {
            GameObject sibiling = transform.parent.GetChild(transform.GetSiblingIndex() + 1).gameObject;
            Vector2 padding = new Vector3(0, 50 + (transform.childCount * 20));

            sibiling.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition - padding;
        }
    }

    public void Expand()
    {
        GameObject[] icons = GameObject.FindGameObjectsWithTag("Icon");
        Vector2 newPos = new Vector3(0, -60 * (area.transform.childCount));
        float iconY = GetComponent<RectTransform>().anchoredPosition.y;

        foreach(GameObject icon in icons)
        {
            RectTransform currentRect = icon.GetComponent<RectTransform>();

            if(currentRect.anchoredPosition.y < iconY)
            {
                currentRect.anchoredPosition += newPos;
            }
        }

        Vector2 areaNew = new Vector2(0, -75 * (area.transform.childCount));

        foreach(RectTransform obj in topLevel)
        {
            if(obj.anchoredPosition.y <= iconY)
            {
                obj.anchoredPosition += areaNew;
            }
        }
        area.SetActive(true);
        gameObject.SetActive(false);
    }
}
