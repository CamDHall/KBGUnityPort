﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatar : MonoBehaviour {

    public static Avatar Instance;

    public GameObject boy, girl;

    [HideInInspector] public Color hairColor, skinColor, colorHatPrime, colorClothPrime, colorClothSecond, colorHatSecond, shadowColor;

    GameObject avatarObj;
    string gender;

    List<SpriteRenderer> primaryClothes = new List<SpriteRenderer>();
    List<SpriteRenderer> spriteHair = new List<SpriteRenderer>();
    List<SpriteRenderer> primaryHat = new List<SpriteRenderer>();
    List<SpriteRenderer> spriteSkin = new List<SpriteRenderer>();
    List<SpriteRenderer> secondaryClothes = new List<SpriteRenderer>();

    SpriteRenderer shadow, secondaryHat;

    public Text testError;

    private void Awake()
    {
        Instance = this;
        if(UserManager.Instance != null)
            UserManager.Instance.GetUserData();

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(Instance);
    }

    public void SetupAvatar(string ingender, GameObject temp)
    {
        avatarObj = Instantiate(temp);
        if(UserManager.Instance._data["gender"].ToString() == "Girl") avatarObj.transform.SetParent(girl.transform);
        else avatarObj.transform.SetParent(boy.transform);

        avatarObj.transform.localPosition = Vector2.zero;

        colorHatSecond = colorHatPrime * 1.2f;
        colorHatSecond.a = 1;

        colorClothSecond = colorClothSecond * 1.2f;
        colorClothSecond.a = 1;

        SpriteRenderer[] childSprites = avatarObj.GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer s in childSprites)
        {
            if (s.tag == "Untagged") continue;

            if (s.tag == "Skin") spriteSkin.Add(s);
            if (s.tag == "PrimaryCloth") primaryClothes.Add(s);
            if (s.tag == "Hair") spriteHair.Add(s);
            if (s.tag == "PrimaryHat") primaryHat.Add(s);
            if (s.tag == "SecondaryCloth") secondaryClothes.Add(s);
            if (s.tag == "SecondaryHat")
            {
                secondaryHat = s;
            }
            if (s.tag == "Shadow") shadow = s;
        }

        SetLocalData();
        
        gender = ingender;
        UserManager.Instance.avatarObj = avatarObj;
    }

    public void SetupAvatar(string scene, Transform _parent)
    {
        // Clear list
        spriteSkin.Clear();
        primaryClothes.Clear();
        secondaryClothes.Clear();
        spriteHair.Clear();
        primaryHat.Clear();

        if (UserManager.Instance.gender == "Boy") avatarObj = Instantiate(Resources.Load("Boy") as GameObject);
        else avatarObj = Instantiate(Resources.Load("Girl") as GameObject);

        if (scene == "House") avatarObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Debug.Log("SCALE: " + avatarObj.transform.localScale);

        avatarObj.transform.parent = _parent;

        avatarObj.transform.localPosition = Vector2.zero;

        colorHatSecond = colorHatPrime * 1.2f;
        colorHatSecond.a = 1;

        colorClothSecond = colorClothSecond * 1.2f;
        colorClothSecond.a = 1;

        SpriteRenderer[] childSprites = avatarObj.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer s in childSprites)
        {
            if (s.tag == "Untagged") continue;

            if (s.tag == "Skin") spriteSkin.Add(s);
            if (s.tag == "PrimaryCloth") primaryClothes.Add(s);
            if (s.tag == "Hair") spriteHair.Add(s);
            if (s.tag == "PrimaryHat") primaryHat.Add(s);
            if (s.tag == "SecondaryCloth") secondaryClothes.Add(s);
            if (s.tag == "SecondaryHat")
            {
                secondaryHat = s;
            }
            if (s.tag == "Shadow") shadow = s;
        }

        SetLocalData();

        gender = UserManager.Instance.gender;
        UserManager.Instance.avatarObj = avatarObj;
    }

    public void UpdateAvatar(string part, Color newColor)
    {
        colorClothSecond = colorClothPrime * 1.2f;
        colorHatSecond = colorHatPrime * 1.2f;

        colorClothSecond.a = 1;
        colorHatSecond.a = 1;

        if (part == "colorClothPrime")
        {
            colorClothPrime = newColor;
            colorClothSecond = colorClothPrime * 1.2f;
            colorClothSecond.a = 1;

            foreach (SpriteRenderer s in primaryClothes) s.color = colorClothPrime;
            foreach (SpriteRenderer s in secondaryClothes) s.color = colorClothSecond;
        }
        else if (part == "colorHatPrime")
        {
            colorHatPrime = newColor;
            colorHatSecond = colorHatPrime * 1.2f;
            colorHatSecond.a = 1.00f;
            foreach (SpriteRenderer s in primaryHat) s.color = colorHatPrime;
            secondaryHat.color = colorHatSecond;
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
        
        colorClothSecond = colorClothPrime * 1.2f;
        colorHatSecond = colorHatPrime * 1.2f;        

        colorClothSecond.a = 1;
        colorHatSecond.a = 1;

        shadowColor = Color.grey;

        foreach (SpriteRenderer s in spriteSkin)
        {
            s.color = skinColor;
        }
        foreach (SpriteRenderer s in spriteHair) s.color = hairColor;
        foreach (SpriteRenderer s in primaryClothes) s.color = colorClothPrime;
        foreach (SpriteRenderer s in secondaryClothes) s.color = colorClothSecond;
        foreach (SpriteRenderer s in primaryHat) s.color = colorHatPrime;

        secondaryHat.color = colorHatSecond;
        shadow.color = shadowColor;
    }
}
