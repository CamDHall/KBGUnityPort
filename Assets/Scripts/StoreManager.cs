using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour {

    public Text coinTxt;

    int coins = 0;
    public Color redBg, blueBg;
    public List<GameObject> options;

    public GameObject content;

    private void Start()
    {
        coins = int.Parse(UserManager.Instance._data["coins"].ToString());
        coinTxt.text = "My Coins: " + coins;

        foreach(GameObject option in options)
        {
            // This catches the parent as well
            RectTransform[] children = option.GetComponentsInChildren<RectTransform>();

            Debug.Log("CHILDREN: " + children.Length);
            Debug.Log("FIRST: " + children[0]);

            Image background = children[3].GetComponent<Image>();
            Text buy = children[4].GetComponent<Text>();
            string price = children[5].GetComponent<Text>().text;

            if (coins >= int.Parse(price))
            {
                background.color = Color.white;
                buy.text = "BUY";
            }
            else
            {
                background.color = redBg;
                buy.text = "CAN'T BUY";
            }
        }
    }

    
}
