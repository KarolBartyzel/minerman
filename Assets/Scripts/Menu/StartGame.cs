using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.IO;
using Newtonsoft.Json;

public class StartGame : MonoBehaviour
{
    public GameObject startGame;
    public GameObject menu;
    private string playerName;

    private void saveGameSettings(String level)
    {
        GameSettings gameSettings = new GameSettings
        {
            playerName=playerName != null ? playerName : "Anonim",
            level=level
        };

        using (StreamWriter streamWriter = File.CreateText(Path.Combine(Application.persistentDataPath, "GameSettings.txt")))
        {
            streamWriter.Write(JsonConvert.SerializeObject(gameSettings, Formatting.Indented));
        }
    }

    public void SaveNick(string nick)
    {
        playerName = nick;
        GlobalState.playerName = nick;
    }

    public void Back()
    {
        startGame.SetActive(false);
        menu.SetActive(true);
    }

    public void Easy()
    {
        saveGameSettings("Easy");
        SceneManager.LoadScene("Game");
    }

    public void Medium()
    {
        saveGameSettings("Medium");
        SceneManager.LoadScene("Game");
    }

    public void Hard()
    {
        saveGameSettings("Hard");
        SceneManager.LoadScene("Game");
    }
}
