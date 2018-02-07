using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameObject avatar;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start () {
		
	}

    private void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetActiveScene().name == "House")
        {
            GameObject tempContainer;

            if(avatar.name == "Boy")
            {
                tempContainer = GameObject.FindGameObjectWithTag("Boy");
            }  else
            {
                tempContainer = GameObject.FindGameObjectWithTag("Girl");
            }

            avatar = Instantiate(UserManager.Instance.avatarObj, Vector2.zero, Quaternion.identity, tempContainer.transform);
            avatar.transform.localScale *= 0.5f;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
