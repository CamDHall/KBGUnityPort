using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollapseSection : MonoBehaviour {

    public bool ageSelected, genderSelected;
    public Button icon;
    RectTransform thisRT;

    public GameObject area;

    private void Start()
    {
        thisRT = GetComponent<RectTransform>();
    }

    public void Collapse()
    {
        if ((gameObject.name == "Background" && ageSelected && genderSelected) || gameObject.name != "Background")
        {
            icon.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        if (transform.name != "Quality" && gameObject.name != "Background")
        {
            GameObject sibiling = transform.parent.GetChild(transform.GetSiblingIndex() + 1).gameObject;
            Vector2 padding = new Vector3(0, 150 + (transform.childCount * 10));

            sibiling.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition - padding;

            Debug.Log("SIBLING: " + sibiling.GetComponent<RectTransform>().anchoredPosition + " NAME: " + sibiling.name);
        }
    }

    public void Expand()
    {
        area.SetActive(true);
        gameObject.SetActive(false);
    }
}
