using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using Newtonsoft.Json;

using UnityEngine;
using UnityEngine.SceneManagement;

public static class GlobalState
{
    public static string playerName { get; set; }
}

public class GlobalStateManager: MonoBehaviour
{
    private IList<PlayerStatus> players = new List<PlayerStatus>();
    public GameObject resultMenuUI;
    public ResultMenu resultMenu;

    public int AddPlayer(string name = "", bool isHuman = false)
    {
        PlayerStatus newPlayer = new PlayerStatus(name, isHuman);
        players.Add(newPlayer);
        return newPlayer.id;
    }

    private void SaveInfoForPlayer(PlayerStatus player, String status)
    {
        ScoreManager.AddResult(new GameResult()
        {
            playerName = player.name,
            points = player.points,
            status = status
        });
    }

    public void PlayerDied(int playerId)
    {
        PlayerStatus deadPlayer = players.Single(player => player.id == playerId);
        deadPlayer.health = 0;

        IEnumerable<PlayerStatus> deadHumanPlayers = players.Where(player => player.health == 0 && player.isHuman);
        IEnumerable<PlayerStatus> alivePlayers = players.Where(player => player.health > 0);
        IEnumerable<PlayerStatus> aliveHumanPlayers = alivePlayers.Where(player => player.isHuman);

        if (alivePlayers.Count() == 0)
        {
            // Draw
            foreach(PlayerStatus player in deadHumanPlayers)
            {
                SaveInfoForPlayer(player, "draw");
            }
            resultMenu.GoToResults("draw");
            
        }
        else if (aliveHumanPlayers.Count() == 0)
        {
            // Loss
            foreach(PlayerStatus player in deadHumanPlayers)
            {
                SaveInfoForPlayer(player, "loss");
            }
            resultMenu.GoToResults("loss");
        }
        else if (alivePlayers.Count() == 1)
        {
            // Win
            foreach(PlayerStatus player in deadHumanPlayers)
            {
                SaveInfoForPlayer(player, "loss");
            }
            PlayerStatus winner = alivePlayers.Single();
            SaveInfoForPlayer(winner, "win");
            resultMenu.GoToResults(winner.name);
        }
    }
}
