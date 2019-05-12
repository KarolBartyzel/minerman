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

    void Start()
    {
        resultLabelUI.text = GlobalState.gameResult;
    }

    private void LoadResults()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        resultLabelUI.text = GlobalState.gameResult;
        resultMenuUI.SetActive(true);
    }

    public void GoToResults()
    {
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
