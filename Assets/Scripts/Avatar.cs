﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatar : MonoBehaviour {

    public static Avatar Instance;

    [HideInInspector] public Color hairColor, skinColor, colorHatPrime, colorClothPrime, colorClothSecond, colorHatSecond, shadowColor;

    GameObject avatarObj;
    string gender;

    List<SpriteRenderer> primaryClothes = new List<SpriteRenderer>();
    List<SpriteRenderer> spriteHair = new List<SpriteRenderer>();
    List<SpriteRenderer> primaryHat = new List<SpriteRenderer>();
    List<SpriteRenderer> spriteSkin = new List<SpriteRenderer>();
    List<SpriteRenderer> secondaryClothes = new List<SpriteRenderer>();
    List<SpriteRenderer> secondaryHat = new List<SpriteRenderer>();
    SpriteRenderer shadow;

    public Text testError;

    private void Awake()
    {
        Instance = this;
        if(UserManager.Instance != null)
            UserManager.Instance.GetUserData();
    }

    public void SetupAvatar(string ingender, GameObject temp)
    {
        avatarObj = Instantiate(temp, transform);
        avatarObj.transform.localPosition = Vector2.zero;

        SpriteRenderer[] childSprites = avatarObj.GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer s in childSprites)
        {
            if (s.tag == "Untagged") continue;

            if (s.tag == "Skin") spriteSkin.Add(s);
            if (s.tag == "PrimaryCloth") primaryClothes.Add(s);
            if (s.tag == "Hair") spriteHair.Add(s);
            if (s.tag == "PrimaryHat") primaryHat.Add(s);
            if (s.tag == "SecondaryCloth") secondaryClothes.Add(s);
            if (s.tag == "SecondaryHat") secondaryHat.Add(s);
            if (s.tag == "Shadow") shadow = s;
        }

        SetLocalData();
        
        gender = ingender;
    }

    public void UpdateAvatar(string part, Color newColor)
    {
        colorClothSecond = colorClothPrime * 0.7f;
        colorHatSecond = colorHatPrime * 0.7f;

        colorClothSecond.a = 1;
        colorHatSecond.a = 1;

        if (part == "colorClothPrime")
        {
            colorClothPrime = newColor;
            colorClothSecond = colorClothPrime * 0.7f;
            colorClothSecond.a = 1;

            foreach (SpriteRenderer s in primaryClothes) s.color = colorClothPrime;
            foreach (SpriteRenderer s in secondaryClothes) s.color = colorClothSecond;
        }
        else if (part == "colorHatPrime")
        {
            colorHatPrime = newColor;
            colorHatSecond = colorClothPrime * 0.7f;
            colorHatSecond.a = 1;

            foreach (SpriteRenderer s in primaryHat) s.color = colorHatPrime;
            foreach (SpriteRenderer s in secondaryHat) s.color = colorHatSecond;
        }
        else if (part == "skinColor")
        {
            skinColor = newColor;
            foreach (SpriteRenderer s in spriteSkin) s.color = skinColor;
        } else if(part == "hairColor")
        {
            hairColor = newColor;
            foreach (SpriteRenderer s in spriteHair) s.color = hairColor;
        }
    }

    void SetLocalData()
    {
        UserManager.Instance.GetUserData();

        skinColor = Utils.GenerateColor(UserManager.Instance._data["skinColor"].ToString());
        hairColor = Utils.GenerateColor(UserManager.Instance._data["hairColor"].ToString());
        colorHatPrime = Utils.GenerateColor(UserManager.Instance._data["colorHatPrime"].ToString());
        colorClothPrime = Utils.GenerateColor(UserManager.Instance._data["colorClothPrime"].ToString());
        
        colorClothSecond = colorClothPrime * 0.7f;
        colorHatSecond = colorHatPrime * 0.7f;        

        colorClothSecond.a = 1;
        colorHatSecond.a = 1;

        shadowColor = Color.grey;
        shadowColor.a = 0.8f;

        foreach (SpriteRenderer s in spriteSkin) s.color = skinColor;
        foreach (SpriteRenderer s in spriteHair) s.color = hairColor;
        foreach (SpriteRenderer s in primaryClothes) s.color = colorClothPrime;
        foreach (SpriteRenderer s in secondaryClothes) s.color = colorClothSecond;
        foreach (SpriteRenderer s in primaryHat) s.color = colorHatPrime;
        foreach (SpriteRenderer s in secondaryHat) s.color = colorHatSecond;

        Debug.Log("WTF: " + skinColor);

        shadow.color = shadowColor;
    }
}
