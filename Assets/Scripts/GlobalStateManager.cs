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
    public static string gameResult { get; set; }
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

    public void PlayerDied(int playerId)
    {
        PlayerStatus deadPlayer = players.Single(player => player.id == playerId);
        deadPlayer.health = 0;

        IEnumerable<PlayerStatus> alivePlayers = players.Where(player => player.health > 0);

        int countOfAlivePlayers = alivePlayers.Count();
        if (countOfAlivePlayers <= 1)
        {
            PlayerStatus winner = alivePlayers.SingleOrDefault();
            PlayerStatus humanPlayer = players.Single(player => player.isHuman);

            GlobalState.gameResult = winner != null ? winner.name + " won!" : "Game ended in a draw!";
            ScoreManager.AddResult(new GameResult()
            {
                playerName = humanPlayer.name,
                points = humanPlayer.points,
                status = winner == null ? "draw" : winner.name == humanPlayer.name ? "Win" : "Loss"
            });

            resultMenu.GoToResults();
        }
    }
}
