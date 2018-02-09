using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour {

    public Text numQuestions, question, score, bonus;
    bool currentTruthy = true, lastAnswer = false;
    int num, scoreNum, bonusNum;

    private void Start()
    {
        GenerateQuestion();
    }

    public void TrueOrFalse()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        string val = clicked.GetComponentInChildren<Text>().text;

        bool answer = false;

        if (val == "True") answer = true;
        if (val == "False") answer = false;

        HandleAnswer(answer);
    }

    void GenerateQuestion()
    {
        if (num == 15) return;
        num++;
        numQuestions.text = "Questions:   " + num + "/15";

        int aChoice = Random.Range(0, 2);
        int operatorChoice = Random.Range(0, 4);

        int first = Random.Range(-30, 30);
        int second = Random.Range(-40, 40);

        if (aChoice == 0) currentTruthy = true;
        else currentTruthy = false;

        string _operator = "";
        if (operatorChoice == 0) _operator = "+";
        else if (operatorChoice == 1) _operator = "-";
        else if (operatorChoice == 2) _operator = "*";
        else _operator = "/";

        question.text = TxT(first, second, _operator);
    }

    string TxT(int first, int second, string _operator)
    {
        if(currentTruthy)
        {
            if (_operator == "+") return first + " " + _operator + " " + second + " = " + (first + second);
            else if (_operator == "-") return first + " " + _operator + " " + second + " = " + (first - second);
            else if (_operator == "*") return first + " " + _operator + " " + second + " = " + (first * second);
            else return first + " " + _operator + " " + second + " = " + (first / second);
        }

        return first + " " + _operator + " " + second + " = " + Random.Range(-30, 30);
    }

    void HandleAnswer(bool answer)
    {
        if (answer == currentTruthy)
        {
            score.text = "Score:     " + (++scoreNum);
            if(lastAnswer)
                bonusNum++;
        } else
        {
            bonusNum = 0;
        }

        bonus.text = "Bonus:     " + bonusNum;

        GenerateQuestion();
    }
}
