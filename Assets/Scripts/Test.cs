using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    IDictionary test = new Dictionary<string, string>();

	void Start () {
        string str = "RGBA(0.918, 0.753, 0.525, 1.000)";
        str = str.Replace("RGBA(", "");
        str = str.Replace(")", "");

        string[] numStr = str.Split(',');
        float[] numValues = System.Array.ConvertAll(numStr, float.Parse);

        Color newColor = new Color(numValues[0], numValues[1], numValues[2], numValues[3]);

        Debug.Log(newColor);
    }
}
