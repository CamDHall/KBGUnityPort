using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    public Color col;
    public SpriteRenderer img;

	void Start () {
        col *= 1.2f;
        col.a = 1;
        img.color = col;
    }
}
