using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	void Start () {
        //Sprite temp = (Resources.Load("door") as GameObject).GetComponent<SpriteRenderer>().sprite;

        GetComponent<SpriteRenderer>().sprite = (Resources.Load("door") as GameObject).GetComponent<SpriteRenderer>().sprite;
    }
}
