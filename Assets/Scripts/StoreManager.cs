using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour {

    public static StoreManager Instance;
    public Text coinTxt;

    int coins = 0;
    public Color redBg, blueBg;
    public List<GameObject> options;

    public GameObject content;
    public bool canBuy;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UserManager.Instance.GetUserData();
        SetupButtons();
    }

    public void SetupButtons()
    {
        // Get update purchases 
        coins = int.Parse(UserManager.Instance._data["coins"].ToString());
        coinTxt.text = "My Coins: " + coins;

        foreach (GameObject option in options)
        {

            // This catches the parent as well
            RectTransform[] children = option.GetComponentsInChildren<RectTransform>();

            Button btn = children[3].GetComponent<Button>();

            Image background = children[3].GetComponent<Image>();
            Text buy = children[4].GetComponent<Text>();
            string price = children[5].GetComponent<Text>().text;

            bool canBuy = true;

            if (UserManager.Instance._data.Contains(btn.name)) canBuy = false;

            if (coins >= int.Parse(price) && canBuy)
            {
                background.color = Color.white;
                buy.text = "BUY";
                btn.enabled = true;
            }
            else
            {
                background.color = redBg;
                buy.text = "CAN'T BUY";
                btn.enabled = false;
            }
        }
    }


    // Setting local data before setup and user data afterwards to prevent delays
    public void Bought(Button btn, int newValue, string item)
    {
        btn.GetComponent<Image>().color = StoreManager.Instance.redBg;
        StoreManager.Instance.coinTxt.text = "My Coins: " + newValue;

        UserManager.Instance.UpdateUserData("coins", newValue);
        UserManager.Instance._data["coins"] = newValue;

        if (UserManager.Instance._data[item] != null)
        {
            UserManager.Instance._data[item] = item;
        }
        else
        {
            UserManager.Instance._data.Add(item, item);
        }
        StoreManager.Instance.SetupButtons();
        UserManager.Instance.AddItem(item);
    }
}
