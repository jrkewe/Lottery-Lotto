using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private int delay = 5;
    private bool delayOn = false;
    private int timeToDraw = 5;

    public bool machineDraws = false;
    public bool machineJumble = false;
    private bool paused;


    public TextMeshProUGUI timerText;
    private GameObject titleScreen;
    public GameObject pauseScreen;

    public void StartGame(bool parametr)
    { 
        delayOn = parametr;
        titleScreen = GameObject.Find("Title Screen");
        titleScreen.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Pause
        if (Input.GetKeyDown(KeyCode.P) && !titleScreen.gameObject.activeSelf && timeToDraw>0)
        {
            ChangePaused();
        }

        //State of machine
        if (delayOn)
        {
            StartCoroutine(Delay());
            delayOn = false;
        }
        if (delay==0 && !machineJumble) 
        {
            machineJumble=true;
            StartCoroutine(Draw());
        }
        if (timeToDraw==0) 
        {
            timerText.gameObject.SetActive(false);
            machineJumble = false;
            machineDraws = true;
        }
    }
    IEnumerator Delay()
    {
        while (delay >= 0)
        {
            timerText.text = "Lottery starts in: " + delay;
            yield return new WaitForSeconds(1);
            delay--;
        }
    }

    IEnumerator Draw()
    {
        while (timeToDraw >= -1)
        {
            timerText.text = "Please wait: " + timeToDraw;
            yield return new WaitForSeconds(1);
            timeToDraw--;
        }
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            timerText.gameObject.SetActive(false);
            Time.timeScale = 0.0f;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            timerText.gameObject.SetActive(true);
            Time.timeScale = 1.0f;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
