using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject startGame;
    public GameObject menu;

    public void SaveNick(string nick)
    {
        GlobalState.playerName = nick;
    }

    public void Back()
    {
        startGame.SetActive(false);
        menu.SetActive(true);
    }

    public void Easy()
    {
        SceneManager.LoadScene("Game");
    }

    public void Medium()
    {
        SceneManager.LoadScene("Game");
    }

    public void Hard()
    {
        SceneManager.LoadScene("Game");
    }
}
