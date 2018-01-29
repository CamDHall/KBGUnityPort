using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollapseSection : MonoBehaviour {

    public bool ageSelected, genderSelected;
    public Button icon;
    RectTransform thisRT;

    private void Start()
    {
        thisRT = GetComponent<RectTransform>();
    }

    public void Collapse()
    {
        if((gameObject.name == "Background" && ageSelected && genderSelected) || gameObject.name != "Background")
        {
            // Deactivate buttons and text
            foreach(Button btn in GetComponentsInChildren<Button>())
            {
                btn.gameObject.SetActive(false);
            }

            foreach(Text txt in GetComponentsInChildren<Text>())
            {
                txt.enabled = false;
            }

            if(GetComponent<Image>() != null)
                GetComponent<Image>().enabled = false;

            icon.gameObject.SetActive(true);

            List<RectTransform> groups = new List<RectTransform>();

            foreach(RectTransform rt in transform.parent.GetComponentInChildren<RectTransform>())
            {
                if(rt == thisRT)
                {

                    continue;
                }
                groups.Add(rt);
            }

            int index = groups.IndexOf(thisRT);

            for(int i = 0; i < groups.Count; i++)
            {
                RectTransform group = groups[i];
                if(group != thisRT)
                {
                    if(gameObject.name == "Background")
                    {
                        group.anchoredPosition += (Vector2.up * 350);
                    } else
                    {
                        if (group.anchoredPosition.y > thisRT.anchoredPosition.y)
                        {
                            continue;
                        }
                        else
                        {
                            if (i == index + 1)
                                group.anchoredPosition += (Vector2.up * 550);
                            else if (i == groups.Count)
                                group.anchoredPosition += (Vector2.up * 50);
                            else
                                group.anchoredPosition += (Vector2.up * 500);
                        }
                    }
                }
            }
        }
    }
}
