using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class Spelling : MonoBehaviour {

    public Text questionText;

    public Button btn1, btn2, btn3;

    //string tempAge = "5-8";
    List<string> words = new List<string>();
    string wrongWord;

    int score, bonus, qNum = 1;
    public Text scoreTxt, bonusTxt, questionsNum;
    bool lastTrue = false;

    string abc = "abcdefghijklmnopqrstuvwxyz";

    TextAsset wordFile;
    private void Start()
    {
        questionsNum.text = "Question: " + qNum + "/15";
        if (UserManager.Instance._data["age"].ToString() =="5-8")
        {
            wordFile = Resources.Load("FirstAge") as TextAsset;
        } else
        {
            wordFile = Resources.Load("SecondAge") as TextAsset;
        }

        words = wordFile.text.Split("\n"[0]).ToList<string>();

        GenerateWord();
    }

    public void CheckAnswer()
    {
        string word = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        if (word == wrongWord)
        {
            score++;
            if (lastTrue) bonus++;
            lastTrue = true;
        }
        else
        {
            lastTrue = false;
            bonus = 0;
        }

        scoreTxt.text = "Score: " + score;
        bonusTxt.text = "Bonus: " + bonus;

        if(qNum == 15)
        {
            UserManager.Instance.UpdateUserData("coins", score * 100);
            UnityEngine.SceneManagement.SceneManager.LoadScene("House");
        } else
        {
            qNum++;
            GenerateWord();
            questionsNum.text = "Question: " + qNum + "/15";
        }
    }

    void GenerateWord()
    {
        string word1 = words[Random.Range(0, words.Count + 1)];
        word1 = word1.Remove(word1.Length - 1);

        string word2 = words[Random.Range(0, words.Count + 1)];
        word2 = word2.Remove(word2.Length - 1);

        string word3 = words[Random.Range(0, words.Count + 1)];
        word3 = word3.Remove(word3.Length - 1);

        int choice = Random.Range(0, 3);

        if (choice == 0)
        {
            word1 = JumbleWord(word1);
            wrongWord = word1;
        }
        else if (choice == 1)
        {
            word2 = JumbleWord(word2);
            wrongWord = word2;
        }
        else
        {
            word3 = JumbleWord(word3);
            wrongWord = word3;
        }

        btn1.GetComponentInChildren<Text>().text = word1;
        btn2.GetComponentInChildren<Text>().text = word2;
        btn3.GetComponentInChildren<Text>().text = word3;
    }

    string JumbleWord(string word)
    {
        string newWord = "";

        if(word.Length -1 <= 4)
        {
            int index = Random.Range(1, word.Length - 1);

            for(int i = 0; i < word.Length; i++)
            {
                if (i != index) newWord += word[i];
                else newWord += abc[Random.Range(0, 26)];
            }
        } else
        {
            int index1 = Random.Range(1, word.Length - 1);
            int index2 = Random.Range(1, word.Length - 1);

            for(int i = 0; i < word.Length; i++)
            {
                if (i != index1 && i != index2) newWord += word[i];
                else newWord += abc[Random.Range(0, 25)];
            }
        }
        return newWord;
    }
}
