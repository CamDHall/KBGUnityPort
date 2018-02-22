using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitButton : MonoBehaviour {

    RectTransform rect;
    public RectTransform quality, curious;
    Vector2 Pos;

	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (quality.gameObject.activeSelf) Pos = new Vector2(418.2f, curious.anchoredPosition.y - 1250);
        else Pos = new Vector2(418.2f, quality.anchoredPosition.y - 200);

        rect.anchoredPosition = Pos;
	}
}
