using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountBalls : MonoBehaviour
{
    private int countBalls;
    private int ballsToCatch = 6;
    public bool allBallsCatched = false;
    private List<int> luckyNumber = new List<int>();
    public TextMeshProUGUI luckyNumbersText;
    public GameObject restartButton;


    // Start is called before the first frame update
    void Start()
    {
        luckyNumbersText.gameObject.SetActive(false);
        countBalls = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (countBalls==ballsToCatch && !allBallsCatched) 
        { 
            allBallsCatched=true;

            InsertionSortOfLuckyNumbers();

            luckyNumbersText.gameObject.SetActive(true);
            luckyNumbersText.text = "Lucky numbers: " + luckyNumber[0] + ", " + luckyNumber[1] + ", " + luckyNumber[2] + ", " + luckyNumber[3] + ", " + luckyNumber[4] + ", " + luckyNumber[5];

            Restart();
        }
    }

    public void AddBall(int amount, int ballNumber)
    {
        luckyNumber.Add(ballNumber);
        countBalls++;
    }

    void Restart() 
    {
        restartButton.gameObject.SetActive(true);
    }

    void InsertionSortOfLuckyNumbers() 
    {
        for (int i = 0; i < ballsToCatch; i++)
        {
            for (int j = i + 1; j < ballsToCatch; j++)
            {
                if (luckyNumber[i] < luckyNumber[j])
                {
                    ;
                }
                else
                {
                    int key = luckyNumber[i];
                    luckyNumber[i] = luckyNumber[j];
                    luckyNumber[j] = key;
                }
            }
        }
    }

}


