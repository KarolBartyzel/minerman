using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public class HardBotPlayer: BotPlayer {
    private static readonly System.Random rnd = new System.Random();

    public GameObject players;

    private List<Transform> otherPlayers;

    protected override void Init()
    {
        otherPlayers = new List<Transform>();
        foreach (Transform player in players.transform)
        {
            if (player.gameObject.name != transform.gameObject.name)
            {
                otherPlayers.Add(player);
            }
        }

        playerId = globalManager.AddPlayer();
    }

    override protected void NextStep()
    {
        if (otherPlayers.Exists(other => Mathf.Abs(Vector3.Distance(other.position, transform.position)) < 3f))
        {
            DropBomb();
        }
    }
}
