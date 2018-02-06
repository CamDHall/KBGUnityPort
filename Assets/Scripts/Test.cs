using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    public Color col;
    public SpriteRenderer img;

	void Start () {
        GameObject temp = Instantiate(Resources.Load("Girl") as GameObject);
    }
}
