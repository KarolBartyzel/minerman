using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject background;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        background.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        AudioListener.volume = 1f;
    }

    void Pause()
    {
        background.SetActive(true);
        background.GetComponent<Image>().color = new Color32(118, 62, 0, 100);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        AudioListener.volume = 0.5f;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        AudioListener.volume = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        AudioListener.volume = 1f;
        SceneManager.LoadScene("Game");
    }
}
