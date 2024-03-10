using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Button startButton;
    private Timer timerScript;

    // Start is called before the first frame update
    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);
        timerScript = GameObject.Find("GameManager").GetComponent<Timer>();
    }

    void StartGame() 
    {
        timerScript.StartGame(true);
    }
}
