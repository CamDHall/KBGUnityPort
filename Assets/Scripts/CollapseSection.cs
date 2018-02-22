using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollapseSection : MonoBehaviour {

    public bool ageSelected, genderSelected;
    public Button icon;
    GameObject submit;
    public GameObject area;
    public RectTransform[] topLevel;
    public RectTransform below;

    private void Start()
    {
        submit = GameObject.FindGameObjectWithTag("Submit");
    }

    public void Collapse()
    {
        if ((gameObject.name == "Background" && ageSelected && genderSelected) || gameObject.name != "Background")
        {
            icon.gameObject.SetActive(true);
            gameObject.SetActive(false);
            //submit.GetComponent<RectTransform>().anchoredPosition += (Vector2.up * 10);
        }

        if (below != null)
        {
            below.anchoredPosition += Vector2.up * 325;
            if (below.GetComponent<CollapseSection>().below != null)
                below.GetComponent<CollapseSection>().below.anchoredPosition += Vector2.up * 325;
        }/*
        if (transform.name != "Quality" && gameObject.name != "Background")
        {
            GameObject sibiling = transform.parent.GetChild(transform.GetSiblingIndex() + 1).gameObject;
            Vector2 padding = new Vector3(0, 50 + (transform.childCount * 20));

            sibiling.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition - padding;
        }

        submit.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 100 + (transform.childCount * 20));*/
    }

    public void Expand()
    {
        if (below != null)
        {
            below.anchoredPosition -= Vector2.up * 325;

            if (below.GetComponent<CollapseSection>().below != null)
                below.GetComponent<CollapseSection>().below.anchoredPosition -= Vector2.up * 325;
        }
        
        /*GameObject[] icons = GameObject.FindGameObjectsWithTag("Icon");
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
        }*/
        area.SetActive(true);
        gameObject.SetActive(false);
    }
}
