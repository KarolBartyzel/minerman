using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject resultMenuUI;
    public Text resultLabelUI;
    public GameObject background;
    public GameObject musicUI;
    public GameMusic gameMusic;
    public GameObject endLightUI;

    void Start()
    {
        gameMusic = musicUI.GetComponent<GameMusic>();
    }

    private void LoadResults()
    {
        background.SetActive(true);
        background.GetComponent<Image>().color = new Color32(118, 62, 0, 100);
        Time.timeScale = 0f;
        GameIsPaused = true;
        resultMenuUI.SetActive(true);
    }

    public void GoToResults(String result)
    {
        endLightUI.SetActive(true);
        if (result == "draw") {
            gameMusic.PlayDraw();
            resultLabelUI.text = "Draw";
        }
        else if (result == "loss")
        {
            gameMusic.PlayLoss();
            resultLabelUI.text = "Computer won.";
        }
        else
        {
            gameMusic.PlayWin();
            resultLabelUI.text = result + " won.";
        }
        Invoke("LoadResults", 3f);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Game");
    }
}
