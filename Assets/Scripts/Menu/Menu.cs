using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject startGame;
    public GameObject scores;
    public GameObject options;
    public GameObject quit;
    public GameObject menu;
    
    public void StartGame()
    {
        startGame.SetActive(true);
        menu.SetActive(false);
    }

    public void Scores()
    {
        scores.SetActive(true);
        menu.SetActive(false);
    }

    public void Options()
    {
        options.SetActive(true);
        menu.SetActive(false);
    }

    public void Quit()
    {
        quit.SetActive(true);
        menu.SetActive(false);
    }
}
