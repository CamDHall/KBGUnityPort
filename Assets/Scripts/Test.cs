using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    IDictionary test = new Dictionary<string, string>();

	void Start () {
        string wtf = "WHAT THE FUCK";
        test.Add("testing", wtf);
        Debug.Log(test["testing"]);
    }
}
